﻿using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;

using YbkManage.App;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using Android.Graphics;
using System.Threading;
using DataService;
using DataEntity;
using YbkManage.Views;
using Android.Support.V4.Widget;

namespace YbkManage.Fragments
{
    /// <summary>
    /// 管理报表页面
    /// </summary>
    public class IndexFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener
    {
        #region UIFields
        // 标题1
        private TextView tvTitle1, tvTitle2;
        // 初中续班率、高中续班率
        private TextView tvRate1, tvRate2;
        // 续班率前三名、后三名容器
        private LinearLayout llAscWrap, llDescWrap;

        //区域模块
        private TextView tvBudgetTitle, tvBudget, tvBudgetRate;
        private LinearLayout llBudgetBefore, llBudgetAfter;

        // 财年
        private TextView tv_year;

        private SwipeRefreshLayout mSwipeRefreshLayout;
        #endregion

        // 查询年份和季度
        private int year = DateTime.Now.Year, quarter = 1;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.fragment_index, container, false);

            //llWrapper = (LinearLayout)view.FindViewById(Resource.Id.ll_wrapper);
            //View viewItem = inflater.Inflate(Resource.Layout.index_report_item, container, false);
            //llWrapper.AddView(viewItem);

            //View viewItem2 = inflater.Inflate(Resource.Layout.index_report_item, container, false);
            //llWrapper.AddView(viewItem2);

            InitViews(view);
            LoadData();

