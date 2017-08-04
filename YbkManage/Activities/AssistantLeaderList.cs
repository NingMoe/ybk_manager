﻿using System;
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
using YbkManage.Activities;
using YbkManage.Adapters;

namespace YbkManage
{
    /// <summary>
    /// 助教组长
    /// </summary>
	[Activity(Label = "AssistantLeaderList", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AssistantLeaderList : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
    {
        // 添加
        private LinearLayout llAdd;

        // 总数
        private TextView tvTeacherCount;

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;

        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        private AssistantAdapter mAdapter;
        // 数据
        private List<AstLeaderListModel> assistantList = new List<AstLeaderListModel>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            //指定Layout页面
            LayoutReourceId = Resource.Layout.activity_assistant_list;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitViews()
        {
            llAdd = FindViewById<LinearLayout>(Resource.Id.ast_ll_add);
            tvTeacherCount = FindViewById<TextView>(Resource.Id.ast_tv_count);

            mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.ast_refresher);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.ast_recycler_view);
            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new AssistantAdapter(CurrContext);
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
            FindViewById<ImageButton>(Resource.Id.ast_imgBtn_back).Click += (sender, e) =>
            {
                CurrActivity.Finish();
                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
            };

            llAdd.Click += (sender, e) =>
            {
                Intent intent = new Intent(CurrActivity, typeof(AssistantAddActivity));
				StartActivity(intent);
				OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
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

				var schoolId = CurrUserInfo.SchoolId;
				var districtCode = CurrUserInfo.DistrictCode;
				new Thread(new ThreadStart(() =>
				{

					assistantList = MeService.GetAssistantLeaderList(schoolId, districtCode);
					RunOnUiThread(() =>
					{
						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						tvTeacherCount.Text = string.Format("助教组长（{0}人）", assistantList.Count);

						mAdapter.SetData(assistantList);
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

        public void OnItemClick(View itemView, int position)
        {
			var assistantItem = assistantList[position];
			Intent intent = new Intent(CurrActivity, typeof(AssistantAddActivity));
			intent.PutExtra("assistantJsonStr", JsonSerializer.ToJsonString(assistantItem));
			StartActivity(intent);
			CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
        }

        public void OnItemLongClick(View itemView, int position)
        {
            //throw new NotImplementedException(
        }
    }
}
