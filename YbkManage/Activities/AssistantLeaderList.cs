
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using YbkManage.Activities;
using YbkManage.Adapters;


namespace YbkManage
{
	[Activity(Label = "AssistantLeaderList")]
	public class AssistantLeaderList : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
	{
		// 返回按钮
		private ImageButton imgbtnBack;
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
		private List<AstLeaderListModel> list = new List<AstLeaderListModel>();


		protected override void OnCreate(Bundle savedInstanceState)
		{
			//指定Layout页面
			LayoutReourceId = Resource.Layout.activity_assistant_list;

			base.OnCreate(savedInstanceState);
		}

		protected override void InitVariables()
		{
			//scopeId = Intent.Extras.GetInt("scopeId");
			//scopeName = Intent.Extras.GetString("scopeName");
			//teacherCount = Intent.Extras.GetInt("teacherCount");

		}

		protected override void InitViews()
		{
			imgbtnBack = FindViewById<ImageButton>(Resource.Id.ast_imgBtn_back);
			llAdd = FindViewById<LinearLayout>(Resource.Id.ast_ll_add);
			tvTeacherCount = FindViewById<TextView>(Resource.Id.ast_tv_count);
			tvTeacherCount.Text = string.Format("助教组长（{0}人）", list.Count);

			mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.ast_refresher);
			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.ast_recycler_view);

			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mAdapter = new AssistantAdapter(CurrContext, list);
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
			imgbtnBack.Click += (sender, e) =>
			{
				CurrActivity.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};

			llAdd.Click += (sender, e) =>
			{
				Intent intent = new Intent(CurrActivity, typeof(TeacherAddActivity));
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
			};
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		protected override void LoadData()
		{
			mSwipeRefreshLayout.Refreshing = true;
			BindData();
		}

		/// <summary>
		/// 下拉刷新
		/// </summary>
		public void OnRefresh()
		{
			mSwipeRefreshLayout.Refreshing = true;
			BindData();
		}


		/// <summary>
		/// 获取教师列表
		/// </summary>
		private void BindData()
		{
			try
			{
				var schoolId = CurrUserInfo.SchoolId;
				var districtCode = CurrUserInfo.DistrictCode;
				list =MeService.GetAssistantLeaderList(schoolId, districtCode??"");
				mAdapter.NotifyDataSetChanged();
				tvTeacherCount.Text = string.Format("助教组长（{0}人）", list.Count);

			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
			finally
			{

				//mSwipeRefreshLayout.Refreshing = false;
			}
		}



		public void OnItemClick(View itemView, int position)
		{
			//var scopeItem = teachScopeList[position];
			//Intent intent = new Intent(CurrActivity, typeof(TeacherList));
			//StartActivity(intent);
			//CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
		}

		public void OnItemLongClick(View itemView, int position)
		{
			//throw new NotImplementedException(
		}
	}
}
