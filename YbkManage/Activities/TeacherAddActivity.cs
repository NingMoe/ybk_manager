
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.Serializer;
using YbkManage.Models;

namespace YbkManage.Activities
{
    /// <summary>
    /// 添加教师页面
    /// </summary>
    [Activity(Label = "TeacherAddActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TeacherAddActivity : AppActivity
    {
        // 返回按钮
        private ImageButton imgbtnBack;

        private RelativeLayout rlGroup, rlRole;

        // 标题
        private TextView tvTitle;
        // 添加、删除按钮
        private TextView tvSave;
        private Button btnAdd, btnDelete;

        private TextView tvRoleLabel, tvScoleLabel;

        private EditText et_teachercode,et_teacheramount, et_teachername;


        private string scopeName = "";
        private TeacherInfoEntity currTeacher = new TeacherInfoEntity();

        private int roleId = 0, scopeId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_add;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            Bundle bundle = Intent.Extras;
            if(bundle != null)
            {
				scopeName = bundle.GetString("scopeName");
				var teacherJsonStr = bundle.GetString("teacherJsonStr");
				if (!string.IsNullOrEmpty(teacherJsonStr))
				{
					currTeacher = JsonSerializer.ToObject<TeacherInfoEntity>(teacherJsonStr);
				}
            }
        }

        protected override void InitViews()
        {
            imgbtnBack = (ImageButton)FindViewById(Resource.Id.imgBtn_back);
            rlGroup = (RelativeLayout)FindViewById(Resource.Id.rl_group);
			rlRole = (RelativeLayout)FindViewById(Resource.Id.rl_role);

			et_teachercode = FindViewById<EditText>(Resource.Id.et_teachercode);
			et_teacheramount = FindViewById<EditText>(Resource.Id.et_teacheramount);
			et_teachername = FindViewById<EditText>(Resource.Id.et_teachername);

            tvTitle = FindViewById<TextView>(Resource.Id.tv_title);
            tvSave = FindViewById<TextView>(Resource.Id.tv_save);
            btnAdd = FindViewById<Button>(Resource.Id.btn_add);
            btnDelete = FindViewById<Button>(Resource.Id.btn_delete);

            tvRoleLabel = FindViewById<TextView>(Resource.Id.tv_teacherrole);
            tvScoleLabel = FindViewById<TextView>(Resource.Id.tv_teachergroup);

            // 添加教师情况
            if (currTeacher == null || string.IsNullOrEmpty(currTeacher.Code))
            {
                if (!string.IsNullOrEmpty(scopeName))
                {
                    tvTitle.Text = scopeName;
                }
                btnAdd.Visibility = ViewStates.Visible;
                btnDelete.Visibility = ViewStates.Gone;
            }
            else
            {
                tvTitle.Text = currTeacher.Name;
                btnAdd.Visibility = ViewStates.Gone;
                btnDelete.Visibility = ViewStates.Visible;

                et_teachercode.Text = currTeacher.Code;
                et_teacheramount.Text = currTeacher.Email;
                et_teachername.Text = currTeacher.Name;
                tvScoleLabel.Text = currTeacher.ScopeName;

				tvRoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
				tvScoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
            }
        }

        protected override void InitEvents()
        {
            // 返回
            imgbtnBack.Click += (sender, e) =>
            {
                CurrActivity.Finish();
                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
            };

            // 选择教研组
            rlGroup.Click += (sender, e) =>
            {
                Intent intent = new Intent(CurrActivity, typeof(TeacherGroupList));
				StartActivityForResult(intent, 1);
                CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);

            };

            // 选择角色
            rlRole.Click += (sender, e) =>
            {
				Intent intent = new Intent(CurrActivity, typeof(TeacherRoleSelectActivity));
                intent.PutExtra("roleId", roleId.ToString());
				StartActivityForResult(intent, 0);
				CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            };

            btnAdd.Click += (sender, e) => 
            {
                DoSave(1);
            };

            tvSave.Click += (sender, e) => 
            {
                DoSave(0);
            };

            btnDelete.Click += (sender, e) => 
            {
                DoDelete();
            };
        }

        /// <summary>
        /// 保存教师信息
        /// </summary>
        /// <param name="saveType">saveType=0: 保存 =1：连续保存</param>
        private async void DoSave(int saveType)
        {
            
        }

        /// <summary>
        /// 删除教师信息
        /// </summary>
        private async void DoDelete()
        {
            
        }


		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok)
			{
                if(requestCode == 0)
                {
                    roleId = data.GetIntExtra("roleId",0);
                    tvRoleLabel.Text = data.GetStringExtra("roleName");

					tvRoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                }
				else if (requestCode == 1)
				{
					scopeId = data.GetIntExtra("scoleId", 0);
					tvScoleLabel.Text = data.GetStringExtra("scopeName");
					tvScoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
				}
			}
		}
    }
}
