
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Fragments;

namespace YbkManage
{
<<<<<<< HEAD
    /// <summary>
    /// 区域-累计-校区维度
    /// </summary>
    public class SumAccountFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, View.IOnClickListener
    {
        #region UIField
        private LayoutInflater layoutInflater;
        //财年、区域按钮
        private TextView tv_year, tv_district, tv_grade, tv_course;
        private PopupWindow popYear, popDistrict, popGrade, popCourse;

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;
        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        public SumAccountAdapter mAdapter;
        #endregion

        #region Field

        private decimal avgGrowthRate = 0;
        private List<PaymentSumAreaEntity> sumList = new List<PaymentSumAreaEntity>();
        private List<QuarterEntity> quarterList = new List<QuarterEntity>();
        private List<string> districtList = new List<string>(),
                             gradeList = new List<string>(),
                             courseList = new List<string>();
        private QuarterEntity searchQuarter = new QuarterEntity { QuarterName = "2018财年Q1", Year = 2018, Quarter = 1 };
        private string searchDistrict = "全部区域", searchCourse = "全部科目";
        private List<string> searchGradeList = new List<string>();

        //累计：1-人次；2-预收；3-行课
        public int dataType = 1;
        //标题排序：1/2-本期累计升/倒序；3/4-去年同期升/倒序；5/6-增长率升/倒序
        int sortType = 6;

        //总计行
        int totalCount = 0;
        bool loadingData = false;
        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            layoutInflater = inflater;
            View view = layoutInflater.Inflate(Resource.Layout.fragment_sumaccount, container, false);

            InitViews(view);
            LoadData();

            InitEvents();

            return view;
        }

        protected void InitEvents()
        { 
            mSwipeRefreshLayout.SetOnRefreshListener(this);

            //var viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
            //mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));

            // 加载更多
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(linearLayoutManager);
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
            {
                if (totalCount > this.sumList.Count)
                {
                    if (!loadingData)
                    {
                        loadingData = true;
                        BindData();
                    }
                }
                else if (totalCount == this.sumList.Count)
                {
                    Toast.MakeText(this.CurrContext, "没有更多了", ToastLength.Short).Show();
                }
            };
            mRecyclerView.AddOnScrollListener(onScrollListener);
        }
                                  

        #region 初始化页面控件

        /// <summary>
        /// 初始化页面控件
        /// </summary>
        protected void InitViews(View view)
        {
            tv_year = view.FindViewById<TextView>(Resource.Id.tv_year);
            tv_district = view.FindViewById<TextView>(Resource.Id.tv_district);
            tv_grade = view.FindViewById<TextView>(Resource.Id.tv_grade);
            tv_course = view.FindViewById<TextView>(Resource.Id.tv_course);

            //添加按钮的事件监控
            tv_year.SetOnClickListener(this);
            tv_district.SetOnClickListener(this);
            tv_grade.SetOnClickListener(this);
            tv_course.SetOnClickListener(this);

            mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
            mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

            //adapter展示列表数据
            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new SumAccountAdapter(CurrActivity, sumList, this.avgGrowthRate, mRecyclerView);
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();

            mSwipeRefreshLayout.SetOnRefreshListener(this);
            //RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
            //mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
        }
        #endregion

        #region 加载数据
        protected void LoadData()
        {
            if (!NetUtil.CheckNetWork(CurrActivity))
            {
                ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
                return;
            }

            //财年数据
            if (BaseApplication.GetInstance().quarterList == null)
            {
                BaseApplication.GetInstance().quarterList = RenewService.GetQuarter(CurrUserInfo.SchoolId);
            }
            //区域数据
            if (BaseApplication.GetInstance().districtList == null)
            {
                BaseApplication.GetInstance().districtList = RenewService.GetDistrictList(CurrUserInfo.SchoolId);
            }
            //年级数据
            if (BaseApplication.GetInstance().gradeList == null)
            {
                BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
            }
            //科目数据
            if (BaseApplication.GetInstance().courseList == null)
            {
                BaseApplication.GetInstance().courseList = DistrictService.GetCourseList(CurrUserInfo.SchoolId);
            }

            //默认财年
            if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
            {
                quarterList = BaseApplication.GetInstance().quarterList;
                searchQuarter = BaseApplication.GetInstance().quarterList.Find(t => t.IsCurrent);
                tv_year.Text = searchQuarter.QuarterName;
            }
            //默认区域
            if (BaseApplication.GetInstance().districtList != null && BaseApplication.GetInstance().districtList.Any())
            {
                districtList = new List<string>(BaseApplication.GetInstance().districtList.Select(i => i.DistrictName).ToArray());
                //加区域的权限判断--区域经理登录
                if (CurrUserInfo.Type == (int)UserType.AreaManager)
                {
                    districtList = districtList.Where(p => p == CurrUserInfo.DistrictName).ToList();
                    tv_district.Text = CurrUserInfo.DistrictName;
                    searchDistrict = CurrUserInfo.DistrictName;
                }
            }
            //默认年级
            if (BaseApplication.GetInstance().gradeList != null && BaseApplication.GetInstance().gradeList.Any())
            {
                gradeList = new List<string>(BaseApplication.GetInstance().gradeList.Select(i => i.GradeName).ToArray());

            }
            if (searchGradeList != null && searchGradeList.Any())
            {
                var selectedgrade = "全部年级";
                if (searchGradeList.Count > 0 && searchGradeList.Count != gradeList.Count)
                {
                    selectedgrade = string.Join(",", searchGradeList.ToArray());
                }

                tv_grade.Text = selectedgrade;
            }
            else
            {
                // 默认全选
                searchGradeList = new List<string>(gradeList.ToArray());
            }
            //默认科目
            if (BaseApplication.GetInstance().courseList != null && BaseApplication.GetInstance().courseList.Any())
            {
                courseList = new List<string>(BaseApplication.GetInstance().courseList.Select(t => t.CourseName).ToArray());
                searchCourse = BaseApplication.GetInstance().courseList[0].CourseName;
                tv_course.Text = searchCourse;
            }

            BindData();
        }
        #endregion

