using System;
using System.Collections.Generic;

namespace DataEntity
{
	/*
	 * 此类为
     * 区域端-累计 模块entity
	*/

	#region 累计-收入/人次-校区维度
	public class PaymentSumAreaEntity : PaymentSumBaseEntity
	{
		public PaymentSumAreaEntity()
		{
			GradeData = new List<PaymentSumBaseEntity>();
			IsFold = true;
		}
		public bool IsFold { get; set; }
		//年级维度的数据列表
		public List<PaymentSumBaseEntity> GradeData { get; set; }
	}

	public class PaymentSumBaseEntity
	{
		//显示名：校区or年级名称
		public string Code { get; set; }
		public string Name { get; set; }
		//本期累计：人次or收入
		public decimal CurrentSum { get; set; }
		//去年同期：人次or收入
		public decimal LastYearSum { get; set; }
		//增长率=（本期累计-去年同期）/去年同期×100%
		public decimal GrowthRate { get; set; }
	}

	//接口返回格式
	public class PaymentSumData
	{
		public PaymentSumData(){
			List = new List<PaymentSumAreaEntity>();
			TotalData = new PaymentSumBaseEntity() { Name="总计"};
		}
		public List<PaymentSumAreaEntity> List { get; set; }
		public PaymentSumBaseEntity TotalData { get; set; }
	}

	#endregion

	#region 累计-收入/人次-教师维度
	public class PaymentSumTeacherEntity
	{
		//教师编号
		public string TeacherCode { get; set; }
		//教师姓名
		public string TeacherName { get; set; }
		//科目名称
		public string CourseName { get; set; }
		//总人次or总收入
		public decimal Total { get; set; }
		//班量
		public decimal ClassCount { get; set; }
		//班均
		public decimal ClassAvg { get; set; }
		//续班率
		public decimal RenewRate { get; set; }
		//退班率
		public decimal RefundRate { get; set; }
	}

	//接口返回格式
	public class PaymentSumTeacherData
	{
		public PaymentSumTeacherData()
		{
			List = new List<PaymentSumTeacherEntity>();
			TotalData = new PaymentSumTeacherEntity();
		}
		public List<PaymentSumTeacherEntity> List { get; set; }
		public PaymentSumTeacherEntity TotalData { get; set; }
	}

	#endregion
}
