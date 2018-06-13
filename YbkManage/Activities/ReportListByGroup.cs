using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
using Android.Content.PM;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Toast;
using YbkManage.Adapters;
using YbkManage.App;

namespace YbkManage.Activities
{
    /// <summary>
    /// 报表列表页
    /// </summary>
	[Activity(Label = "ReportListByGroup", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ReportListByGroup : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
    {
        // 季度、年级、区域 筛选按钮
        private TextView tv_btn1, tv_btn2, tv_btn3;
        private PopupWindow popWin1, popWin2, popWin3;

        // 报表的筛选条件
        private List<QuarterEntity> quarterList = new List<QuarterEntity>();
        private List<string> districtList = new List<string>(), gradeList = new List<string>();

        private QuarterEntity searchQuarter = new QuarterEntity { QuarterName = "2018财年Q1", Year = 2018, Quarter = 2 };
        private List<string> searchGradeList = new List<string>();
        private string searchDistrict = "全部区域";

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;

        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        private RenewReportAdapter mAdapter;

        // 教师数据
        private List<RenewInfo> teachReportList = new List<RenewInfo>();

        private RenewInfo currReportInfo = new RenewInfo();

        // 所在项目组的平均续班率
        private decimal avgRenewRateScope = 1;

		//班级开课状态：1-开课中（默认），3-全部
		private int classStatus;

		// 筛选器时上下小箭头图标
		Drawable arrowDown, arrowDownOn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_report_list_bygroup;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            Bundle bundle = Intent.Extras;
            if (bundle != null)
            {
                var reportJsonStr = bundle.GetString("reportJsonStr");
                currReportInfo = JsonSerializer.ToObject<RenewInfo>(reportJsonStr);

                avgRenewRateScope = decimal.Parse(bundle.GetString("avgRenewRate", "1"));

                var searchQuarterJsonStr = bundle.GetString("searchQuarter");
                searchQuarter = JsonSerializer.ToObject<QuarterEntity>(searchQuarterJsonStr);
                var searchGradeListStr = bundle.GetString("searchGradeList");
                if(!string.IsNullOrEmpty(searchGradeListStr))
                {
                    searchGradeList = searchGradeListStr.Split(',').ToList();
                }
                searchDistrict = bundle.GetString("searchDistrict");

				classStatus = bundle.GetInt("classStatus");


				Android.Util.Log.Verbose("classStatus-1", classStatus.ToString());
            }

				Android.Util.Log.Verbose("classStatus-2", classStatus.ToString());
        }

        protected override void InitViews()
        {
            FindViewById<TextView>(Resource.Id.tv_title).Text = string.Format("{0}教学报表", currReportInfo.Item3);

            tv_btn1 = FindViewById<TextView>(Resource.Id.tv_btn1);
            tv_btn2 = FindViewById<TextView>(Resource.Id.tv_btn2);
            tv_btn3 = FindViewById<TextView>(Resource.Id.tv_btn3);

            mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new RenewReportAdapter(CurrContext, teachReportList);
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();

            mSwipeRefreshLayout.SetOnRefreshListener(this);
            //mSwipeRefreshLayout.SetOnScrollChangeListener(this);

            //mAdapter.SetOnItemClickListener(this);

            RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));

            arrowDown = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down);
			arrowDownOn = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down_on);

			SetClassStatusImg(FindViewById<ImageButton>(Resource.Id.imgBtn_lessonIng), "init");
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


			//开课中／全部切换事件
			FindViewById<ImageButton>(Resource.Id.imgBtn_lessonIng).Click += (sender, e) =>
			{
				var imgButton = (sender as ImageButton);
				SetClassStatusImg(imgButton, "clickButton");
			};
        }


		#region 设置班级开课状态图片（初始化or点击按钮）
		protected void SetClassStatusImg(ImageButton imgButton, string operateType)
		{
			//开课中图片
			var imgButtonDrawable = Resource.Drawable.lesson_ing;
			if (operateType == "clickButton")
			{
				if (classStatus == 1) //当前开课中，点击切换为全部
				{
					classStatus = 3;
					imgButtonDrawable = Resource.Drawable.lesson_all;
				}
				else
				{
					classStatus = 1;
				}
			}
			else
			{
				//全部
				if (classStatus == 3)
				{
					imgButtonDrawable = Resource.Drawable.lesson_all;
				}
			}

			var image = AppUtils.GetDrawable(CurrContext, imgButtonDrawable);
			imgButton.SetImageDrawable(image);

			//android4.4.4 下面这段报错
			//if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) //> Android 5.0
			//{
			//	var image = ContextCompat.GetDrawable(CurrActivity.ApplicationContext, imgButtonDrawable);
			//	imgButton.SetImageDrawable(image);
			//}
			//else
			//{
			//	var image = Android.Support.Graphics.Drawable.VectorDrawableCompat.Create(this.Resources, imgButtonDrawable, null);
			//	imgButton.SetImageDrawable(image);
			//}

			//切换按钮状态（默认页面初始化）
			if (operateType == "clickButton")
			{
				LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
				GetRenewInfoInTeacherByGroupCode();
			}

		}
		#endregion

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
            LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");

            if (BaseApplication.GetInstance().quarterList == null)
            {
                BaseApplication.GetInstance().quarterList = RenewService.GetQuarter(CurrUserInfo.SchoolId);
            }
            if (BaseApplication.GetInstance().gradeList == null)
            {
                BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
            }
            if (BaseApplication.GetInstance().districtList == null)
            {
                BaseApplication.GetInstance().districtList = RenewService.GetDistrictList(CurrUserInfo.SchoolId);
            }

			if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
			{
				quarterList = BaseApplication.GetInstance().quarterList;
			}
            if (searchQuarter != null)
            {
                tv_btn1.Text = searchQuarter.QuarterName;
            }
            else
            {
                if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
                {
                    searchQuarter = BaseApplication.GetInstance().quarterList.Find(t => t.IsCurrent);
                    tv_btn1.Text = searchQuarter.QuarterName;
                }
            }
            if (BaseApplication.GetInstance().gradeList != null && BaseApplication.GetInstance().gradeList.Any())
            {
                gradeList = new List<string>(BaseApplication.GetInstance().gradeList.Select(i => i.GradeName).ToArray());

            }
            if (searchGradeList != null && searchGradeList.Any())
            {
                var selectedgrade = "全部年级";
                if (searchGradeList.Count>0 && searchGradeList.Count != gradeList.Count)
                {
                    selectedgrade = string.Join(",", searchGradeList.ToArray());
                }

                tv_btn2.Text = selectedgrade;
            }
            else
            {
                // 默认全选
                searchGradeList = new List<string>(gradeList.ToArray());
            }
            if (BaseApplication.GetInstance().districtList != null && BaseApplication.GetInstance().districtList.Any())
            {
                districtList = new List<string>(BaseApplication.GetInstance().districtList.Select(i => i.DistrictName).ToArray());
            }
            if (!string.IsNullOrEmpty(searchDistrict))
            {
                tv_btn3.Text = searchDistrict;
            }


            LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
            GetRenewInfoInTeacherByGroupCode();
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
                GetRenewInfoInTeacherByGroupCode();
            }
        }

        /// <summary>
        /// 获取报表数据
        /// </summary>
        private void GetRenewInfoInTeacherByGroupCode()
        {
			try
			{
				new Thread(new ThreadStart(() =>
				{
					var gradeStr = "";
					if (searchGradeList.Any())
					{
						gradeStr = string.Join(",", searchGradeList.ToArray());
					}
					var districtStr = "";
					if (!string.IsNullOrEmpty(searchDistrict) && !searchDistrict.Equals("全部区域"))
					{
						districtStr = searchDistrict;
					}

					var result = RenewService.GetRenewInfoInTeacherByGroupCode(CurrUserInfo.SchoolId, searchQuarter.Year, searchQuarter.Quarter, gradeStr, districtStr, currReportInfo.Item2.ToString(), 1, 6, classStatus);

					RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						if (result != null)
						{
							teachReportList = result.RenewInfo;
							mAdapter.SetData(teachReportList);
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

        public void OnItemClick(View itemView, int position)
        {
			var reportItem = teachReportList[position];
			if (!string.IsNullOrEmpty(reportItem.Item2))
			{
                Intent intent = new Intent(CurrActivity, typeof(ReportListByTeacher));

				intent.PutExtra("avgRenewRate", avgRenewRateScope.ToString());
				intent.PutExtra("reportJsonStr", JsonSerializer.ToJsonString(reportItem));
				// 搜索条件
				intent.PutExtra("searchQuarter", JsonSerializer.ToJsonString<QuarterEntity>(searchQuarter));
				var selectedgrade = "";
				if (searchGradeList.Count != gradeList.Count)
				{
					selectedgrade = string.Join(",", searchGradeList.ToArray());
				}
				intent.PutExtra("searchGradeList", selectedgrade);
				intent.PutExtra("searchDistrict", searchDistrict);

				intent.PutExtra("classStatus", classStatus);

				Android.Util.Log.Verbose("classStatus", classStatus.ToString());

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
                        adaptera.SetSelectedValue(searchQuarter.QuarterName);
                        listview1.Adapter = adaptera;

                        popWin1 = new PopupWindow(popViwe1, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        popWin1.Touchable = true;
                        popWin1.Focusable = true;
                        popWin1.OutsideTouchable = false;
                        popWin1.SetBackgroundDrawable(new BitmapDrawable());

                        popWin1.DismissEvent += (sender, e) =>
                        {
                            tv_btn1.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                            arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
                            tv_btn1.SetCompoundDrawables(null, null, arrowDown, null);
                        };

                        listview1.ItemClick += (sender, e) =>
                        {
                            searchQuarter = this.quarterList[e.Position];
                            tv_btn1.Text = searchQuarter.QuarterName;

                            popWin1.Dismiss();

                            adaptera.SetSelectedValue(searchQuarter.QuarterName);
                            adaptera.NotifyDataSetChanged();

                            //Toast.MakeText(CurrActivity, searchQuarter.QuarterName, ToastLength.Short).Show();
                            GetRenewInfoInTeacherByGroupCode();
                        };

                    }
                    if (!popWin1.IsShowing)
                    {
                        tv_btn1.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn1.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popWin1.ShowAtLocation(this.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popWin1.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
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
						Button btnOk = popViwe2.FindViewById<Button>(Resource.Id.btn_ok);

						var screenWidth = Resources.DisplayMetrics.WidthPixels;
						var wrapperWidth = screenWidth - AppUtils.dip2px(CurrActivity, 24);
						var itemWidth = (int)Math.Round((wrapperWidth / 4) * 0.8);
						var marginRight = (wrapperWidth - itemWidth * 4) / 3;

						var tvAll = popViwe2.FindViewById<TextView>(Resource.Id.tv_all);
						ViewGroup.LayoutParams tvallParams = tvAll.LayoutParameters;
						tvallParams.Width = itemWidth;
						tvAll.LayoutParameters = tvallParams;
						if (searchGradeList.Count == gradeList.Count)
						{
							tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
							tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						}
						else
						{
							tvAll.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);
							tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
						}

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
							tvGrade.SetPadding(0, AppUtils.dip2px(CurrActivity, 5), 0, AppUtils.dip2px(CurrActivity, 5));
							if (searchGradeList.Contains(itemGrade))
							{
								tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg_on);
								tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
							}
							else
							{
								tvGrade.Background = AppUtils.GetDrawable(CurrActivity,Resource.Drawable.textview_bg);
								tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
							}


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
							popWin2.Dismiss();
							var selectedgrade = "全部年级";
							if (searchGradeList.Count != gradeList.Count)
							{
								selectedgrade = string.Join(",", searchGradeList.ToArray());
							}

							tv_btn2.Text = selectedgrade;

							GetRenewInfoInTeacherByGroupCode();
						};
						#endregion

						popWin2 = new PopupWindow(popViwe2, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
						popWin2.Touchable = true;
						popWin2.Focusable = true;
						popWin2.OutsideTouchable = true; ;
						popWin2.SetBackgroundDrawable(new BitmapDrawable());

						popWin2.DismissEvent += (sender, e) =>
						{
							tv_btn2.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
							arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
							tv_btn2.SetCompoundDrawables(null, null, arrowDown, null);
						};

					}
                    if (!popWin2.IsShowing)
                    {
                        tv_btn2.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn2.SetCompoundDrawables(null, null, arrowDownOn, null);

                        //var aaa = (int)(Build.VERSION.SdkInt);

						if ((int)(Build.VERSION.SdkInt) >=  24)
                        {
							int[] a = new int[2];
                            v.GetLocationInWindow(a);
                            popWin2.ShowAtLocation(this.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height+AppUtils.dip2px(CurrActivity, 11));
                        }
                        else
                        {
							popWin2.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
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

                            //Toast.MakeText(CurrActivity, searchDistrict, ToastLength.Short).Show();
                            GetRenewInfoInTeacherByGroupCode();
                        };
                    }
                    if (!popWin3.IsShowing)
                    {
                        tv_btn3.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn3.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popWin3.ShowAtLocation(this.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popWin3.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
						}
                    }

                }
            }
        }
    }
}
