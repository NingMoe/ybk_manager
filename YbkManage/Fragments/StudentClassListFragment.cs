
using System;
using System.Collections.Generic;
using System.Json;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using xxxxxLibrary.Network;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Models;

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

        private List<ClassEntity> classList = new List<ClassEntity>();

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
            GetClassListOfStudentFromDataMart();
        }

        public void OnRefresh()
        {
            mSwipeRefreshLayout.Refreshing = true;
            GetClassListOfStudentFromDataMart();
        }

        /// <summary>
        /// 获取报表数据
        /// </summary>
        private async void GetClassListOfStudentFromDataMart()
        {
            try
            {
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("classStatus", classStatus + "");
                requstParams.Add("direc", "asc");
                requstParams.Add("method", "GetClassListOfStudentFromDataMart");
                requstParams.Add("orderBy", "BeginDate");
                requstParams.Add("pageIndex", "1");
                requstParams.Add("pageSize", "200");
                requstParams.Add("schoolId", CurrUserInfo.SchoolId+"");
                requstParams.Add("studentCode", studentCode);
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_CLASS_INFO, requstParams);


                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                if (state == 1)
                {
                    classList.Clear();

                    var jsonArr = JsonValue.Parse(data["Data"].ToString());
                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        ClassEntity item = new ClassEntity();
                        item.Id = int.Parse(jsonArr[i]["Id"].ToString());
                        item.SchoolId = int.Parse(jsonArr[i]["SchoolId"].ToString());
                        item.ClassCode = jsonArr[i]["ClassCode"].ToString().Replace("\"", "");
                        item.ClassName = jsonArr[i]["ClassName"].ToString().Replace("\"", "");
                        item.BeginDate = jsonArr[i]["BeginDate"].ToString().Replace("\"", "");
                        item.EndDate = jsonArr[i]["EndDate"].ToString().Replace("\"", "");
                        item.PrintAddress = jsonArr[i]["PrintAddress"].ToString().Replace("\"", "");
                        item.PrintTime = jsonArr[i]["PrintTime"].ToString().Replace("\"", "");
                        item.TeacherNames = jsonArr[i]["TeacherNames"].ToString().Replace("\"", "");

                        classList.Add(item);
                    }
                    mAdapter.NotifyDataSetChanged();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            finally
            {
                //LoadingDialogUtil.DismissLoadingDialog();
                mSwipeRefreshLayout.Refreshing = false;
            }
        }
    }
}
