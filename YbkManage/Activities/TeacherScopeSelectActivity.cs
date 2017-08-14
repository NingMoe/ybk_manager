using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Toast;
using YbkManage.App;
using YbkManage.Models;


namespace YbkManage.Activities
{
    [Activity(Label = "TeacherScopeSelectActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TeacherScopeSelectActivity : AppActivity
    {
        private LinearLayout llBox;

        private Intent fromIntent;

        private int scopeId = 0;

        private List<ScopeModel> scopeList = new List<ScopeModel>();

        private MeService _meService = new MeService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_scope_select;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            fromIntent = this.Intent;
            Bundle bundle = fromIntent.Extras;
            if (bundle != null)
            {
                scopeId = bundle.GetInt("scopeId", 0);
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
				CurrActivity.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
            };

            llBox.RemoveAllViews();
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
                                var grade = CurrUserInfo.Grade;
                                scopeList = _meService.GetScopeByGrade(schoolId, grade??0);

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
            foreach (var scope in scopeList)
			{
				var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_role_select, llBox, false);
				var itemWrapper = itemBox.FindViewById<RelativeLayout>(Resource.Id.ll_wrapper);
				var roleLabel = itemBox.FindViewById<TextView>(Resource.Id.iv_role_label);
				var roleIcon = itemBox.FindViewById<ImageView>(Resource.Id.iv_role_icon);

				roleLabel.Text = scope.Name;
				roleIcon.SetImageResource(Resource.Drawable.icn_selected);

				if (scope.Id == scopeId)
				{
					roleIcon.Visibility = ViewStates.Visible;
					roleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
				}

				llBox.AddView(itemBox);

				itemWrapper.Click += (sender, e) =>
				{

					fromIntent.SetClass(this, typeof(TeacherAddActivity));
					fromIntent.PutExtra("scopeId", scope.Id.ToString());
					fromIntent.PutExtra("scopeName", scope.Name);
                    SetResult(Android.App.Result.Ok, fromIntent);
					Finish();
					OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
				};
			}
		}
    }
}
