using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Toast;
using YbkManage.Activities;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Fragments;

namespace YbkManage 
{
	/// <summary>
	/// 区域-预算
	/// </summary>
	public class BudgeFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		
		#region UIField
		private LayoutInflater layoutInflater;
		//财年、区域按钮
		private TextView tv_year, tv_district;
		public TextView tv_title_payment;
		private PopupWindow popYear, popDistrict;

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
		private List<QuarterEntity> quarterList = new List<QuarterEntity>();
		private List<string> districtList = new List<string>();
		private QuarterEntity searchQuarter = new QuarterEntity { QuarterName = "2018财年Q1", Year = 2018, Quarter = 2 };
		private string searchDistrict = "全部区域";

		//预算／行课
		public int dataType = 1;
		//标题排序  1-营收目标升序；2-营收目标倒序；3-预收款升序；4-预收款倒序；5-完成率升序；6-完成率倒序。
		private int sortType = 6;

		#endregion

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			
			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_budge, container, false);

			InitViews(view);
			LoadData();

			return view;
		}
		#region 初始化页面控件

		/// <summary>
		/// 初始化页面控件
		/// </summary>
		protected void InitViews(View view)
		{
			tv_year = view.FindViewById<TextView>(Resource.Id.tv_year);
			tv_district = view.FindViewById<TextView>(Resource.Id.tv_district);
			tv_title_payment = view.FindViewById<TextView>(Resource.Id.tv_title_payment);

			//添加按钮的事件监控
			tv_year.SetOnClickListener(this);
			tv_district.SetOnClickListener(this);


			mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
			mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

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

		#region 绑定数据
		/// <summary>
		/// 页面数据
		/// </summary>
		protected void LoadData()
		{
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
			//设置默认的财年选项
			if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
			{
				quarterList = BaseApplication.GetInstance().quarterList;
				searchQuarter = BaseApplication.GetInstance().quarterList.Find(t => t.IsCurrent);
				tv_year.Text = searchQuarter.QuarterName;
			}

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


			BindData();
		}

		/// <summary>
		/// 获取报表数据
		/// </summary>
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
				var year = searchQuarter.Year;
				var quarter = searchQuarter.Quarter;
				var district = searchDistrict;

				//加校区查询权限判断--店长登录
				var areaCodes = "";
				if (CurrUserInfo.Type == (int)UserType.ShopManager)
					areaCodes = CurrUserInfo.AreaCodes;

				new Thread(new ThreadStart(() =>
				{
					var list = BudgetService.GetAreaPaymentList(schoolId, year, quarter, district, sortType, dataType, areaCodes);
					CurrActivity.RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						if (list != null)
						{
							paymentList = list;
							mAdapter.SetData(paymentList);
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

		#region 页面点击事件处理
		/// <summary>
		/// 页面点击事件处理
		/// </summary>
		/// <param name="v">V.</param>
		public void OnClick(View v)
		{
			//财年
			if (v.Id == Resource.Id.tv_year)
			{
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
							var arrowDown = CurrActivity.GetDrawable(Resource.Drawable.arrow_down);
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
						var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
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
			}
			//区域
			else if (v.Id == Resource.Id.tv_district)
			{
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
							var arrowDown = CurrActivity.GetDrawable(Resource.Drawable.arrow_down);
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
						var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
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
			}
		}
		#endregion

		#region 行点击事件
		public void OnItemClick(View itemView, int position)
		{
			var data = paymentList[position];
			Intent intent = new Intent(CurrActivity, typeof(SumByTeacherActivity));
			intent.PutExtra("areaCode",data.AreaCode);
			intent.PutExtra("areaName",data.AreaName);
			intent.PutExtra("course", "语文");
			intent.PutExtra("gradeList","初一,初二");
			StartActivity(intent);
			CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
		}

		public void OnItemLongClick(View itemView, int position){

		}
		#endregion

		#region 页面刷新
		public void OnRefresh()
		{
			BindData();
		}
		#endregion

	}
}
