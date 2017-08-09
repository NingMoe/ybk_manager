using System;
using System.Text;

namespace System
{
	/// <summary>
	/// 字符串处理扩展类
	/// </summary>
	public static class StringExtention
	{
		#region 删除字符串尾部的回车/换行/空格
		/// <summary>
		/// 删除字符串尾部的回车/换行/空格
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string RightTrim(this string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty;
			}
			return input.TrimEnd(' ', '\r', '\n');
		}
		#endregion

		#region 清除回车和换行
		/// <summary>
		/// 清除给定字符串中的回车及换行符
		/// </summary>
		/// <param name="input">要清除的字符串</param>
		/// <returns>清除后返回的字符串</returns>
		public static string ClearBr(this string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return string.Empty;
			return input.Replace("\r\n", string.Empty);
		}
		#endregion

		#region 字符串截取
		/// <summary>
		/// 从字符串的指定位置截取指定长度的子字符串(不报异常的substring)
		/// </summary>
		/// <param name="input">原字符串</param>
		/// <param name="startIndex">子字符串的起始位置</param>
		/// <param name="length">子字符串的长度</param>
		/// <returns>子字符串</returns>
		public static string CutString(this string input, int startIndex, int length)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty;
			}
			if (startIndex >= 0 && length > 0 && startIndex + length <= input.Length)
			{
				return input.Substring(startIndex, length);
			}
			return input;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string CutString(this string input, int length)
		{
			return input.CutString(0, length);
		}
		/// <summary>
		/// 中英文字符截字
		/// </summary>
		/// <param name="input"></param>
		/// <param name="len"></param>
		/// <param name="tail"> </param>
		/// <returns></returns>
		public static string CutStringCN(this string input, int len, string tail = null)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty;
			}

			int l = input.Length;

			for (int i = 0; i < l && i < len; i++)
			{
				if (input[i] > 0xFF)
					len--;
			}

			if (l < len)
				return input;

			string result = input.Substring(0, len);

			return string.IsNullOrWhiteSpace(tail) ? result : result + tail;
		}
		#endregion

		#region 字符串长度
		/// <summary>
		/// 获取中文字符串长度
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static int StringLength(this string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return 0;
			}
			int result = input.Length;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] > 0xFF)
					result++;
			}
			return result;
		}

		#endregion

		#region 中英文标点

		///转全角的函数(SBC case)
		///To全角字符串(半角转全角)
		///全角空格为12288,半角空格为32
		///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248 ///
		public static string ToWide(this string input)
		{
			if (input == null)
			{
				return null;
			}
			char[] c = input.ToCharArray();
			for (int i = 0; i < c.Length; i++)
			{
				if (c[i] == 32)
				{
					c[i] = (char)12288; continue;
				}
				if (c[i] < 127) c[i] = (char)(c[i] + 65248);
			}
			return new string(c);
		}

		///转半角的函数(DBC case)
		///To半角字符串
		///全角空格为12288，半角空格为32
		///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248 ///
		public static string ToNarrow(this string input)
		{
			if (input == null)
			{
				return null;
			}
			char[] c = input.ToCharArray();
			for (int i = 0; i < c.Length; i++)
			{
				if (c[i] == 12288)
				{
					c[i] = (char)32; continue;
				}
				if (c[i] > 65280 && c[i] < 65375)
					c[i] = (char)(c[i] - 65248);
			}
			return new string(c);
		}

		#endregion

		

		//#region 从HTML中获取文本,保留br,p,img
		//static readonly Regex RegEx = new Regex(@"</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
		///// <summary>
		///// 从HTML中获取文本,保留br,p,img
		///// </summary>
		///// <param name="html"></param>
		///// <returns></returns>
		//public static string GetTextFromHTML(this string html)
		//{
		//    return RegEx.Replace(html, "");
		//}
		//#endregion

		#region 删除最后一个字符
		/// <summary>
		/// 删除最后一个字符
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ClearLastChar(this string input)
		{
			return (string.IsNullOrWhiteSpace(input)) ? string.Empty : input.Substring(0, input.Length - 1);
		}
		#endregion

		#region 全角数字转化为数字
		/// <summary>
		/// 将全角数字转换为数字
		/// </summary>
		/// <param name="sbcCase"></param>
		/// <returns></returns>
		public static string SbcCaseToNumberic(this string sbcCase)
		{
			char[] c = sbcCase.ToCharArray();
			for (int i = 0; i < c.Length; i++)
			{
				byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
				if (b.Length == 2)
				{
					if (b[1] == 255)
					{
						b[0] = (byte)(b[0] + 32);
						b[1] = 0;
						c[i] = Encoding.Unicode.GetChars(b)[0];
					}
				}
			}
			return new string(c);
		}
		#endregion

		

		#region 是否为空字符
		/// <summary>
		/// 是否为空白字符
		/// </summary>
		/// <param name="input">input</param>
		/// <returns></returns>
		public static bool IsBlank(this string input)
		{
			return string.IsNullOrWhiteSpace(input);
		}
		#endregion

		#region 是否不为空字符
		/// <summary>
		/// 是否为空白字符
		/// </summary>
		/// <param name="input">input</param>
		/// <returns></returns>
		public static bool IsNotBlank(this string input)
		{
			return !string.IsNullOrWhiteSpace(input);
		}
		#endregion

		#region A或者B
		/// <summary>
		/// 优先显示A
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string Or(this string a, string b)
		{
			return string.IsNullOrWhiteSpace(a) ? b : a;
		}
		#endregion

	}
}