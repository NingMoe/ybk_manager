
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace xxxxxLibrary.Activity
{
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			InitVariables();

			InitViews();

			InitEvents();

			LoadData();
		}

        /// <summary>
        /// 初始化变量
        /// </summary>
		protected abstract void InitVariables();

        /// <summary>
        /// 装载页面控件
        /// </summary>
		protected abstract void InitViews();

        /// <summary>
        /// 页面事件
        /// </summary>
		protected abstract void InitEvents();

        /// <summary>
        /// 加载页面数据
        /// </summary>
		protected abstract void LoadData();
    }
}
