
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace YbkManage.Activities
{
    [Activity(Label = "TeacherAddActivity")]
    public class TeacherAddActivity : AppActivity
	{
		// 返回按钮
		private ImageButton imgbtnBack;

        private RelativeLayout rlGroup, rlRole;

        protected override void OnCreate(Bundle savedInstanceState)
        {

			initSystemBar(Resource.Color.actionbar_bg);
			SetContentView(Resource.Layout.activity_teacher_add);

            base.OnCreate(savedInstanceState);

			// Create your application here
		}

		protected override void InitViews()
		{
			imgbtnBack = (ImageButton)FindViewById(Resource.Id.imgBtn_back);
			rlGroup = (RelativeLayout)FindViewById(Resource.Id.rl_group);
			rlRole = (RelativeLayout)FindViewById(Resource.Id.rl_role);
		}

		protected override void InitEvents()
		{
			// 返回
			imgbtnBack.Click += (sender, e) =>
			{
				CurrActivity.Finish();
			};

			rlGroup.Click += (sender, e) =>
			{
				Intent intent = new Intent(CurrActivity, typeof(TeacherGroupList));
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);

			};

			rlRole.Click += (sender, e) =>
			{
				Intent intent = new Intent(CurrActivity, typeof(TeacherRoleList));
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);

			};
		}
    }
}
