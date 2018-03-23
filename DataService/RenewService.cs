using System;
using System.Collections.Generic;
using System.Linq;
using DataEntity;

namespace DataService
{
	//报表-续班相关
	public class RenewService
	{
		#region 获取首页-续班率统计（初中、高中两组） Type值：1-初中；2-高中，需页面调整。
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="year">财年，比如2018</param>
		/// <param name="quarter">季度，Q1即写1</param>
		/// <returns></returns>
		public static List<ResultData_DepartmentRenewInfo> GetIndexRenewInfoByDepartment(int schoolId, int year, int quarter)
		{
			var tuple = GetYearSeason(year, quarter);
			year = tuple.Item1;
			quarter = tuple.Item2;

			var list = new List<ResultData_DepartmentRenewInfo>();
			try
			{
				var apiUrl = Config.RenewApiUrl;
				var dict = new Dictionary<string, string>();
				var method = "GetRenewInfoByDepartment"; //方法名称，固定值
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("SchoolId", schoolId.ToString());
				dict.Add("Year", year.ToString());
				dict.Add("Quarter", quarter.ToString());
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<ResultData_DepartmentRenewInfo>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data;
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

		#region 首页-续班统计（前三名、后三名）  已按续班率倒序排列，需页面处理：前三名取Top3，后三名取Last3！！
		/// <summary>
		/// 入参默认值无需调整
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="year">财年，例如：2018</param>
		/// <param name="quarter">季度，例如:Q1即写1</param>
		/// <param name="grade">年级（多选用,分隔）,例如：初一,初二,高二</param>
		/// <param name="district">区域，例如：东南区</param>
		/// <param name="needTotal">是否需要总计行，0-否；1-是</param>
		/// <param name="sortType">排序列：原班人数倒序（默认）；1-原班人数升序；2-原班人数倒序；3-续班人数升序；4-续班人数倒序；5-平均续班率升序；6-平均续班率倒序。</param>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">条数</param>
		/// <returns></returns>
		public static List<RenewInfo> GetIndexRenewInfoInGroup(int schoolId, int year, int quarter,
																 string grade, string district, int needTotal = 0,
																 int sortType = 6, int pageIndex = 1, int pageSize = 30)
		{
			if (sortType == 0) sortType = 6;
			if (pageIndex == 0) pageIndex = 1;
			if (pageSize == 0) pageSize = 30;
			if (district == "全部区域") district = "";

			var tuple = GetYearSeason(year, quarter);
			year = tuple.Item1;
			quarter = tuple.Item2;

			var list = new List<RenewInfo>();
			try
			{
				var apiUrl = Config.RenewApiUrl;
				var method = "GetRenewInfoInGroup";
				var dict = new Dictionary<string, string>();
				dict.Add("method", method);
				dict.Add("SchoolId", schoolId.ToString());
				dict.Add("Year", year.ToString());
				dict.Add("Quarter", quarter.ToString());
				dict.Add("Grade", grade);
				dict.Add("District", district);
				dict.Add("NeedTotal", needTotal.ToString());
				dict.Add("SortType", sortType.ToString());
				dict.Add("PageIndex", pageIndex.ToString());
				dict.Add("PageSize", pageSize.ToString());
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<ResultData_RenewInfoInGroup>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data.RenewInfo;
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

		#region 获取教学报表-教研组维度统计数据
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="year">财年，例如：2018</param>
		/// <param name="quarter">季度，例如:Q1即写1</param>
		/// <param name="grade">年级（多选用,分隔）,例如：初一,初二,高二</param>
		/// <param name="district">区域，例如：东南区</param>
		/// <param name="needTotal">是否需要总计行，0-否；1-是</param>
		/// <param name="sortType">排序列：原班人数倒序（默认）；1-原班人数升序；2-原班人数倒序；3-续班人数升序；4-续班人数倒序；5-平均续班率升序；6-平均续班率倒序。</param>
		/// <param name="classStatus">班级开课状态：0-开课中（默认），3-全部</param>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">条数</param>
		/// <returns></returns>
		public static ResultData_RenewInfoInGroup GetRenewInfoInGroup(int schoolId, int year, int quarter,
																	  string grade, string district, int needTotal = 1,
																	  int sortType = 6, int pageIndex = 1, int pageSize = 30,
																	  int? classStatus = 0)
		{
			if (sortType == 0) sortType = 6;
			if (pageIndex == 0) pageIndex = 1;
			if (pageSize == 0) pageSize = 30;
			if (district == "全部区域") district = "";

			var beginDate = "";
			var endDate = "";
			if (classStatus == 1)
			{
				var tupleDate = GetSeasonDateSpan(year, quarter);
				beginDate = tupleDate.Item1;
				endDate = tupleDate.Item2;
			}

			var tuple = GetYearSeason(year, quarter);
			year = tuple.Item1;
			quarter = tuple.Item2;

			var data = new ResultData_RenewInfoInGroup();
			try
			{
				var apiUrl = Config.RenewApiUrl;
				var method = "GetRenewInfoInGroup";
				var dict = new Dictionary<string, string>();
				dict.Add("method", method);
				dict.Add("SchoolId", schoolId.ToString());
				dict.Add("Year", year.ToString());
				dict.Add("Quarter", quarter.ToString());
				dict.Add("Grade", grade);
				dict.Add("District", district);
				dict.Add("NeedTotal", needTotal.ToString());
				dict.Add("SortType", sortType.ToString());
				dict.Add("PageIndex", pageIndex.ToString());
				dict.Add("PageSize", pageSize.ToString());
				dict.Add("BeginDate", beginDate);
				dict.Add("EndDate", endDate);
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<ResultData_RenewInfoInGroup>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					data = resultData.Data;
					var renewInfoList = data.RenewInfo;
					if (renewInfoList == null)
					{
						renewInfoList = new List<RenewInfo>();
					}
					var renewInfo = new RenewInfo();
					renewInfo.Item1 = schoolId;
					renewInfo.Item2 = "";
					renewInfo.Item3 = "总计";
					renewInfo.Item4 = data.TotalNum;
					renewInfo.Item5 = data.RenewNum;
					renewInfo.Item6 = data.RenewRate;
					renewInfoList.Add(renewInfo);
					data.RenewInfo = renewInfoList;

					return data;
				}
				return data;
			}
			catch (Exception ex)
			{
				return data;
			}
		}
		#endregion

		#region 获取教学报表-教师维度统计数据
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schoolId">学校Id</param>
		/// <param name="year">财年，例如：2018</param>
		/// <param name="quarter">季度，例如:Q1即写1</param>
		/// <param name="grade">年级（多选用,分隔）,例如：初一,初二,高二</param>
		/// <param name="district">区域，例如：东南区</param>
		/// <param name="needTotal">是否需要总计行，0-否；1-是</param>
		/// <param name="groupCode">教研组编号，例如：132</param>
		/// <param name="sortType">排序列：原班人数倒序（默认）；1-原班人数升序；2-原班人数倒序；3-续班人数升序；4-续班人数倒序；5-平均续班率升序；6-平均续班率倒序。</param>
		/// <param name="classStatus">班级开课状态：0-开课中（默认），3-全部</param>
		/// <returns></returns>
		public static ResultData_RenewInfoInGroup GetRenewInfoInTeacherByGroupCode(int schoolId, int year, int quarter,
																				   string grade, string district, string groupCode,
																				   int needTotal = 1, int sortType = 6,
																					 int? classStatus = 0)
		{
			if (sortType == 0) sortType = 6;
			if (district == "全部区域") district = "";

			var beginDate = "";
			var endDate = "";
			if (classStatus == 1)
			{
				var tupleDate = GetSeasonDateSpan(year, quarter);
				beginDate = tupleDate.Item1;
				endDate = tupleDate.Item2;
			}

			var tuple = GetYearSeason(year, quarter);
			year = tuple.Item1;
			quarter = tuple.Item2;

			var data = new ResultData_RenewInfoInGroup();
			try
			{
				var apiUrl = Config.RenewApiUrl;
				var method = "GetRenewInfoInTeacherByGroupCode";
				var dict = new Dictionary<string, string>();
				dict.Add("method", method);
				dict.Add("SchoolId", schoolId.ToString());
				dict.Add("Year", year.ToString());
				dict.Add("Quarter", quarter.ToString());
				dict.Add("Grade", grade);
				dict.Add("District", district);
				dict.Add("GroupCode", groupCode);
				dict.Add("NeedTotal", needTotal.ToString());
				dict.Add("SortType", sortType.ToString());
				dict.Add("BeginDate", beginDate);
				dict.Add("EndDate", endDate);
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<ResultData_RenewInfoInGroup>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					data = resultData.Data;
					var renewInfoList = data.RenewInfo;
					if (renewInfoList == null)
					{
						renewInfoList = new List<RenewInfo>();
					}
					var renewInfo = new RenewInfo();
					renewInfo.Item1 = schoolId;
					renewInfo.Item2 = "";
					renewInfo.Item3 = "总计";
					renewInfo.Item4 = data.TotalNum;
					renewInfo.Item5 = data.RenewNum;
					renewInfo.Item6 = data.RenewRate;
					renewInfoList.Add(renewInfo);
					data.RenewInfo = renewInfoList;
					return data;
				}
				return data;
			}
			catch (Exception ex)
			{
				return data;
			}
		}
		#endregion

		#region 获取教学报表-班级维度统计数据
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="schoolId">学校Id</param>
		/// <param name="year">财年，例如：2018</param>
		/// <param name="quarter">季度，例如:Q1即写1</param>
		/// <param name="grade">年级（多选用,分隔）,例如：初一,初二,高二</param>
		/// <param name="district">区域，例如：东南区</param>
		/// <param name="teacherCode">教师编号</param>
		/// <param name="needTotal">是否需要总计行，0-否；1-是</param>
		/// <param name="sortType">排序列：原班人数倒序（默认）；1-原班人数升序；2-原班人数倒序；3-续班人数升序；4-续班人数倒序；5-平均续班率升序；6-平均续班率倒序。</param>
		/// <param name="classStatus">班级开课状态：0-开课中（默认），3-全部</param>
		/// <returns></returns>
		public static ResultData_RenewInfoInClass GetRenewInfoInClassByTeacher(int schoolId, int year, int quarter,
																			   string grade, string district, string teacherCode,
																			   int needTotal = 1, int sortType = 6,
																				 int? classStatus = 0)
		{
			if (sortType == 0) sortType = 6;
			if (district == "全部区域") district = "";

			var beginDate = "";
			var endDate = "";
			if (classStatus == 1)
			{
				var tupleDate = GetSeasonDateSpan(year, quarter);
				beginDate = tupleDate.Item1;
				endDate = tupleDate.Item2;
			}

			var tuple = GetYearSeason(year, quarter);
			year = tuple.Item1;
			quarter = tuple.Item2;

			var data = new ResultData_RenewInfoInClass();

			try
			{
				var apiUrl = Config.RenewApiUrl;
				var method = "GetRenewInfoInClassByTeacher";
				var dict = new Dictionary<string, string>();
				dict.Add("method", method);
				dict.Add("schoolId", schoolId.ToString());
				dict.Add("Year", year.ToString());
				dict.Add("Quarter", quarter.ToString());
				dict.Add("Grade", grade);
				dict.Add("District", district);
				dict.Add("TeacherCode", teacherCode);
				dict.Add("NeedTotal", needTotal.ToString());
				dict.Add("SortType", sortType.ToString());
				dict.Add("BeginDate", beginDate);
				dict.Add("EndDate", endDate);
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<ResultData_RenewInfoInClass>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					data = resultData.Data;
					var renewInfoList = data.RenewInfo;
					if (renewInfoList == null)
					{
						renewInfoList = new List<Statistics_ClassRenewSummary>();
					}
					var renewInfo = new Statistics_ClassRenewSummary();
					renewInfo.ClassName = "总计";
					renewInfo.TotalStudentNum = data.TotalNum;
					renewInfo.RenewStudentNum = data.RenewNum;
					renewInfo.RenewRate = data.RenewRate;
					renewInfoList.Add(renewInfo);
					data.RenewInfo = renewInfoList;
					return data;
				}
				return data;
			}
			catch (Exception ex)
			{
				return data;
			}
		}
		#endregion

		#region 获取班级学员续班统计数据
		/// <summary>
		/// 
		/// </summary>
		///<param name="schoolId">学校Id</param>
		/// <param name="classCode">班级编号</param>
		/// <returns></returns>
		public static ClassRenewEntity GetStudentRenewInfoListByClassCode(int schoolId, string classCode)
		{
			var classRenewEntity = new ClassRenewEntity();
			try
			{
				var apiUrl = Config.RenewApiUrl;
				var dict = new Dictionary<string, string>();
				var method = "GetStudentRenewInfoListByClassCode"; //方法名称，固定值
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("schoolId", schoolId.ToString());
				dict.Add("classCode", classCode);
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<StudentRenewModel>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					var data = resultData.Data;
					var renewStudents = data.Where(t => t.isRenew == 1).ToList();
					var notRenewStudents = data.Where(t => t.isRenew != 1).ToList();
					classRenewEntity.RenewStudents = renewStudents;
					classRenewEntity.NotRenewStudents = notRenewStudents;
					#region 已续班学员头像
					if (renewStudents != null && renewStudents.Count > 0)
					{
						var userIds = string.Join(",", renewStudents.Where(t => t.userId != "").Select(t => t.userId).ToArray());
						userIds = userIds.TrimEnd(',');
						var avatars = UserService.GetUserAvatar(userIds);
						for (int i = 0; i < renewStudents.Count; i++)
						{
							var student = renewStudents[i] as StudentRenewModel;
							if (string.IsNullOrWhiteSpace(student.userId))
								continue;

							var avatar = avatars.Where(t => t.UserId == student.userId).FirstOrDefault();
							if (avatar != null && !(string.IsNullOrWhiteSpace(avatar.Avatar)))
								student.avatar = avatar.Avatar;
							else
								student.avatar = "";
						}
					}
					#endregion
					#region 未续班学员头像
					if (notRenewStudents != null && notRenewStudents.Count > 0)
					{
						var userIds = string.Join(",", notRenewStudents.Where(t => t.userId != "").Select(t => t.userId).ToArray());
						userIds = userIds.TrimEnd(',');
						var avatars = UserService.GetUserAvatar(userIds);
						for (int i = 0; i < notRenewStudents.Count; i++)
						{
							var student = notRenewStudents[i] as StudentRenewModel;
							if (string.IsNullOrWhiteSpace(student.userId))
								continue;
							var avatar = avatars.Where(t => t.UserId == student.userId).FirstOrDefault();
							if (avatar != null && !(string.IsNullOrWhiteSpace(avatar.Avatar)))
								student.avatar = avatar.Avatar;
							else
								student.avatar = "";
						}
					}
					#endregion
					return classRenewEntity;
				}
				return classRenewEntity;

			}
			catch (Exception ex)
			{
				return classRenewEntity;
			}
		}
		#endregion

