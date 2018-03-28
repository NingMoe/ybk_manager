
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.Activities;
using YbkManage.Adapters;

namespace YbkManage
{
	[Activity(Label = "ShopManagerListActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class ShopManagerListActivity : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
	{


		protected override void OnCreate(Bundle savedInstanceState)
		{
			//LayoutReourceId = Resource.Layout.dis;

			base.OnCreate(savedInstanceState);
		}

		/// <summary>
		/// 下拉刷新
		/// </summary>
		public void OnRefresh()
		{
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
			}
			else
			{
				//GetTeacherScopeListByGrade();
			}
		}
		/// <summary>
		/// 行点击事件
		/// </summary>
		public void OnItemClick(View itemView, int position)
		{
			//var scopeItem = teachScopeList[position];
			//Intent intent = new Intent(CurrActivity, typeof(TeacherListActivity));
			//intent.PutExtra("scopeId", scopeItem.Id);
			//intent.PutExtra("scopeName", scopeItem.Name);
			//intent.PutExtra("teacherCount", scopeItem.TeacherCount ?? 0);
			//StartActivity(intent);
			//CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
		}
		public void OnItemLongClick(View itemView, int position)
		{
			//throw new NotImplementedException(   );
		}
	}
