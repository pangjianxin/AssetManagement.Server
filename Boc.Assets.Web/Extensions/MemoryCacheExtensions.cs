using Microsoft.Extensions.Caching.Memory;

namespace Boc.Assets.Web.Extensions
{
    public static class MemoryCacheExtensions
    {    /// <summary>
         /// 扩展IMemoryCache检索权限信息
         /// </summary>
         /// <param name="cache"></param>
         /// <param name="roleId"></param>
         /// <param name="controller"></param>
         /// <param name="action"></param>
         /// <returns></returns>
        public static bool CheckPermission(this IMemoryCache cache, string roleId, string controller, string action)
        {
            //var exist = cache.TryGetValue<List<PermissionDto>>("permissions", out var list);
            //var shitList = cache.Get<List<PermissionDto>>("permissions");
            //return exist && list.Any(it => it.RoleId == roleId && it.ControllerName == controller && it.ActionName == action);
            return false;
        }
    }
}