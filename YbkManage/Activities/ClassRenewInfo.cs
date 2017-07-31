using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using YbkManage.App;
using Square.Picasso;
using DataEntity;
using xxxxxLibrary.Toast;
using xxxxxLibrary.LoadingDialog;
using System.Threading;
using DataService;
using Android.Content.PM;

namespace YbkManage.Activities
{
    /// <summary>
    /// 班级续班详情
    /// </summary>
    [Activity(Label = "ClassRenewInfo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ClassRenewInfo : AppActivity
    {
        private TextView tv_title, tv_rate, tv_className, tv_area, tv_classCode, tv_teacher, tv_renewNum, tv_noRenewNum;
        private ScrollView scrolllview_1, scrolllview_2;
        private GridLayout gridlayout_1, gridlayout_2;

        private Statistics_ClassRenewSummary currRenewInfo;

		// 已续班学生集合
		private List<StudentRenewModel> renewStudentList = new List<StudentRenewModel>();
		// 未续班学生集合
		private List<StudentRenewModel> notRenewStudentList = new List<StudentRenewModel>();

        private Picasso picasso;

        // 显示头像的列数
        private int avatarColumns = 4;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_class_renew_info;

            base.OnCreate(savedInstanceState);
        }

        /// <summary>
        /// 页面参数值
        /// </summary>
		protected override void InitVariables()
        {
			Bundle bundle = Intent.Extras;
            if (bundle != null)
            {
                var renewJsonStr = bundle.GetString("renewJsonStr");
                currRenewInfo = JsonSerializer.ToObject<Statistics_ClassRenewSummary>(renewJsonStr);
            }

            picasso  = Picasso.With(CurrContext);
        }

        /// <summary>
        /// 页面控件
        /// </summary>
        protected override void InitViews()
        {
            tv_title = FindViewById<TextView>(Resource.Id.tv_title);
            tv_rate = FindViewById<TextView>(Resource.Id.tv_rate);
            tv_className = FindViewById<TextView>(Resource.Id.tv_className);
            tv_area = FindViewById<TextView>(Resource.Id.tv_area);
            tv_classCode = FindViewById<TextView>(Resource.Id.tv_classCode);
            tv_teacher = FindViewById<TextView>(Resource.Id.tv_teacher);
            tv_renewNum = FindViewById<TextView>(Resource.Id.tv_renewNum);
            tv_noRenewNum = FindViewById<TextView>(Resource.Id.tv_noRenewNum);
            scrolllview_1 = FindViewById<ScrollView>(Resource.Id.scrolllview_1);
            scrolllview_2 = FindViewById<ScrollView>(Resource.Id.scrolllview_2);
            gridlayout_1 = FindViewById<GridLayout>(Resource.Id.gridlayout_1);
            gridlayout_2 = FindViewById<GridLayout>(Resource.Id.gridlayout_2);

            gridlayout_1.ColumnCount = avatarColumns;
			gridlayout_2.ColumnCount = avatarColumns;

            tv_title.Text = currRenewInfo.ClassCode;
            tv_rate.Text = Math.Round((currRenewInfo.RenewRate * 100),1) + "%";
            tv_classCode.Text = currRenewInfo.ClassCode;
            tv_className.Text = currRenewInfo.ClassName;
            tv_area.Text = currRenewInfo.AreaName;
            tv_teacher.Text = currRenewInfo.TeacherName;
        }

        /// <summary>
        /// 页面事件
        /// </summary>
		protected override void InitEvents()
        {
            // 返回
            FindViewById<ImageButton>(Resource.Id.imgBtn_back).Click += (sender, e) =>
            {
                CurrActivity.Finish();
                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
            };
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
			LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
            GetStudentRenewInfoListByClassCode();
        }


        /// <summary>
        /// 获取报表数据
        /// </summary>
        private void GetStudentRenewInfoListByClassCode()
        {
			try
			{
				new Thread(new ThreadStart(() =>
				{
                    var result = RenewService.GetStudentRenewInfoListByClassCode(CurrUserInfo.SchoolId,currRenewInfo.ClassCode );
					RunOnUiThread(() =>
					{
						LoadingDialogUtil.DismissLoadingDialog();

                        if (result != null)
                        {
                            renewStudentList = result.RenewStudents;
                            notRenewStudentList = result.NotRenewStudents;

                            LoadStudents();
                        }
					});
				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				LoadingDialogUtil.DismissLoadingDialog();
			}
        }

        private void LoadStudents()
        {
            var screenWidth = Resources.DisplayMetrics.WidthPixels;
			var wrapperWidth = screenWidth - AppUtils.dip2px(CurrContext, 24); 
            var itemWidth = (int)Math.Round((wrapperWidth / avatarColumns) * 0.8);
			var marginRight = (wrapperWidth - itemWidth * avatarColumns) / 3;

            // 设置scrollview高度（2行高度）
            var scrollviewHeight = (itemWidth + AppUtils.dip2px(CurrContext, 30)) * 2;
            LinearLayout.LayoutParams scrollParas = new LinearLayout.LayoutParams(wrapperWidth,scrollviewHeight);
			scrolllview_1.LayoutParameters = scrollParas;
			scrolllview_2.LayoutParameters = scrollParas;

            tv_renewNum.Text = string.Format("已续班（{0}人）", renewStudentList.Count);
            for (var i = 0; i < renewStudentList.Count ;i++)
            {
                var student = renewStudentList[i];

                var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_renew_avatar, gridlayout_1, false);

				ImageView ivAvatar = itemBox.FindViewById<ImageView>(Resource.Id.iv_avatar);
                // 设置照片宽度和高度
                var avatarWidth = itemWidth;
                var avatarHeight = avatarWidth;
                LinearLayout.LayoutParams parasAvatar = new LinearLayout.LayoutParams(avatarWidth, avatarHeight);
                parasAvatar.Gravity = GravityFlags.Center;
                ivAvatar.LayoutParameters = parasAvatar;
                if(!string.IsNullOrEmpty(student.avatar))
				{
                    picasso.Load(student.avatar).Placeholder(Resource.Drawable.avatar_student).Error(Resource.Drawable.avatar_student)
						.Transform(new CircleImageTransformation(picasso))
						   .Into(ivAvatar);     
                }
                
                TextView tvName = itemBox.FindViewById<TextView>(Resource.Id.tv_name);
                tvName.Text = student.name;

                GridLayout.LayoutParams parasBox = new GridLayout.LayoutParams();
				parasBox.Width = itemWidth;
				if (i % avatarColumns != 3)
				{
					parasBox.RightMargin = marginRight;      
                }
				itemBox.LayoutParameters = parasBox;
                gridlayout_1.AddView(itemBox);

                // 添加点击事件
                itemBox.Click += (sender, e) => 
                {
					Intent intent = new Intent(CurrActivity, typeof(StuentClassActivity));
					intent.PutExtra("studentJsonStr", JsonSerializer.ToJsonString(student));
					StartActivity(intent);
					CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
                };
            }

            tv_noRenewNum.Text = string.Format("未续班（{0}人）", notRenewStudentList.Count);
            for (var i = 0; i < notRenewStudentList.Count; i++)
			{
                var student = notRenewStudentList[i];

				var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_renew_avatar, gridlayout_2, false);

				ImageView ivAvatar = itemBox.FindViewById<ImageView>(Resource.Id.iv_avatar);
				// 设置照片宽度和高度
				var avatarWidth = itemWidth;
				var avatarHeight = avatarWidth;
				LinearLayout.LayoutParams parasAvatar = new LinearLayout.LayoutParams(avatarWidth, avatarHeight);
				parasAvatar.Gravity = GravityFlags.Center;
				ivAvatar.LayoutParameters = parasAvatar;
				if (!string.IsNullOrEmpty(student.avatar))
				{
					picasso.Load(student.avatar).Placeholder(Resource.Drawable.avatar_student).Error(Resource.Drawable.avatar_student)
						.Transform(new CircleImageTransformation(picasso))
						   .Into(ivAvatar);
				}

				TextView tvName = itemBox.FindViewById<TextView>(Resource.Id.tv_name);
				tvName.Text = student.name;

				GridLayout.LayoutParams parasBox = new GridLayout.LayoutParams();
				parasBox.Width = itemWidth;
				if (i % avatarColumns != 3)
				{
					parasBox.RightMargin = marginRight;
				}
				itemBox.LayoutParameters = parasBox;
				gridlayout_2.AddView(itemBox);

				// 添加点击事件
				itemBox.Click += (sender, e) =>
				{
					Intent intent = new Intent(CurrActivity, typeof(StuentClassActivity));
					intent.PutExtra("studentJsonStr", JsonSerializer.ToJsonString(student));
					StartActivity(intent);
					CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
				};
			}
        }
    }
}
