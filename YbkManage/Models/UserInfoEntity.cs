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

		private string name = "张三";
		/// <summary>
		/// 当前用户姓名
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

        private string school = "北京新东方";
        /// <summary>
        /// 所属学校
        /// </summary>
        /// <value>The school.</value>
		public string School
        {
            get
            {
                return this.school;
            }
            set
            {
                this.school = value;
            }
        }
    }
}
