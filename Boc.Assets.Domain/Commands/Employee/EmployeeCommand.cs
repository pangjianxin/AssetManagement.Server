using Boc.Assets.Domain.Core.Commands;

namespace Boc.Assets.Domain.Commands.Employee
{
    public abstract class EmployeeCommand : Command
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