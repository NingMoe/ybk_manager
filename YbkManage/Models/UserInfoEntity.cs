using System;
namespace YbkManage.Models
{
	/// <summary>
	/// 登录用户信息
	/// </summary>
	public class UserInfoEntity
	{
		/// <summary>
		/// 登录账号
		/// </summary>
		/// <value>The login account.</value>
		public string LoginAccount { get; set; }

		/// <summary>
		/// 登录密码
		/// </summary>
		/// <value>The login password.</value>
		public string LoginPassword { get; set; }

		/// <summary>
		/// 当前用户的头像
		/// </summary>
		/// <value>The avatar.</value>
		public string Avatar { get; set; }

		/// <summary>
		/// 当前用户姓名
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		public string UserId { get; set; }

		public int SchoolId { get; set; }

		/// <summary>
		/// 所属学校名称
		/// </summary>
		/// <value>The name of the school.</value>
		public string SchoolName { get; set; }

		public int Grade { get; set; }
	}
}
