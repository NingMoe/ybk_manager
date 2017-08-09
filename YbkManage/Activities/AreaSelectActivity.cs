
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.App;

namespace YbkManage.Activities
{
    /// <summary>
    /// 地域选择
    /// </summary>
    [Activity(Label = "AreaSelectActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class AreaSelectActivity : AppActivity
	{
		private LinearLayout llBox;

        private Intent fromIntent;

		private string sName = "";

        private List<AreaModel> schoolAreaList = new List<AreaModel>();

		protected override void OnCreate(Bundle savedInstanceState)
		{
            LayoutReourceId = Resource.Layout.activity_area_select;

			base.OnCreate(savedInstanceState);
		}

		protected override void InitVariables()
		{
            fromIntent = this.Intent;
			Bundle bundle = fromIntent.Extras;
			if (bundle != null)
			{
                sName = bundle.GetString("sname", "");
			}
		}

		protected override void InitViews()
		{
			llBox = FindViewById<LinearLayout>(Resource.Id.ll_box);
		}

		protected override void InitEvents()
		{
			// 返回
			((ImageButton)FindViewById(Resource.Id.imgBtn_back)).Click += (sender, e) =>
			{
				this.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};
		}

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
			try
			{
				LoadingDialogUtil.ShowLoadingDialog(this, "数据获取中...");

				new Thread(new ThreadStart(() =>
							{
								var schoolId = CurrUserInfo.SchoolId;
					var districtCode = CurrUserInfo.DistrictCode;
					schoolAreaList = new MeService().GetAreaByDistrict(schoolId, districtCode);

								RunOnUiThread(() =>
								{
									LoadingDialogUtil.DismissLoadingDialog();
									InitList();

								});

							})).Start();

			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				ToastUtil.ShowErrorToast(this, "操作失败");
			}
			finally
			{
				LoadingDialogUtil.DismissLoadingDialog();
			}
		}

        private void InitList()
        {
			llBox.RemoveAllViews();
			foreach (var area in schoolAreaList)
			{
				var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_role_select, llBox, false);
				var itemWrapper = itemBox.FindViewById<RelativeLayout>(Resource.Id.ll_wrapper);
				var roleLabel = itemBox.FindViewById<TextView>(Resource.Id.iv_role_label);
				var roleIcon = itemBox.FindViewById<ImageView>(Resource.Id.iv_role_icon);

                roleLabel.Text = area.sName;
				roleIcon.SetImageResource(Resource.Drawable.icn_selected);

                if (area.sName == sName)
				{
					roleIcon.Visibility = ViewStates.Visible;
					roleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
				}

				llBox.AddView(itemBox);

				itemWrapper.Click += (sender, e) =>
				{

					//Intent myIntent = new Intent(this, typeof(AssistantAddActivity));
                    fromIntent.SetClass(this,typeof(AssistantAddActivity));
                    fromIntent.PutExtra("scode", area.sCode);
                    fromIntent.PutExtra("sname", area.sName);
                    SetResult(Android.App.Result.Ok, fromIntent);
					Finish();
					OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
				};
			}
            LoadingDialogUtil.DismissLoadingDialog();
        }
	}
}
