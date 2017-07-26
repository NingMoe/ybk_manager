using System;
using System.Collections.Generic;
using System.Text;
using DataEntity;

namespace DataService
{
	/// <summary>
	/// 用户服务类
	/// </summary>
	public class UserService
	{
		#region 校验登录
		public static Result<LoginUserInfoEntity> GetUser(string account, string pwd)
		{
			var encodeUser = Helper.Encode(account, Config.EncodeKey);
			var encodePwd = Helper.Encode(pwd, Config.EncodeKey);
			var result = new Result<LoginUserInfoEntity>();
			try
			{
				var apiUrl = Config.UpocManagerUserUrl + "User/Index";
				var dict = new Dictionary<string, string>();
				var method = "GetManagementLoginUser"; //方法名称，固定值
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("encodeUser", encodeUser);
				dict.Add("encodePwd", encodePwd);
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var resultStr = Helper.DoPost(apiUrl, dict); //提交post请求
				resultStr = resultStr.Replace("\r\n", "").Replace("\\", "");
				result = Helper.FromJsonTo<Result<LoginUserInfoEntity>>(resultStr);
				//查询头像
				if (result.State == 1 && result.Data != null)
				{
					var user = result.Data;

					if (user != null && !string.IsNullOrWhiteSpace(user.UserId)) 					{ 						var userAvatar = GetUserAvatar(user.UserId); 						if (userAvatar != null && userAvatar.Count > 0) 						{ 							var avatar = userAvatar[0]; 							if (avatar != null && !string.IsNullOrWhiteSpace(avatar.Avatar))
							{ 								user.Avatar = avatar.Avatar; 							} 						} 					}
				}
				return result;
			}
			catch (Exception ex)
			{
				return result;
			}
		}
		#endregion

		#region 获取用户头像

		public static List<AvatarInfo> GetUserAvatar(string userIds)
		{
			var avatarList = new List<AvatarInfo>();

			try
			{
				var apiUrl = Config.AvatarApiUrl;
				var dict = new Dictionary<string, string>();
				dict.Add("u", userIds);
				dict.Add("isDefault", "0"); //没有头像不需要默认头像
				var result = Helper.DoGet(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var avatarResult = Helper.FromJsonTo<AvatarResult>(result);

				if (avatarResult.Status == 1 && avatarResult.Pic != null)
				{
					var dictAvatar = Helper.FromJsonTo<Dictionary<string, AvatarInfo>>(avatarResult.Pic.ToString());
					var avatar = new AvatarInfo();
					foreach (var key in dictAvatar.Keys)
					{
						avatar = dictAvatar[key];
						if (avatar != null)
						{
							avatar.UserId = key;

							if (!string.IsNullOrWhiteSpace(avatar.AvatarUrl_big) && avatar.AvatarUrl_big.Contains("upload"))
								avatar.Avatar = avatar.AvatarUrl_big;
							else
								avatar.Avatar = "";

							avatarList.Add(avatar);
						}
					}

					return avatarList;
				}

				return avatarList;
			}
			catch (Exception ex)
			{
				return avatarList;

			}
		}

		#endregion
	}


}
