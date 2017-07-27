
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Models;

namespace YbkManage.Activities
{
    /// <summary>
    /// 教师管理页
    /// </summary>
    [Activity(Label = "TeacherManage", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TeacherManage : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
    {
        // 返回按钮
        private ImageButton imgbtnBack;

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
        private List<TeacherScopeEntity> teachScopeList = new List<TeacherScopeEntity>();

        // 教研组教师总数
        private int teacherCount = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_manage;

            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void InitViews()
        {
            imgbtnBack = FindViewById<ImageButton>(Resource.Id.imgBtn_back);
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
            imgbtnBack.Click += (sender, e) =>
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
            GetTeacherScopeListByGrade();
        }

        /// <summary>
        /// 获取教研组
        /// </summary>
        private async void GetTeacherScopeListByGrade()
        {
            try
            {
                //LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数11据中...");
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("method", "GetTeacherScopeListByGrade");
                requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
                requstParams.Add("grade", CurrUserInfo.Grade.ToString());
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_TEACHER_MANAGE, requstParams);

                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                if (state == 1)
                {
                    var jsonArr = JsonValue.Parse(data["Data"].ToString());

                    teachScopeList.Clear();
                    teacherCount = 0;
                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        TeacherScopeEntity teacherScope = new TeacherScopeEntity();
                        teacherScope.Id = int.Parse(jsonArr[i]["Id"].ToString());
                        teacherScope.SchoolId = int.Parse(jsonArr[i]["SchoolId"].ToString());
                        teacherScope.TeacherCount = int.Parse(jsonArr[i]["TeacherCount"].ToString());
                        teacherScope.ScopeName = jsonArr[i]["Name"].ToString().Replace("\"", "");
                        teachScopeList.Add(teacherScope);
                        teacherCount += teacherScope.TeacherCount;
                    }
                    mAdapter.NotifyDataSetChanged();

                    tvTeacherCount.Text = string.Format("我的教研组（{0}人）", teacherCount);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            finally
            {
                LoadingDialogUtil.DismissLoadingDialog();
                mSwipeRefreshLayout.Refreshing = false;
            }
        }

        public void OnItemClick(View itemView, int position)
        {
            var scopeItem = teachScopeList[position];
            Intent intent = new Intent(CurrActivity, typeof(TeacherListActivity));
            intent.PutExtra("scopeId", scopeItem.Id);
            intent.PutExtra("scopeName", scopeItem.ScopeName);
            intent.PutExtra("teacherCount", scopeItem.TeacherCount);
            StartActivity(intent);
            CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
        }

        public void OnItemLongClick(View itemView, int position)
        {
            //throw new NotImplementedException();
        }
    }
}
