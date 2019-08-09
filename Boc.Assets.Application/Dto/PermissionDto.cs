namespace Boc.Assets.Application.Dto
{
    public class PermissionDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 控制器的名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}