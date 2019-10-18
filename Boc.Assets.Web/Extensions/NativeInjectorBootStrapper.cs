
using AutoMapper;
using Boc.Assets.Application.AutoMapper;
using Boc.Assets.Application.ServiceImplements;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Authentication;
using Boc.Assets.Domain.CommandHandlers.Organization;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.EventsHandler.SignalR;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using Boc.Assets.Infrastructure.Bus;
using Boc.Assets.Infrastructure.DataBase;
using Boc.Assets.Infrastructure.Identity;
using Boc.Assets.Infrastructure.Repository;
using Boc.Assets.Infrastructure.Repository.EventSourcing;
using Boc.Assets.Infrastructure.UnitOfWork;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Extensions
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region immutability
            //mvc
            services.AddMvc(action =>
            {
                action.Filters.Add<ModelStateActionFilter>();
                // https://blogs.msdn.microsoft.com/webdev/2018/08/27/asp-net-core-2-2-0-preview1-endpoint-routing/
                // Because conflicts with ODataRouting as of this version could improve performance though
                action.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //OData
            services.AddOData();
            services.AddODataQueryFilter();
            //密码hash
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            //注入ApplicationModelProvider,用于获取controller的相关信息
            // services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ControllerPermissionApplicationModelProvider>());
            //添加授权handler
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //ef core dbContext 
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //下面注释的两句是要记录或者忽略这个错误
                //options.ConfigureWarnings(warnning => warnning.Log(CoreEventId.DetachedLazyLoadingWarning));
                //options.ConfigureWarnings(warnning => warnning.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            });
            services.AddDbContext<EventStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            //application UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile).Assembly);
            //配置跨域规则
            services.AddCors(option =>
            {
                option.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:4200",
                                       "http://21.33.129.180:4201",
                                       "http://localhost:4201");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    //policy.AllowAnyOrigin();
                    policy.AllowCredentials();
                    policy.WithExposedHeaders("X-Pagination");
                    policy.SetPreflightMaxAge(TimeSpan.FromMinutes(60));
                });
            });
            //jwt authentication
            AddJwtAuthentication(services, configuration);
            //授权
            services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("user", policy =>
                {
                    policy.AuthenticationSchemes = new[] { JwtBearerDefaults.AuthenticationScheme };
                    policy.Requirements.Add(new RoleRequirement(1));

                });
                option.AddPolicy("manage", policy =>
                {
                    policy.AuthenticationSchemes = new[] { JwtBearerDefaults.AuthenticationScheme };
                    policy.Requirements.Add(new RoleRequirement(2));
                });
            });
            //添加缓存
            services.AddMemoryCache();
            //HttpContext accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //application user(organization)
            services.AddScoped<IUser, ApplicationUserAccessor>();
            //domain service
            services.AddScoped<IAssetDomainService, AssetDomainService>();
            //Mediator
            services.AddMediatR(typeof(OrganizationCommandHandler));
            //domain event handlers
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //bus
            services.AddScoped<IBus, MediatorBus>();

            //signalR
            services.AddSignalR();
            services.AddSingleton<IOnlineUserInfo, OnLineUserInfoInMemory>();
            services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
            #endregion
            //event sourcing
            services.AddScoped<IEventStore, EfCoreEventStore>();
            services.AddScoped<IEventRepository, EfCoreEventRepository>();
            //entities repositories
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IAssetCategoryRepository, AssetCategoryRepository>();
            services.AddScoped<IOrgSpaceRepository, OrgSpaceRepository>();
            services.AddScoped<IAssetDeployRepository, AssetDeployRepository>();
            services.AddScoped<IAssetInventoryRepository, AssetInventoryRepository>();
            services.AddScoped<IAssetInventoryRegisterRepository, AssetInventoryRegisterRepository>();
            services.AddScoped<IAssetInventoryDetailRepository, AssetInventoryDetailRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMaintainerRepository, MaintainerRepository>();
            services.AddScoped<IOrgRoleRepository, OrgRoleRepository>();
            services.AddScoped<IAssetApplyRepository, AssetApplyRepository>();
            services.AddScoped<IAssetReturnRepository, AssetReturnRepository>();
            services.AddScoped<IAssetExchangeRepository, AssetExchangeRepository>();
            services.AddScoped<ICategoryManageRegisterRepository, CategoryManageRegisterRepository>();

            //application services
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetCategoryService, AssetCategoryService>();
            services.AddScoped<IOrgSpaceService, OrgSpaceService>();
            services.AddScoped<IAssetDeployService, AssetDeployService>();
            services.AddScoped<IAssetInventoryService, AssetInventoryService>();
            services.AddScoped<IEmployeeService, EmployService>();
            services.AddScoped<IMaintainerService, MaintainerService>();
            services.AddScoped<IAssetApplyService, AssetApplyService>();
            services.AddScoped<IAssetReturnService, AssetReturnService>();
            services.AddScoped<IAssetExchangeService, AssetExchangeService>();
            services.AddScoped<IOrganizationRoleService, OrganizationRoleService>();
            services.AddScoped<ICategoryManageResgisterService, CategoryManageResgisterService>();

        }
        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration config)
        {
            var configSection = config.GetSection("JwtSettings");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configSection.GetSection("Key").Value));
            services.Configure<JwtIssuerOptions>(option =>
            {
                option.Issuer = configSection.GetSection("Issuer").Value;
                option.Audience = configSection.GetSection("Audience").Value;
                option.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        //RoleClaimType和NameClaimType的作用是将自定义的claim转化成标准的claim
                        RoleClaimType = "orgRole",
                        NameClaimType = "orgIdentifier",
                        //ensure the token was issued by a trusted authorization server (default value true)
                        ValidateIssuer = true,
                        ValidIssuer = configSection.GetSection("Issuer").Value,
                        //ensure the token audience matches our audience value(default value true)
                        ValidateAudience = true,
                        ValidAudience = configSection.GetSection("Audience").Value,
                        ValidateIssuerSigningKey = true,
                        //specify the key used to sign the token
                        IssuerSigningKey = signingKey,
                        RequireSignedTokens = true,
                        //ensure the token hasn't expired
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        //clock skew compensates fro server time drift. we recommend 5 minutes or less
                        ClockSkew = TimeSpan.FromMinutes(5),

                    };
                    //SignalR需要这个配置
                    option.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/eventMessage") || path.StartsWithSegments("/chat"))
                            {
                                var accessToken = context.Request.Query["access_token"];
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        },
                    };
                });
        }
    }
}