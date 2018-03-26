using System;
using System.Collections.Generic;

namespace DataEntity
{
	/*
	 * 此类为
	 * 区域端- 增量、招新 模块entity
	*/
	#region 招新
	public class NewStudentSumData
	{
		public NewStudentSumData()
		{
			List = new List<NewStudentSumAreaEntity>();
			TotalData = new NewStudentSumBaseEntity();
		}
		public List<NewStudentSumAreaEntity> List { get; set; }
		public NewStudentSumBaseEntity TotalData { get; set; }
	}
	public class NewStudentSumAreaEntity : NewStudentSumBaseEntity
	{
		public NewStudentSumAreaEntity()
		{
			GradeData = new List<NewStudentSumBaseEntity>();
			IsFold = true;
		}
		public bool IsFold { get; set; }
		//年级维度的数据列表
		public List<NewStudentSumBaseEntity> GradeData { get; set; }
	}
	public class NewStudentSumBaseEntity
	{
		//显示名：校区or年级名称
		public string Code { get; set; }
		public string Name { get; set; }
		//人数or人次
		public decimal StudentCount { get; set; }
		//新生
		public decimal Total { get; set; }
		//新生占比=纯新生人数/新生总人数
		public decimal Rate { get; set; }
	}
	//学员类型实体
	[Serializable]
	public class StudentCategoryEntity
	{
		public int DataType { get; set; }
		public string CategoryName { get; set; }
		public string CategoryValue { get; set; }
	}
	#endregion


}
