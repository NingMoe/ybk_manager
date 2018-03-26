using System;
using System.Collections.Generic;
using DataEntity;

namespace DataService
{
	/// <summary>
	/// 区域功能相关
	/// </summary>
	public class DistrictService
	{
		public DistrictService()
		{
		}  


		#region 获取科目列表
		public static List<CourseEntity> GetCourseList(int schoolId)
		{
			var list = new List<CourseEntity>();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "Common/Index";
				var method = "GetCourseList";
				var dict = new Dictionary<string, string>();
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("schoolId", schoolId.ToString());
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<CourseEntity>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data;
					var defaultEntity = new CourseEntity();
					defaultEntity.CourseName = "全部科目";
					list.Insert(0, defaultEntity);
					return list;
				}
				return list;
			}
			catch (Exception ex)
			{
				return list;
			}
		}
		#endregion
	}
}