            return view;
        }


        /// <summary>
        /// 页面控件
        /// </summary>
        protected void InitViews(View view)
        {
            tvTitle1 = view.FindViewById<TextView>(Resource.Id.tv_title_1);
            tvTitle2 = view.FindViewById<TextView>(Resource.Id.tv_title_2);
            tvRate1 = view.FindViewById<TextView>(Resource.Id.tv_rate_1);
            tvRate2 = view.FindViewById<TextView>(Resource.Id.tv_rate_2);
            llAscWrap = view.FindViewById<LinearLayout>(Resource.Id.ll_asc_wrap);
            llDescWrap = view.FindViewById<LinearLayout>(Resource.Id.ll_desc_wrap);

            //区域模块
            tvBudgetTitle = view.FindViewById<TextView>(Resource.Id.tv_area_title);
            tvBudget = view.FindViewById<TextView>(Resource.Id.tv_area_budget);
            tvBudgetRate = view.FindViewById<TextView>(Resource.Id.tv_area_rate);
            llBudgetBefore = view.FindViewById<LinearLayout>(Resource.Id.ll_area_before);
            llBudgetAfter = view.FindViewById<LinearLayout>(Resource.Id.ll_area_after);

            // 财年
            tv_year = view.FindViewById<TextView>(Resource.Id.tv_year);

            mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));
            mSwipeRefreshLayout.SetOnRefreshListener(this);
        }

        private bool LoadedRenewInfoInGroup5 = false, LoadedRenewInfoInGroup6 = false;
        private bool LoadedBudgetInfoBefore = false, LoadedBudgetInfoAfter = false;

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
            else
            {
                LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");

                if (BaseApplication.GetInstance().quarterList == null || !BaseApplication.GetInstance().quarterList.Any())
                {
                    BaseApplication.GetInstance().quarterList = RenewService.GetQuarter(CurrUserInfo.SchoolId);
                }
                if (BaseApplication.GetInstance().gradeList == null)
                {
                    BaseApplication.GetInstance().gradeList = RenewService.GetGradeList(CurrUserInfo.SchoolId);
                }

                GetRenewData();

                // 财年选择
                #region
                if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
                {
                    tv_year.Visibility = ViewStates.Visible;
                    tv_year.Click += (sender, e) =>
                    {
                        if (financialYearPopWin == null)
                        {
                            financialYearPopWin = new PopWin_IndexFinancialYear(CurrActivity, BaseApplication.GetInstance().quarterList);
                            financialYearPopWin.clickItem += new PopWin_IndexFinancialYear.ClickItem(clickFinancialYear);
                        }

                        financialYearPopWin.OutsideTouchable = true;
                        if (!financialYearPopWin.IsShowing)
                        {
                            financialYearPopWin.ShowAsDropDown(tv_year, 0, -15);
                        }
                    };
                }
                #endregion
            }
        }


        private PopWin_IndexFinancialYear financialYearPopWin;
        public void clickFinancialYear(QuarterEntity quarter)
        {
            if (financialYearPopWin.IsShowing)
            {
                financialYearPopWin.Dismiss();
            }

            if (BaseApplication.GetInstance().quarterList != null && BaseApplication.GetInstance().quarterList.Any())
            {
                var selected = BaseApplication.GetInstance().quarterList.FirstOrDefault(i => i.IsCurrent);
                if (quarter.QuarterName == selected.QuarterName)
                {
                    return;
                }
                foreach (var item in BaseApplication.GetInstance().quarterList)
                {
                    if (quarter.QuarterName == item.QuarterName)
                    {
                        item.IsCurrent = true;
                    }
                    else
                    {
                        item.IsCurrent = false;
                    }
                }
                financialYearPopWin.SetSelectedColor();
                GetRenewData();
            }
        }

        /// <summary>
        /// 获取续班率，返回如：{"State":1,"Data":[{"SchoolId":1,"Year":2017,"Season":2,"Type":1,"RenewRate":0.3209},{"SchoolId":1,"Year":2017,"Season":2,"Type":2,"RenewRate":0.2541},{"SchoolId":1,"Year":2017,"Season":2,"Type":3,"RenewRate":0.2920}],"Error":null,"DataCount":0}
        /// </summary>
        private void GetRenewData()
        {
            var schoolId = CurrUserInfo.SchoolId;
            try
            {
                var currQuarter = BaseApplication.GetInstance().quarterList.FirstOrDefault(p => p.IsCurrent);
                if (currQuarter != null)
                {
                    year = currQuarter.Year;
                    quarter = currQuarter.Quarter;
                }
                tvTitle1.Text = string.Format("{0}财年Q{1}续班率", year, quarter);
                tvTitle2.Text = string.Format("{0}财年Q{1}续班率排名", year, quarter);
                tvBudgetTitle.Text = string.Format("{0}财年{1}预算完成率", year, quarter);
                try
                {

                    #region 教学模块
                    new Thread(new ThreadStart(() =>
                    {

                        var renewList = RenewService.GetIndexRenewInfoByDepartment(schoolId, year, quarter);

                        CurrActivity.RunOnUiThread(() =>
                        {
                            //初中续班率
                            var middleInfo = renewList.FirstOrDefault(p => p.Type == 1);
							if (middleInfo != null)
								tvRate1.Text = (middleInfo.RenewRate * 100).ToString("f1") + "%";
							else
								tvRate1.Text = "0.0%";

                            //高中续班率
                            var hightInfo = renewList.FirstOrDefault(p => p.Type == 2);
							if (hightInfo != null)
								tvRate2.Text = (hightInfo.RenewRate * 100).ToString("f1") + "%";
							else
								tvRate2.Text = "0.0%";
                        });
                    })).Start();


                    var grade = "";
                    if (BaseApplication.GetInstance().gradeList != null)
                    {
                        grade = string.Join(",", BaseApplication.GetInstance().gradeList.Select(t => t.GradeName).ToArray());
                    }
                    // 前三名
                    new Thread(new ThreadStart(() =>
                    {
                        var beforeList = RenewService.GetIndexRenewInfoInGroup(schoolId, year, quarter, grade, "", 0, 6, 1, 3);

                        CurrActivity.RunOnUiThread(() =>
                        {
                            InitRenewViews(beforeList, 6);
                        });
                    })).Start();

                    // 后三名
                    new Thread(new ThreadStart(() =>
                    {
                        var lastList = RenewService.GetIndexRenewInfoInGroup(schoolId, year, quarter, grade, "", 0, 5, 1, 3);

                        CurrActivity.RunOnUiThread(() =>
                        {
                            InitRenewViews(lastList, 5);
                        });
                    })).Start();
                    #endregion

                    #region 区域模块
                    new Thread(new ThreadStart(() =>
                    {
                        //query
                        //6-按预算完成率倒序排
                        int sortType = 6;
                        //1-预收款
                        int dataType = 1;
                        var allBudgetlist = BudgetService.GetAreaPaymentList(schoolId, year, quarter, "", sortType, dataType);
                        //预收款与完成率 取总计行
                        var budget = "0";
                        var completionRate = "0.0%";
                        var totalModel = allBudgetlist.FirstOrDefault(p => p.AreaName == "总计");
                        if (totalModel != null)
                        {
                            budget = (totalModel.Budget / 10000).ToString("f1");
                            completionRate = (totalModel.CompletionRate * 100).ToString("f1") + "%";
                        }


                        PaymentEntity[] budgetBefore = new PaymentEntity[3];
                        PaymentEntity[] budgetAfter = new PaymentEntity[3];
                        if (allBudgetlist.Count > 0)
                        {
                            //移除总计行后，取前三名&后三名	
                            allBudgetlist.RemoveAt(allBudgetlist.Count - 1);
                            //移除营收目标=0的行
                            allBudgetlist.RemoveAll(p => p.Budget == 0);
                        }
                        var budgetCount = allBudgetlist.Count;
                        if (allBudgetlist.Count > 3) budgetCount = 3;
                        if (budgetCount > 0)
                        {
                            allBudgetlist.CopyTo(0, budgetBefore, 0, budgetCount);
                            allBudgetlist.CopyTo(allBudgetlist.Count - budgetCount, budgetAfter, 0, budgetCount);

                            budgetBefore = budgetBefore.Where(t => t != null).ToArray();
                            budgetAfter = budgetAfter.Where(t => t != null).ToArray();
                        }

                        CurrActivity.RunOnUiThread(() =>
                        {

                            //预收款
                            tvBudget.Text = budget;
							//预收款完成率
							tvBudgetRate.Text = ((totalModel != null && totalModel.Budget == 0) ? "--" : completionRate);

                            //前三名
                            InitBudgetViews(budgetBefore.ToList(), true);
                            //后三名
                            InitBudgetViews(budgetAfter.Reverse().ToList(), false);

                            mSwipeRefreshLayout.Refreshing = false;
                        });



                    })).Start();

                    #endregion
                }
                catch (Exception ex)
                {
                    var msg = ex.Message.ToString();
                }


            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
        }

        /// <summary>
        /// Inits the renew views.
        /// </summary>
        /// <param name="renewList">Renew list.</param>
        /// <param name="sortType">Sort type.</param>
        private void InitRenewViews(List<RenewInfo> renewList, int sortType)
        {
            try
            {
                var itemWrap = llAscWrap;
                if (sortType == 5)
                {
                    itemWrap = llDescWrap;
                }
                itemWrap.RemoveAllViews();
                itemWrap.Visibility = ViewStates.Visible;

                foreach (var renew in renewList)
                {
                    var rate = Math.Round(renew.Item6 * 100, 2).ToString("f1") + "%";
                    var itemView = LayoutInflater.From(CurrActivity).Inflate(Resource.Layout.item_index_renewrate, null);
                    itemView.FindViewById<TextView>(Resource.Id.tv_label_l_1).Text = renew.Item3;
                    itemView.FindViewById<TextView>(Resource.Id.tv_value_l_1).Text = rate;

                    if (sortType == 5)
                    {
                        itemView.FindViewById<TextView>(Resource.Id.tv_value_l_1).SetTextColor(Color.ParseColor("#f46d5f"));
                    }
                    itemWrap.AddView(itemView);
                }

                if (sortType == 5)
                {
                    LoadedRenewInfoInGroup5 = true;
                }
                else
                {
                    LoadedRenewInfoInGroup6 = true;
                }
                IsDissDialog();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                IsDissDialog();
            }
        }

        private void InitBudgetViews(List<PaymentEntity> budgetList, bool isBefore)
        {
            try
            {
                var itemWrap = llBudgetBefore;
                if (!isBefore)
                {
                    itemWrap = llBudgetAfter;
                }
                itemWrap.RemoveAllViews();
                itemWrap.Visibility = ViewStates.Visible;

                foreach (var b in budgetList)
                {
                    var rate = Math.Round(b.CompletionRate * 100, 2).ToString("f1") + "%";
                    var itemView = LayoutInflater.From(CurrActivity).Inflate(Resource.Layout.item_index_renewrate, null);
                    itemView.FindViewById<TextView>(Resource.Id.tv_label_l_1).Text = b.AreaName;
                    itemView.FindViewById<TextView>(Resource.Id.tv_value_l_1).Text = rate;

                    if (!isBefore)
                    {
                        itemView.FindViewById<TextView>(Resource.Id.tv_value_l_1).SetTextColor(Color.ParseColor("#f46d5f"));
                    }
                    itemWrap.AddView(itemView);
                }

                if (isBefore)
                {
                    LoadedBudgetInfoBefore = true;
                }
                else
                {
                    LoadedBudgetInfoAfter = true;
                }
                IsDissDialog();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                IsDissDialog();
            }
        }

        private void IsDissDialog()
        {
            if (LoadedRenewInfoInGroup5 && LoadedRenewInfoInGroup6)
            {
                LoadingDialogUtil.DismissLoadingDialog();
            }

            if (LoadedBudgetInfoBefore && LoadedBudgetInfoAfter)
            {
                LoadingDialogUtil.DismissLoadingDialog();
            }
        }

        public void OnRefresh()
        {
            GetRenewData();
        }
    }
}
