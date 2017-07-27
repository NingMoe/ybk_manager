
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Content.PM;
using Android.Graphics;

using YbkManage.App;
using YbkManage.Models;

using xxxxxLibrary.Activity;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Utils;
using Android.Support.V4.Content;
using System;

namespace YbkManage.Activities
{
    [Activity(Label = "BaseActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AppActivity : BaseActivity
    {
        protected Context CurrContext;

        /// <summary>
        /// 当前activity
        /// </summary>
        protected Activity CurrActivity;

        /// <summary>
        /// 当前登录信息
        /// </summary>
        protected UserInfoEntity CurrUserInfo;

        /// <summary>
        /// 是否全屏
        /// </summary>
        protected bool IsFullScreen = true;

        protected int LayoutReourceId;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            CurrContext = ApplicationContext;
            CurrActivity = this;

            if (IsFullScreen)
            {
                initSystemBar(Resource.Color.actionbar_bg);
            }

            string userinfoStr = (string)SharedPreferencesUtil.GetParam(CurrActivity, AppConfig.SP_USERINFO, "");
            if (!string.IsNullOrEmpty(userinfoStr))
            {
                CurrUserInfo = JsonSerializer.ToObject<UserInfoEntity>(userinfoStr);
            }

            if (CurrUserInfo == null)
            {
                CurrActivity.Finish();
            }

            if (LayoutReourceId > 0)
            {
                SetContentView(LayoutReourceId);
            }

            base.OnCreate(savedInstanceState);
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
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                //清除透明状态栏,使内容不再覆盖状态栏  
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                var Color = new Color(ContextCompat.GetColor(this, Resource.Color.actionbar_bg));
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
