using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 模拟数据用，后根据具体字段做修改
    /// </summary>
    public class TeachReportEntity
    {

		public int Id { get; set; }

		public int ScopeId { get; set; }

		/// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
		public string Name { get; set; }

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

		/// <summary>
        /// Gets or sets the teacher code.
        /// </summary>
        /// <value>The teacher code.</value>
		public string TeacherCode { get; set; }
    }
}
