
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.Activities;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Fragments;

namespace YbkManage
{
	/// <summary>
	/// 区域-累计-教师维度
	/// </summary>
	[Activity(Label = "SumByTeacherActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class SumByTeacherActivity : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		#region UIField
		//财年、区域按钮
		private TextView tv_grade, tv_course,tv_title;
		private PopupWindow popGrade, popCourse;

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;
		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		private SumByTeacherAdapter mAdapter;
		#endregion

		#region Field
		// 教学报表数据
		private List<PaymentSumTeacherEntity> sumTeacherList = new List<PaymentSumTeacherEntity>();
		// 报表的筛选条件
		private List<string> gradeList = new List<string>(),courseList = new List<string>();
		private List<string> searchGradeList = new List<string>();
		private string searchCourse = "全部科目";

		//页面传递的参数
		private int year, quarter, dataType, sortType;
		private string areaCode, areaName;

		#endregion

		#region 指定Layout
		protected override void OnCreate(Bundle savedInstanceState)
		{
			LayoutReourceId = Resource.Layout.activity_sumbyteacher;

			base.OnCreate(savedInstanceState);
		}
		#endregion

		#region 初始化页面控件

		/// <summary>
		/// 初始化页面控件
		/// </summary>
		protected override void InitViews()
		{
			tv_grade = FindViewById<TextView>(Resource.Id.tv_grade);
			tv_course = FindViewById<TextView>(Resource.Id.tv_course);
			tv_title = FindViewById<TextView>(Resource.Id.tv_title);

			tv_title.Text = areaName;

			mRecyclerView = (RecyclerView)FindViewById(Resource.Id.recycler_view);
			//adapter展示列表数据
			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mAdapter = new SumByTeacherAdapter(CurrContext, sumTeacherList);
			mRecyclerView.SetLayoutManager(linearLayoutManager);
			mRecyclerView.SetAdapter(mAdapter);
			mAdapter.NotifyDataSetChanged();

			RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));

			//下拉刷新
			mSwipeRefreshLayout = (SwipeRefreshLayout)FindViewById(Resource.Id.refresher);
			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));
			mSwipeRefreshLayout.SetOnRefreshListener(this);

		}
		#endregion

		#region 初始化参数
		protected override void InitVariables()
		{
			Bundle bundle = Intent.Extras;
			if (bundle != null)
			{
				year = bundle.GetInt("year");
				quarter = bundle.GetInt("quarter");
				dataType = bundle.GetInt("dataType");
				sortType = 4;
				areaCode = bundle.GetString("areaCode");
				areaName = bundle.GetString("areaName");
				var searchGradeListStr = bundle.GetString("gradeList");
				searchCourse = bundle.GetString("course");
				if (!string.IsNullOrEmpty(searchGradeListStr))
				{
					searchGradeList = searchGradeListStr.Split(',').ToList();
				}


			}


		}
		#endregion

		#region 监听页面点击事件

		protected override void InitEvents()
		{
			// 返回
			FindViewById<ImageButton>(Resource.Id.imgBtn_back).Click += (sender, e) =>
			{
				CurrActivity.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};

			tv_grade.SetOnClickListener(this);
			tv_course.SetOnClickListener(this);

		}
		public void OnClick(View v)
		{
			//全部年级
			if (v.Id == Resource.Id.tv_grade)
			{
				ShowGrade(v);
			}
			//全部科目
			else if (v.Id == Resource.Id.tv_course)
			{
				ShowCourse(v);
			}
		}

		#region 全部年级
		public void ShowGrade(View v)
		{
			if (gradeList != null && gradeList.Any())
			{
				if (popGrade == null)
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
						tvAll.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg_on);
						tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
					}
					else
					{
						tvAll.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg);
						tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
					}

					GridLayout gridlayout_1 = popViwe2.FindViewById<GridLayout>(Resource.Id.gridlayout_1);
					for (var i = 0; i < gradeList.Count; i++)
					{
						TextView tvGrade = new TextView(CurrActivity);
						var itemGrade = gradeList[i];
						GridLayout.LayoutParams gradeParams = new GridLayout.LayoutParams();
						gradeParams.Width = itemWidth;

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
							tvGrade.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg_on);
							tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						}
						else
						{
							tvGrade.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg);
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

						#endregion
					}
					#region tvAll.Click
					tvAll.Click += (sender, e) =>
					{
						if (tvAll.CurrentTextColor == ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh))
						{
							tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
							tvAll.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg);

							for (var i = 0; i < gridlayout_1.ChildCount; i++)
							{
								var tv = (TextView)gridlayout_1.GetChildAt(i);
								tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
								tv.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg);
							}
							searchGradeList = new List<string>();
						}
						else
						{
							tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
							tvAll.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg_on);

							for (var i = 0; i < gridlayout_1.ChildCount; i++)
							{
								var tv = (TextView)gridlayout_1.GetChildAt(i);
								tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
								tv.Background = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.textview_bg_on);
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
						popGrade.ShowAtLocation(Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
					}
					else
					{
						popGrade.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
					}
				}
			}
		}
		#endregion

		#region 全部科目
		public void ShowCourse(View v)
		{
			if (courseList != null && courseList.Any())
			{
				if (popCourse == null)
				{
					View popViwe3 = LayoutInflater.Inflate(Resource.Layout.popup_select1, null);
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
						popCourse.ShowAtLocation(Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
					}
					else
					{
						popCourse.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
					}
				}

			}
		}
		#endregion
		#endregion

		#region 绑定数据
		protected override void LoadData()
		{
			//年级数据
			if (BaseApplication.GetInstance().gradeList == null)
			{
				BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
			}
			if (BaseApplication.GetInstance().gradeList != null && BaseApplication.GetInstance().gradeList.Any())
			{
				gradeList = new List<string>(BaseApplication.GetInstance().gradeList.Select(i => i.GradeName).ToArray());

			}
			//默认年级全选
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
				searchGradeList = new List<string>(gradeList.ToArray());
			}

			//科目数据
			if (BaseApplication.GetInstance().courseList == null)
			{
				BaseApplication.GetInstance().courseList = DistrictService.GetCourseList(CurrUserInfo.SchoolId);
			}
			if (BaseApplication.GetInstance().courseList != null && BaseApplication.GetInstance().courseList.Any())
			{
				courseList = new List<string>(BaseApplication.GetInstance().courseList.Select(t => t.CourseName).ToArray());
			}
			//默认科目
			if (!string.IsNullOrEmpty(searchCourse))
			{
				tv_course.Text = searchCourse;
			}
			else
			{
				searchCourse = BaseApplication.GetInstance().courseList[0].CourseName;
				tv_course.Text = searchCourse;
			}


			BindData();
		}
		public void BindData()
		{
			try
			{
				if (!NetUtil.CheckNetWork(CurrActivity))
				{
					ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
					return;
				}
				LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");

				var schoolId = CurrUserInfo.SchoolId;
				var grade= "";
					if (searchGradeList.Any())
					{
						grade = string.Join(",", searchGradeList.ToArray());
					}

				//加校区查询权限判断--店长登录
				var areaCodes = "";
				if (CurrUserInfo.Type == (int)UserType.ShopManager)
					areaCodes = CurrUserInfo.AreaCodes;

				new Thread(new ThreadStart(() =>
				{
					var list = SumService.GetSumPaymentListByTeacher(schoolId, year, quarter,dataType,sortType,areaCode,grade,searchCourse);
					CurrActivity.RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						if (list != null)
						{
							sumTeacherList = list.List;
							//添加合计行
							sumTeacherList.Add(list.TotalData);

							mAdapter.SetData(sumTeacherList);
							mAdapter.NotifyDataSetChanged();
						}
					});
				})).Start();
			}
			catch (Exception ex)
			{
				//var msg = ex.Message.ToString();
				LoadingDialogUtil.DismissLoadingDialog();
				mSwipeRefreshLayout.Refreshing = false;
			}
		}
		#endregion
		public void OnItemClick(View itemView, int position)
		{

		}

		public void OnItemLongClick(View itemView, int position)
		{

		}

		public void OnRefresh()
		{
			BindData();
		}


	}
}
