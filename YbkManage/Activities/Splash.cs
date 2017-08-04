using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Android.Widget;
using Android.Support.V4.App;

using YbkManage.App;
using YbkManage.Models;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Utils;
using DataEntity;
using System.Threading;
using DataService;
using System;
using xxxxxLibrary.Network;

namespace YbkManage.Activities
{
    /// <summary>
    /// app 启动页面，处理一些启动前的初始化的工作
    /// 可以将一些初始化的耗时的逻辑放到这儿
    /// </summary>
    [Activity(Label = "@string/app_name", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon", Theme = "@style/splashTheme")]
    public class Splash : Activity
    {
        private LoginUserInfoEntity currUserInfo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_splash);

            string userinfoStr = (string)SharedPreferencesUtil.GetParam(this, AppConfig.SP_USERINFO, "");
            if (!string.IsNullOrEmpty(userinfoStr))
            {
                currUserInfo = DataService.Helper.FromJsonTo<LoginUserInfoEntity>(userinfoStr);
            }

            // 将一些字典数据提前加载
            if (NetUtil.CheckNetWork(this) && currUserInfo != null)
            {
                LoadQuarterData();
                LoadGradeData();
                LoadDistrictData();
            }

            new Handler().PostDelayed(() =>
             {
                 Intent intent = new Intent(this, typeof(Login));
                 if (currUserInfo != null)
                 {

                     intent.SetClass(this, typeof(Main));
                 }
                 StartActivity(intent);
                 Finish();
                 OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
             }, 1500);


		}

		/// <summary>
		/// 获取财年季度数据
		/// </summary>
		private void LoadQuarterData()
		{
			try
			{
				new Thread(new ThreadStart(() =>
				{
					BaseApplication.GetInstance().quarterList = RenewService.GetQuarter(currUserInfo.SchoolId);
				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
			}
		}

		/// <summary>
		/// 获取年级数据
		/// </summary>
		private void LoadGradeData()
		{
			try
			{
				new Thread(new ThreadStart(() =>
				{
					BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(currUserInfo.SchoolId);
				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
			}
		}

		/// <summary>
		/// 获取区域数据
		/// </summary>
		private void LoadDistrictData()
		{
			try
			{
				new Thread(new ThreadStart(() =>
				{
                    BaseApplication.GetInstance().districtList = RenewService.GetDistrictList(currUserInfo.SchoolId);
				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
			}
		}
    }
}
