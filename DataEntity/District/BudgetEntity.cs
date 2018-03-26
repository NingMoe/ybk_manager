using System;
namespace DataEntity
{
	/*
	 *此类为
	 *区域端-预算 模块entity
	*/

	/// <summary>
	/// 预算-预收款
	/// </summary>
	public class PaymentEntity
	{
		public string AreaCode { get; set; }
		/// <summary>
		/// 校区名称
		/// </summary>
		public string AreaName { get; set; }
		/// <summary>
		/// 营收目标
		/// </summary>
		public decimal Budget { get; set; }
		/// <summary>
		/// 预收款
		/// </summary>
		public decimal Payment { get; set; }
		/// <summary>
		/// 完成率
		/// </summary>
		public decimal CompletionRate { get; set; }
	}
}
