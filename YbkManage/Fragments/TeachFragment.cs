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


namespace YbkManage.Fragments
{
    /// <summary>
    /// 教学报表首页
    /// </summary>
    public class TeachFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
    {
        // 季度、年级、区域 筛选按钮
        private TextView tv_btn1, tv_btn2, tv_btn3;
        private PopupWindow popWin1, popWin2, popWin3;

        private LayoutInflater layoutInflater;

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;

        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        private RenewReportAdapter mAdapter;

        // 教学报表数据
        private List<RenewInfo> teachReportList = new List<RenewInfo>();

		// 报表的筛选条件
		private List<QuarterEntity> quarterList = new List<QuarterEntity>();
		private List<string> districtList = new List<string>(), gradeList = new List<string>();

        private QuarterEntity searchQuarter = new QuarterEntity { QuarterName="2018财年Q1", Year = 2018, Quarter = 2 };
        private List<string> searchGradeList = new List<string>();
        private string searchDistrict = "";


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            layoutInflater = inflater;
            View view = layoutInflater.Inflate(Resource.Layout.fragment_teach, container, false);

            InitViews(view);
            LoadData();

            return view;
        }

        /// <summary>
        /// 页面控件
        /// </summary>
        protected void InitViews(View view)
        {
            tv_btn1 = view.FindViewById<TextView>(Resource.Id.tv_btn1);
            tv_btn2 = view.FindViewById<TextView>(Resource.Id.tv_btn2);
			tv_btn3 = view.FindViewById<TextView>(Resource.Id.tv_btn3);
			tv_btn1.SetOnClickListener(this);
			tv_btn2.SetOnClickListener(this);
			tv_btn3.SetOnClickListener(this);


            mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
            mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));
            //mSwipeRefreshLayout.SetColorScheme(Resource.Color.xam_dark_blue,
            //Resource.Color.xam_purple,
            //Resource.Color.xam_gray,
            //Resource.Color.xam_green);


            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new RenewReportAdapter(CurrActivity, teachReportList);
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();

            mSwipeRefreshLayout.SetOnRefreshListener(this);
            RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
            mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
        }

        /// <summary>
        /// 页面数据
        /// </summary>
        protected void LoadData()
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
				searchQuarter = BaseApplication.GetInstance().quarterList.Find(t => t.IsCurrent);
                tv_btn1.Text = searchQuarter.QuarterName;
			}
			if (BaseApplication.GetInstance().gradeList != null && BaseApplication.GetInstance().gradeList.Any())
			{
				gradeList = new List<string>(BaseApplication.GetInstance().gradeList.Select(i => i.GradeName).ToArray());
				// 默认全选
				searchGradeList = new List<string>(gradeList.ToArray());
			}
			if (BaseApplication.GetInstance().districtList != null && BaseApplication.GetInstance().districtList.Any())
			{
                districtList = new List<string>(BaseApplication.GetInstance().districtList.Select(i => i.DistrictName).ToArray());
			}


			GetRenewInfoInGroup();
        }

        public void OnRefresh()
        {
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
			}
			else
			{
				GetRenewInfoInGroup();
			}
        }

        /// <summary>
        /// 获取报表数据
        /// </summary>
        private void GetRenewInfoInGroup()
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

                    var result = RenewService.GetRenewInfoInGroup(CurrUserInfo.SchoolId, searchQuarter.Year, searchQuarter.Quarter, gradeStr, districtStr, 1, 6, 1, 30);

                    CurrActivity.RunOnUiThread(() =>
					{

						LoadingDialogUtil.DismissLoadingDialog();
						mSwipeRefreshLayout.Refreshing = false;

                        if(result!=null)
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
                //Toast.MakeText(CurrActivity, reportItem.Item3, ToastLength.Short).Show();
                Intent intent = new Intent(CurrActivity, typeof(ReportListByGroup));
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
                intent.PutExtra("avgRenewRate", reportItem.Item6.ToString());
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
                        View popViwe1 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
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

                            //Toast.MakeText(CurrActivity, searchQuarter.QuarterName, ToastLength.Short).Show();
                            GetRenewInfoInGroup();
                        };

                    }
                    if (!popWin1.IsShowing)
                    {
                        tv_btn1.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn1.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
                            popWin1.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
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
							popWin2.Dismiss();
							var selectedgrade = "全部年级";
							if (searchGradeList.Count != gradeList.Count)
							{
								selectedgrade = string.Join(",", searchGradeList.ToArray());
							}

							tv_btn2.Text = selectedgrade;

							GetRenewInfoInGroup();
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


						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popWin2.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
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
                        View popViwe3 = layoutInflater.Inflate(Resource.Layout.popup_select1, null);
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

                            //Toast.MakeText(CurrActivity, searchDistrict, ToastLength.Short).Show();
                            GetRenewInfoInGroup();
                        };
                    }
                    if (!popWin3.IsShowing)
                    {
                        tv_btn3.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
                        var arrowDownOn = CurrActivity.GetDrawable(Resource.Drawable.arrow_down_on);
                        arrowDownOn.SetBounds(0, 0, arrowDownOn.MinimumWidth, arrowDownOn.MinimumHeight);
                        tv_btn3.SetCompoundDrawables(null, null, arrowDownOn, null);

						if ((int)(Build.VERSION.SdkInt) >= 24)
						{
							int[] a = new int[2];
							v.GetLocationInWindow(a);
							popWin3.ShowAtLocation(this.Activity.Window.DecorView, GravityFlags.NoGravity, 0, a[1] + v.Height + AppUtils.dip2px(CurrActivity, 11));
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
