using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 教师的实体类
    /// </summary>
    public class TeacherInfoEntity
    {

		/// <summary>
        /// 教师Id
        /// </summary>
        /// <value>The teacher identifier.</value>
		public int TeacherId { get; set; }

		/// <summary>
		/// 教师编码
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }

		/// <summary>
		/// 教师姓名
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// 教师头像
		/// </summary>
		/// <value>The avatar.</value>
		public string Avatar { get; set; }

		/// <summary>
		/// 教师的电子邮箱
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }

		/// <summary>
		/// 手机号码
		/// </summary>
		/// <value>The mobile.</value>
		public string Mobile { get; set; }

        /// <summary>
        /// TODO 啥意思？
        /// </summary>
        /// <value>The user identifier.</value>
		public string UserId { get; set; }

		/// <summary>
		/// 所属学校Id
		/// </summary>
		/// <value>The school identifier.</value>
		public int SchoolId { get; set; }

		/// <summary>
        /// 所属教研组？
        /// </summary>
        /// <value>The scope code.</value>
		public int ScopeCode { get; set; }

		/// <summary>
		/// 所属教研组？
		/// </summary>
		/// <value>The name of the scope.</value>
		public string ScopeName { get; set; }

		/// <summary>
		/// TODO
		/// </summary>
		/// <value>The type.</value>
		public int Type { get; set; }

		/// <summary>
		/// TODO
		/// </summary>
		/// <value>The project code.</value>
		public string ProjectCode { get; set; }


		public int IsAdmin { get; set; }

		public string BindDate { get; set; }
    }
}
