
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
	/// <summary>
	/// 区域-累计-教师维度
	/// </summary>
	public class SumByTeacherFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		#region UIField
		private LayoutInflater layoutInflater;
		//财年、区域按钮
		private TextView tv_grade, tv_course;
		private PopupWindow popGrade, popCourse;

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;
		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		private BudgeAdapter mAdapter;
		#endregion

#region Field
// 教学报表数据
private List<PaymentEntity> paymentList = new List<PaymentEntity>();
// 报表的筛选条件
private List<string> gradeList = new List<string>();
private List<string> searchGradeList = new List<string>();

//预算／行课
public int dataType = 1;
//标题排序  1-营收目标升序；2-营收目标倒序；3-预收款升序；4-预收款倒序；5-完成率升序；6-完成率倒序。
private int sortType = 6;

#endregion

		#region 指定Layout
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_sumbyteacher, container, false);

			InitViews(view);
			LoadBaseData();

			return view;
		}
		#endregion

#region 初始化页面控件

/// <summary>
/// 初始化页面控件
/// </summary>
protected void InitViews(View view)
{
			tv_grade = view.FindViewById<TextView>(Resource.Id.tv_grade);
			tv_course = view.FindViewById<TextView>(Resource.Id.tv_course);

	//添加按钮的事件监控
tv_grade.SetOnClickListener(this);
tv_course.SetOnClickListener(this);


	mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
	mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

	mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#17bfa0"));

	//adapter展示列表数据
	linearLayoutManager = new LinearLayoutManager(CurrActivity);
	mAdapter = new BudgeAdapter(CurrActivity, paymentList);
	mRecyclerView.SetLayoutManager(linearLayoutManager);
	mRecyclerView.SetAdapter(mAdapter);
	mAdapter.NotifyDataSetChanged();

	mSwipeRefreshLayout.SetOnRefreshListener(this);
	RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
	mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
}
		#endregion


		#region 页面点击事件
		public void OnClick(View v)
		{
			if (v.Id == Resource.Id.tv_grade)
			{
				ShowGrade(v);
			}
			else if (v.Id == Resource.Id.tv_course)
			{
				
			}
		}

		#region 全部年级
		public void ShowGrade(View v)
		{
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
						tvGrade.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);
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
								tvGrade.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);
							}
							else
							{
								searchGradeList.Remove(itemGrade);

								tvGrade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
								tvGrade.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg);

								tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
								tvAll.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg);
							}

						//控制全部年级按钮颜色
						if (gradeList.Count == searchGradeList.Count)
							{
								tvAll.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
								tvAll.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg_on);
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
							tvAll.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg);

							for (var i = 0; i < gridlayout_1.ChildCount; i++)
							{
								var tv = (TextView)gridlayout_1.GetChildAt(i);
								tv.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
								tv.Background = CurrActivity.GetDrawable(Resource.Drawable.textview_bg);
							}
							searchGradeList = new List<string>();
						}
						else
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
						var arrowDown = CurrActivity.GetDrawable(Resource.Drawable.arrow_down);
						arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
						tv_grade.SetCompoundDrawables(null, null, arrowDown, null);
					};

				}
				if (!popGrade.IsShowing)
				{
					tv_grade.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
					var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
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
		}
		#endregion

		#endregion

		#region 绑定数据
		public void LoadBaseData()
		{
			//年级数据
			if (BaseApplication.GetInstance().gradeList == null)
			{
				BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
			}
			if (BaseApplication.GetInstance().gradeList != null && BaseApplication.GetInstance().gradeList.Any())
			{
				gradeList = new List<string>(BaseApplication.GetInstance().gradeList.Select(i => i.GradeName).ToArray());
				// 默认全选
				searchGradeList = new List<string>(gradeList.ToArray());
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


				//加校区查询权限判断--店长登录
				var areaCodes = "";
				if (CurrUserInfo.Type == (int)UserType.ShopManager)
					areaCodes = CurrUserInfo.AreaCodes;

				new Thread(new ThreadStart(() =>
				{
					//var list = BudgetService.GetAreaPaymentList(schoolId, year, quarter, district, sortType, dataType, areaCodes);
					CurrActivity.RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						//if (list != null)
						//{
						//	paymentList = list;
						//	mAdapter.SetData(paymentList);
						//	mAdapter.NotifyDataSetChanged();
						//}
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
		public void OnItemClick(View itemView, int position)
		{
			
		}

		public void OnItemLongClick(View itemView, int position)
		{
			
		}

		public void OnRefresh()
		{
			
		}


	}
}
