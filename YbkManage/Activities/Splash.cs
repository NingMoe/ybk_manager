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

namespace YbkManage.Activities
{
    /// <summary>
    /// app 启动页面，处理一些启动前的初始化的工作
    /// 可以将一些初始化的耗时的逻辑放到这儿
    /// </summary>
    [Activity(Label = "@string/app_name", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon", Theme = "@style/splashTheme")]
    public class Splash : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_splash);

            new Handler().PostDelayed(() =>
             {
                 Intent intent = new Intent(this, typeof(Login));
                 string userinfoStr = (string)SharedPreferencesUtil.GetParam(this, AppConfig.SP_USERINFO, "");
                 if (!string.IsNullOrEmpty(userinfoStr))
                 {
                     LoginUserInfoEntity currUserInfo = DataService.Helper.FromJsonTo<LoginUserInfoEntity>(userinfoStr);
                     intent.SetClass(this, typeof(Main));
                 }
                 StartActivity(intent);
                 Finish();
                 OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
             }, 1500);


        }
    }
}