		#region 获取学员的报班记录列表
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schoolId"></param>
		/// <param name="studentCode">学员编号</param>
		/// <param name="classStatus">班级状态：0-上课中；1-已结课；2-未开课；3-全部</param>
		/// <returns></returns>
		public static List<PureClassEntity> GetClassListOfStudent(int schoolId, string studentCode, int classStatus)
		{
			try
			{
				var orderBy = "BeginDate";
				var direc = "asc";
				var pageIndex = 1;
				var pageSize = 200;
				var apiUrl = Config.SpocApiUrl + "Class";
				var dict = new Dictionary<string, string>();
				var method = "GetClassListOfStudentFromDataMart"; //方法名称，固定值
				dict.Add("appId", Config.AppId);
				dict.Add("classStatus", classStatus.ToString());
				dict.Add("direc", direc);
				dict.Add("method", method);
				dict.Add("orderBy", orderBy);
				dict.Add("pageIndex", pageIndex.ToString());
				dict.Add("pageSize", pageSize.ToString());
				dict.Add("schoolId", schoolId.ToString());
				dict.Add("studentCode", studentCode);
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<PureClassEntity>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					return resultData.Data;
				}
				return new List<PureClassEntity>();

			}
			catch (Exception ex)
			{
				return new List<PureClassEntity>();
			}
		}
		#endregion