        #region 绑定数据
        public void BindData()
        {
            LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");

            try
            {
                var schoolId = CurrUserInfo.SchoolId;
                var year = searchQuarter.Year;
                var quarter = searchQuarter.Quarter;
                var district = searchDistrict;
                var course = searchCourse;
                var grade = "";
                if (searchGradeList.Any())
                {
                    grade = string.Join(",", searchGradeList.ToArray());
                }

                var areaCodes = ""; //店长登录用
                if (CurrUserInfo.Type == (int)UserType.ShopManager)
                    areaCodes = CurrUserInfo.AreaCodes;

                new System.Threading.Thread(new ThreadStart(() =>
                {
                    var list = SumService.GetSumPaymentListByArea(schoolId, year, quarter,dataType,sortType, district,grade,course, out totalCount,1, 1, 500, areaCodes);
                    CurrActivity.RunOnUiThread(() =>
                    {

                        LoadingDialogUtil.DismissLoadingDialog();
                        mSwipeRefreshLayout.Refreshing = false;

                        if (list != null)
                        {
                            var totalData = list.TotalData;
                            this.avgGrowthRate = totalData.GrowthRate;
                            this.sumList = list.List;
                            var totalEntity = new PaymentSumAreaEntity() { Name = "总计", CurrentSum=totalData.CurrentSum, LastYearSum = totalData.LastYearSum, GrowthRate = totalData.GrowthRate };
                            this.sumList.Add(totalEntity);

                            var gradeListParam = "";
                            if (searchGradeList.Count != gradeList.Count)
                            {
                                gradeListParam = string.Join(",", searchGradeList.ToArray());
                            }
                            mAdapter.SetData(this.sumList,this.avgGrowthRate, this.searchQuarter, this.dataType,this.searchCourse,gradeListParam);
                            mAdapter.NotifyDataSetChanged();
                        }
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
        #endregion

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.tv_year)
            {
                #region Quarter
                if (quarterList != null && quarterList.Any())
                {
                    if (popYear == null)
                    {
                        View popViwe1 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
                        ListView listview1 = popViwe1.FindViewById<ListView>(Resource.Id.lv);
                        QuarterSelectAdapter adaptera = new QuarterSelectAdapter(CurrActivity, quarterList);
                        adaptera.SetSelectedValue(searchQuarter.QuarterName);
                        listview1.Adapter = adaptera;

                        popYear = new PopupWindow(popViwe1, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popYear.Touchable = true;
                        popYear.Focusable = true;
                        popYear.OutsideTouchable = false;
                        popYear.SetBackgroundDrawable(new BitmapDrawable());

                        popYear.DismissEvent += (sender, e) =>
                        {
                            tv_year.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_year.SetCompoundDrawables(null, null, arrowDown, null);
                        };
                        //每一行的点击事件
                        listview1.ItemClick += (sender, e) =>
                        {
                            searchQuarter = this.quarterList[e.Position];
                            tv_year.Text = searchQuarter.QuarterName;

                            popYear.Dismiss();

                            adaptera.SetSelectedValue(searchQuarter.QuarterName);
                            adaptera.NotifyDataSetChanged();

                            BindData();
                        };
                    }
                    if (!popYear.IsShowing)
                    {
                        tv_year.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_year.SetCompoundDrawables(null, null, arrowDownOn, null);

                        if ((int)(Build.VERSION.SdkInt) >= 24)
                        {
                            int[] a = new int[2];
                            v.GetLocationInWindow(a);
                            popYear.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
                        }
                        else
                        {
                            popYear.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                        }
                    }
                }
                #endregion
            }
            else if (v.Id == Resource.Id.tv_district)
            {
                #region Distirct
                if (districtList != null && districtList.Any())
                {
                    if (popDistrict == null)
                    {
                        View popViwe3 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
                        ListView listview3 = popViwe3.FindViewById<ListView>(Resource.Id.lv);
                        DistrictSelectAdapter adaptera = new DistrictSelectAdapter(CurrActivity, districtList);
                        adaptera.SetSelectedValue(searchDistrict);
                        listview3.Adapter = adaptera;

                        popDistrict = new PopupWindow(popViwe3, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popDistrict.Touchable = true;
                        popDistrict.Focusable = true;
                        popDistrict.OutsideTouchable = true; ;
                        popDistrict.SetBackgroundDrawable(new BitmapDrawable());

                        popDistrict.DismissEvent += (sender, e) =>
                        {
                            tv_district.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_district.SetCompoundDrawables(null, null, arrowDown, null);
                        };
                        //每一行的点击事件
                        listview3.ItemClick += (sender, e) =>
                        {
                            searchDistrict = this.districtList[e.Position];
                            tv_district.Text = searchDistrict;

                            popDistrict.Dismiss();

                            adaptera.SetSelectedValue(searchDistrict);
                            adaptera.NotifyDataSetChanged();

                            BindData();
                        };
                    }
                    if (!popDistrict.IsShowing)
                    {
                        tv_district.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_district.SetCompoundDrawables(null, null, arrowDownOn, null);

                        if ((int)(Build.VERSION.SdkInt) >= 24)
                        {
                            int[] a = new int[2];
                            v.GetLocationInWindow(a);
                            popDistrict.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
                        }
                        else
                        {
                            popDistrict.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                        }
                    }

                }
                #endregion
            }
            else if (v.Id == Resource.Id.tv_grade)
            {
                #region Grade
                if (gradeList != null && gradeList.Any())
                {
                    if (popGrade == null)
                    {
                        View popViwe2 = layoutInflater.Inflate(Resource.Layout.popup_grade, null);
                        Button btnOk = popViwe2.FindViewById<Button>(Resource.Id.btn_ok);

                        var screenWidth = Resources.DisplayMetrics.WidthPixels;
                        var wrapperWidth = screenWidth - AppUtils.dip2px(CurrActivity, 24);
                        var itemWidth = (int)Math.Round((wrapperWidth / 4) * 0.8);
                        var marginRight = (wrapperWidth - itemWidth * 4) / 3;

                        var tvAll = popViwe2.FindViewById<TextView>(Resource.Id.tv_all);
                        ViewGroup.LayoutParams tvallParams = tvAll.LayoutParameters;
                        tvallParams.Width = itemWidth;
                        tvAll.LayoutParameters = tvallParams;

                        GridLayout gridlayout_1 = popViwe2.FindViewById<GridLayout>(Resource.Id.gridlayout_1);
                        for (var i = 0; i < gradeList.Count; i++)
                        {
                            TextView tvGrade = new TextView(CurrActivity);
                            var itemGrade = gradeList[i];
                            GridLayout.LayoutParams gradeParams = new GridLayout.LayoutParams();
                            gradeParams.Width = itemWidth;
                            //parasBox.Height = 
                            if (i % 4 != 3)
                            {
                                gradeParams.RightMargin = marginRight;
                            }
                            gradeParams.TopMargin = AppUtils.dip2px(CurrActivity, 5);
                            gradeParams.BottomMargin = AppUtils.dip2px(CurrActivity, 5);
                            tvGrade.LayoutParameters = gradeParams;
                            tvGrade.Text = itemGrade;
                            tvGrade.TextSize = 14;
                            tvGrade.Gravity = GravityFlags.Center;
                            tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
                            tvGrade.SetPadding(0, AppUtils.dip2px(CurrActivity, 5), 0, AppUtils.dip2px(CurrActivity, 5));
                            tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));

                            gridlayout_1.AddView(tvGrade);

                            #region tvGrade.Click
                            tvGrade.Click += (sender, e) =>
                            {
                                if (!searchGradeList.Contains(itemGrade))
                                {
                                    searchGradeList.Add(itemGrade);

                                    tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                    tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
                                }
                                else
                                {
                                    searchGradeList.Remove(itemGrade);

                                    tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                    tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);

                                    tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                    tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);
                                }

                                //控制全部年级按钮颜色
                                if (gradeList.Count == searchGradeList.Count)
                                {
                                    tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                    tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
                                }
                                //控制确定按钮是否可用
                                if (searchGradeList.Count == 0)
                                {
                                    btnOk.Enabled = false;
                                    btnOk.SetBackgroundResource(Resource.Drawable.button_bg_disabled);
                                }
                                else
                                {
                                    btnOk.Enabled = true;
                                    btnOk.SetBackgroundResource(Resource.Drawable.button_bg);
                                }
                            };
                        }
                        #endregion

                        #region tvAll.Click
                        tvAll.Click += (sender, e) =>
                        {
                            if (tvAll.CurrentTextColor == ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh))
                            {
                                tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);

                                for (var i = 0; i < gridlayout_1.ChildCount; i++)
                                {
                                    var tv = (TextView)gridlayout_1.GetChildAt(i);
                                    tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                    tv.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);
                                }
                                searchGradeList = new List<string>();
                            }
                            else
                            {
                                tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);

                                for (var i = 0; i < gridlayout_1.ChildCount; i++)
                                {
                                    var tv = (TextView)gridlayout_1.GetChildAt(i);
                                    tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                    tv.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
                                }
                                searchGradeList = new List<string>(gradeList.ToArray());
                            }

                            //控制确定按钮是否可用
                            if (searchGradeList.Count == 0)
                            {
                                btnOk.Enabled = false;
                                btnOk.SetBackgroundResource(Resource.Drawable.button_bg_disabled);
                            }
                            else
                            {
                                btnOk.Enabled = true;
                                btnOk.SetBackgroundResource(Resource.Drawable.button_bg);
                            }
                        };
                        #endregion

                        #region btnOK.Click
                        btnOk.Click += (sender, e) =>
                        {
                            popGrade.Dismiss();
                            var selectedgrade = "全部年级";
                            if (searchGradeList.Count != gradeList.Count)
                            {
                                selectedgrade = string.Join(",", searchGradeList.ToArray());
                            }

                            tv_grade.Text = selectedgrade;

                            BindData();
                        };
                        #endregion

                        popGrade = new PopupWindow(popViwe2, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popGrade.Touchable = true;
                        popGrade.Focusable = true;
                        popGrade.OutsideTouchable = true; ;
                        popGrade.SetBackgroundDrawable(new BitmapDrawable());

                        popGrade.DismissEvent += (sender, e) =>
                        {
                            tv_grade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_grade.SetCompoundDrawables(null, null, arrowDown, null);
                        };

                    }
                    if (!popGrade.IsShowing)
                    {
                        tv_grade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_grade.SetCompoundDrawables(null, null, arrowDownOn, null);


                        if ((int)(Build.VERSION.SdkInt) >= 24)
                        {
                            int[] a = new int[2];
                            v.GetLocationInWindow(a);
                            popGrade.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
                        }
                        else
                        {
                            popGrade.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                        }
                    }
                }
                #endregion
            }
            else if (v.Id == Resource.Id.tv_course)
            {
                #region Course
                if (courseList != null && courseList.Any())
                {
                    if (popCourse == null)
                    {
                        View popViwe3 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
                        ListView listview3 = popViwe3.FindViewById<ListView>(Resource.Id.lv);
                        CourseSelectAdapter adaptera = new CourseSelectAdapter(CurrActivity, courseList);
                        adaptera.SetSelectedValue(searchCourse);
                        listview3.Adapter = adaptera;

                        popCourse = new PopupWindow(popViwe3, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popCourse.Touchable = true;
                        popCourse.Focusable = true;
                        popCourse.OutsideTouchable = true; ;
                        popCourse.SetBackgroundDrawable(new BitmapDrawable());

                        popCourse.DismissEvent += (sender, e) =>
                        {
                            tv_course.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_course.SetCompoundDrawables(null, null, arrowDown, null);
                        };

                        listview3.ItemClick += (sender, e) =>
                        {
                            searchCourse = this.courseList[e.Position];
                            tv_course.Text = searchCourse;

                            popCourse.Dismiss();

                            adaptera.SetSelectedValue(searchCourse);
                            adaptera.NotifyDataSetChanged();

                            //Toast.MakeText(CurrActivity, searchCourse, ToastLength.Short).Show();
                            BindData();
                        };
                    }
                    if (!popCourse.IsShowing)
                    {
                        tv_course.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_course.SetCompoundDrawables(null, null, arrowDownOn, null);

                        if ((int)(Build.VERSION.SdkInt) >= 24)
                        {
                            int[] a = new int[2];
                            v.GetLocationInWindow(a);
                            popCourse.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
                        }
                        else
                        {
                            popCourse.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                        }
                    }

                }
                #endregion
            }
        }

        public void clickItemTitle(PaymentSumAreaEntity item)
        {

            item.IsFold = !item.IsFold;

            mAdapter.NotifyDataSetChanged();
        }

        public void OnRefresh()
        {
            if (!NetUtil.CheckNetWork(CurrActivity))
            {
                ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
            }
            else
            {
                BindData();
            }
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

                var totalItemCount = recyclerView.GetAdapter().ItemCount;
                var lastVisibleItemPosition = LayoutManager.FindLastVisibleItemPosition();
                Log.Debug("test", "totalItemCount =" + totalItemCount + "-----" + "lastVisibleItemPosition =" + lastVisibleItemPosition);

                if (totalItemCount == (lastVisibleItemPosition + 1) && LoadMoreEvent != null)
                {
                    LoadMoreEvent(this, null);
                }
            }
        }
    }
