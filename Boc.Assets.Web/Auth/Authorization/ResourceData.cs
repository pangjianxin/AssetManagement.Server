using System.Collections.Generic;

namespace Boc.Assets.Web.Auth.Authorization
{
    public class ResourceData
    {
        static ResourceData()
        {
            Resources = new Dictionary<string, List<string>>();
        }

        public static void AddResource(string controller, string action)
        {
            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                return;
            }
            if (!Resources.ContainsKey(controller))
            {
                Resources.Add(controller, new List<string>());
            }
            if (!Resources[controller].Contains(action))
            {
                Resources[controller].Add(action);
            }
        }
        public static Dictionary<string, List<string>> Resources { get; set; }
    }
}