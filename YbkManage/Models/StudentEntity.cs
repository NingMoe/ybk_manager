using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentEntity
    {
		public string Code { get; set; }
		public string Name { get; set; }
		public string SchoolId { get; set; }
		public string UserId { get; set; }
		public string Avatar { get; set; }
        public int IsRenew { get; set; }
		public int IsValid { get; set; }
		public int IsBind { get; set; }
		public int StudentStatus { get; set; }
		public string RenewTimeStr { get; set; }
    }
}
