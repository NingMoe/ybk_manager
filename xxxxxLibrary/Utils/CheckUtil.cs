using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace xxxxxLibrary.Utils
{ 
    /// <summary>
    /// 字符串验证格式帮助类
    /// </summary>
    public class CheckUtil
	{
		#region 是否Email
		/// <summary>
		/// 检测是否符合email格式
		/// </summary>
		/// <param name="strEmail">要判断的email字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsValidEmail(string strEmail)
		{
			return Regex.IsMatch(strEmail, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
		}
		#endregion

		#region 是否手机号码
		/// <summary>
		/// 检测是否符合手机号码格式
		/// </summary>
		/// <param name="phone">要判断的phone字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsValidPhone(string phone)
		{
            return Regex.IsMatch(phone, @"^1\d{10}$");
		}
		#endregion

		#region 是否Url格式的
		/// <summary>
		/// 检测是否是正确的Url
		/// </summary>
		/// <param name="strUrl">要验证的Url</param>
		/// <returns>判断结果</returns>
		public static bool IsURL(string strUrl)
		{
			return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
		}
		#endregion

		#region 判断是否为base64字符串
		/// <summary>
		/// 判断是否为base64字符串
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsBase64String(string str)
		{
			//A-Z, a-z, 0-9, +, /, =
			return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
		}
		#endregion

		#region 是否危险sql
		/// <summary>
		/// 检测是否有Sql危险字符
		/// </summary>
		/// <param name="str">要判断字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsSafeSqlString(string str)
		{
			return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
		}
		#endregion

		#region 是否是时间格式
		/// <summary>
		/// 是否时间格式
		/// </summary>
		/// <returns></returns>
		public static bool IsTimeString(string timeval)
		{
			return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
		}
		#endregion

		#region 是否日期格式
		/// <summary>
		/// 判断字符串是否是yy-mm-dd字符串
		/// </summary>
		/// <param name="str">待判断字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsDateString(string str)
		{
			return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
		}
		#endregion

		#region 是否IP
		/// <summary>
		/// 是否为ip
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIP(string ip)
		{
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
		}
		#endregion

		#region 是否double
		/// <summary>
		/// 是否double
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsDouble(string str)
		{
			double result;
			return double.TryParse(str, out result);
		}
		#endregion

		#region 判断是否为数字
		/// <summary>
		/// 是否为数字
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsNumber(string input)
		{
			long ret;
			return long.TryParse(input, out ret);
		}
		#endregion

		#region 判断是否为正整数
		/// <summary>
		/// 是否正整数
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsPositiveInt(string input)
		{
			long ret;
			return long.TryParse(input, out ret) && ret > 0;
		}
		#endregion
	}
}
