
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Utils;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Models;

namespace YbkManage.Activities
{
    /// <summary>
    /// 报表列表页
    /// </summary>
    [Activity(Label = "ReportListByGroup")]
    public class ReportListByGroup : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
    {
		// 季度、年级、区域 筛选按钮
		private TextView tv_btn1, tv_btn2, tv_btn3;
		private List<QuarterEntity> quarterList = new List<QuarterEntity>();
		private List<string> districtList = new List<string>(), gradeList = new List<string>();
		private PopupWindow popWin1, popWin2, popWin3;

		// 报表的筛选条件
		private QuarterEntity searchQuarter = new QuarterEntity { Year = 2018, Quarter = 2 };
		private List<string> searchGradeList = new List<string>();
		private string searchDistrict = "";

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;

		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		private ReportAdapter mAdapter;

		// 教师数据
		private List<TeachReportEntity> teachReportList = new List<TeachReportEntity>();

        private TeachReportEntity currReportInfo=new TeachReportEntity();

		protected override void OnCreate(Bundle savedInstanceState)
		{
            LayoutReourceId = Resource.Layout.activity_report_list_bygroup;

			base.OnCreate(savedInstanceState);
		}

		protected override void InitVariables()
		{
			var reportJsonStr = Intent.Extras.GetString("reportJsonStr");
            currReportInfo = JsonSerializer.ToObject<TeachReportEntity>(reportJsonStr);
		}

