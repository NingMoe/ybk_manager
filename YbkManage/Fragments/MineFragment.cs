﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using YbkManage.Activities;
using YbkManage.App;

namespace YbkManage.Fragments
{
    /// <summary>
    /// 我的页面
    /// </summary>
    public class MineFragment : BaseFragment
    {
        // 头像
        private ImageView ivAvatar;

        // 姓名、学校
        private TextView tvName, tvSchool;

        // 教师管理、退出
        private RelativeLayout rlTeacherManage,rlLogout;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			View view = inflater.Inflate(Resource.Layout.fragment_mine, container, false);
            InitViews(view);

			return view;
        }

        /// <summary>
        /// 页面控件
        /// </summary>
		protected void InitViews(View view)
		{
			ivAvatar = (ImageView)view.FindViewById(Resource.Id.iv_avatar);
			tvName = (TextView)view.FindViewById(Resource.Id.tv_name);
			tvSchool = (TextView)view.FindViewById(Resource.Id.tv_school);

			rlTeacherManage = (RelativeLayout)view.FindViewById(Resource.Id.rl_teacher);
            rlLogout = (RelativeLayout)view.FindViewById(Resource.Id.rl_logout);
		}

		/// <summary>
		/// 页面事件
		/// </summary>
		protected void InitEvents()
		{
            // 教师管理
			rlTeacherManage.Click += (sender, e) =>
			 {
				 Intent intent = new Intent(CurrActivity, typeof(TeacherManage));
				 StartActivity(intent);
				 CurrActivity.OverridePendingTransition(Android.Resource.Animation.SlideOutRight, Android.Resource.Animation.SlideInLeft);
			 };

            // 退出操作
            rlLogout.Click += (sender, e) => 
            {
                AppUtils.ShowDialog(CurrActivity,"提示", "您确认要退出账号吗？", 2, new MainHandler());

            };
		}

        /// <summary>
        /// 页面数据
        /// </summary>
		protected void LoadData()
		{
			// 头像
			Picasso picasso = Picasso.With(CurrActivity);
			picasso.Load("http://i.imgur.com/DvpvklR.png")
				.Transform(new CircleImageTransformation(picasso))
			   .Into(ivAvatar);

            tvName.SetText(CurrUserInfo.Name,TextView.BufferType.Normal);
            tvSchool.SetText(CurrUserInfo.School,TextView.BufferType.Normal);
		}

		public class MainHandler : Handler
		{
			public override void HandleMessage(Message msg)
			{
				if (msg.What == 1)
				{
					//new Thread(new Runnable() {
     //                   public void run()
					//	{
					//		doLogout();
					//	}
					//}).Start();

                    //doLogout();
			    }
			}
		}
    }
}
