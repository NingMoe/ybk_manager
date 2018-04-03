
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
using YbkManage.Activities;
using YbkManage.App;

namespace YbkManage
{
	[Activity(Label = "AreaSelectMultiActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class AreaSelectMultiActivity : AppActivity
	{
		#region UIFields
		private LinearLayout llBox;
		private Intent fromIntent;
		private TextView tv_save;
		#endregion

		private List<AreaModel> areaList = new List<AreaModel>();
		//记录选中的AreaCodes
		public string AreaCodes { get; set; }



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
				AreaCodes = bundle.GetString("areaCodes", "");
			}
		}

		protected override void InitViews()
		{
			llBox = FindViewById<LinearLayout>(Resource.Id.ll_box);
			var tv_title = FindViewById<TextView>(Resource.Id.tv_title);
			tv_save = FindViewById<TextView>(Resource.Id.tv_save);

			tv_title.Text = "选择门店";
			tv_save.Visibility = ViewStates.Visible;
		}

		protected override void InitEvents()
		{
			// 返回
			((ImageButton)FindViewById(Resource.Id.imgBtn_back)).Click += (sender, e) =>
			{
				this.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};

			//完成
			tv_save.Click += (sender, e) =>
			  {
				  var areaCodes = "";
				  var areaNames = "";
				  //遍历，获取所有选中的校区
				  foreach (var area in areaList)
				  {
					  if (area.IsSelect)
					  {
						  areaCodes += area.sCode + ",";
						  areaNames += area.sName + ",";
					  }
				  }
				  if (string.IsNullOrEmpty(areaCodes))
				  {
					  ToastUtil.ShowWarningToast(CurrActivity, "您没有选中任何信息");
					  return;
				  }


				  fromIntent.SetClass(this, typeof(ShopManagerAddActivity));
				  fromIntent.PutExtra("areaCodes", areaCodes.TrimEnd(','));
				  fromIntent.PutExtra("areaNames", areaNames.TrimEnd(','));
				  SetResult(Android.App.Result.Ok, fromIntent);
				  Finish();
				  OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			  };
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		protected override void LoadData()
		{

			OnRefresh();
		}

		#region BindData
		public void OnRefresh()
		{
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
				return;
			}
			else
			{
				LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
                BindData();
			}
		}

		private void BindData()
		{
			try
			{
				var schoolId = CurrUserInfo.SchoolId;
				var districtCode = CurrUserInfo.DistrictCode;
				new Thread(new ThreadStart(() =>
							{

								areaList = new MeService().GetAreaByDistrict(schoolId, districtCode);

								RunOnUiThread(() =>
								{
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
		#endregion

		private void InitList()
		{
			llBox.RemoveAllViews();
			foreach (var area in areaList)
			{
				var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_role_select, llBox, false);
				var itemWrapper = itemBox.FindViewById<RelativeLayout>(Resource.Id.ll_wrapper);
				var roleLabel = itemBox.FindViewById<TextView>(Resource.Id.iv_role_label);
				var roleIcon = itemBox.FindViewById<ImageView>(Resource.Id.iv_role_icon);

				roleLabel.Text = area.sName;
				roleIcon.SetImageResource(Resource.Drawable.icn_selected);

				//设置选中行样式
				if (AreaCodes.Contains(area.sCode))
				{
					roleIcon.Visibility = ViewStates.Visible;
					roleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
					area.IsSelect = true;
				}
				else
				{
					area.IsSelect = false;
				}

				llBox.AddView(itemBox);

				itemWrapper.Click += (sender, e) =>
				{
					

					//点击后，更改为选中状态
					var currentState = roleIcon.Visibility;
					if (currentState == ViewStates.Visible)
					{
						roleIcon.Visibility = ViewStates.Gone;
						roleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
						area.IsSelect = false;
					}
					else
					{
						roleIcon.Visibility = ViewStates.Visible;
						roleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
						area.IsSelect = true;
					}
				};
			}

		}
	}
}
