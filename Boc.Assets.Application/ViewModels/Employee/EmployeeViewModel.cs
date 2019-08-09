namespace Boc.Assets.Application.ViewModels.Employee
{
    public class EmployeeViewModel : ViewModel
    {
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 员工号
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 所属二级行
        /// </summary>
        public string Org2 { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 办公室电话
        /// </summary>
        public string OfficePhone { get; set; }
    }
}