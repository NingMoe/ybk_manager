
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Content.PM;
using static Android.OS.Build;

using YbkManage.App;
using YbkManage.Models;

using xxxxxLibrary.Activity;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Utils;

namespace YbkManage.Activities
{
    [Activity(Label = "BaseActivity",ScreenOrientation = ScreenOrientation.Portrait)]
    public class AppActivity : BaseActivity
	{
		/// <summary>
		/// 当前activity
		/// </summary>
		protected Activity  CurrActivity;

		/// <summary>
		/// 当前登录信息
		/// </summary>
		protected UserInfoEntity CurrUserInfo;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CurrActivity = this;

			string userinfoStr = (string)SharedPreferencesUtil.GetParam(CurrActivity, AppConfig.SP_USERINFO, "");
			if (!string.IsNullOrEmpty(userinfoStr))
			{
				CurrUserInfo = JsonSerializer.ToObject<UserInfoEntity>(userinfoStr);
			}

            if(CurrUserInfo == null)
            {
                CurrActivity.Finish();    
            }
        }


        #region
        protected override void InitVariables()
        {

        }


        protected override void InitViews()
        {

        }


        protected override void InitEvents()
        {

        }


        protected override void LoadData()
        {

        }
		#endregion


		/// <summary>
		/// 设置状态栏背景状态
		/// </summary>
        public void initSystemBar(int colorResourceId)
		{
            Window.RequestFeature(WindowFeatures.NoTitle);
            if (VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				//清除透明状态栏,使内容不再覆盖状态栏  
				Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
				Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                var Color =  Resources.GetColor(colorResourceId);
				Window.SetStatusBarColor(Color);
				//透明导航栏 部分手机导航栏不是虚拟的,比如小米的  
				Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
				Window.SetNavigationBarColor(Color);
			}
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat && Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
			{
				//状态栏透明  
				Window.AddFlags(WindowManagerFlags.TranslucentStatus);
				//透明导航栏  
				Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
			}
		}


		/*
		 * 隐藏键盘
		 */
		public void hideKeyboard()
		{
			InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
			if (imm != null)
			{
				imm.HideSoftInputFromWindow(this.Window.DecorView.WindowToken, 0);
			}
		}


    }
}
