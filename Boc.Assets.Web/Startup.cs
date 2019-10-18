using Boc.Assets.Application.Dto;
using Boc.Assets.Domain.EventsHandler.SignalR;
using Boc.Assets.Web.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using System;
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
            NativeInjectorBootstrapper.RegisterServices(services, Configuration);
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
            //asset apply
            builder.EntitySet<AssetApplyDto>("AssetApply")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetApplyDto>("AssetApply")
                .EntityType.Collection.Function("GetRurrent").Returns<IQueryable<AssetApplyDto>>();
            builder.EntitySet<AssetApplyDto>("AssetApply")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<AssetApplyDto>>();
            //asset category
            builder.EntitySet<AssetCategoryDto>("AssetCategory")
                .EntityType.Filter().Count().Expand().OrderBy().Page().Select();
            builder.EntitySet<AssetCategoryDto>("AssetCategory")
                .EntityType.Collection.Function("GetRurrent").Returns<IQueryable<AssetCategoryDto>>();
            builder.EntitySet<AssetCategoryDto>("AssetCategory")
                .EntityType.Collection.Function("GetManage").Returns<IQueryable<AssetCategoryDto>>();
            //asset


            return builder.GetEdmModel();
        }
    }
}
