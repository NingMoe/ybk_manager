
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
    /// 教师管理列表页
    /// </summary>
    [Activity(Label = "TeacherListActivity")]
    public class TeacherListActivity : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
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
        private TeacherListAdapter mAdapter;

        // 教师数据
        private List<TeacherInfoEntity> teacherList = new List<TeacherInfoEntity>();

        // 教师总数
        private int teacherCount = 0;

        private int pageIndex = 1, pageSize = 10;

        private int scopeId = 0;
        private string scopeName = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_list;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            scopeId = Intent.Extras.GetInt("scopeId");
            scopeName = Intent.Extras.GetString("scopeName");
            teacherCount = Intent.Extras.GetInt("teacherCount");
        }

        protected override void InitViews()
        {
            imgbtnBack = FindViewById<ImageButton>(Resource.Id.imgBtn_back);
            llAdd = FindViewById<LinearLayout>(Resource.Id.ll_add);
            tvTeacherCount = FindViewById<TextView>(Resource.Id.tv_teachercount);
            tvTeacherCount.Text = string.Format("所有教师（{0}人）", teacherCount);

            mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new TeacherListAdapter(CurrContext, teacherList);
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
                CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
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
                GetTeacherListByScope();
            }
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        public void OnRefresh()
        {
            pageIndex = 1;
            teacherList.Clear();
            //mSwipeRefreshLayout.Refreshing = true;
            GetTeacherListByScope();
        }

        /// <summary>
        /// 滑动到底部加载更多
        /// </summary>
        /// <param name="v">V.</param>
        /// <param name="scrollX">Scroll x.</param>
        /// <param name="scrollY">Scroll y.</param>
        /// <param name="oldScrollX">Old scroll x.</param>
        /// <param name="oldScrollY">Old scroll y.</param>
        public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
            int lastvisibleItemPosition = linearLayoutManager.FindLastVisibleItemPosition();
            if (lastvisibleItemPosition + 1 == mAdapter.ItemCount)
            {
                GetTeacherListByScope();
            }

        }

        /// <summary>
        /// 获取教师列表
        /// </summary>
        private async void GetTeacherListByScope()
        {
            try
            {
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("method", "GetTeacherListByScope");
                requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
                requstParams.Add("scope", scopeId.ToString());
                requstParams.Add("pageIndex", pageIndex.ToString());
                requstParams.Add("pageSize", pageSize.ToString());
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_TEACHER_MANAGE, requstParams);


                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                teacherCount = int.Parse(data["DataCount"].ToString());
                if (state == 1)
                {
                    var jsonArr = JsonValue.Parse(data["Data"].ToString());

                    List<TeacherInfoEntity> pagerList = new List<TeacherInfoEntity>();
                    teacherList.Clear();
                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        TeacherInfoEntity teacherScope = new TeacherInfoEntity();
                        teacherScope.ScopeCode = int.Parse(jsonArr[i]["ScopeCode"].ToString());
                        teacherScope.SchoolId = int.Parse(jsonArr[i]["SchoolId"].ToString());
                        teacherScope.Type = int.Parse(jsonArr[i]["Type"].ToString());
                        teacherScope.TeacherId = int.Parse(jsonArr[i]["TeacherId"].ToString());
                        teacherScope.Name = jsonArr[i]["Name"].ToString().Replace("\"", "");
                        teacherScope.Email = jsonArr[i]["Email"].ToString().Replace("\"", "");
                        teacherScope.UserId = jsonArr[i]["UserId"].ToString().Replace("\"", "");
                        teacherScope.ScopeName = jsonArr[i]["ScopeName"].ToString().Replace("\"", "");
                        teacherScope.ProjectCode = jsonArr[i]["ProjectCode"].ToString().Replace("\"", "");
                        teacherScope.Code = jsonArr[i]["Code"].ToString().Replace("\"", "");
                        pagerList.Add(teacherScope);
                    }

                    teacherList.AddRange(pagerList);
                    pageIndex++;
                    mAdapter.NotifyDataSetChanged();

                    GetTeacherListAvatar();
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

        /// <summary>
        /// 获取教师头像
        /// </summary>
        private async void GetTeacherListAvatar()
        {
            try
            {
                var codes = string.Empty;
                var noAvatarTeacherList = new List<TeacherInfoEntity>();
                foreach (var teacher in teacherList)
                {
                    if (string.IsNullOrEmpty(teacher.Avatar))
                    {
                        codes += teacher.Code + "_1,";
                        noAvatarTeacherList.Add(teacher);
                    }
                }
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", "1004");
                requstParams.Add("userType", "2");
                requstParams.Add("rongyunIds", codes);
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_TEACHER_INFO, requstParams);

                var data = (JsonObject)result;
                var state = int.Parse(data["status"].ToString().Replace("\"", ""));
                if (state == 1)
                {
                    var jsonArr = JsonValue.Parse(data["data"].ToString());

                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        var teacherCode = jsonArr[i]["rongyunId"].ToString().Replace("\"", "").Split('_')[0];
                        var teacher = noAvatarTeacherList.Where(t => t.Code == teacherCode).FirstOrDefault();
                        if (teacher != null)
                        {
                            teacher.Avatar = jsonArr[i]["portrait"].ToString().Replace("\"", "");
                        }
                    }
                    mAdapter.NotifyDataSetChanged();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
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
            //throw new NotImplementedException();
        }
    }
}
