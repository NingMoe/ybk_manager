
using System;
using System.Collections.Generic;
using System.Json;
using System.Threading;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.Adapters;

namespace YbkManage.Fragments
{
    /// <summary>
    /// 课程列表
    /// </summary>
    public class StudentClassListFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener
    {
        // 课程类型 0=开课中 1=已结课 2=未开课
        private int classStatus;

        private string studentCode;

        private List<PureClassEntity> classList = new List<PureClassEntity>();

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;

        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        private StudentClassAdapter mAdapter;

        public StudentClassListFragment(string studentCode, int classStatus)
        {
            this.studentCode = studentCode;
            this.classStatus = classStatus;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_studentclass_list, container, false);
            InitViews(view);
            LoadData();
            return view;
        }

        /// <summary>
        /// 页面控件
        /// </summary>
        protected void InitViews(View view)
        {
            mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
            mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));
            //mSwipeRefreshLayout.SetColorScheme(Resource.Color.xam_dark_blue,
            //Resource.Color.xam_purple,
            //Resource.Color.xam_gray,
            //Resource.Color.xam_green);

            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new StudentClassAdapter(CurrActivity, classList);
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();

            mSwipeRefreshLayout.SetOnRefreshListener(this);
            //RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
            //mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));

        }

        /// <summary>
        /// 页面数据
        /// </summary>
        protected void LoadData()
        {
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
				return;
			}
			//LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
            GetClassListOfStudentFromDataMart();
        }

        public void OnRefresh()
        {
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
			}
			else
			{
				GetClassListOfStudentFromDataMart();
			}
        }

        /// <summary>
        /// 获取报表数据
        /// </summary>
        private void GetClassListOfStudentFromDataMart()
        {
			try
			{
				new Thread(new ThreadStart(() =>
				{
                    classList = RenewService.GetClassListOfStudent(CurrUserInfo.SchoolId, studentCode,classStatus);
					CurrActivity.RunOnUiThread(() =>
					{
						LoadingDialogUtil.DismissLoadingDialog();
                        mSwipeRefreshLayout.Refreshing = false;

						if (classList != null)
						{
                            mAdapter.SetData(classList);
							mAdapter.NotifyDataSetChanged();
						}
					});
				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				LoadingDialogUtil.DismissLoadingDialog();
			}
        }
    }
}
