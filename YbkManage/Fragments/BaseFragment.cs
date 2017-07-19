
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
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
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnResume()
        {
            base.OnResume();
            string userinfoStr = (string)SharedPreferencesUtil.GetParam(CurrActivity, AppConfig.SP_USERINFO, "");
            if(!string.IsNullOrEmpty(userinfoStr))
            {
                CurrUserInfo = JsonSerializer.ToObject<UserInfoEntity>(userinfoStr);
            }
        }
    }
}
