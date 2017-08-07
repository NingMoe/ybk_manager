using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEntity;

namespace DataService
{
	public class MeService
	{
		#region 助教条线
		/// <summary>
		/// 获取校区列表
		/// </summary>
		/// <returns>The area by district.</returns>
		/// <param name="schoolId">学校Id</param>
		/// <param name="districtCode">大区域编号</param>
		public List<AreaModel> GetAreaByDistrict(int schoolId, string districtCode)
		{
			try
			{
				var apiUrl = Config.UpocCommonUrl + "Common/Index";
				var dic = new Dictionary<string, string>();
				var method = "GetAreaByDistrictCode"; //方法名称，固定值

				dic.Add("appId", Config.AppId);
				dic.Add("method", method);
				dic.Add("schoolId", schoolId.ToString());
				dic.Add("districtCode", districtCode);
				var sign = Helper.GetSign(dic);
				dic.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, dic); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<AreaModel>>>(resultStr);
				if (resultData.State == 1 && resultData.Data != null)
				{
					return resultData.Data;
				}
				return new List<AreaModel>();

			}
			catch (Exception)
			{
				return new List<AreaModel>();
			}
		}
		/// <summary>
		/// 获取助教组长管理列表
		/// </summary>
		/// <returns>The assistant leader list.</returns>
		/// <param name="schoolId">学校Id</param>
		/// <param name="districtCode">大区域编号</param>
		public static List<AstLeaderListModel> GetAssistantLeaderList(int schoolId, string districtCode)
		{
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Assistant/Index";
				var dic = new Dictionary<string, string>();
				var method = "GetAssistantLeaderList"; //方法名称，固定值

				dic.Add("appId", Config.AppId);
				dic.Add("method", method);
				dic.Add("schoolId", schoolId.ToString());
				dic.Add("districtCode", districtCode);
				var sign = Helper.GetSign(dic);
				dic.Add("sign", sign);
				var resultStr =Helper.DoPost(apiUrl, dic); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<AstLeaderListModel>>>(resultStr.ToString());
				if (resultData.State == 1 && resultData.Data != null)
				{
					return resultData.Data;
				}
				return new List<AstLeaderListModel>();

			}
			catch (Exception)
			{
				return new List<AstLeaderListModel>();
			}
		}
		/// <summary>
		/// 保存助教和教学区对应关系
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public Result SaveUserArea(List<UserAreaRelationModel> list)
		{
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Assistant/Index";
				var data = Helper.ToJsonItem(list);

				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("appid", Config.AppId);
				param.Add("method", "SaveUserArea");
				param.Add("data", data);
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, param);
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result>(resultStr);
				return resultData;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}

		}
		/// <summary>
		/// 删除助教和教学区对应关系
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public Result DeleteUserArea(int schoolId, string mobile)
		{
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Assistant/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("appid", Config.AppId);
				param.Add("method", "DeleteUserArea");
				param.Add("schoolId", schoolId.ToString());
				param.Add("mobile", mobile);
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, param);
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result>(resultStr);
				return resultData;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}
		}

		#endregion

		#region 管理端用户
		/// <summary>
		/// 登陆后验证管理段用户身份，不存在返回null
		/// </summary>
		/// <returns>The manager user.</returns>
		/// <param name="email">U2-邮箱</param>
		/// <param name="mobile">U2-手机号</param>
		/// <param name="userId">登录成功，回写UserId</param>
		//public UserModel GetManagerUser(string email, string mobile, string userId)
		//{
		//    try
		//    {
		//        var apiUrl = Config.UpocManagerUserUrl + "User/Index";
		//        Dictionary<string, string> param = new Dictionary<string, string>();
		//        param.Add("appid", Config.AppId);
		//        param.Add("method", "GetManagerUser");
		//        param.Add("email", email);
		//        param.Add("mobile", mobile);
		//        param.Add("userId", userId);
		//        string sign = Helper.GetSign(param);
		//        param.Add("sign", sign);
		//        var resultStr = Helper.DoPost(apiUrl, param);
		//        resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
		//        var resultData = Helper.FromJsonTo<Result<UserModel>>(resultStr);
		//        if (resultData.State == 1 && resultData.Data != null)
		//        {
		//            return resultData.Data;
		//        }
		//        return null;
		//    }
		//    catch (Exception)
		//    {
		//        return null;
		//    }
		//}

		/// <summary>
		/// 添加管理端用户
		/// </summary>
		/// <returns>The manager user.</returns>
		/// <param name="model">Model.</param>
		/// <param name="areaCode">教学区编号</param>
		/// <param name="areaName"> 教学区名称</param>
		/// <param name="scopeId">教研组Id</param>
		public Result AddManagerUser(ManagerUserInfo model, string areaCode, string areaName, int scopeId)
		{
			try
			{
				var rd = new Result();
				#region 助教&教学区

				if (model.UserType == (int)UserType.AssistantLeader)
				{
					var relation = new UserAreaRelationModel();
					relation.AreaCode = areaCode;
					relation.AreaName = areaName;
					relation.AssistantMobile = model.Mobile;
					relation.Creator = model.Creator;
					relation.Modifier = model.Modifier;
					relation.SchoolId = model.SchoolId;
					var list = new List<UserAreaRelationModel>();
					list.Add(relation);
					rd = SaveUserArea(list);
				}
				#endregion

				#region 教师&教研组

				if (model.UserType == (int)UserType.TeacherDirector ||
					model.UserType == (int)UserType.TeacherArea ||
					model.UserType == (int)UserType.Teacher)
				{
					rd = SaveScopeTeacher(model.SchoolId, model.Code, model.Name, model.Email, scopeId, model.UserType);
				}
				#endregion

				if (rd.State == 1)
				{
					var data = Helper.ToJsonItem(model);
					var apiUrl = Config.UpocManagerUserUrl + "User/Index";
					Dictionary<string, string> param = new Dictionary<string, string>();
					param.Add("appid", Config.AppId);
					param.Add("method", "AddManagerUser");
					param.Add("data", data);
					string sign = Helper.GetSign(param);
					param.Add("sign", sign);
					var resultStr = Helper.DoPost(apiUrl, param);
					resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
					rd = Helper.FromJsonTo<Result>(resultStr);
					return rd;
				}
				return rd;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}
		}
		/// <summary>
		/// 更新管理端用户
		/// </summary>
		/// <returns>The manager user.</returns>
		/// <param name="model">Model.</param>
		/// <param name="areaCode">教学区编号</param>
		/// <param name="areaName"> 教学区名称</param>
		/// <param name="scopeId">教研组Id</param>
		/// <returns></returns>
		public Result UpdateManagerUser(ManagerUserInfo model, string areaCode, string areaName, int scopeId)
		{
			try
			{
				var rd = new Result();
				#region 助教&教学区

				if (model.UserType == (int)UserType.AssistantLeader)
				{
					var relation = new UserAreaRelationModel();
					relation.AreaCode = areaCode;
					relation.AreaName = areaName;
					relation.AssistantMobile = model.Mobile;
					relation.Creator = model.Creator;
					relation.Modifier = model.Modifier;
					relation.SchoolId = model.SchoolId;
					var list = new List<UserAreaRelationModel>();
					list.Add(relation);
					rd = SaveUserArea(list);
				}
				#endregion

				#region 教师&教研组

				if (model.UserType == (int)UserType.TeacherDirector ||
					model.UserType == (int)UserType.TeacherArea ||
					model.UserType == (int)UserType.Teacher)
				{
					rd = SaveScopeTeacher(model.SchoolId, model.Code, model.Name, model.Email, scopeId, model.UserType);
				}
				#endregion

				if (rd.State == 1)
				{
					var data = Helper.ToJsonItem(model);
					var apiUrl = Config.UpocManagerUserUrl + "User/Index";
					Dictionary<string, string> param = new Dictionary<string, string>();
					param.Add("appid", Config.AppId);
					param.Add("method", "UpdateManagerUser");
					param.Add("data", data);
					string sign = Helper.GetSign(param);
					param.Add("sign", sign);
					var resultStr = Helper.DoPost(apiUrl, param);
					resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
					rd = Helper.FromJsonTo<Result>(resultStr);
					return rd;
				}
				return rd;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="type">type=1 助教相关身份  type=2 教师相关身份</param>
		/// <param name="keyword">type=1 根据Mobile删除，Type=2 根据Code删除</param>
		/// <returns></returns>
		public Result DeleteManagerUser(int schoolId, string type, string keyword, string modifier)
		{

			try
			{
				var rd = new Result();
				#region 删除助教&教学区

				if (type == "1")
				{
					rd = DeleteUserArea(schoolId, keyword);
				}
				#endregion

				#region 删除教师&教研组

				if (type == "2")
				{
					rd = RemoveScopeTeacher(Convert.ToInt32(schoolId), keyword);
				}
				#endregion

				if (rd.State == 1)
				{

					var apiUrl = Config.UpocManagerUserUrl + "User/Index";
					Dictionary<string, string> param = new Dictionary<string, string>();
					param.Add("appid", Config.AppId);
					param.Add("method", "DeleteManagerUser");
					param.Add("schoolId", schoolId.ToString());
					param.Add("type", type);
					param.Add("keyword", keyword);
					param.Add("Modifier", modifier);
					string sign = Helper.GetSign(param);
					param.Add("sign", sign);
					var resultStr = Helper.DoPost(apiUrl, param);
					resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
					rd = Helper.FromJsonTo<Result>(resultStr);
					return rd;
				}
				return rd;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}
		}
		#endregion

		#region 教师条线
		/// <summary>
		/// 获取教研组列表
		/// </summary>
		/// <returns></returns>
		/// <param name="schoolId">学校Id</param>
		/// <param name="grade">年级</param>
		public List<ScopeModel> GetScopeByGrade(int schoolId, int grade)
		{
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Teacher/Index";
				var dic = new Dictionary<string, string>();
				var method = "GetTeacherScopeListByGrade"; //方法名称，固定值

				dic.Add("appId", Config.AppId);
				dic.Add("method", method);
				dic.Add("schoolId", schoolId.ToString());
				dic.Add("grade", grade.ToString());
				var sign = Helper.GetSign(dic);
				dic.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, dic); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<ScopeModel>>>(resultStr);
				if (resultData.State == 1 && resultData.Data != null)
				{
					return resultData.Data;
				}
				return new List<ScopeModel>();

			}
			catch (Exception)
			{
				return new List<ScopeModel>();
			}
		}
		/// <summary>
		/// 保存教师-教研组关系
		/// </summary>
		/// <param name="schoolId">学校Id</param>
		/// <param name="teacherCode">教师编号</param>
		/// <param name="teacherName">教师姓名</param>
		/// <param name="email">邮箱</param>
		/// <param name="scope">教研组Id</param>
		/// <param name="userType">用户类型</param>
		/// <returns></returns>
		public Result SaveScopeTeacher(int schoolId, string teacherCode, string teacherName, string email, int scope, int userType)
		{
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Teacher/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("appid", Config.AppId);
				param.Add("method", "SaveScopeTeacher");
				param.Add("schoolId", schoolId.ToString());
				param.Add("teacherCode", teacherCode);
				param.Add("teacherName", teacherName);
				param.Add("email", email ?? "");
				param.Add("scope", scope.ToString());
				param.Add("userType", userType.ToString());
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, param); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result>(resultStr);
				return resultData;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}

		}
		/// <summary>
		/// 移除教师-教研组关系
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="teacherCode"></param>
		/// <returns></returns>
		public Result RemoveScopeTeacher(int schoolId, string teacherCode)
		{
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Teacher/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("appid", Config.AppId);
				param.Add("method", "RemoveScopeTeacher");
				param.Add("schoolId", schoolId.ToString());
				param.Add("teacherCode", teacherCode);
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, param); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result>(resultStr);
				return resultData;
			}
			catch (Exception ex)
			{
				return new Result() { State = 0, Error = ex.Message };
			}

		}
		/// <summary>
		/// 获取某一教研组下面的老师
		/// </summary>
		/// <param name="schoolId">学校Id</param>
		/// <param name="scope">教研组Id</param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public List<TeacherListModel> GetTeacherListByScope(int schoolId, int scope, int pageIndex, int pageSize, out int totalCount)
		{
			var list = new List<TeacherListModel>();
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Teacher/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("appid", Config.AppId);
				param.Add("method", "GetTeacherListByScope");
				param.Add("schoolId", schoolId.ToString());
				param.Add("scope", scope.ToString());
				param.Add("pageIndex", pageIndex.ToString());
				param.Add("pageSize", pageSize.ToString());
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, param); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<TeacherListModel>>>(resultStr);
				if (resultData.State == 1 && resultData.Data != null)
				{
					totalCount = resultData.DataCount;
					list = resultData.Data;
					#region 遍历头像

					var keys = "";
					list.ForEach(p =>
					{
						keys += p.UserId + ",";
					});
					keys = keys.TrimEnd(',');
					var avatarList = UserService.GetUserAvatar(keys);
					list.ForEach(p =>
										{
											var key = p.UserId;
											var avatar = avatarList.FirstOrDefault(a => a.UserId == key);
											p.Avatar = avatar == null ? "" : avatar.Avatar;
										});
					#endregion

					return list;
				}
				totalCount = 0;
				return list;
			}
			catch (Exception)
			{
				totalCount = 0;
				return list;
			}

		}
		/// <summary>
		/// 获取教学主管列表
		/// </summary>
		/// <param name="schoolId">学校Id</param>
		/// <param name="grade">年级</param>
		/// <returns></returns>
		public List<TeacherListModel> GetTeacherDirectorListByGrade(int schoolId, int grade)
		{
			var list = new List<TeacherListModel>();
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "Teacher/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("appid", Config.AppId);
				param.Add("method", "GetTeacherDirectorListByGrade");
				param.Add("schoolId", schoolId.ToString());
				param.Add("grade", grade.ToString());
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, param); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<TeacherListModel>>>(resultStr);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data;
					#region 遍历头像

					var keys = "";
					list.ForEach(p =>
					{
						keys += p.UserId + ",";
					});
					keys = keys.TrimEnd(',');
					var avatarList = UserService.GetUserAvatar(keys);
					list.ForEach(p =>
					{
						var key = p.UserId;
						var avatar = avatarList.FirstOrDefault(a => a.UserId == key);
						p.Avatar = avatar == null ? "" : avatar.Avatar;
					});
					#endregion
				}
				return list;
			}
			catch (Exception)
			{
				return list;
			}

		}
		#endregion

	}
}