		protected override void InitViews()
		{
            FindViewById<TextView>(Resource.Id.tv_title).Text = string.Format("{0}教学报表",currReportInfo.Name);

			tv_btn1 = FindViewById<TextView>(Resource.Id.tv_btn1);
			tv_btn2 = FindViewById<TextView>(Resource.Id.tv_btn2);
			tv_btn3 = FindViewById<TextView>(Resource.Id.tv_btn3);

			mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mAdapter = new ReportAdapter(CurrContext, teachReportList);
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

			tv_btn1.SetOnClickListener(this);
			tv_btn2.SetOnClickListener(this);
			tv_btn3.SetOnClickListener(this);
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		protected override void LoadData()
		{
			var quarters = SharedPreferencesUtil.GetParam(CurrActivity, AppConfig.SP_QUARTER, "").ToString();
			if (!string.IsNullOrEmpty(quarters))
			{
				quarterList = JsonSerializer.ToObjectList<QuarterEntity>(quarters);
			}
			GetDistrictList();
			GetGradeList();

			mSwipeRefreshLayout.Refreshing = true;
			GetRenewInfoInTeacherByGroupCode();
		}

		/// <summary>
		/// 下拉刷新
		/// </summary>
		public void OnRefresh()
		{
			mSwipeRefreshLayout.Refreshing = true;
			GetRenewInfoInTeacherByGroupCode();
		}

		/// <summary>
		/// 获取区域数据
		/// </summary>
		private async void GetGradeList()
		{
			try
			{
				Dictionary<string, string> requstParams = new Dictionary<string, string>();
				requstParams.Add("appId", AppConfig.APP_ID);
				requstParams.Add("method", "GetGradeList");
				requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
				requstParams.Add("sign", AppUtils.GetSign(requstParams));
				var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT, requstParams);

				var data = (JsonObject)result;
				var state = int.Parse(data["State"].ToString());
				if (state == 1)
				{
					gradeList.Clear();

					var jsonArr = JsonValue.Parse(data["Data"].ToString());
					for (int i = 0; i < jsonArr.Count; i++)
					{
						gradeList.Add(jsonArr[i]["GradeName"].ToString().Replace("\"", ""));
					}

					// 默认全选
					searchGradeList = new List<string>(gradeList.ToArray());
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
			}
		}

		/// <summary>
		/// 获取区域数据
		/// </summary>
		private async void GetDistrictList()
		{
			try
			{
				Dictionary<string, string> requstParams = new Dictionary<string, string>();
				requstParams.Add("appId", AppConfig.APP_ID);
				requstParams.Add("method", "GetDistrictList");
				requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
				requstParams.Add("sign", AppUtils.GetSign(requstParams));
				var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT, requstParams);

				var data = (JsonObject)result;
				var state = int.Parse(data["State"].ToString());
				if (state == 1)
				{
					districtList.Clear();

					var jsonArr = JsonValue.Parse(data["Data"].ToString());
					for (int i = 0; i < jsonArr.Count; i++)
					{
						districtList.Add(jsonArr[i]["DistrictName"].ToString().Replace("\"", ""));
					}
					if (districtList.Count > 1)
					{
						districtList.Insert(0, "全部区域");
					}
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
			}
		}

		/// <summary>
		/// 获取报表数据
		/// </summary>
		private async void GetRenewInfoInTeacherByGroupCode()
		{
			try
			{
				//LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
				Dictionary<string, string> requstParams = new Dictionary<string, string>();
				//requstParams.Add("appId", "1004");
				requstParams.Add("method", "GetRenewInfoInTeacherByGroupCode");
				requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
				requstParams.Add("Year", (searchQuarter.Year - 1).ToString());
				requstParams.Add("Quarter", searchQuarter.Quarter.ToString());
				requstParams.Add("NeedTotal", "1");
				requstParams.Add("SortType", "6");
                requstParams.Add("GroupCode", currReportInfo.ScopeId.ToString());
				if (!string.IsNullOrEmpty(searchDistrict) && !searchDistrict.Equals("全部区域"))
				{
					requstParams.Add("District", searchDistrict);
				}
				if (searchGradeList.Any())
				{
					var gradeStr = string.Join(",", searchGradeList.ToArray());
					requstParams.Add("Grade", gradeStr);
				}
				requstParams.Add("sign", AppUtils.GetSign(requstParams));
				var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT2, requstParams);


				var data = (JsonObject)result;
				var state = int.Parse(data["State"].ToString());
				if (state == 1)
				{
                    teachReportList.Clear();

					var data1 = JsonValue.Parse(data["Data"].ToString());
					TeachReportEntity total = new TeachReportEntity();
					total.Id = 0;
					total.Name = "总计";
					total.ClassSize = double.Parse(data1["TotalNum"].ToString());
					total.ClassContinueSize = double.Parse(data1["RenewNum"].ToString());
					total.ContinueRate = double.Parse(data1["RenewRate"].ToString());

					var jsonArr = JsonValue.Parse(data1["RenewInfo"].ToString());
					for (int i = 0; i < jsonArr.Count; i++)
					{
						TeachReportEntity item = new TeachReportEntity();
						item.Id = int.Parse(jsonArr[i]["Item1"].ToString());
						item.TeacherCode = jsonArr[i]["Item2"].ToString().Replace("\"", "");
						item.Name = jsonArr[i]["Item3"].ToString().Replace("\"", "");
						item.ClassSize = double.Parse(jsonArr[i]["Item4"].ToString());
						item.ClassContinueSize = double.Parse(jsonArr[i]["Item5"].ToString());
						item.ContinueRate = double.Parse(jsonArr[i]["Item6"].ToString());
						teachReportList.Add(item);
					}
                    if(teachReportList.Any())
                    {
                        teachReportList.Add(total);
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

		public void OnItemClick(View itemView, int position)
		{
			var reportItem = teachReportList[position];
			if (reportItem.Id > 0)
			{
				Intent intent = new Intent(CurrActivity, typeof(ReportListByTeacher));
				//intent.PutExtra("scopeId", scopeItem.Id);
				intent.PutExtra("teacherName", reportItem.Name);
				intent.PutExtra("teacherCode", reportItem.TeacherCode);
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
			}
		}

		public void OnItemLongClick(View itemView, int position)
		{
			//throw new NotImplementedException();
		}

        /// <summary>
        /// 页面点击事件处理
        /// </summary>
        /// <param name="v">V.</param>
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.tv_btn1)
            {
                if (quarterList != null && quarterList.Any())
                {
                    if (popWin1 == null)
                    {
                        View popViwe1 = LayoutInflater.Inflate(Resource.Layout.popup_select1, null);
                        ListView listview1 = popViwe1.FindViewById<ListView>(Resource.Id.lv);
                        QuarterSelectAdapter adaptera = new QuarterSelectAdapter(CurrActivity, quarterList);
                        adaptera.SetSelectedValue(quarterList[0].QuarterName);
                        listview1.Adapter = adaptera;

                        popWin1 = new PopupWindow(popViwe1, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popWin1.Touchable = true;
                        popWin1.Focusable = true;
                        popWin1.OutsideTouchable = false;
                        popWin1.SetBackgroundDrawable(new BitmapDrawable());

                        popWin1.DismissEvent += (sender, e) =>
                        {
                            tv_btn1.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = CurrActivity.GetDrawable(Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_btn1.SetCompoundDrawables(null, null, arrowDown, null);
                        };

                        listview1.ItemClick += (sender, e) =>
                        {
                            searchQuarter = this.quarterList[e.Position];
                            tv_btn1.Text = searchQuarter.QuarterName;

                            popWin1.Dismiss();

                            adaptera.SetSelectedValue(quarterList[0].QuarterName);
                            adaptera.NotifyDataSetChanged();

                            Toast.MakeText(CurrActivity, searchQuarter.QuarterName, ToastLength.Short).Show();
                            GetRenewInfoInTeacherByGroupCode();
                        };

                    }
                    if (!popWin1.IsShowing)
                    {
                        tv_btn1.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn1.SetCompoundDrawables(null, null, arrowDownOn, null);

                        popWin1.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                    }
                }
            }
            else if (v.Id == Resource.Id.tv_btn2)
            {
                if (gradeList != null && gradeList.Any())
                {
                    if (popWin2 == null)
                    {
                        View popViwe2 = LayoutInflater.Inflate(Resource.Layout.popup_grade, null);

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
                            tvGrade.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);
                            tvGrade.SetPadding(0, AppUtils.dip2px(CurrActivity, 5), 0, AppUtils.dip2px(CurrActivity, 5));
                            tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));

                            gridlayout_1.AddView(tvGrade);

                            tvGrade.Click += (sender, e) =>
                            {
                                if (!searchGradeList.Contains(itemGrade))
                                {
                                    tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                    tvGrade.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);

                                    if (gradeList.Count == searchGradeList.Count)
                                    {
                                        tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                        tvAll.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);
                                    }


                                    searchGradeList.Add(itemGrade);

                                }
                                else
                                {
                                    tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                    tvGrade.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg);

                                    tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                    tvAll.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg);


                                    searchGradeList.Remove(itemGrade);
                                }
                            };
                        }

