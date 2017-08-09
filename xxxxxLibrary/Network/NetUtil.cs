using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Android.Net;
using Android.Content;

namespace xxxxxLibrary.Network
{
    public class NetUtil
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
			catch (Exception ex) { 
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

    }
}