		#region 财年Q转年季度
		public static Tuple<int, int> GetYearSeason(int fiscalYear, int quarter)
		{
			var year = fiscalYear;
			var season = quarter;

			if (quarter < 1 || quarter > 4)
			{
				return new Tuple<int, int>(0, 0);
			}

			if (quarter < 4)
			{
				year = fiscalYear - 1;
				season = quarter + 1;
			}
			else
			{
				year = fiscalYear;
				season = 1;
			}


			return new Tuple<int, int>(year, season);
		}

		#endregion

		#region  财年Q转时间段 即2018财年Q1转换为2017.6.1-2017.8.31，为接口数据容易计算结束值为2017.9.1
		public static Tuple<string, string> GetSeasonDateSpan(int fiscalYear, int quarter)
		{
			var beginDate = new DateTime();
			var endDate = new DateTime();

			if (quarter < 1 || quarter > 4)
			{
				return new Tuple<string, string>("", "");
			}

			if (quarter == 1)
			{
				beginDate = new DateTime(fiscalYear - 1, 6, 1);
				endDate = new DateTime(fiscalYear - 1, 9, 1);
			}
			else if (quarter == 2)
			{
				beginDate = new DateTime(fiscalYear - 1, 9, 1);
				endDate = new DateTime(fiscalYear - 1, 12, 1);
			}
			else if (quarter == 3)
			{
				beginDate = new DateTime(fiscalYear - 1, 12, 1);
				endDate = new DateTime(fiscalYear, 3, 1);
			}
			else
			{
				beginDate = new DateTime(fiscalYear, 3, 1);
				endDate = new DateTime(fiscalYear, 6, 1);
			}
			return new Tuple<string, string>(beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
		}
		#endregion

		/*报表顶部筛选框数据源*/

		#region 获取财年季度数据
		public static List<QuarterEntity> GetQuarter(int schoolId)
		{
			var list = new List<QuarterEntity>();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "Common/Index";

				var method = "GetQuarter";
				var dict = new Dictionary<string, string>();
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("schoolId", schoolId.ToString());
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<QuarterEntity>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data;
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

		#region 获取年级列表
		public static List<GradeEntity> GetGradeList(int schoolId)
		{
			var list = new List<GradeEntity>();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "Common/Index";
				var method = "GetGradeList";
				var dict = new Dictionary<string, string>();
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("schoolId", schoolId.ToString());
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<GradeEntity>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data;
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

		#region 获取区域列表
		public static List<DistrictEntity> GetDistrictList(int schoolId)
		{
			var list = new List<DistrictEntity>();
			try
			{
				var apiUrl = Config.UpocCommonUrl + "Common/Index";
				var method = "GetDistrictList";
				var dict = new Dictionary<string, string>();
				dict.Add("appId", Config.AppId);
				dict.Add("method", method);
				dict.Add("schoolId", schoolId.ToString());
				var sign = Helper.GetSign(dict);
				dict.Add("sign", sign);
				var result = Helper.DoPost(apiUrl, dict); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<DistrictEntity>>>(result);
				if (resultData.State == 1 && resultData.Data != null)
				{
					list = resultData.Data;
					list.ForEach(t => t.DistrictCode = t.DistrictCode.ToLower());
					var defaultEntity = new DistrictEntity();
					defaultEntity.DistrictName = "全部区域";
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