
using System;
using System.Collections.Generic;
using System.Linq;
using System.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using YbkManage.App;
using YbkManage.Models;
using Square.Picasso;

namespace YbkManage.Activities
{
    /// <summary>
    /// 班级续班详情
    /// </summary>
    [Activity(Label = "ClassRenewInfo")]
    public class renewInfoList : AppActivity
    {
        // 返回按钮
        private ImageButton imgbtnBack;

        private TextView tv_title, tv_rate, tv_className, tv_area, tv_classCode, tv_teacher, tv_renewNum, tv_noRenewNum;
        private ScrollView scrolllview_1, scrolllview_2;
        private GridLayout gridlayout_1, gridlayout_2;

        private RenewInfoEntity currRenewInfo;

        private List<StudentEntity> studentList = new List<StudentEntity>();

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
            var renewJsonStr = Intent.Extras.GetString("renewJsonStr");
            currRenewInfo = JsonSerializer.ToObject<RenewInfoEntity>(renewJsonStr);

            picasso  = Picasso.With(CurrContext);
        }

        /// <summary>
        /// 页面控件
        /// </summary>
        protected override void InitViews()
        {
            imgbtnBack = FindViewById<ImageButton>(Resource.Id.imgBtn_back);

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
            tv_renewNum.Text = string.Format("已续班（{0}人）", currRenewInfo.RenewStudentNum);
            tv_noRenewNum.Text = string.Format("未续班（{0}人）", currRenewInfo.TotalStudentNum - currRenewInfo.RenewStudentNum);
        }

        /// <summary>
        /// 页面事件
        /// </summary>
		protected override void InitEvents()
        {
            // 返回
            imgbtnBack.Click += (sender, e) =>
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
            GetStudentRenewInfoListByClassCode();
        }


        /// <summary>
        /// 获取报表数据
        /// </summary>
        private async void GetStudentRenewInfoListByClassCode()
        {
            try
            {
                //LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("method", "GetStudentRenewInfoListByClassCode");
                requstParams.Add("schoolId", CurrUserInfo.SchoolId.ToString());
                requstParams.Add("classCode", currRenewInfo.ClassCode);
                var sign = AppUtils.GetSign(requstParams);
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_INDEX_REPORT2, requstParams);


                var data = (JsonObject)result;
                var state = int.Parse(data["State"].ToString());
                if (state == 1)
                {
                    studentList.Clear();
                    var jsonArr = JsonValue.Parse(data["Data"].ToString());

                    for (int i = 0; i < jsonArr.Count; i++)
                    {
						StudentEntity item = new StudentEntity();
						item.IsRenew = int.Parse(jsonArr[i]["isRenew"].ToString().Replace("\"", ""));
                        item.IsValid = int.Parse(jsonArr[i]["isValid"].ToString().Replace("\"", ""));
						item.IsBind = int.Parse(jsonArr[i]["isBind"].ToString().Replace("\"", ""));
						item.Name = jsonArr[i]["name"].ToString().Replace("\"", "");
						item.Code = jsonArr[i]["code"].ToString().Replace("\"", "");
						item.SchoolId = jsonArr[i]["schoolId"].ToString().Replace("\"", "");
                        item.UserId = jsonArr[i]["userId"].ToString().Replace("\"", "");
                        studentList.Add(item);
                    }
                    GetStudentListAvatar();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            finally
            {
                //LoadingDialogUtil.DismissLoadingDialog();
            }
        }

		/// <summary>
		/// 获取教师头像
		/// </summary>
		private async void GetStudentListAvatar()
		{
			try
			{
				var codes = string.Empty;
				foreach (var student in studentList)
				{
					if (string.IsNullOrEmpty(student.Avatar))
					{
                        codes += student.UserId + ",";
					}
				}
				Dictionary<string, string> requstParams = new Dictionary<string, string>();
				requstParams.Add("appId", AppConfig.APP_ID);
				requstParams.Add("userType", "1");
				requstParams.Add("rongyunIds", codes);
				var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_TEACHER_INFO, requstParams);

				var data = (JsonObject)result;
				var state = int.Parse(data["status"].ToString().Replace("\"", ""));
				if (state == 1)
				{
					var jsonArr = JsonValue.Parse(data["data"].ToString());

					for (int i = 0; i < jsonArr.Count; i++)
					{
						var studentUserId = jsonArr[i]["rongyunId"].ToString().Replace("\"", "").Split('_')[0];
                        var student = studentList.Where(t => t.UserId == studentUserId).FirstOrDefault();
						if (student != null)
						{
							student.Avatar = jsonArr[i]["portrait"].ToString().Replace("\"", "");
						}
					}
                    LoadStudents();
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
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

			var yetList = studentList.Where(i => i.IsRenew == 1).ToList();
            tv_renewNum.Text = string.Format("已续班（{0}人）", yetList.Count);
            for (var i = 0; i < yetList.Count ;i++)
            {
                var student = yetList[i];

                var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_renew_avatar, gridlayout_1, false);

				ImageView ivAvatar = itemBox.FindViewById<ImageView>(Resource.Id.iv_avatar);
                // 设置照片宽度和高度
                var avatarWidth = itemWidth;
                var avatarHeight = avatarWidth;
                LinearLayout.LayoutParams parasAvatar = new LinearLayout.LayoutParams(avatarWidth, avatarHeight);
                parasAvatar.Gravity = GravityFlags.Center;
                ivAvatar.LayoutParameters = parasAvatar;
				picasso.Load(student.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
					.Transform(new CircleImageTransformation(picasso))
					   .Into(ivAvatar);
                
                TextView tvName = itemBox.FindViewById<TextView>(Resource.Id.tv_name);
                tvName.Text = student.Name;

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

            var noList = studentList.Where(i => i.IsRenew == 0).ToList();
            tv_noRenewNum.Text = string.Format("未续班（{0}人）", noList.Count);
            for (var i = 0; i < noList.Count; i++)
			{
                var student = studentList[i];

				var itemBox = LayoutInflater.From(CurrContext).Inflate(Resource.Layout.item_renew_avatar, gridlayout_2, false);

				ImageView ivAvatar = itemBox.FindViewById<ImageView>(Resource.Id.iv_avatar);
				// 设置照片宽度和高度
				var avatarWidth = itemWidth;
				var avatarHeight = avatarWidth;
				LinearLayout.LayoutParams parasAvatar = new LinearLayout.LayoutParams(avatarWidth, avatarHeight);
				parasAvatar.Gravity = GravityFlags.Center;
				ivAvatar.LayoutParameters = parasAvatar;
				picasso.Load(student.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
					.Transform(new CircleImageTransformation(picasso))
					   .Into(ivAvatar);

				TextView tvName = itemBox.FindViewById<TextView>(Resource.Id.tv_name);
				tvName.Text = student.Name;

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
