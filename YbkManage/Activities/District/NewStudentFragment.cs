
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
	/// <summary>
	/// 区域-招新
	/// </summary>
	public class NewStudentFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, View.IOnClickListener
	{
		#region UIField
		private LayoutInflater layoutInflater;
		//财年、区域按钮
		private TextView tv_year, tv_district, tv_cagetory;
		private PopupWindow popYear, popDistrict, popCategory;

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;
		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		public NewStudentAdapter mAdapter;
		//人次和人数切换时标题更改
		public TextView tv_totalcount;
		#endregion

		#region Field

		private decimal avgGrowthRate = 0;
		private List<NewStudentSumAreaEntity> sumList = new List<NewStudentSumAreaEntity>();
		private List<QuarterEntity> quarterList = new List<QuarterEntity>();
		private List<StudentCategoryEntity> categoryEntityList = new List<StudentCategoryEntity>();
		private List<string> districtList = new List<string>(),
							 categoryList = new List<string>();
		private QuarterEntity searchQuarter = new QuarterEntity { QuarterName = "2018财年Q1", Year = 2018, Quarter = 1 };
		private string searchDistrict = "全部区域", searchCagetory = "全部新生";

		//招新：1-人次；2-人数；
		public int dataType = 1;
		//标题排序：1/2-人次升/倒序；3/4-新生升/倒序；5/6-新生占比升/倒序
		int sortType = 6;

		//总计行
		int totalCount = 0;
		bool loadingData = false;
		#endregion

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_newstudent, container, false);

			InitViews(view);
			LoadData();

			InitEvents();

			return view;
		}

		protected void InitEvents()
		{
			mSwipeRefreshLayout.SetOnRefreshListener(this);

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
			tv_totalcount = view.FindViewById<TextView>(Resource.Id.tv_totalcount);

			tv_year = view.FindViewById<TextView>(Resource.Id.tv_year);
			tv_district = view.FindViewById<TextView>(Resource.Id.tv_district);
			tv_cagetory = view.FindViewById<TextView>(Resource.Id.tv_category);

			//添加按钮的事件监控
			tv_year.SetOnClickListener(this);
			tv_district.SetOnClickListener(this);
			tv_cagetory.SetOnClickListener(this);

			mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));
			mSwipeRefreshLayout.SetOnRefreshListener(this);

			//adapter展示列表数据
			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);
			mAdapter = new NewStudentAdapter(CurrActivity, sumList, this.avgGrowthRate, mRecyclerView);
			mRecyclerView.SetLayoutManager(linearLayoutManager);
			mRecyclerView.SetAdapter(mAdapter);
			mAdapter.NotifyDataSetChanged();

			//搜索条件弹框
			popYear = null;
			popCategory = null;
			popDistrict = null;

			//初始化查询类型
			dataType = 1;
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

			//默认新生类型
			if (categoryList != null && categoryList.Any())
			{
				searchCagetory = categoryList[0];
				tv_cagetory.Text = searchCagetory;
			}
			else
			{
				BindCategoryList();
			}

			BindData();
		}
		//切换右上角 人次／人数时，需要调用此方法
		public void BindCategoryList()
		{
			categoryEntityList = NewService.GetStudentCategoryList(CurrUserInfo.SchoolId).Where(p => p.DataType == dataType).ToList();
			categoryList = new List<string>(categoryEntityList.Select(t => t.CategoryName).ToArray());
			searchCagetory = categoryList[0];
			tv_cagetory.Text = searchCagetory;
			//重置查询弹框，否则adapter不会更新
			popCategory = null;
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
				var categoryEntity = categoryEntityList.FirstOrDefault(p => p.CategoryName == searchCagetory);
				var category = categoryEntity==null?"":categoryEntity.CategoryValue;

				//负责校区
				var areaCodes = ""; //店长登录用
				if (CurrUserInfo.Type == (int)UserType.ShopManager)
					areaCodes = CurrUserInfo.AreaCodes;
				//年级
				var gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
				var grade = string.Join(",", gradeList.Select(p => p.GradeName));

				new System.Threading.Thread(new ThreadStart(() =>
				{
					var list = NewService.GetSumNewStudentList(schoolId, year, quarter, dataType, sortType, district, category, grade, out totalCount, 1, 1, 500, areaCodes);
					CurrActivity.RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

						if (list != null)
						{
							var totalData = list.TotalData;
							this.avgGrowthRate = totalData.Rate;
							this.sumList = list.List;
							var totalEntity = new NewStudentSumAreaEntity() { Name = totalData.Name, StudentCount = totalData.StudentCount, Total = totalData.Total, Rate = totalData.Rate };
							this.sumList.Add(totalEntity);


							mAdapter.SetData(this.sumList, this.avgGrowthRate);
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
							var arrowDown = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down);
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
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down_on);
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
							var arrowDown = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down);
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
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down_on);
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
			else if (v.Id == Resource.Id.tv_category)
			{
				#region Category
				if (categoryList != null && categoryList.Any())
				{
					if (popCategory == null)
					{
						View popViwe3 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
						ListView listview3 = popViwe3.FindViewById<ListView>(Resource.Id.lv);
						CourseSelectAdapter adaptera = new CourseSelectAdapter(CurrActivity, categoryList);
						adaptera.SetSelectedValue(searchCagetory);
						listview3.Adapter = adaptera;

						popCategory = new PopupWindow(popViwe3, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
						popCategory.Touchable = true;
						popCategory.Focusable = true;
						popCategory.OutsideTouchable = true; ;
						popCategory.SetBackgroundDrawable(new BitmapDrawable());

						popCategory.DismissEvent += (sender, e) =>
						{
							tv_cagetory.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
							var arrowDown = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down);
							arrowDown.SetBounds(0, 0, arrowDown.MinimumWidth, arrowDown.MinimumHeight);
							tv_cagetory.SetCompoundDrawables(null, null, arrowDown, null);
						};

						listview3.ItemClick += (sender, e) =>
						{
							searchCagetory = this.categoryList[e.Position];
							tv_cagetory.Text = searchCagetory;

							popCategory.Dismiss();

							adaptera.SetSelectedValue(searchCagetory);
							adaptera.NotifyDataSetChanged();

							BindData();
						};
					}
					if (!popCategory.IsShowing)
					{
						tv_cagetory.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						var arrowDownOn = AppUtils.GetDrawable(CurrActivity, Resource.Drawable.arrow_down_on);
						arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
						tv_cagetory.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popCategory.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
						}
						else
						{
							popCategory.ShowAsDropDown(v, 0, AppUtils.dip2px(CurrActivity, 11));
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


				if (totalItemCount == (lastVisibleItemPosition + 1) && LoadMoreEvent != null)
				{
					LoadMoreEvent(this, null);
				}
			}



		}
	}
}
