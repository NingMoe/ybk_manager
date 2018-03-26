using System;
using System.Collections.Generic;
using DataEntity;
using System.Linq;

namespace DataService
{
	public class TotalService
	{
		#region 区域端-累计-人次／收入 根据条件获取校区累计收入列表（包含二级表格-年级列表）
		public static PaymentSumData GetSumPaymentListByArea(int schoolId, int year, int quarter,
													int dataType, int sortType,
													string district, string grade, string course,out int totalCount,
													int needTotal = 1, int pageIndex = 1, int pageSize = 2000,string areaCodes="")
		{
			if (sortType == 0) sortType = 6;
			if (district == "全部区域") district = "";
			if (course == "全部科目")
			{
				var list = DistrictService.GetCourseList(schoolId);
				var entityToRemove = list.Where(t => t.CourseName == "全部科目").FirstOrDefault();
				list.Remove(entityToRemove);
				course = "";
				list.ForEach(t => course += string.Format("{0},",t.CourseName));
				course = course.TrimEnd(',');
			}

			var paymentSum = new PaymentSumData();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "District/Index";

				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("method", "GetSumPaymentListByArea");
				param.Add("appid", Config.AppId);
				param.Add("schoolId", schoolId.ToString());
				param.Add("year", year.ToString());
				param.Add("quarter", quarter.ToString());
				param.Add("dataType", dataType.ToString());
				param.Add("sortType", sortType.ToString());
				param.Add("needTotal", needTotal.ToString());
				param.Add("district", district);
				param.Add("grade", grade);
				param.Add("course", course);
				param.Add("pageIndex", pageIndex.ToString());
				param.Add("pageSize", pageSize.ToString());
				param.Add("areaCodes", areaCodes);
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, param); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<PaymentSumData>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					paymentSum = resultData.Data;
					totalCount = resultData.DataCount;
					return paymentSum;
				}
				totalCount = 0;
				return paymentSum;
			}
			catch (Exception ex)
			{
				totalCount = 0;
				return paymentSum;
			}
		}
		#endregion

		#region 获取累计-教师维度数据
        /// <summary>
        /// Gets the sum payment list by teacher.
        /// </summary>
        /// <returns>The sum payment list by teacher.</returns>
        /// <param name="schoolId">School identifier.</param>
        /// <param name="year">Year.</param>
        /// <param name="quarter">Quarter.</param>
        /// <param name="dataType">1=人次 2=收入</param>
        /// <param name="sortType">排序类型： 1=科目升序 2=科目倒序 3=总人次升序 4=总人次倒序（默认）
        ///  5=班量升序 6=班量倒序  7=班均升序 8=班均倒序  9=续班率升序 10=续班率倒序 11=退班率升序 12=退班率倒序</param>
        /// <param name="areaCode">Area code.</param>
        /// <param name="grade">Grade.</param>
        /// <param name="course">Course.</param>
        /// <param name="needTotal">Need total.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
		public static PaymentSumTeacherData GetSumPaymentListByTeacher(int schoolId, int year, int quarter,
											int dataType, int sortType,
											string areaCode, string grade, string course, int needTotal = 1,
											int pageIndex = 1, int pageSize = 2000)
		{
			if (course == "全部科目")
			{
				var list = DistrictService.GetCourseList(schoolId);
				var entityToRemove = list.Where(t => t.CourseName == "全部科目").FirstOrDefault();
				list.Remove(entityToRemove);
				course = "";
				list.ForEach(t => course += string.Format("{0},", t.CourseName));
				course = course.TrimEnd(',');
			}


			var paymentSumTeacher = new PaymentSumTeacherData();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "District/Index";
				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("method", "GetSumPaymentListByTeacher");
				param.Add("appid", Config.AppId);
				param.Add("schoolId", schoolId.ToString());
				param.Add("year", year.ToString());
				param.Add("quarter", quarter.ToString());
				param.Add("dataType", dataType.ToString());
				param.Add("sortType", sortType.ToString());
				param.Add("needTotal", needTotal.ToString());
				param.Add("areaCode", areaCode);
				param.Add("grade", grade);
				param.Add("course", course);
				param.Add("pageIndex", pageIndex.ToString());
				param.Add("pageSize", pageSize.ToString());
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, param); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<PaymentSumTeacherData>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					paymentSumTeacher = resultData.Data;
					return paymentSumTeacher;
				}
				return paymentSumTeacher;
			}
			catch (Exception ex)
			{
				return paymentSumTeacher;
			}
		}
		#endregion

	}
}
