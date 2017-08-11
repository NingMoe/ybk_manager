using Android.OS;
using Android.Support.V4.App;
using DataEntity;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Utils;
using YbkManage.Activities;
using YbkManage.App;
using YbkManage.Models;

using Com.Umeng.Analytics;
using DataService;

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
		protected LoginUserInfoEntity CurrUserInfo;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			CurrActivity = (AppActivity)this.Activity;
			string userinfoStr = (string)SharedPreferencesUtil.GetParam(CurrActivity, AppConfig.SP_USERINFO, "");
			if (!string.IsNullOrEmpty(userinfoStr))
			{
				CurrUserInfo = DataService.Helper.FromJsonTo<LoginUserInfoEntity>(userinfoStr);
			}
		}

		#region 友盟统计
		public override void OnResume()
		{
			base.OnResume();
			if (Config.UMengIsOpen)
			{
				MobclickAgent.OnPageStart(CurrActivity.Title);
				MobclickAgent.OnResume(CurrActivity.BaseContext);
			}
		}

		public override void OnPause()
		{
			base.OnPause();
			if (Config.UMengIsOpen)
			{
				MobclickAgent.OnPageEnd(CurrActivity.Title);
				MobclickAgent.OnPause(CurrActivity.BaseContext);
			}
		}
		#endregion
    }
}
