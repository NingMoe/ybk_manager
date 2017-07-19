
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
    [Activity(Label = "TeacherManage")]
    public class TeacherManage : AppActivity
    {
        // 返回按钮
        private ImageButton imgbtnBack;

        private LinearLayout llAdd;
        private RelativeLayout rlItem;

        protected override void OnCreate(Bundle savedInstanceState)
		{

			initSystemBar(Resource.Color.actionbar_bg);
			SetContentView(Resource.Layout.activity_teacher_manage);

            base.OnCreate(savedInstanceState);

            // Create your application here
        }

		protected override void InitViews()
		{
            imgbtnBack = (ImageButton)FindViewById(Resource.Id.imgBtn_back);
            llAdd = (LinearLayout)FindViewById(Resource.Id.ll_add);
			rlItem = (RelativeLayout)FindViewById(Resource.Id.rl_item);
		}

        protected override void InitEvents()
        {
            // 返回
            imgbtnBack.Click += (sender, e) => 
            {
                CurrActivity.Finish();
			};

			llAdd.Click += (sender, e) =>
			{
				Intent intent = new Intent(CurrActivity, typeof(TeacherAddActivity));
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideOutRight, Android.Resource.Animation.SlideInLeft);
			};

            rlItem.Click += (sender, e) => 
            {
				Intent intent = new Intent(CurrActivity, typeof(TeacherListActivity));
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideOutRight, Android.Resource.Animation.SlideInLeft);
			};
        }
    }
}
