using Boc.Assets.Application.Dto;
using Boc.Assets.Domain.EventsHandler.SignalR;
using Boc.Assets.Domain.Models.Assets.TableViews;
using Boc.Assets.Web.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boc.Assets.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            app.UseAuthentication();
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chat");
            });
            app.UseMvc(routeBuilder =>
            {
                //注册非odata路由
                routeBuilder.EnableDependencyInjection();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModels(app.ApplicationServices));
            });

        }

        private IEdmModel GetEdmModels(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EnableLowerCamelCase();
            //organization
            builder.EntitySet<OrgDto>("Organization").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            //employee
            builder.EntitySet<EmployeeDto>("Employee").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            //orgSpaceDto
            builder.EntitySet<OrgSpaceDto>("OrgSpace").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            //asset apply
            builder.EntitySet<AssetApplyDto>("AssetApply")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetApplyDto>("AssetApply")
                .EntityType.Collection.Function("GetCurrent").Returns<IQueryable<AssetApplyDto>>();
            builder.EntitySet<AssetApplyDto>("AssetApply")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<AssetApplyDto>>();


            //asset category
            builder.EntitySet<AssetCategoryDto>("AssetCategory")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetCategoryDto>("AssetCategory")
                .EntityType.Collection.Function("GetCurrent").Returns<IQueryable<AssetCategoryDto>>();
            builder.EntitySet<AssetCategoryDto>("AssetCategory")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<AssetCategoryDto>>();

            //asset
            builder.EntitySet<AssetDto>("Asset")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetDto>("Asset")
                .EntityType.Collection.Function("GetManage").ReturnsFromEntitySet<AssetDto>("Asset");
            builder.EntitySet<AssetDto>("Asset")
                .EntityType.Collection.Function("GetCurrent").ReturnsCollectionFromEntitySet<AssetDto>("Asset");
            builder.EntitySet<AssetDto>("Asset")
                .EntityType.Collection.Function("GetAssetsWithoutInventory").ReturnsFromEntitySet<AssetDto>("Asset");
            builder.Function("GetCurrentSumarryByCategory").Returns<IQueryable<AssetSumarryByCategory>>();
            builder.Function("GetManageSumarryByCategory").Returns<IQueryable<AssetSumarryByCategory>>();
            builder.Function("GetManageSumarryByCount").Returns<IQueryable<AssetSumarryByCount>>();
            //aset exchange
            builder.EntitySet<AssetExchangeDto>("AssetExchange").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetExchangeDto>("AssetExchange").EntityType.Collection
                .Function("GetCurrent").Returns<IQueryable<AssetExchangeDto>>();
            builder.EntitySet<AssetExchangeDto>("AssetExchange").EntityType.Collection
                .Function("GetManage").Returns<IQueryable<AssetExchangeDto>>();

            //asset inventory
            builder.EntitySet<AssetInventoryDto>("AssetInventory").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetInventoryDto>("AssetInventory")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<AssetInventoryDto>>();
            //asset inventory register
            builder.EntitySet<AssetInventoryRegisterDto>("AssetInventoryRegister").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetInventoryRegisterDto>("AssetInventoryRegister").EntityType.Collection.Function("GetCurrent")
                .ReturnsFromEntitySet<AssetInventoryRegisterDto>("AssetInventoryRegister");
            builder.EntitySet<AssetInventoryRegisterDto>("AssetInventoryRegister").EntityType.Collection.Function("GetManage")
                .ReturnsFromEntitySet<AssetInventoryRegisterDto>("AssetInventoryRegister");

            //asset inventory detail
            builder.EntitySet<AssetInventoryDetailDto>("AssetInventoryDetail")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetInventoryDetailDto>("AssetInventoryDetail")
                .EntityType.Collection.Function("GetCurrent").ReturnsFromEntitySet<AssetInventoryDetailDto>("AssetInventoryDetail");
            //asset return
            builder.EntitySet<AssetReturnDto>("AssetReturn").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetReturnDto>("AssetReturn")
                .EntityType.Collection.Function("GetCurrent").Returns<IQueryable<AssetReturnDto>>();
            builder.EntitySet<AssetReturnDto>("AssetReturn")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<AssetReturnDto>>();

            //资产分类的管理机构CategoryOrgRegistration
            builder.EntitySet<OrgDto>("CategoryManagerRegister").EntityType
                .Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<OrgDto>("CategoryManagerRegister")
                .EntityType.Collection.Function("GetOrgByCategory").Returns<IEnumerable<OrgDto>>();

            //identity
            builder.EntitySet<OrgDto>("Identity")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            //maintainer
            builder.EntitySet<MaintainerDto>("Maintainer")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<MaintainerDto>("Maintainer")
                .EntityType.Collection.Function("GetCurrent").Returns<IQueryable<MaintainerDto>>();
            builder.EntitySet<MaintainerDto>("Maintainer")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<MaintainerDto>>();
            builder.EntitySet<MaintainerDto>("Maintainer")
                .EntityType.Collection.Function("GetByCategoryId").Returns<IQueryable<MaintainerDto>>();
            // asset deploy
            builder.EntitySet<AssetDeployDto>("AssetDeploy")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            return builder.GetEdmModel();
        }
    }
}
