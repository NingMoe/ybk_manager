using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 模拟数据用，后根据具体字段做修改
    /// </summary>
    public class TeachReportEntity
    {
        public TeachReportEntity()
        {
        }

		/// <summary>
		/// 教研组
		/// </summary>
		/// <value>The name of the group.</value>
		public string GroupName { get; set; }

		/// <summary>
		/// 班级人数
		/// </summary>
		/// <value>The size of the class.</value>
		public double ClassSize { get; set; }

		/// <summary>
		/// 班级续班人数
		/// </summary>
		/// <value>The size of the class continue.</value>
		public double ClassContinueSize { get; set; }

		/// <summary>
		/// 平均续班率
		/// </summary>
		/// <value>The continue rate.</value>
		public double ContinueRate { get; set; }
    }
}
