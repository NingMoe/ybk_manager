using System;
using System.Collections.Generic;
using System.Linq;
using DataEntity;

namespace DataService
{
	public class BudgetService
	{

		#region 区域-预算-预收款／行课收入，根据财年和区域获取校区应收款列表
		public static List<PaymentEntity> GetAreaPaymentList(int schoolId,int year,int quarter,string district,int sortType,int dataType,string areaCodes="")
		{
			if (sortType == 0) sortType = 6;
			if (district == "全部区域") district = "";


			var list = new List<PaymentEntity>();

			try
			{
				var apiUrl = Config.UpocCommonUrl + "District/Index";

				Dictionary<string, string> param = new Dictionary<string, string>();
				param.Add("method", "GetAreaPaymentList");
				param.Add("appid", Config.AppId);
				param.Add("schoolId", schoolId.ToString());
				param.Add("year",year.ToString());
				param.Add("quarter",quarter.ToString());
				param.Add("districtName", district);
				param.Add("sortType",sortType.ToString());
				param.Add("dataType",dataType.ToString());
				param.Add("areaCodes",areaCodes);
				string sign = Helper.GetSign(param);
				param.Add("sign", sign);

				var result = Helper.DoPost(apiUrl, param); //提交post请求
				result = result.Replace("\r\n", "").Replace("\\", "");
				var resultData = Helper.FromJsonTo<Result<List<PaymentEntity>>>(result);
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
	}
}
