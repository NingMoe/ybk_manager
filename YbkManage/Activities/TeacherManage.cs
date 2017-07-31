
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
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
using xxxxxLibrary.Toast;
using YbkManage.Adapters;
using YbkManage.App;

namespace YbkManage.Activities
{
    /// <summary>
    /// 教师管理页
    /// </summary>
    [Activity(Label = "TeacherManage", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TeacherManage : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
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
        private TeacherScopeAdapter mAdapter;

        // 教研组数据
        private List<ScopeModel> teachScopeList = new List<ScopeModel>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_manage;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitViews()
        {
            llAdd = FindViewById<LinearLayout>(Resource.Id.ll_add);
            tvTeacherCount = FindViewById<TextView>(Resource.Id.tv_teachercount);

            mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new TeacherScopeAdapter(CurrContext, teachScopeList);
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();

            mSwipeRefreshLayout.SetOnRefreshListener(this);
            //mSwipeRefreshLayout.SetOnScrollChangeListener(this);

            //mAdapter.SetOnItemClickListener(this);

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

            llAdd.Click += (sender, e) =>
            {
                Intent intent = new Intent(CurrActivity, typeof(TeacherAddActivity));
                StartActivity(intent);
                CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            };
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
                GetTeacherScopeListByGrade();

                //mSwipeRefreshLayout.PostDelayed(() =>
                //{
                //    mSwipeRefreshLayout.Refreshing = true;
                //    GetTeacherScopeListByGrade();
                //}, 0);
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
				GetTeacherScopeListByGrade();
			}
        }

        /// <summary>
        /// 获取教研组
        /// </summary>
        private void GetTeacherScopeListByGrade()
        {
            try
            {
                var grade = CurrUserInfo.Grade;
                var schoolId = CurrUserInfo.SchoolId;
                new Thread(new ThreadStart(() =>
                {

                    teachScopeList = new MeService().GetScopeByGrade(schoolId, grade ?? 0);
                    RunOnUiThread(() =>
                    {
                        LoadingDialogUtil.DismissLoadingDialog();

                        var teacherCount = teachScopeList.Sum(i => i.TeacherCount ?? 0);
                        tvTeacherCount.Text = string.Format("我的教研组（{0}人）", teacherCount);
                        mAdapter.SetData(teachScopeList);
                        mAdapter.NotifyDataSetChanged();
                        mSwipeRefreshLayout.Refreshing = false;

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
            var scopeItem = teachScopeList[position];
            Intent intent = new Intent(CurrActivity, typeof(TeacherListActivity));
            intent.PutExtra("scopeId", scopeItem.Id);
            intent.PutExtra("scopeName", scopeItem.Name);
            intent.PutExtra("teacherCount", scopeItem.TeacherCount ?? 0);
            StartActivity(intent);
            CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
        }

        public void OnItemLongClick(View itemView, int position)
        {
            //throw new NotImplementedException();
        }
    }
}
