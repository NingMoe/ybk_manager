using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 班级的详细续班情况？
    /// </summary>
    public class RenewInfoEntity
    {
		public int Id { get; set; }

		public int SchoolId { get; set; }

		public string ClassCode { get; set; }

		public string ClassName { get; set; }

		public int Type { get; set; }

		public int GradeType { get; set; }

		public string AreaCode { get; set; }

		public string AreaName { get; set; }

		public string DistrictCode { get; set; }

		public string DistrictName { get; set; }

		public string Grade { get; set; }

		public string GradeName { get; set; }

		public string GroupCode { get; set; }

		public string GroupName { get; set; }

		public string TeacherCode { get; set; }

		public string TeacherName { get; set; }

		public string CourseName { get; set; }

		public string PrintAddress { get; set; }

		public int Year { get; set; }

		public int Quarter { get; set; }

		public double TotalStudentNum { get; set; }

		public double RenewRate { get; set; }

		public double RenewStudentNum { get; set; }

		public int Status { get; set; }
    }
}
