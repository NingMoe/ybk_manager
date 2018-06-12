using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using DataEntity;
using Square.Picasso;
using xxxxxLibrary.Utils;
using YbkManage.Activities;
using YbkManage.App;

namespace YbkManage.Fragments
{
    /// <summary>
    /// 我的页面
    /// </summary>
    public class MineFragment : BaseFragment
    {
		#region UIFields
        // 头像
        private ImageView ivAvatar;

        // 姓名、学校
        private TextView tvName, tvSchool;

        private RelativeLayout rlTeacherManage,rlDirector,rlAssistantLeader,rlShopManager, rlLogout;
        private View viewLineTeacher, viewLineDirector, viewLineAssistant,viewLineShopManager;
		#endregion

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_mine, container, false);
            InitViews(view);
            InitEvents();
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
            rlDirector = (RelativeLayout)view.FindViewById(Resource.Id.rl_director);
			rlAssistantLeader = (RelativeLayout)view.FindViewById(Resource.Id.rl_assistant_leader);
			rlShopManager = (RelativeLayout)view.FindViewById(Resource.Id.rl_shopmanager);

			viewLineTeacher = view.FindViewById<View>(Resource.Id.v_teacher_line);
            viewLineDirector = view.FindViewById<View>(Resource.Id.v_director_line);
            viewLineAssistant = view.FindViewById<View>(Resource.Id.v_assistant_line);
			viewLineShopManager = view.FindViewById<View>(Resource.Id.v_shopmanager_line);


            view.FindViewById<TextView>(Resource.Id.tv_version).Text = this.Activity.PackageManager.GetPackageInfo(this.Activity.PackageName, PackageInfoFlags.MatchAll).VersionName;

            rlLogout = (RelativeLayout)view.FindViewById(Resource.Id.rl_logout);

			// 头像
			Picasso picasso = Picasso.With(CurrActivity);
			picasso.Load(CurrUserInfo.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
					.Transform(new CircleImageTransformation(picasso))
					   .Into(ivAvatar);

			tvName.Text = CurrUserInfo.Name;
			tvSchool.Text = CurrUserInfo.SchoolName;

			// 教学经理--教师管理与教学主管管理
			if(CurrUserInfo.Type == (int)UserType.Manager || CurrUserInfo.Type == (int)UserType.TeacherManager)
            {
                rlTeacherManage.Visibility = ViewStates.Visible;
                rlDirector.Visibility = ViewStates.Visible;
                viewLineTeacher.Visibility = ViewStates.Visible;
                viewLineDirector.Visibility = ViewStates.Visible;
			}
			// 教学主管--教师管理
			else if (CurrUserInfo.Type == (int)UserType.TeacherDirector)
			{
                rlTeacherManage.Visibility = ViewStates.Visible;
                viewLineTeacher.Visibility = ViewStates.Visible;
			}
			// 助教主管--助教组长管理
			else if (CurrUserInfo.Type == (int)UserType.AssistantDirector)
			{
                rlAssistantLeader.Visibility = ViewStates.Visible;
                viewLineAssistant.Visibility = ViewStates.Visible;
			}
			// 区域经理--店长管理
			else if (CurrUserInfo.Type == (int)UserType.AreaManager)
			{
				rlShopManager.Visibility = ViewStates.Visible;
				viewLineShopManager.Visibility = ViewStates.Visible;
			}
        }
        

        /// <summary>
        /// 页面事件
        /// </summary>
        protected void InitEvents()
        {
            // 教师管理
            rlTeacherManage.Click += (sender, e) =>
             {
				 if (CurrUserInfo.Type == (int)UserType.TeacherDirector)
				 {
					 Intent intent = new Intent(CurrActivity, typeof(TeacherListActivity));
					 intent.PutExtra("scopeId", CurrUserInfo.ScopeCode);
					 intent.PutExtra("scopeName", CurrUserInfo.ScopeName);

					 var teacherTotalCount = 0;
					var scopeTeacherList = new DataService.MeService().GetTeacherListByScope(CurrUserInfo.SchoolId, int.Parse(CurrUserInfo.ScopeCode), 1, 1, out teacherTotalCount);
					 intent.PutExtra("teacherCount", teacherTotalCount);

					 StartActivity(intent);
					 CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
				 }
				 else
				 {
					 Intent intent = new Intent(CurrActivity, typeof(TeacherManage));
					 StartActivity(intent);
					 CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
				 }
             };
            // 教学主管
            rlDirector.Click += (sender, e) =>
             {
                 Intent intent = new Intent(CurrActivity, typeof(DirectorListActivity));
                 StartActivity(intent);
                 CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
             };
            // 助教组长
            rlAssistantLeader.Click += (sender, e) =>
             {
                 Intent intent = new Intent(CurrActivity, typeof(AssistantLeaderList));
                 StartActivity(intent);
                 CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
             };
			// 店长管理
			rlShopManager.Click += (sender, e) =>
			  {
				Intent intent = new Intent(CurrActivity, typeof(ShopManagerListActivity));
				  StartActivity(intent);
				  CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
			  };
            // 退出操作
            rlLogout.Click += (sender, e) =>
            {
                var callbackFunc = new AppUtils.ShowDialogClick(CallbackFun);
                AppUtils.ShowDialog(CurrActivity, "提示", "您确认要退出账号吗？", 2, callbackFunc);

            };
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void DoLogout()
        {
            SharedPreferencesUtil.SetParam(CurrActivity, AppConfig.SP_USERINFO, "");

            Intent intent = new Intent(CurrActivity, typeof(Login));
            StartActivity(intent);
            CurrActivity.Finish();
            CurrActivity.OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
        }

        private void CallbackFun(int type)
        {
            if (type == 1)
            {
                DoLogout();
            }
        }

    }
}
