using Android.OS;
using Android.Support.V4.App;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Utils;
using YbkManage.Activities;
using YbkManage.App;
using YbkManage.Models;

namespace YbkManage.Fragments
{
	/// <summary>
	/// 所有Fragment的基类
	/// </summary>
	public class BaseFragment : Fragment
    {
        /// <summary>
        /// 当前所在的Activity
        /// </summary>
        public AppActivity CurrActivity;

        /// <summary>
        /// 当前登录信息
        /// </summary>
        protected UserInfoEntity CurrUserInfo;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			CurrActivity = (AppActivity)this.Activity;
			string userinfoStr = (string)SharedPreferencesUtil.GetParam(CurrActivity, AppConfig.SP_USERINFO, "");
			if (!string.IsNullOrEmpty(userinfoStr))
			{
				CurrUserInfo = JsonSerializer.ToObject<UserInfoEntity>(userinfoStr);
			}
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}
