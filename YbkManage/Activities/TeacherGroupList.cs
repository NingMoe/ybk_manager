
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
    [Activity(Label = "TeacherGroupList")]
	public class TeacherGroupList : AppActivity
	{
		// 返回按钮
		private ImageButton imgbtnBack;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			initSystemBar(Resource.Color.actionbar_bg);
			SetContentView(Resource.Layout.activity_teacher_group_list);

			base.OnCreate(savedInstanceState);
		}

		protected override void InitViews()
		{
			imgbtnBack = (ImageButton)FindViewById(Resource.Id.imgBtn_back);
		}

		protected override void InitEvents()
		{
			// 返回
			imgbtnBack.Click += (sender, e) =>
			{
				CurrActivity.Finish();
			};
		}
    }
}