                        tvAll.Click += (sender, e) =>
                        {
                            tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                            tvAll.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);

                            for (var i = 0; i < gridlayout_1.ChildCount; i++)
                            {
                                var tv = (TextView)gridlayout_1.GetChildAt(i);
                                tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                                tv.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);
                            }

                            searchGradeList = new List<string>(gradeList.ToArray());
                        };

                        Button btnOk = popViwe2.FindViewById<Button>(Resource.Id.btn_ok);
                        btnOk.Click += (sender, e) =>
                        {
                            popWin2.Dismiss();
                            var selectedgrade = "全部年级";
                            if (searchGradeList.Count != gradeList.Count)
                            {
                                selectedgrade = string.Join(",", searchGradeList.ToArray());
                            }

                            tv_btn2.Text = selectedgrade;

                            GetRenewInfoInTeacherByGroupCode();
                        };

                        popWin2 = new PopupWindow(popViwe2, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popWin2.Touchable = true;
                        popWin2.Focusable = true;
                        popWin2.OutsideTouchable = true; ;
                        popWin2.SetBackgroundDrawable(new BitmapDrawable());

                        popWin2.DismissEvent += (sender, e) =>
                        {
                            tv_btn2.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = CurrActivity.GetDrawable(Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_btn2.SetCompoundDrawables(null, null, arrowDown, null);
                        };

                    }
                    if (!popWin2.IsShowing)
                    {
                        tv_btn2.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn2.SetCompoundDrawables(null, null, arrowDownOn, null);

                        popWin2.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                    }
                }
            }
            else if (v.Id == Resource.Id.tv_btn3)
            {
                if (districtList != null && districtList.Any())
                {
                    if (popWin3 == null)
                    {
                        View popViwe3 = LayoutInflater.Inflate(Resource.Layout.popup_select1, null);
                        ListView listview3 = popViwe3.FindViewById<ListView>(Resource.Id.lv);
                        DistrictSelectAdapter adaptera = new DistrictSelectAdapter(CurrActivity, districtList);
                        adaptera.SetSelectedValue(searchDistrict);
                        listview3.Adapter = adaptera;

                        popWin3 = new PopupWindow(popViwe3, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popWin3.Touchable = true;
                        popWin3.Focusable = true;
                        popWin3.OutsideTouchable = true; ;
                        popWin3.SetBackgroundDrawable(new BitmapDrawable());

                        popWin3.DismissEvent += (sender, e) =>
                        {
                            tv_btn3.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            var arrowDown = CurrActivity.GetDrawable(Resource.Drawable.arrow_down);
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_btn3.SetCompoundDrawables(null, null, arrowDown, null);
                        };

                        listview3.ItemClick += (sender, e) =>
                        {
                            searchDistrict = this.districtList[e.Position];
                            tv_btn3.Text = searchDistrict;

                            popWin3.Dismiss();

                            adaptera.SetSelectedValue(searchDistrict);
                            adaptera.NotifyDataSetChanged();

                            Toast.MakeText(CurrActivity, searchDistrict, ToastLength.Short).Show();
                            GetRenewInfoInTeacherByGroupCode();
                        };
                    }
                    if (!popWin3.IsShowing)
                    {
                        tv_btn3.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn3.SetCompoundDrawables(null, null, arrowDownOn, null);

                        popWin3.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
                    }

                }
            }
        }
	}
}
