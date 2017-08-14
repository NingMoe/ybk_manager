using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Toast;
using YbkManage.Adapters;

namespace YbkManage.Activities
{
    /// <summary>
    /// 教学主管
    /// </summary>
    [Activity(Label = "DirectorListActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class DirectorListActivity : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
	{
		// 总数
		private TextView tvTeacherCount;

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;

		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		private TeacherListAdapter mAdapter;
		// 数据
		private List<TeacherListModel> teacherList = new List<TeacherListModel>();


		protected override void OnCreate(Bundle savedInstanceState)
		{
			//指定Layout页面
            LayoutReourceId = Resource.Layout.activity_director_list;

			base.OnCreate(savedInstanceState);
		}

		protected override void InitViews()
		{
			tvTeacherCount = FindViewById<TextView>(Resource.Id.tv_teachercount);

			mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mAdapter = new TeacherListAdapter(this,2);
            mAdapter.HideFootere(true);
			mRecyclerView.SetLayoutManager(linearLayoutManager);
			mRecyclerView.SetAdapter(mAdapter);
			mAdapter.NotifyDataSetChanged();

			mSwipeRefreshLayout.SetOnRefreshListener(this);

			RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
		}

		protected override void InitEvents()
		{
			// 返回
			FindViewById<ImageButton>(Resource.Id.imgBtn_back).Click += (sender, e) =>
			{
				CurrActivity.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};
		}

		protected override void OnResume()
		{
			base.OnResume();
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
				return;
			}
			else
			{
				LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
				GetTeacherListByScope();
			}
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
                GetTeacherListByScope();
            }
		}

		/// <summary>
		/// 获取助教组长列表
		/// </summary>
		private void GetTeacherListByScope()
		{
			try
			{
                var grade = CurrUserInfo.Grade;
				var schoolId = CurrUserInfo.SchoolId;
				new Thread(new ThreadStart(() =>
				{

					teacherList = new MeService().GetTeacherDirectorListByGrade(schoolId, grade ?? 0);
                    RunOnUiThread(() =>
                    {
						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

                        tvTeacherCount.Text = string.Format("所有教学主管（{0}人）", teacherList.Count);

                        mAdapter.SetData(teacherList);
                        mAdapter.NotifyDataSetChanged();
                    });
				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				LoadingDialogUtil.DismissLoadingDialog();
				mSwipeRefreshLayout.Refreshing = false;
			}
		}

        /// <summary>
        /// 列表单击
        /// </summary>
        /// <param name="itemView">Item view.</param>
        /// <param name="position">Position.</param>
		public void OnItemClick(View itemView, int position)
		{
			var teacherItem = teacherList[position];
			Intent intent = new Intent(CurrActivity, typeof(TeacherAddActivity));
			intent.PutExtra("teacherJsonStr", JsonSerializer.ToJsonString(teacherItem));
            intent.PutExtra("pageFromType",2);
			StartActivity(intent);
			CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
		}

		public void OnItemLongClick(View itemView, int position)
		{
			//throw new NotImplementedException(
		}
	}
}
