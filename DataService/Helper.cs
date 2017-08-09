using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Android.Net;
using Android.Content;
using Android.Telephony;
using System.Security.Cryptography;
using Org.Apache.Http.Protocol;
using System.IO.Compression;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using Android.Media;
using System.Text.RegularExpressions;

namespace DataService
{
	public class Helper
	{
		/// <summary>
		/// 获取网络状态
		/// </summary>
		/// <returns><c>true</c>, if net work was checked, <c>false</c> otherwise.</returns>
		/// <param name="context">Context.</param>
		public static bool CheckNetWork(Context context)
		{

			var connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
			if (connectivityManager.ActiveNetworkInfo != null)
			{
				NetworkInfo.State netWorkState = connectivityManager.ActiveNetworkInfo.GetState();
				if (netWorkState == NetworkInfo.State.Connected)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Sets the cache by key.
		/// </summary>
		/// <returns><c>true</c>, if cache by key was set, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public static bool SetCacheByKey(string key, string value)
		{

			var baseDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filename = System.IO.Path.Combine(baseDir, "cache_" + key + "_temp");

			bool rtn = false;
			try
			{
				StreamWriter swFile = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
				swFile.Write(value);
				rtn = true;
				swFile.Close();
			}
			catch (Exception ex)
			{
				string temp = ex.Message;
				rtn = false;
			}
			return rtn;
		}
		/// <summary>
		/// Deletes the cache by key.
		/// </summary>
		/// <returns><c>true</c>, if cache by key was deleted, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		public static bool DeleteCacheByKey(string key)
		{
			var baseDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filename = System.IO.Path.Combine(baseDir, "cache_" + key + "_temp");
			var _file = new System.IO.FileInfo(filename);
			bool returnFlag = false;
			if (_file.Exists)
			{

				try
				{
					_file.Delete();
					returnFlag = true;
				}
				catch
				{

				}

			}
			return returnFlag;
		}
		/// <summary>
		/// Gets the cache by key.
		/// </summary>
		/// <returns>The cache by key.</returns>
		/// <param name="key">Key.</param>
		public static string GetCacheByKey(string key, bool isCache = false, long cacheTime = 0)
		{
			var baseDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filename = System.IO.Path.Combine(baseDir, "cache_" + key + "_temp");
			var _file = new System.IO.FileInfo(filename);
			string readTxt = string.Empty;
			if (_file.Exists)
			{
				if (isCache && _file.LastWriteTime.AddMinutes(cacheTime) < DateTime.Now)
				{
					return string.Empty;
				}
				StreamReader fileReader = null;
				try
				{
					fileReader = new StreamReader(filename, System.Text.Encoding.UTF8);
					readTxt = fileReader.ReadToEnd();
					fileReader.Close();
				}
				catch (Exception ex)
				{
					string temp = ex.Message;
					return string.Empty;

				}
				finally
				{
					if (fileReader != null)
						fileReader.Close();
				}
			}
			return readTxt;
		}
		/// <summary>
		/// 判断字符串是否是数字
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static int IsIntNum(string s)
		{
			int rtn = 0;
			try
			{
				long num = long.Parse(s);
				rtn = 1;

			}
			catch
			{
				rtn = 0;
			}
			return rtn;
		}
		/// <summary>
		/// 获取网络数据
		/// </summary>
		public static string RequestUrl(string url, Dictionary<string, string> requestParams = null, bool isPost = false, bool hasFile = false)
		{

			string postData = string.Empty;
			if (requestParams != null)
			{
				postData = string.Join("&", requestParams.Select(c => c.Key + "=" + c.Value));
			}

			String responseFromServer = string.Empty;
			try
			{
				// Create a request using a URL that can receive a post.
				if (!isPost)
				{
					url += string.IsNullOrEmpty(postData) ? string.Empty
							: url.IndexOf("?") > -1 ? "&" : "?" + postData;
				}
				Console.WriteLine(url);
				WebRequest webRequest = HttpWebRequest.Create(url);
				{
					try
					{
						((HttpWebRequest)webRequest).KeepAlive = false;
						webRequest.Timeout = 1000 * 30; //
						if (isPost)
						{
							// Set the Method property of the request to POST.
							webRequest.Method = "POST";
							webRequest.Proxy = null;
							// Create POST data and convert it to a byte array.
							byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(postData);
							// Set the ContentType property of the WebRequest.
							webRequest.ContentType = "application/x-www-form-urlencoded";
							// Set the ContentLength property of the WebRequest.
							webRequest.ContentLength = byteArray.Length;
							// Get the request stream.
							using (System.IO.Stream dataStream = webRequest.GetRequestStream())
							{
								dataStream.Write(byteArray, 0, byteArray.Length);
							}
						}
					}
					catch
					{
					}
					// Get the response.
					using (WebResponse response = webRequest.GetResponse())
					{
						//Console.WriteLine(((HttpWebResponse)response).StatusDescription);
						// Get the stream containing content returned by the server.
						using (System.IO.Stream responseStream = response.GetResponseStream())
						{
							using (StreamReader reader = new StreamReader(responseStream))
							{
								responseFromServer = reader.ReadToEnd();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				String error = ex.Message;
				System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
				String method = stackTrace.GetFrame(0).GetMethod().Name;
			}
			return responseFromServer;
		}

		/// <summary>
		/// 网络请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="requestData"></param>
		/// <param name="isPost"></param>
		/// <returns></returns>
		public static string RequestUrl2(string url, Dictionary<string, string> requestParams = null, bool isPost = false, bool hasFile = false)
		{

			string reStr = string.Empty;

			System.Text.Encoding encoding = System.Text.Encoding.UTF8;
			WebClient webClient = new WebClient() { Encoding = encoding };

			//参数整理
			string requestData = string.Empty;
			if (requestParams != null)
			{
				requestData = string.Join("&", requestParams.Select(c => c.Key + "=" + c.Value));
			}
			//请求
			if (isPost) //post
			{

				byte[] sendData = encoding.GetBytes(requestData);
				webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
				webClient.Headers.Add("ContentLength", sendData.Length.ToString());

				byte[] reByte = webClient.UploadData(url, "POST", sendData);
				reStr = encoding.GetString(reByte);

				//var postVars = new System.Collections.Specialized.NameValueCollection();
				//reStr = Encoding.UTF8.GetString(webClient.UploadValues(url, "post", postVars));
			}
			else //get
			{
				//url = "http://passport.wodubao.com/api/login.ashx?action=login";

				url += string.IsNullOrEmpty(requestData) ? string.Empty
					: url.IndexOf("?") > -1 ? "&" : "?" + requestData;
				reStr = webClient.DownloadString(url);

			}
			return reStr;
		}

		public static string RequestUrl(string url, byte[] postData)
		{

			string reStr = string.Empty;

			System.Text.Encoding encoding = System.Text.Encoding.UTF8;
			WebClient webClient = new WebClient() { Encoding = encoding };
			webClient.Headers.Add("Content-Type", "image/jpeg");
			webClient.Headers.Add("ContentLength", postData.Length.ToString());
			byte[] reByte = webClient.UploadData(url, postData);
			reStr = encoding.GetString(reByte);
			return reStr;
		}
		public static string GetMD5(string myString)
		{
			//byte[] b = Encoding.UTF8.GetBytes(myString);
			//b = new MD5CryptoServiceProvider().ComputeHash(b);
			//string ret = "";
			//for (int i = 0; i < b.Length; i++)
			//    ret += b[i].ToString("x").PadLeft(2, '0');
			//return ret;
			string sTemp = "";
			//MD5计算类
			using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
			{
				byte[] bytValue, bytHash;
				//将要计算的字符串转换为字节数组
				bytValue = System.Text.Encoding.UTF8.GetBytes(myString);
				//计算结果同样是字节数组
				bytHash = md5.ComputeHash(bytValue);
				//将字节数组转换为字符串

				for (int i = 0; i < bytHash.Length; i++)
				{
					sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
				}

			}

			return sTemp.ToUpper();
		}

		/// <summary>
		/// Json.NET组件，内部加try catch，记录出错信息
		/// xqy  20131203
		/// </summary> 
		public static T FromJsonTo<T>(string jsonString)
		{
			try
			{

				return JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
			}
			catch (Exception ex)
			{
				//Logs.AppError("【FromJsonTo报错】jsonString：" + jsonString + ex.ToString());
				return JsonConvert.DeserializeObject<T>("{\"id\":\"null\"}", new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

			}
		}

		public static string FromTToString<T>(T vm)
		{
			try
			{
				return JsonConvert.SerializeObject(vm, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
			}
			catch (Exception ex)
			{
				//Logs.AppError("【FromTToString报错】T：" + typeof(T).ToString() + ex.ToString());
				return JsonConvert.SerializeObject(new { id = "" }, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

			}
		}
		/// <summary>
		/// 执行HTTP POST请求。
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="parameters">请求参数</param>
		/// <returns>HTTP响应</returns>
		public static string DoPost(string url, IDictionary<string, string> parameters)
		{
			return DoPost(url, parameters, 0);
		}

		/// <summary>
		/// 执行HTTP POST请求，增加超时时间参数
		/// Mike 20130204 </summary>
		/// <param name="url">请求地址</param>
		/// <param name="parameters">请求参数</param>
		/// <param name="second">超时时间</param>
		/// <returns>HTTP响应</returns>
		public static string DoPost(string url, IDictionary<string, string> parameters, int second)
		{
			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.KeepAlive = true;
				//req.UserAgent = "Top4Net";
				//if (HttpContext.Current != null && HttpContext.Current.Request != null)
				//req.UserAgent = HttpContext.Current.Request.UserAgent;
				req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

				if (second > 0) //超时时间
					req.Timeout = 1000 * second;

				byte[] postData = System.Text.Encoding.UTF8.GetBytes(BuildPostData(parameters));
				System.IO.Stream reqStream = req.GetRequestStream();
				reqStream.Write(postData, 0, postData.Length);
				reqStream.Close();

				var response = req.GetResponse();
				HttpWebResponse rsp = (HttpWebResponse)response;
				if (rsp == null || rsp.CharacterSet == null)
					return "";
				System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(rsp.CharacterSet);
				return GetResponseAsString(rsp, encoding);
			}
			catch (Exception ex) { return ex.Message; }
		}
		/// <summary>
		/// 把响应流转换为文本。
		/// </summary>
		/// <param name="rsp">响应流对象</param>
		/// <param name="encoding">编码方式</param>
		/// <returns>响应文本</returns>
		private static string GetResponseAsString(HttpWebResponse rsp, System.Text.Encoding encoding)
		{
			StringBuilder result = new StringBuilder();
			System.IO.Stream stream = null;
			StreamReader reader = null;

			try
			{
				// 以字符流的方式读取HTTP响应
				stream = rsp.GetResponseStream();
				reader = new StreamReader(stream, encoding);

				// 每次读取不大于256个字符，并写入字符串
				char[] buffer = new char[256];
				int readBytes = 0;
				while ((readBytes = reader.Read(buffer, 0, buffer.Length)) > 0)
				{
					result.Append(buffer, 0, readBytes);
				}
			}
			finally
			{
				// 释放资源
				if (reader != null) reader.Close();
				if (stream != null) stream.Close();
				if (rsp != null) rsp.Close();
			}

			return result.ToString();
		}

		/// <summary>
		/// 执行HTTP GET请求，内部已加try、catch
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="parameters">请求参数</param>
		/// <returns>HTTP响应</returns>
		public static string DoGet(string url, IDictionary<string, string> parameters)
		{
			return DoGet(url, parameters, 0);
		}

		/// <summary>
		/// 执行HTTP GET请求，增加超时时间参数，内部已加try、catch
		/// xqy 20151112
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="parameters">请求参数</param>
		/// <returns>HTTP响应</returns>
		public static string DoGet(string url, IDictionary<string, string> parameters, int second)
		{
			try
			{
				if (parameters != null && parameters.Count > 0)
				{
					if (url.Contains("?"))
					{
						url = url + "&" + BuildPostData(parameters);
					}
					else
					{
						url = url + "?" + BuildPostData(parameters);
					}
				}

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.KeepAlive = true;
				////req.UserAgent = "Top4Net";
				//if (HttpContext.Current != null && HttpContext.Current.Request != null)
				//    req.UserAgent = HttpContext.Current.Request.UserAgent;
				req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

				if (second > 0) //超时时间
					req.Timeout = 1000 * second;

				HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
				if (rsp == null || rsp.CharacterSet == null)
					return string.Empty;
				System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(rsp.CharacterSet);
				return GetResponseAsString(rsp, encoding);
			}
			catch (Exception ex)
			{
				//Logs.AppError(url + Environment.NewLine + ex.ToString()); 
				return ex.Message;
			}
		}
		/// <summary>
		/// 执行HTTP GET请求，增加超时时间参数，内部已加try、catch
		/// xqy 20151112
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="parameters">请求参数</param>
		/// <returns>HTTP响应</returns>
		public static string DoGet1(string url, IDictionary<string, string> parameters)
		{
			String responseFromServer = string.Empty;
			try
			{
				if (parameters != null && parameters.Count > 0)
				{
					if (url.Contains("?"))
					{
						url = url + "&" + BuildPostData(parameters);
					}
					else
					{
						url = url + "?" + BuildPostData(parameters);
					}
				}
				Console.WriteLine(url);
				WebRequest webRequest = HttpWebRequest.Create(url);
				{
					((HttpWebRequest)webRequest).KeepAlive = false;
					webRequest.Timeout = 1000 * 30; //
													// Get the response.
					using (WebResponse response = webRequest.GetResponse())
					{
						//Console.WriteLine(((HttpWebResponse)response).StatusDescription);
						// Get the stream containing content returned by the server.
						using (System.IO.Stream responseStream = response.GetResponseStream())
						{
							using (StreamReader reader = new StreamReader(responseStream))
							{
								responseFromServer = reader.ReadToEnd();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				String error = ex.Message;
				System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
				String method = stackTrace.GetFrame(0).GetMethod().Name;
			}
			return responseFromServer;
		}

		/// <summary>
		/// 拼接URL字符串
		/// xqy 20151112
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static string GetUrl(string url, IDictionary<string, string> parameters)
		{
			if (parameters != null && parameters.Count > 0)
			{
				if (url.Contains("?"))
				{
					url = url + "&" + BuildPostData(parameters);
				}
				else
				{
					url = url + "?" + BuildPostData(parameters);
				}
			}
			return url;
		}




		/// <summary>
		/// 拼接参数，参数值在内部自动url编码，例如：token=1&userId=2
		/// Mike 20131120
		/// </summary>
		public static string BuildPostData(IDictionary<string, string> dic)
		{
			if (dic == null)
				return string.Empty;

			StringBuilder postData = new StringBuilder();
			bool hasParam = false;

			IEnumerator<KeyValuePair<string, string>> dem = dic.GetEnumerator();
			while (dem.MoveNext())
			{
				string name = dem.Current.Key;
				string value = dem.Current.Value;
				// 忽略参数名或参数值为空的参数
				if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
				{
					if (hasParam)
					{
						postData.Append("&");
					}

					postData.Append(name);
					postData.Append("=");
					postData.Append(System.Uri.EscapeDataString(value));
					hasParam = true;
				}
			}

			return postData.ToString();
		}
		public static string DateStr(DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd HH:mm:ss");
		}

		/// <summary>
		/// Json.NET组件，为null时返回空值，
		/// </summary> 
		public static string ToJsonItem(object item)
		{
			if (item == null)
				return "";

			try
			{
				return JsonConvert.SerializeObject(item, new IsoDateTimeConverter());
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}







		/// <summary>
		/// AES 加密 C#与PHP兼容
		/// </summary>        
		public static string Encode(string message, string encryptKey)
		{
			MemoryStream mStream = new MemoryStream();
			RijndaelManaged aes = new RijndaelManaged();

			byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(message);
			Byte[] bKey = new Byte[32];
			Array.Copy(System.Text.Encoding.UTF8.GetBytes(encryptKey.PadRight(bKey.Length)), bKey, bKey.Length);
			aes.Mode = CipherMode.ECB;
			aes.Padding = PaddingMode.Zeros;
			aes.KeySize = 128;
			aes.Key = bKey;
			CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
			try
			{
				cryptoStream.Write(plainBytes, 0, plainBytes.Length);
				cryptoStream.FlushFinalBlock();
				return Convert.ToBase64String(mStream.ToArray());
			}
			finally
			{
				cryptoStream.Close();
				mStream.Close();
				aes.Clear();
			}

		}
		/// <summary>  
		/// AES解密
		/// </summary>  
		/// <param name="encryptedBytes">被加密的明文</param>  
		/// <param name="key">密钥</param>  
		/// <returns>明文</returns>  
		public static string Decode(string Data, string Key)
		{
			Byte[] encryptedBytes = Convert.FromBase64String(Data);
			Byte[] bKey = new Byte[32];
			Array.Copy(System.Text.Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

			MemoryStream mStream = new MemoryStream(encryptedBytes);
			//mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
			//mStream.Seek( 0, SeekOrigin.Begin );  
			RijndaelManaged aes = new RijndaelManaged();
			aes.Mode = CipherMode.ECB;
			aes.Padding = PaddingMode.Zeros;
			aes.KeySize = 128;
			aes.Key = bKey;
			//aes.IV = _iV;  
			CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
			try
			{
				byte[] tmp = new byte[encryptedBytes.Length + 32];
				int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
				byte[] ret = new byte[len];
				Array.Copy(tmp, 0, ret, 0, len);
				return System.Text.Encoding.UTF8.GetString(ret).Trim('\0');
			}
			finally
			{
				cryptoStream.Close();
				mStream.Close();
				aes.Clear();
			}
		}


		/// <summary>
		/// 拼接参数，参数值在内部自动url编码，根据url是否包含问号及按自动处理, 
		/// 已存在同名参数时自动替换参数值，
		/// 例如：http://demo.com/1.aspx问号token=1按userId=2
		/// Mike 20140108
		/// </summary>
		public static string BuildQueryUrl(string url, IDictionary<string, string> dic)
		{
			if (dic == null)
				return url;
			foreach (var item in dic)
			{
				url = AddUrlKeyValue(url, item.Key, item.Value);
			}
			return url;
		}
		/// <summary>
		/// 追加Url参数，如果存在则替换值，自动加问题或安号
		/// </summary>   
		public static string AddUrlKeyValue(string url, string key, string value)
		{
			url = RemoveUrlKey(url, key); //清除原key
			url = url.TrimEnd('&'); //清除最后一个&

			if (url.IndexOf("?") == -1) // 追加?问号
				url = url + "?";

			if (url.Substring(url.Length - 1) == "?") //判断最后一个字符是否?问号
				url += string.Format(@"{0}={1}", key, value);
			else
				url += string.Format(@"&{0}={1}", key, value);

			//url = url.Replace("?&", "?"); //清除多余& 这样不行

			return url;
		}
		/// <summary>
		/// 清除已有的Url参数，例如 PageIndex=2
		/// url与key大小写都可以
		/// </summary>   
		public static string RemoveUrlKey(string url, string key)
		{
			if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
				return url;

			key = key.ToLower();

			if (url.IndexOf("?") > -1 && ((url.IndexOf(key) > -1) || (url.ToLower().IndexOf(key) > -1)))
			{
				var urlParts = url.Split('?'); //根据问号拆分为两部分

				if (url.IndexOf("&") > -1)
				{
					StringBuilder sb = new StringBuilder();
					var keyValues = urlParts[1].Split('&'); //只拆分问号后面的部分
					foreach (var item in keyValues)
					{
						//if (item.Contains(key)) //Contains区分大小写
						var urlKey = item.Split('=')[0];
						if (urlKey.ToLower() == key.ToLower())
							continue;

						sb.AppendFormat("{0}&", item);
					}
					return urlParts[0] + "?" + sb.ToString().TrimEnd('&');
				}
				else
				{
					return url.Remove(url.IndexOf("?"));
				}
			}
			return url;
		}



		/// <summary>
		///将URL参数解析成Dictionary
		/// </summary>
		/// <param name="urlparma"></param>
		/// <returns></returns>
		public static Dictionary<string, string> GetUrlParmaOrDic(string urlparma)
		{
			var dic = new Dictionary<string, string>();
			var str1 = urlparma.Split('&');
			if (str1 != null)
			{
				for (int i = 0; i < str1.Length; i++)
				{
					var str2 = str1[i].Split('=');
					if (str2.Length == 2)
						dic.Add(str2[0], str2[1]);
				}
			}
			return dic;
		}

		#region GetSign
		public static string GetSign(Dictionary<string, string> dic)
		{
			var signText = "";
			foreach (var item in dic)
			{
				if (!String.IsNullOrEmpty(item.Value))
				{
					signText += item.Key + "=" + item.Value + "&";
				}
			}
			signText += "appkey=" + Config.AppKey;
			signText = signText.ToLower();
			var sign = GetMD5(signText);
			return sign;
		}
		#endregion

		/// <summary>
		/// 是否中国大陆手机号 ^0?1[3456789]\d{9}$，
		/// 必须加前^后$限制，否则abc15877778888abc也被认为是手机1\d{10}
		/// 内部已判断非空
		/// </summary>
		public static bool IsMobile(string txt)
		{
			if (string.IsNullOrEmpty(txt))
				return false;

			//var regx = @"0?1[3|5|8]\d{9}"; // |符号被误认为正确
			//var regx = @"0?1\d{10}";
			//var regx = @"1\d{10}"; //有bug，abc15877778888abc也被认为是手机，必须加前^后$限制
			//var regx = @"^1\d{10}$"; 
			var regx = @"^0?1[3456789]\d{9}$";
			Match m = Regex.Match(txt, regx);
			if (m.Success)
				return true;
			else
				return false;
		}
		public static bool IsEmail(string txt)
		{
			if (string.IsNullOrEmpty(txt))
				return false;


			var regx = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
			Match m = Regex.Match(txt, regx);
			if (m.Success)
				return true;
			else
				return false;
		}


	}

}