=======
	/// <summary>
	/// 区域-累计-校区维度
	/// </summary>
	public class SumAccountFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		#region UIField
		private LayoutInflater layoutInflater;
		//财年、区域按钮
		private TextView tv_year, tv_district, tv_grade, tv_course;
		private PopupWindow popYear, popDistrict, popGrade, popCourse;

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;
		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		private SumAccountAdapter mAdapter;
		#endregion

		#region Field

		private decimal avgGrowthRate = 0;
		private List<PaymentSumAreaEntity> sumList = new List<PaymentSumAreaEntity>();
		private List<QuarterEntity> quarterList = new List<QuarterEntity>();
		private List<string> districtList = new List<string>(),
							 gradeList = new List<string>(),
							 courseList = new List<string>();
		private QuarterEntity searchQuarter = new QuarterEntity { QuarterName = "2018财年Q1", Year = 2018, Quarter = 1 };
		private string searchDistrict = "全部区域", searchCourse = "全部科目";
		private List<string> searchGradeList = new List<string>();

		//累计：1-人次；2-预收；3-行课
		public int dataType = 1;
		//标题排序：1/2-本期累计升/倒序；3/4-去年同期升/倒序；5/6-增长率升/倒序
		int sortType = 6;

		//总计行
		int totalCount = 0;
		bool loadingData = false;
		#endregion

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_sumaccount, container, false);

			InitViews(view);
			LoadData();

			InitEvents();

			return view;
		}

		protected void InitEvents()
		{ 
			mSwipeRefreshLayout.SetOnRefreshListener(this);

			var viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));

			// 加载更多
			var onScrollListener = new XamarinRecyclerViewOnScrollListener(linearLayoutManager);
			onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
			{
				if (totalCount > this.sumList.Count)
				{
					if (!loadingData)
					{
						loadingData = true;
						BindData();
					}
				}
				else if (totalCount == this.sumList.Count)
				{
					Toast.MakeText(this.CurrContext, "没有更多了", ToastLength.Short).Show();
				}
			};
			mRecyclerView.AddOnScrollListener(onScrollListener);
		}
		                          

		#region 初始化页面控件

		/// <summary>
		/// 初始化页面控件
		/// </summary>
		protected void InitViews(View view)
		{
			tv_year = view.FindViewById<TextView>(Resource.Id.tv_year);
			tv_district = view.FindViewById<TextView>(Resource.Id.tv_district);
			tv_grade = view.FindViewById<TextView>(Resource.Id.tv_grade);
			tv_course = view.FindViewById<TextView>(Resource.Id.tv_course);

			//添加按钮的事件监控
			tv_year.SetOnClickListener(this);
			tv_district.SetOnClickListener(this);
			tv_grade.SetOnClickListener(this);
			tv_course.SetOnClickListener(this);

			mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
			mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

			//adapter展示列表数据
			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mAdapter = new SumAccountAdapter(CurrActivity, sumList, this.avgGrowthRate);
			mRecyclerView.SetLayoutManager(linearLayoutManager);
			mRecyclerView.SetAdapter(mAdapter);
			mAdapter.NotifyDataSetChanged();

			mSwipeRefreshLayout.SetOnRefreshListener(this);
			RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
		}
		#endregion

		#region 加载数据
		protected void LoadData()
		{
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
				return;
			}

			//财年数据
			if (BaseApplication.GetInstance().quarterList == null)
			{
				BaseApplication.GetInstance().quarterList = RenewService.GetQuarter(CurrUserInfo.SchoolId);
			}
			//区域数据
			if (BaseApplication.GetInstance().districtList == null)
			{
				BaseApplication.GetInstance().districtList = RenewService.GetDistrictList(CurrUserInfo.SchoolId);
			}
			//年级数据
			if (BaseApplication.GetInstance().gradeList == null)
			{
				BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
			}
			//科目数据
			if (BaseApplication.GetInstance().courseList == null)
			{
				BaseApplication.GetInstance().courseList = DistrictService.GetCourseList(CurrUserInfo.SchoolId);
			}

			//默认财年
			if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
			{
				quarterList = BaseApplication.GetInstance().quarterList;
				searchQuarter = BaseApplication.GetInstance().quarterList.Find(t => t.IsCurrent);
				tv_year.Text = searchQuarter.QuarterName;
			}
			//默认区域
			if (BaseApplication.GetInstance().districtList != null && BaseApplication.GetInstance().districtList.Any())
			{
				districtList = new List<string>(BaseApplication.GetInstance().districtList.Select(i => i.DistrictName).ToArray());
				//加区域的权限判断--区域经理登录
				if (CurrUserInfo.Type == (int)UserType.AreaManager)
				{
					districtList = districtList.Where(p => p == CurrUserInfo.DistrictName).ToList();
					tv_district.Text = CurrUserInfo.DistrictName;
					searchDistrict = CurrUserInfo.DistrictName;
				}
			}
			//默认年级
			if (BaseApplication.GetInstance().gradeList != null && BaseApplication.GetInstance().gradeList.Any())
			{
				gradeList = new List<string>(BaseApplication.GetInstance().gradeList.Select(i => i.GradeName).ToArray());

			}
			if (searchGradeList != null && searchGradeList.Any())
			{
				var selectedgrade = "全部年级";
				if (searchGradeList.Count > 0 && searchGradeList.Count != gradeList.Count)
				{
					selectedgrade = string.Join(",", searchGradeList.ToArray());
				}

				tv_grade.Text = selectedgrade;
			}
			else
			{
				// 默认全选
				searchGradeList = new List<string>(gradeList.ToArray());
			}
			//默认科目
			if (BaseApplication.GetInstance().courseList != null && BaseApplication.GetInstance().courseList.Any())
			{
				courseList = new List<string>(BaseApplication.GetInstance().courseList.Select(t => t.CourseName).ToArray());
				searchCourse = BaseApplication.GetInstance().courseList[0].CourseName;
				tv_course.Text = searchCourse;
			}

			BindData();
		}
		#endregion

		#region 绑定数据
		public void BindData()
		{
			LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");

			try
			{
				var schoolId = CurrUserInfo.SchoolId;
				var year = searchQuarter.Year;
				var quarter = searchQuarter.Quarter;
				var district = searchDistrict;
				var course = searchCourse;
				var grade = "";
				if (searchGradeList.Any())
				{
					grade = string.Join(",", searchGradeList.ToArray());
				}

				var areaCodes = ""; //店长登录用
				if (CurrUserInfo.Type == (int)UserType.ShopManager)
					areaCodes = CurrUserInfo.AreaCodes;

				new System.Threading.Thread(new ThreadStart(() =>
				{
					var list = SumService.GetSumPaymentListByArea(schoolId, year, quarter,dataType,sortType, district,grade,course, out totalCount,1, 1, 500, areaCodes);
					CurrActivity.RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						if (list != null)
						{
							var totalData = list.TotalData;
							this.avgGrowthRate = totalData.GrowthRate;
							this.sumList = list.List;
							var totalEntity = new PaymentSumAreaEntity() { Name = "总计", CurrentSum=totalData.CurrentSum, LastYearSum = totalData.LastYearSum, GrowthRate = totalData.GrowthRate };
							this.sumList.Add(totalEntity);
							mAdapter.SetData(this.sumList,this.avgGrowthRate);
							mAdapter.NotifyDataSetChanged();
						}
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
		#endregion

		public void OnClick(View v)
		{
			if (v.Id == Resource.Id.tv_year)
			{
				#region Quarter
				if (quarterList != null && quarterList.Any())
				{
					if (popYear == null)
					{
						View popViwe1 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
						ListView listview1 = popViwe1.FindViewById<ListView>(Resource.Id.lv);
						QuarterSelectAdapter adaptera = new QuarterSelectAdapter(CurrActivity, quarterList);
						adaptera.SetSelectedValue(searchQuarter.QuarterName);
						listview1.Adapter = adaptera;

						popYear = new PopupWindow(popViwe1, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
						popYear.Touchable = true;
						popYear.Focusable = true;
						popYear.OutsideTouchable = false;
						popYear.SetBackgroundDrawable(new BitmapDrawable());

						popYear.DismissEvent += (sender, e) =>
						{
							tv_year.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
							var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
							arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
							tv_year.SetCompoundDrawables(null, null, arrowDown, null);
						};
						//每一行的点击事件
						listview1.ItemClick += (sender, e) =>
						{
							searchQuarter = this.quarterList[e.Position];
							tv_year.Text = searchQuarter.QuarterName;

							popYear.Dismiss();

							adaptera.SetSelectedValue(searchQuarter.QuarterName);
							adaptera.NotifyDataSetChanged();

							BindData();
						};
					}
					if (!popYear.IsShowing)
					{
						tv_year.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
						arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
						tv_year.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popYear.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popYear.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
					}
				}
				#endregion
			}
			else if (v.Id == Resource.Id.tv_district)
			{
				#region Distirct
				if (districtList != null && districtList.Any())
				{
					if (popDistrict == null)
					{
						View popViwe3 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
						ListView listview3 = popViwe3.FindViewById<ListView>(Resource.Id.lv);
						DistrictSelectAdapter adaptera = new DistrictSelectAdapter(CurrActivity, districtList);
						adaptera.SetSelectedValue(searchDistrict);
						listview3.Adapter = adaptera;

						popDistrict = new PopupWindow(popViwe3, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
						popDistrict.Touchable = true;
						popDistrict.Focusable = true;
						popDistrict.OutsideTouchable = true; ;
						popDistrict.SetBackgroundDrawable(new BitmapDrawable());

						popDistrict.DismissEvent += (sender, e) =>
						{
							tv_district.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
							var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
							arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
							tv_district.SetCompoundDrawables(null, null, arrowDown, null);
						};
						//每一行的点击事件
						listview3.ItemClick += (sender, e) =>
						{
							searchDistrict = this.districtList[e.Position];
							tv_district.Text = searchDistrict;

							popDistrict.Dismiss();

							adaptera.SetSelectedValue(searchDistrict);
							adaptera.NotifyDataSetChanged();

							BindData();
						};
					}
					if (!popDistrict.IsShowing)
					{
						tv_district.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
						arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
						tv_district.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popDistrict.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popDistrict.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
					}

				}
				#endregion
			}
			else if (v.Id == Resource.Id.tv_grade)
			{
				#region Grade
				if (gradeList != null && gradeList.Any())
				{
					if (popGrade == null)
					{
						View popViwe2 = layoutInflater.Inflate(Resource.Layout.popup_grade, null);
						Button btnOk = popViwe2.FindViewById<Button>(Resource.Id.btn_ok);

						var screenWidth = Resources.DisplayMetrics.WidthPixels;
						var wrapperWidth = screenWidth - AppUtils.dip2px(CurrActivity, 24);
						var itemWidth = (int)Math.Round((wrapperWidth / 4) * 0.8);
						var marginRight = (wrapperWidth - itemWidth * 4) / 3;

						var tvAll = popViwe2.FindViewById<TextView>(Resource.Id.tv_all);
						ViewGroup.LayoutParams tvallParams = tvAll.LayoutParameters;
						tvallParams.Width = itemWidth;
						tvAll.LayoutParameters = tvallParams;

						GridLayout gridlayout_1 = popViwe2.FindViewById<GridLayout>(Resource.Id.gridlayout_1);
						for (var i = 0; i < gradeList.Count; i++)
						{
							TextView tvGrade = new TextView(CurrActivity);
							var itemGrade = gradeList[i];
							GridLayout.LayoutParams gradeParams = new GridLayout.LayoutParams();
							gradeParams.Width = itemWidth;
							//parasBox.Height = 
							if (i % 4 != 3)
							{
								gradeParams.RightMargin = marginRight;
							}
							gradeParams.TopMargin = AppUtils.dip2px(CurrActivity, 5);
							gradeParams.BottomMargin = AppUtils.dip2px(CurrActivity, 5);
							tvGrade.LayoutParameters = gradeParams;
							tvGrade.Text = itemGrade;
							tvGrade.TextSize = 14;
							tvGrade.Gravity = GravityFlags.Center;
							tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
							tvGrade.SetPadding(0, AppUtils.dip2px(CurrActivity, 5), 0, AppUtils.dip2px(CurrActivity, 5));
							tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));

							gridlayout_1.AddView(tvGrade);

							#region tvGrade.Click
							tvGrade.Click += (sender, e) =>
							{
								if (!searchGradeList.Contains(itemGrade))
								{
									searchGradeList.Add(itemGrade);

									tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
									tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
								}
								else
								{
									searchGradeList.Remove(itemGrade);

									tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
									tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);

									tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
									tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);
								}

								//控制全部年级按钮颜色
								if (gradeList.Count == searchGradeList.Count)
								{
									tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
									tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
								}
								//控制确定按钮是否可用
								if (searchGradeList.Count == 0)
								{
									btnOk.Enabled = false;
									btnOk.SetBackgroundResource(Resource.Drawable.button_bg_disabled);
								}
								else
								{
									btnOk.Enabled = true;
									btnOk.SetBackgroundResource(Resource.Drawable.button_bg);
								}
							};
						}
						#endregion

						#region tvAll.Click
						tvAll.Click += (sender, e) =>
						{
							if (tvAll.CurrentTextColor == ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh))
							{
								tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
								tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);

								for (var i = 0; i < gridlayout_1.ChildCount; i++)
								{
									var tv = (TextView)gridlayout_1.GetChildAt(i);
									tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
									tv.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);
								}
								searchGradeList = new List<string>();
							}
							else
							{
								tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
								tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);

								for (var i = 0; i < gridlayout_1.ChildCount; i++)
								{
									var tv = (TextView)gridlayout_1.GetChildAt(i);
									tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
									tv.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
								}
								searchGradeList = new List<string>(gradeList.ToArray());
							}

							//控制确定按钮是否可用
							if (searchGradeList.Count == 0)
							{
								btnOk.Enabled = false;
								btnOk.SetBackgroundResource(Resource.Drawable.button_bg_disabled);
							}
							else
							{
								btnOk.Enabled = true;
								btnOk.SetBackgroundResource(Resource.Drawable.button_bg);
							}
						};
						#endregion

						#region btnOK.Click
						btnOk.Click += (sender, e) =>
						{
							popGrade.Dismiss();
							var selectedgrade = "全部年级";
							if (searchGradeList.Count != gradeList.Count)
							{
								selectedgrade = string.Join(",", searchGradeList.ToArray());
							}

							tv_grade.Text = selectedgrade;

							BindData();
						};
						#endregion

						popGrade = new PopupWindow(popViwe2, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
						popGrade.Touchable = true;
						popGrade.Focusable = true;
						popGrade.OutsideTouchable = true; ;
						popGrade.SetBackgroundDrawable(new BitmapDrawable());

						popGrade.DismissEvent += (sender, e) =>
						{
							tv_grade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
							var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
							arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
							tv_grade.SetCompoundDrawables(null, null, arrowDown, null);
						};

					}
					if (!popGrade.IsShowing)
					{
						tv_grade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
						arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
						tv_grade.SetCompoundDrawables(null, null, arrowDownOn, null);


						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popGrade.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popGrade.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
					}
				}
				#endregion
			}
			else if (v.Id == Resource.Id.tv_course)
			{
				#region Course
				if (courseList != null && courseList.Any())
				{
					if (popCourse == null)
					{
						View popViwe3 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
						ListView listview3 = popViwe3.FindViewById<ListView>(Resource.Id.lv);
						CourseSelectAdapter adaptera = new CourseSelectAdapter(CurrActivity, courseList);
						adaptera.SetSelectedValue(searchCourse);
						listview3.Adapter = adaptera;

						popCourse = new PopupWindow(popViwe3, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
						popCourse.Touchable = true;
						popCourse.Focusable = true;
						popCourse.OutsideTouchable = true; ;
						popCourse.SetBackgroundDrawable(new BitmapDrawable());

						popCourse.DismissEvent += (sender, e) =>
						{
							tv_course.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
							var arrowDown = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down);
							arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
							tv_course.SetCompoundDrawables(null, null, arrowDown, null);
						};

						listview3.ItemClick += (sender, e) =>
						{
							searchCourse = this.courseList[e.Position];
							tv_course.Text = searchCourse;

							popCourse.Dismiss();

							adaptera.SetSelectedValue(searchCourse);
							adaptera.NotifyDataSetChanged();

							//Toast.MakeText(CurrActivity, searchCourse, ToastLength.Short).Show();
							BindData();
						};
					}
					if (!popCourse.IsShowing)
					{
						tv_course.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.arrow_down_on);
						arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
						tv_course.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popCourse.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popCourse.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
					}

				}
				#endregion
			}
		}

		public void OnItemClick(View itemView, int position)
		{
			var recyclerView = itemView as RecyclerView;
			var itemRecyclerView = (itemView as LinearLayout).GetChildAt(2) as RecyclerView;//(RecyclerView)itemView.FindViewById(Resource.Id.item_recycler_view);
			if (itemRecyclerView.Id == Resource.Id.item_recycler_view)
			{
				
			}

			if (itemRecyclerView.Id == Resource.Id.recycler_view)
			{ 
			
			}

			var item = sumList[position];

			if (item.GradeData != null)
			{
				//var itemRecyclerView = (itemView as LinearLayout).GetChildAt(2) as RecyclerView;//(RecyclerView)itemView.FindViewById(Resource.Id.item_recycler_view);

				//判断当前行的item显示隐藏状态：显示时设置隐藏；反之显示
				var viewState = itemRecyclerView.Visibility;
				if (viewState == ViewStates.Visible)
				{
					itemRecyclerView.Visibility = ViewStates.Gone;
				}
				else
				{
					itemRecyclerView.Visibility = ViewStates.Visible;
					//adapter展示列表数据
					var itemLinearLayoutManager = new LinearLayoutManager(CurrActivity);
					var sumAdapter = new SumAccountAdapter(CurrActivity, item.GradeData, item.GrowthRate);
					itemRecyclerView.SetLayoutManager(itemLinearLayoutManager);
					itemRecyclerView.SetAdapter(sumAdapter);
					sumAdapter.NotifyDataSetChanged();
				}
			}
			else
			{ 
				var intent = new Intent(CurrActivity, typeof(SumByTeacherActivity));
				intent.PutExtra("year", this.searchQuarter.Year);
				intent.PutExtra("quarter", this.searchQuarter.Quarter);
				intent.PutExtra("dataType", this.dataType);
				intent.PutExtra("areaCode", item.Code);
				intent.PutExtra("areaName", item.Name);
				intent.PutExtra("course", this.searchCourse);
				var gradeListParam = "";
				if (searchGradeList.Count != gradeList.Count)
				{
					gradeListParam = string.Join(",", searchGradeList.ToArray());
				}
				intent.PutExtra("gradeList", gradeListParam);
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
			}
		}

		public void OnItemLongClick(View itemView, int position)
		{
			return;
		}

		public void OnRefresh()
		{
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
			}
			else
			{
				BindData();
			}
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

				var totalItemCount = recyclerView.GetAdapter().ItemCount;
				var lastVisibleItemPosition = LayoutManager.FindLastVisibleItemPosition();
				Log.Debug("test", "totalItemCount =" + totalItemCount + "-----" + "lastVisibleItemPosition =" + lastVisibleItemPosition);

				if (totalItemCount == (lastVisibleItemPosition + 1) && LoadMoreEvent != null)
				{
					LoadMoreEvent(this, null);
				}
			}
		}
	}
>>>>>>> d816c02... commit,sum
}
