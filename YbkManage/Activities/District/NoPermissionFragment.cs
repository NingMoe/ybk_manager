
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using YbkManage.Fragments;

namespace YbkManage
{
	/// <summary>
	/// 区域-无权限页面
	/// </summary>
	public class NoPermissionFragment : BaseFragment
	{
		private LayoutInflater layoutInflater;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.activity_nopermission, container, false);


			return view;
		}
	}
}
