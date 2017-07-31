
using System;
using System.Collections.Generic;
using System.Json;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
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
    /// 教师管理列表页
    /// </summary>
    [Activity(Label = "TeacherListActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TeacherListActivity : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
    {
        private LinearLayout llAdd;

        // 教师总数
        private TextView tvTeacherCount;

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;

        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        private TeacherListAdapter mAdapter;

        // 教师数据
        private List<TeacherListModel> teacherList = new List<TeacherListModel>();

        private MeService _meService = new MeService();

        private int pageIndex = 1, pageSize = 10, totalCount = 0;

        private bool loadingData = false;

        private int scopeId = 0;
        private string scopeName = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_list;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            Bundle bundle = Intent.Extras;
            if (bundle != null)
            {
                scopeId = bundle.GetInt("scopeId");
                scopeName = bundle.GetString("scopeName");
            }
        }

        protected override void InitViews()
        {
            llAdd = FindViewById<LinearLayout>(Resource.Id.ll_add);
            tvTeacherCount = FindViewById<TextView>(Resource.Id.tv_teachercount);
            FindViewById<TextView>(Resource.Id.tv_title).Text = scopeName;

            mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new TeacherListAdapter(CurrContext, 1);
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();
        }

        protected override void InitEvents()
        {
            // 返回
            FindViewById<ImageButton>(Resource.Id.imgBtn_back).Click += (sender, e) =>
            {
                CurrActivity.Finish();
                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
            };

            llAdd.Click += (sender, e) =>
            {
                Intent intent = new Intent(CurrActivity, typeof(TeacherAddActivity));
                intent.PutExtra("scopeName", scopeName);
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            };

            mSwipeRefreshLayout.SetOnRefreshListener(this);

            RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
            mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));

            // 加载更多
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(linearLayoutManager);
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
            {
                if (totalCount > teacherList.Count)
                {
                    if (!loadingData)
                    {
                        GetTeacherListByScope();
                    }
                }
                else if (totalCount == teacherList.Count)
                {
                    Toast.MakeText(this, "没有更多了", ToastLength.Short).Show();
                }
            };
            mRecyclerView.AddOnScrollListener(onScrollListener);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected override void LoadData()
        {
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
                pageIndex = 1;

                //mSwipeRefreshLayout.Refreshing = true;
                GetTeacherListByScope();
            }
        }

        /// <summary>
        /// 获取教师列表
        /// </summary>
        private void GetTeacherListByScope()
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    loadingData = true;
                    var schoolId = CurrUserInfo.SchoolId;
                    var pagerList = _meService.GetTeacherListByScope(schoolId, scopeId, pageIndex, pageSize, out totalCount);
                    RunOnUiThread(() =>
                    {
                        loadingData = false;

                        LoadingDialogUtil.DismissLoadingDialog();
                        mSwipeRefreshLayout.Refreshing = false;

                        tvTeacherCount.Text = string.Format("所有教师（{0}人）", totalCount);

						if (pageIndex == 1)
						{
							teacherList.Clear();
						}
                        teacherList.AddRange(pagerList);
                        pageIndex++;
                        mAdapter.HideFootere(teacherList.Count >= totalCount);

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
                loadingData = false;
            }
        }


        public void OnItemClick(View itemView, int position)
        {
            var teacherItem = teacherList[position];
            Intent intent = new Intent(CurrActivity, typeof(TeacherAddActivity));
            intent.PutExtra("scopeName", teacherItem.ScopeName);
            intent.PutExtra("teacherJsonStr", JsonSerializer.ToJsonString(teacherItem));
            StartActivity(intent);
            CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
        }

        public void OnItemLongClick(View itemView, int position)
        {
            //throw new NotImplementedException();
        }

        public class XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
        {
            public delegate void LoadMoreEventHandler(object sender, EventArgs e);
			public event LoadMoreEventHandler LoadMoreEvent;

			private LinearLayoutManager LayoutManager;

            public XamarinRecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
            {
                LayoutManager = layoutManager;
            }

            public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                base.OnScrolled(recyclerView, dx, dy);

                //var visibleItemCount = recyclerView.ChildCount;
                var totalItemCount = recyclerView.GetAdapter().ItemCount;
                var lastVisibleItemPosition = LayoutManager.FindLastVisibleItemPosition();
                //var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();
                Log.Debug("test", "totalItemCount =" + totalItemCount + "-----" + "lastVisibleItemPosition =" + lastVisibleItemPosition);

                if (totalItemCount == (lastVisibleItemPosition + 1) && LoadMoreEvent!=null)
                {
                    LoadMoreEvent(this, null);
                }
            }
        }
    }
}
