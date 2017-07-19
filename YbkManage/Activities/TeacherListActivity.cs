
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
    [Activity(Label = "TeacherListActivity")]
    public class TeacherListActivity : AppActivity
	{
		// 返回按钮
		private ImageButton imgbtnBack;

		private LinearLayout llAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {

			initSystemBar(Resource.Color.actionbar_bg);
			SetContentView(Resource.Layout.activity_teacher_list);

            base.OnCreate(savedInstanceState);

			// Create your application here
		}

		protected override void InitViews()
		{
			imgbtnBack = (ImageButton)FindViewById(Resource.Id.imgBtn_back);
			llAdd = (LinearLayout)FindViewById(Resource.Id.ll_add);
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
                CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
			};
		}
    }
}
