using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using xxxxxLibrary.LoadingDialog;
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
        // 头像
        private ImageView ivAvatar;

        // 姓名、学校
        private TextView tvName, tvSchool;

        // 教师管理、退出
        private RelativeLayout rlTeacherManage, rlLogout;

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
            rlLogout = (RelativeLayout)view.FindViewById(Resource.Id.rl_logout);

			// 头像
			Picasso picasso = Picasso.With(CurrActivity);
			picasso.Load(CurrUserInfo.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
					.Transform(new CircleImageTransformation(picasso))
					   .Into(ivAvatar);

			tvName.Text = CurrUserInfo.Name;
			tvSchool.Text = CurrUserInfo.SchoolName;
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
        /// 页面数据
        /// </summary>
		protected void LoadData()
        {
           
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void DoLogout()
        {
            SharedPreferencesUtil.SetParam(CurrActivity, AppConfig.SP_USERINFO, "");

            Intent intent = new Intent(CurrActivity, typeof(Login));
            StartActivity(intent);
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
