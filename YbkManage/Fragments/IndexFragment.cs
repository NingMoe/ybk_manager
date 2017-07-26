using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;

using YbkManage.App;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Utils;

namespace YbkManage.Fragments
{
    /// <summary>
    /// 管理报表页面
    /// </summary>
    public class IndexFragment : BaseFragment
    {
        // 标题1
        private TextView tvTitle1, tvTitle2;
        // 初中续班率、高中续班率
        private TextView tvRate1, tvRate2;
        // 续班率前三名、后三名容器
        private LinearLayout llAscWrap, llDescWrap;

        // 查询年份和季度
        private int year = 2017, quarter = 1;


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
        }



        private bool LoadedQuarter = false, LoadedRenewInfoByDepartment = false, LoadedRenewInfoInGroup5 = false, LoadedRenewInfoInGroup6 = false;

        /// <summary>
        /// 页面数据
        /// </summary>
        protected void LoadData()
        {
            LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
            GetQuarter();
            GetRenewInfoByDepartment();
            GetRenewInfoInGroup("6");
            GetRenewInfoInGroup("5");
        }

        /// <summary>
        /// 获取续班率，返回如：{{"Data": [{"IsCurrent": true, "Quarter": 1, "QuarterName": "2018财年Q1", "Year": 2018}], "DataCount": 1, "Error": "", "State": 1}}
        /// </summary>
        private async void GetQuarter()
        {
            try
            {
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("method", "GetQuarter");
                requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                //requstParams.Add("sign", "20B5D2D33544F61B0A8BBCFAF447B838");
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT, requstParams);

                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                if (state == 1)
                {
                    SharedPreferencesUtil.SetParam(CurrActivity, AppConfig.SP_QUARTER, data["Data"].ToString());

                    var quarterName = data["Data"][0]["QuarterName"].ToString().Replace("\"", "");
                    tvTitle1.Text = quarterName + "续班率";
                    tvTitle2.Text = quarterName + "续班率排名";
                    year = int.Parse(data["Data"][0]["Year"].ToString().Replace("\"", ""));
                    quarter = int.Parse(data["Data"][0]["Quarter"].ToString().Replace("\"", ""));
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            finally
            {
                LoadedQuarter = true;
                IsDissDialog();
            }
        }

        /// <summary>
        /// 获取续班率，返回如：{"State":1,"Data":[{"SchoolId":1,"Year":2017,"Season":2,"Type":1,"RenewRate":0.3209},{"SchoolId":1,"Year":2017,"Season":2,"Type":2,"RenewRate":0.2541},{"SchoolId":1,"Year":2017,"Season":2,"Type":3,"RenewRate":0.2920}],"Error":null,"DataCount":0}
        /// </summary>
        private async void GetRenewInfoByDepartment()
        {
            try
            {
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
                requstParams.Add("method", "GetRenewInfoByDepartment");
                requstParams.Add("Year", "2017");
                requstParams.Add("Quarter", "2");
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT2, requstParams);

                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                if (state == 1)
                {
                    var jsonArr = JsonValue.Parse(data["Data"].ToString());

                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        if (jsonArr[i]["Type"].ToString() == "1")
                        {
                            tvRate1.Text = Math.Round(double.Parse(jsonArr[i]["RenewRate"].ToString()) * 100, 2).ToString() + "%";
                            continue;
                        }
                        if (jsonArr[i]["Type"].ToString() == "2")
                        {
                            tvRate2.Text = Math.Round(double.Parse(jsonArr[i]["RenewRate"].ToString()) * 100, 2).ToString() + "%";
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            finally
            {
                LoadedRenewInfoByDepartment = true;
                IsDissDialog();
            }
        }

        /// <summary>
        /// 获取续班率，返回如：{"State":1,"Data":{"SchoolId":0,"Year":0,"Season":0,"TotalNum":0,"RenewNum":0,"DataGap":0,"RenewRate":0,"RenewInfo":[{"Item1":1,"Item2":"159","Item3":"优才项目","Item4":1706.0000,"Item5":714.5000,"Item6":0.4188},{"Item1":1,"Item2":"147","Item3":"初中数学","Item4":51127.0000,"Item5":17871.3000,"Item6":0.3495},{"Item1":1,"Item2":"157","Item3":"高中英语","Item4":11122.0000,"Item5":3827.5000,"Item6":0.3441}]},"Error":null,"DataCount":0}
        /// </summary>
        private async void GetRenewInfoInGroup(string sortType)
        {
            try
            {
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
                requstParams.Add("method", "GetRenewInfoInGroup");
                requstParams.Add("Year", "2017");
                requstParams.Add("Quarter", "2");
                requstParams.Add("NeedTotal", "0");
                requstParams.Add("PageIndex", "1");
                requstParams.Add("PageSize", "3");
                requstParams.Add("SortType", sortType);
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT2, requstParams);

                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                if (state == 1)
                {
                    var jsonArr = JsonValue.Parse(data["Data"]["RenewInfo"].ToString());

                    var itemWrap = llAscWrap;
                    if (sortType == "5")
                    {
                        itemWrap = llDescWrap;
                    }
                    itemWrap.RemoveAllViews();
                    itemWrap.Visibility = ViewStates.Visible;

                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        var name = jsonArr[i]["Item3"].ToString().Replace("\"", "");
                        var rate = Math.Round(double.Parse(jsonArr[i]["Item6"].ToString()) * 100, 2).ToString() + "%";
                        var itemView = LayoutInflater.From(CurrActivity).Inflate(Resource.Layout.list_renew_rate, null);
                        itemView.FindViewById<TextView>(Resource.Id.tv_label_l_1).Text = name;
                        itemView.FindViewById<TextView>(Resource.Id.tv_value_l_1).Text = rate;
                        itemWrap.AddView(itemView);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            finally
            {
                if (sortType == "5")
                {
                    LoadedRenewInfoInGroup5 = true;
                }
                else
                {
                    LoadedRenewInfoInGroup6 = true;
                }
                IsDissDialog();
            }
        }

        private void IsDissDialog()
        {
            if (LoadedQuarter && LoadedRenewInfoByDepartment && LoadedRenewInfoInGroup5 && LoadedRenewInfoInGroup6)
            {
                LoadingDialogUtil.DismissLoadingDialog();
            }
        }
    }
}
