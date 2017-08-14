
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using YbkManage.App;
using YbkManage.Models;

namespace YbkManage.Activities
{
    /// <summary>
    /// 选择角色页面
    /// </summary>
    [Activity(Label = "ActivityRoleList", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TeacherRoleSelectActivity : AppActivity
    {
		private LinearLayout llBox;

		private Intent fromIntent;

        private int roleId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_role_select;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
			fromIntent = this.Intent;
			Bundle bundle = fromIntent.Extras;
			if (bundle != null)
            {
                roleId = bundle.GetInt("roleId",0) ;
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
            List<TeacherRoleEntity> roleList = AppUtils.GetTeacherRoleList();
            foreach (var role in roleList)
            {
				var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_role_select, llBox, false);
                var itemWrapper = itemBox.FindViewById<RelativeLayout>(Resource.Id.ll_wrapper);
				var roleLabel = itemBox.FindViewById<TextView>(Resource.Id.iv_role_label);
                var roleIcon = itemBox.FindViewById<ImageView>(Resource.Id.iv_role_icon);

                roleLabel.Text = role.RoleName;
                roleIcon.SetImageResource(Resource.Drawable.icn_selected);

                if (role.RoleId == roleId)
                {
                    roleIcon.Visibility = ViewStates.Visible;
                    roleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorHigh)));
				}

				llBox.AddView(itemBox);

				itemWrapper.Click += (sender, e) =>
				{

					fromIntent.SetClass(this, typeof(TeacherAddActivity));
                    fromIntent.PutExtra("roleId", role.RoleId.ToString());
					fromIntent.PutExtra("roleName", role.RoleName);
					SetResult(Result.Ok, fromIntent);
					Finish();
					OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
				};
            }
        }
    }
}
