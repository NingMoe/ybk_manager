using System;
using System.Collections.Generic;

namespace DataEntity
{

	#region 续班率 （适用于首页初中、高中续班率统计）
	[Serializable]
	public class ResultData_DepartmentRenewInfo
	{
		//学校Id
		public int SchoolId { get; set; }
		//财年
		public int Year { get; set; }
		//季度
		public int Season { get; set; }
		//年级：1-初中；2-高中
		public short Type { get; set; }
		//续班率，比如0.245，即页面显示24.5%
		public decimal RenewRate { get; set; }
	}
	#endregion

	#region 列表总计列数据
	[Serializable]
	public class ResultData_TotalData
	{
		//学校Id
		public int SchoolId { get; set; }
		//财年
		public int Year { get; set; }
		//季度
		public int Season { get; set; }
		//原班人数
		public decimal TotalNum { get; set; }
		//续班人数
		public decimal RenewNum { get; set; }
		//比平均值（页面计算）
		public decimal DataGap { get; set; }
		//平均续班率
		public decimal RenewRate { get; set; }
	}
	#endregion

	#region 续班率列表 （适用于首页前三、后三排名列表，报表教研组、教师、班级维度列表）
	[Serializable]
	public class ResultData_RenewInfoInGroup : ResultData_TotalData
	{
		//列表各行列数据
		//public List<RenewInfo> RenewInfoX { get; set; }
		//public List<Tuple<int, string, string, decimal, decimal, decimal>> RenewInfo { get; set; }
		public List<RenewInfo> RenewInfo { get; set; }
	}

	[Serializable]
	public class RenewInfo
	{
		//学校Id
		public int Item1 { get; set; }
		//编号
		public string Item2 { get; set; }
		//名称
		public string Item3 { get; set; }
		//原班人数
		public decimal Item4 { get; set; }
		//续班人数
		public decimal Item5 { get; set; }
		//续班率
		public decimal Item6 { get; set; }
	}

	#region 列表各列
	[Serializable]
	public class RenewInfoX
	{
		//学校Id
		public int SchoolId { get; set; }
		//编号（教研组/教师/班级）
		public string Code { get; set; }
		//名称（教研组/教师/班级）
		public string Name { get; set; }
		//地址
		public string Address { get; set; }

		//原班人数
		public decimal TotalNum { get; set; }
		//续班人数
		public decimal RenewNum { get; set; }
		//平均续班率
		public decimal Data { get; set; }
	}
	#endregion

	#endregion

	#region 续班班级维度

	[Serializable]
	public class ResultData_RenewInfoInClass : ResultData_TotalData
	{
		//列表各行列数据
		public List<Statistics_ClassRenewSummary> RenewInfo { get; set; }
	}

	#region 列表班级续班对象
	[Serializable]
	public class Statistics_ClassRenewSummary
	{
		public int ID { get; set; }

		public int SchoolId { get; set; }

		public string ClassCode { get; set; }

		public string ClassName { get; set; }

		public short Type { get; set; }

		public short GradeType { get; set; }

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

		public short Quarter { get; set; }

		public decimal TotalStudentNum { get; set; }

		public decimal RenewRate { get; set; }

		public decimal RenewStudentNum { get; set; }

		public DateTime CreateTime { get; set; }

		public short Status { get; set; }

		public DateTime ModifyTime { get; set; }

		public string Memo { get; set; }


	}
	#endregion
	#endregion

	#region 班级续班学员情况
	[Serializable]
	public class ClassRenewEntity
	{
		//已续班学员列表
		public List<StudentRenewModel> RenewStudents { get; set; }
		//未续班学员列表
		public List<StudentRenewModel> NotRenewStudents { get; set; }
	}

	#region 班级学员续班
	[Serializable]
	public class StudentRenewModel
	{
		//学校Id
		public int schoolId { get; set; }
		//学员编号
		public string code { get; set; }
		//学员姓名
		public string name { get; set; }
		//学员通行证Id，UserId
		public string userId { get; set; }
		//是否续班：0-否；1-是
		public int isRenew { get; set; }
		//是否有效：0-否；1-是
		public int isValid { get; set; }
		//是否已绑定学员号：0-否；1-是
		public int isBind { get; set; }
		//学员状态：
		public int studentStatus { get; set; }
		//续班时间
		public DateTime? renewTimeStr { get; set; }

		//头像（来源另外一个接口）
		public string avatar { get; set; }
	}
	#endregion

	#endregion

	#region 学员报班记录实体
	[Serializable]
	public class PureClassEntity
	{
		public int? Id { get; set; }
		//学校Id
		public int? SchoolId { get; set; }
		//班级编号
		public string ClassCode { get; set; }
		//班级名称
		public string ClassName { get; set; }
		//班级开课时间
		public DateTime? BeginDate { get; set; }
		//班级结课时间
		public DateTime? EndDate { get; set; }
		//班级上课地点
		public string PrintAddress { get; set; }
		//班级上课时间
		public string PrintTime { get; set; }
		//班级费用
		public double? Fee { get; set; }
		//班级状态：0-上课中；1-已结课；2-未开课；3-全部
		public int? ClassStatus { get; set; }
		//班级授课教师名称,分隔
		public string TeacherNames { get; set; }

		/*以下属性备用*/
		public string Hot { get; set; }

		public int? MemberCount { get; set; }

		public int? NormalCount { get; set; }
		//班级上课学员名称,分隔
		public string StudentNames { get; set; }
		//校区编号
		public string AreaCode { get; set; }
		//校区名称
		public string AreaName { get; set; }
		//是否spoc班：0-否；1-是。
		public int IsSpoc { get; set; }
		//班级总课次
		public int TotalLessonCount { get; set; }
		//班级已上课次
		public int HasOutLessonCount { get; set; }
	}
	#endregion

	/*报表顶部筛选框数据源*/

	#region 财年季度实体
	[Serializable]
	public class QuarterEntity
	{
		//财年
		public int Year { get; set; }
		//季度
		public int Quarter { get; set; }
		//季度名称
		public string QuarterName { get; set; }
		//是否当前Q
		public bool IsCurrent { get; set; }
	}
	#endregion

	#region 年级实体
	[Serializable]
	public class GradeGroupEntity
	{
		public string GradeGroup { get; set; }
		public List<GradeEntity> GradeList { get; set; }
	}
	[Serializable]
	public class GradeEntity
	{
		public string GradeName { get; set; }
	}
	#endregion

	#region 区域实体
	[Serializable]
	public class DistrictEntity
	{
		public string DistrictCode { get; set; }
		public string DistrictName { get; set; }
	}
	#endregion

}
