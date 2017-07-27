using System;
namespace DataEntity
{
	[Serializable]
	public class ManagerUserInfo
	{
		public int SchoolId { get; set; }
		public int UserType { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Creator { get; set; }
		public bool? IsCanLogin { get; set; }
		public string Modifier { get; set; }
	}


	#region 教师条线
	/// <summary>
	/// 教研组列表
	/// </summary>
	[Serializable]
	public class ScopeModel
	{
		public int SchoolId { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public int? TeacherCount { get; set; }
	}
	/// <summary>
	/// 某个教研组下的教师列表
	/// </summary>
	[Serializable]
	public class TeacherListModel
	{
		public int? SchoolId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string ProjectCode { get; set; }
		public int? ScopeCode { get; set; }
		public string ScopeName { get; set; }
		public string Email { get; set; }
		public string UserId { get; set; }
		public int? TeacherId { get; set; }
		public int? Type { get; set; }
		public int? RowNum { get; set; }
		public string Avatar { get; set; }
	}


	#endregion

	#region 助教条线
	/// <summary>
	/// 校区列表
	/// </summary>
	[Serializable]
	public class AreaModel
	{
		public string DistrictCode { get; set; }
		public string DistrictName { get; set; }
		public string sCode { get; set; }
		public string sName { get; set; }
	}
	/// <summary>
	/// 助教组长管理列表
	/// </summary>
	[Serializable]
	public class AstLeaderListModel
	{
		public string Name { get; set; }
		public string Mobile { get; set; }
		public string AreaCode { get; set; }
		public string AreaName { get; set; }
		public string DistrictCode { get; set; }
		public string DistrictName { get; set; }
	}
	[Serializable]
	public class UserAreaRelationModel
	{
		public int SchoolId { get; set; }
		public string AreaCode { get; set; }
		public string AreaName { get; set; }
		public string AssistantMobile { get; set; }
		public string Creator { get; set; }
		public string Modifier { get; set; }
	}
	#endregion

	#region 登录用户信息，包括U2登录信息
	[Serializable]
	public class LoginUserInfoEntity
	{
		/// <summary>
		/// 学校Id
		/// </summary>
		public int SchoolId { get; set; }
		/// <summary>
		/// 学校名称
		/// </summary>
		public string SchoolName { get; set; }
		/// <summary>
		/// 用户类型 助教=120
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		/// 编号
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// UserId
		/// </summary>
		public string UserId { get; set; }
		/// <summary>
		/// 教学经理-年级
		/// </summary>
		public int? Grade { get; set; }
		/// <summary>
		/// 助教主管-大区域
		/// </summary>
		public string DistrictCode { get; set; }
		/// <summary>
		/// 教研组编号
		/// </summary>
		public string ScopeCode { get; set; }
		/// <summary>
		/// 教研组名称
		/// </summary>
		public string ScopeName { get; set; }
		//头像
		public string Avatar { get; set; }
		/*U2相关用户信息*/
		public string LoginToken { get; set; }
		public string Sign { get; set; }
		public string AccessToken { get; set; }
		public string AccessTokenExpireTime { get; set; }
	}
	#endregion

	#region 用户头像
	/// <summary>
	/// api返回值
	/// </summary>
	[Serializable]
	public class AvatarResult
	{
		//1-成功；0-失败
		public int Status { get; set; }
		public object Pic { get; set; }
		//失败描述}
		public string Error { get; set; }
		//原图
		public string AvatarUrl { get; set; }
	}
	//pic用户头像
	[Serializable]
	public class AvatarInfo
	{
		public string AvatarUrl { get; set; }
		public string AvatarUrl_big { get; set; }
		public string AvatarUrl_middle { get; set; }
		public string AvatarUrl_small { get; set; }

		public string UserId { get; set; }
		//取big图片地址
		public string Avatar { get; set; }
	}

	#endregion

}
