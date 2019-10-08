using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boc.Assets.Web.Auth.Authorization
{
    public class ControllerPermissionApplicationModelProvider : IApplicationModelProvider
    {
        public ControllerPermissionApplicationModelProvider()
        {

        }
        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {

        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            List<PermissionAttribute> attributeData = new List<PermissionAttribute>();
            foreach (var controllerModel in context.Result.Controllers)
            {
                var resourceData = controllerModel.Attributes.OfType<PermissionAttribute>().ToArray();
                if (resourceData.Length > 0)
                {
                    attributeData.AddRange(resourceData);
                }

                foreach (var actionModel in controllerModel.Actions)
                {
                    var actionResourceData = actionModel.Attributes.OfType<PermissionAttribute>().ToArray();
                    if (actionResourceData.Length > 0)
                    {
                        attributeData.AddRange(actionResourceData);
                    }
                }
            }
            foreach (var item in attributeData)
            {
                ResourceData.AddResource(item.Controller, item.Action);
            }
        }
        public int Order => -1000 + 11;
    }
}