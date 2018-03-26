using System;
using System.Collections.Generic;
using DataEntity;

namespace DataService
{
	public class NewService
	{
		public NewService()
		{
		}
		#region 获取新生类型
		public static  List<StudentCategoryEntity> GetStudentCategoryList(int schoolId = 1)
		{
			try
			{
				var apiUrl = Config.UpocCommonUrl + "Common/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("method", "GetStudentCategoryList");
				param.Add("appid", Config.AppId);
				param.Add("schoolId", schoolId.ToString());
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, param); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<StudentCategoryEntity>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					var list = resultData.Data;

					var defaultEntity1 = new StudentCategoryEntity();
					defaultEntity1.CategoryName = "全部新生";
					defaultEntity1.CategoryValue = "";
					defaultEntity1.DataType = 1;
					list.Insert(0, defaultEntity1);

					var defaultEntity2 = new StudentCategoryEntity();
					defaultEntity2.CategoryName = "全部新生";
					defaultEntity2.CategoryValue = "";
					defaultEntity2.DataType = 2;
					list.Insert(0, defaultEntity2);

					return list;
				}
				return new List<StudentCategoryEntity>();
			}
			catch (Exception ex)
			{
				return new List<StudentCategoryEntity>();
			}
		}
		#endregion

		#region 区域端-招新-校区维度数据
		/// <summary>
		/// Gets the sum new student list.
		/// </summary>
		/// <returns>The sum new student list.</returns>
		/// <param name="schoolId">学校Id</param>
		/// <param name="year">财年</param>
		/// <param name="quarter">季度</param>
		/// <param name="dataType">Data type.</param>
		/// <param name="sortType">Sort type.</param>
		/// <param name="needTotal">Need total.</param>
		/// <param name="district">District.</param>
		/// <param name="category">Category.</param>
		/// <param name="grade">Grade.</param>
		/// <param name="pageIndex">Page index.</param>
		/// <param name="pageSize">Page size.</param>
		public static NewStudentSumData GetSumNewStudentList(int schoolId, int year, int quarter,
													int dataType, int sortType,
													string district, string category,
													string grade, out int totalCount, int needTotal = 1,
													int pageIndex = 1, int pageSize = 2000, string areaCodes = "")
		{
			if (sortType == 0) sortType = 6;
			if (district == "全部区域") district = "";

			var newData = new NewStudentSumData();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "District/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("method", "GetSumNewStudentList");
				param.Add("appid", Config.AppId);
				param.Add("schoolId", schoolId.ToString());
				param.Add("year", year.ToString());
				param.Add("quarter", quarter.ToString());
				param.Add("dataType", dataType.ToString());
				param.Add("sortType", sortType.ToString());
				param.Add("needTotal", needTotal.ToString());
				param.Add("district", district);
				param.Add("category", category);
				param.Add("grade", grade);
				param.Add("pageIndex", pageIndex.ToString());
				param.Add("pageSize", pageSize.ToString());
				param.Add("areaCodes",areaCodes);
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, param); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<NewStudentSumData>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					newData = resultData.Data;
					totalCount = resultData.DataCount;
					return newData;
				}
				totalCount = 0;
				return newData;
			}
			catch (Exception ex)
			{
				totalCount = 0;
				return newData;
			}
		}
		#endregion
	}
}
