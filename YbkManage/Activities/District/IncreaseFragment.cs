
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using YbkManage.Adapters;
using YbkManage.Fragments;
using DataService;
using DataEntity;
using xxxxxLibrary.LoadingDialog;

namespace YbkManage
{
	/// <summary>
	/// 区域-增量
	/// </summary>
	public class IncreaseFragment : BaseFragment
	{
		#region UIField
		private LayoutInflater layoutInflater;
		#endregion


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_increase, container, false);

			InitViews(view);

			return view;
		}

		#region 初始化页面控件

		/// <summary>
		/// 初始化页面控件
		/// </summary>
		protected void InitViews(View view)
		{
			var wv_container = view.FindViewById<WebView>(Resource.Id.wv_container);
			//声明WebSettings子类
			WebSettings webSettings = wv_container.Settings;

			//如果访问的页面中要与Javascript交互，则webview必须设置支持Javascript
			webSettings.JavaScriptEnabled = true;

			//设置自适应屏幕，两者合用
			webSettings.UseWideViewPort = true; //将图片调整到适合webview的大小 
			webSettings.LoadWithOverviewMode = true; // 缩放至屏幕的大小

			//缩放操作
			webSettings.SupportZoom(); //支持缩放，默认为true。是下面那个的前提。
			webSettings.BuiltInZoomControls = true; //设置内置的缩放控件。若为false，则该WebView不可缩放
			webSettings.DisplayZoomControls = true; //隐藏原生的缩放控件

			//其他细节操作
			webSettings.CacheMode = CacheModes.NoCache; //关闭webview中缓存 
			webSettings.AllowFileAccess = true; //设置可以访问文件 
			webSettings.JavaScriptCanOpenWindowsAutomatically = true; //支持通过JS打开新窗口 
			webSettings.LoadsImagesAutomatically = true; //支持自动加载图片
			webSettings.DefaultTextEncodingName = "utf-8";//设置编码格式



			#region H5URL
			var schoolId = CurrUserInfo.SchoolId;
			var districtCode = CurrUserInfo.DistrictCode;
			//店长指定校区查询
			var areaCodes = "";
			if (CurrUserInfo.Type == (int)UserType.ShopManager)
				areaCodes = CurrUserInfo.AreaCodes;
			var dis = new Dictionary<string, string>();
			dis.Add("schoolId", schoolId + "");
			dis.Add("areaCodes", areaCodes);
			dis.Add("districtCode", districtCode);
			var sign = Helper.GetSign(dis);
			var h5Url = Config.UpocManagerH5 + "increase/index?schoolId=" + schoolId + "&areaCodes=" + areaCodes + "&districtCode=" + districtCode + "&sign=" + sign;
			#endregion


			wv_container.SetWebViewClient(new UpocWebViewClient());

			wv_container.LoadUrl(h5Url);

		}
		#endregion



	}

	#region 重写WebViewClient部分方法
	public class UpocWebViewClient : WebViewClient
	{
		//使用webview打开url，而非浏览器
		public override bool ShouldOverrideUrlLoading(WebView view, string url)
		{
			//return base.ShouldOverrideUrlLoading(view, url);
			view.LoadUrl(url);
			return true;
		}

		public override void OnPageStarted(WebView view, string url, Android.Graphics.Bitmap favicon)
		{
			//base.OnPageStarted(view, url, favicon);
			LoadingDialogUtil.ShowLoadingDialog(view.Context, "获取数据中...");
		}

		public override void OnPageFinished(WebView view, string url)
		{
			//base.OnPageFinished(view, url);
			LoadingDialogUtil.DismissLoadingDialog();
		}
	}
	#endregion

}
