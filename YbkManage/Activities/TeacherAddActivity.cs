using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Toast;
using xxxxxLibrary.Utils;
using YbkManage.App;
using System.Linq;

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

        private EditText et_teachercode, et_teacheramount, et_teachername;


        private bool isNewAdd = true;

        private string scopeName = "";
        private TeacherListModel currTeacher = new TeacherListModel();

        private MeService _meService = new MeService();

		// pageFromType=1 教师列表页过来 pageFromType=2教学主管
		private int pageFromType = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_teacher_add;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitVariables()
        {
            Bundle bundle = Intent.Extras;
            if (bundle != null)
            {
                scopeName = bundle.GetString("scopeName");
                pageFromType = bundle.GetInt("pageFromType",1);
                var teacherJsonStr = bundle.GetString("teacherJsonStr");
                if (!string.IsNullOrEmpty(teacherJsonStr))
                {
                    currTeacher = JsonSerializer.ToObject<TeacherListModel>(teacherJsonStr);

                    if (pageFromType==1 && BaseApplication.GetInstance().teacherList != null)
                    {
                        currTeacher = BaseApplication.GetInstance().teacherList.FirstOrDefault(i => i.Code == currTeacher.Code);
                    }
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
            tvScoleLabel = FindViewById<TextView>(Resource.Id.tv_teacherscope);

            // 添加教师情况
            if (currTeacher == null || string.IsNullOrEmpty(currTeacher.Code))
            {
                if (!string.IsNullOrEmpty(scopeName))
                {
                    tvTitle.Text = scopeName;
                }
                btnAdd.Visibility = ViewStates.Visible;
                btnDelete.Visibility = ViewStates.Gone;

                currTeacher = new TeacherListModel();
            }
            else
            {
                isNewAdd = false;

                tvTitle.Text = currTeacher.Name;
                btnAdd.Visibility = ViewStates.Gone;
                btnDelete.Visibility = ViewStates.Visible;

                et_teachercode.Enabled = false;
                et_teachername.Enabled = false;
                et_teacheramount.Enabled = false;
                et_teachercode.Text = currTeacher.Code;
                et_teacheramount.Text = currTeacher.Email;
                et_teachername.Text = currTeacher.Name;
                tvScoleLabel.Text = currTeacher.ScopeName;
                tvRoleLabel.Text = AppUtils.GetRoleName(currTeacher.Type ?? 0);

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
                Intent intent = new Intent(CurrActivity, typeof(TeacherScopeSelectActivity));
                intent.PutExtra("scopeId", currTeacher.ScopeCode ?? 0);
                StartActivityForResult(intent, 1);
                CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);

            };

            // 选择角色
            rlRole.Click += (sender, e) =>
            {
                Intent intent = new Intent(CurrActivity, typeof(TeacherRoleSelectActivity));
                intent.PutExtra("roleId", currTeacher.Type ?? 0);
                StartActivityForResult(intent, 0);
                CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            };

            btnAdd.Click += (sender, e) =>
            {
                DoSave(true);
            };

            tvSave.Click += (sender, e) =>
            {
                DoSave(false);
            };

            btnDelete.Click += (sender, e) =>
            {
                var callbackFunc = new AppUtils.ShowDialogClick(CallbackFun);
                AppUtils.ShowDialog(CurrActivity, "提示", "您确认要删除此信息吗？", 2, callbackFunc);
            };
        }

        private void CallbackFun(int type)
        {
            if (type == 1)
            {
                DoDelete();
            }
        }

        /// <summary>
        /// 保存教师信息
        /// </summary>
        /// <param name="isContinueAdd">isContinueAdd=true：连续保存</param>
        private void DoSave(bool isContinueAdd)
        {
            if (!NetUtil.CheckNetWork(CurrActivity))
            {
                ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
                return;
            }
            try
            {
                currTeacher.Code = et_teachercode.Text.Trim();
                currTeacher.Name = et_teachername.Text.Trim();
                currTeacher.Email = et_teacheramount.Text.Trim();
                if (string.IsNullOrEmpty(currTeacher.Code))
                {
                    ToastUtil.ShowWarningToast(this, "请输入教师编码");
                    et_teachercode.RequestFocus();
                    return;
                }
                if (string.IsNullOrEmpty(currTeacher.Email))
                {
                    ToastUtil.ShowWarningToast(this, "请输入登录账号");
                    et_teacheramount.RequestFocus();
                    return;
                }
                if (!CheckUtil.IsValidEmail(currTeacher.Email))
                {
                    ToastUtil.ShowWarningToast(this, "登录账号应为邮箱");
                    et_teacheramount.RequestFocus();
                    return;
                }
                if (string.IsNullOrEmpty(currTeacher.Name))
                {
                    ToastUtil.ShowWarningToast(this, "请输入姓名");
                    et_teachername.RequestFocus();
                    return;
                }
                if (currTeacher.ScopeCode == null || currTeacher.ScopeCode == 0)
                {
                    ToastUtil.ShowWarningToast(this, "请选择教研组");
                    return;
                }
                if (currTeacher.Type == null || currTeacher.Type == 0)
                {
                    ToastUtil.ShowWarningToast(this, "请选择角色");
                    return;
                }


                LoadingDialogUtil.ShowLoadingDialog(this, "保存中...");

                new Thread(new ThreadStart(() =>
                            {
                                //新增操作
                                var model = new ManagerUserInfo();
                                model.Code = et_teachercode.Text;
                                model.Email = et_teacheramount.Text;
                                model.Name = et_teachername.Text;
                                model.UserType = currTeacher.Type ?? 0;
                                if (model.UserType == (int)UserType.TeacherDirector || model.UserType == (int)UserType.TeacherArea)
                                {
                                    model.IsCanLogin = true;
                                }
                                else
                                {
                                    model.IsCanLogin = false;
                                }
                                model.SchoolId = CurrUserInfo.SchoolId;
                                model.Creator = CurrUserInfo.Name;
                                model.Modifier = CurrUserInfo.Name;

                                DataEntity.Result resultData;

                                if (isNewAdd)
                                {
                                    resultData = _meService.AddManagerUser(model, "", "", currTeacher.ScopeCode ?? 0);

                                }
                                else
                                {
                                    resultData = _meService.UpdateManagerUser(model, "", "", currTeacher.ScopeCode ?? 0);
                                }

                                RunOnUiThread(() =>
                                {
                                    LoadingDialogUtil.DismissLoadingDialog();
                                    if (resultData.State == 1)
                                    {
                                        ToastUtil.ShowSuccessToast(this, "操作成功");

                                        if (BaseApplication.GetInstance().teacherList != null)
                                        {
                                            if (isNewAdd)
                                            {
                                                BaseApplication.GetInstance().teacherList.Add(currTeacher);
                                            }
                                            else
                                            {
                                                if (scopeName != currTeacher.ScopeName)
                                                {
                                                    BaseApplication.GetInstance().teacherList.Remove(currTeacher);
                                                }
                                            }
                                        }


                                        //保存并继续添加爱
                                        if (isContinueAdd)
                                        {
                                            currTeacher = new TeacherListModel();
                                            et_teachercode.Text = "";
                                            et_teachername.Text = "";
                                            et_teacheramount.Text = "";
                                            tvRoleLabel.Text = "未设置";
                                            tvRoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                            tvScoleLabel.Text = "未设置";
                                            tvScoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorSecond)));
                                        }
                                        //完成
                                        else
                                        {
                                            new Handler().PostDelayed(() =>
                                                {

                                                    Finish();
                                                    OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
                                                }, 1000);
                                        }
                                    }
                                    else
                                    {
                                        ToastUtil.ShowErrorToast(this, (string.IsNullOrEmpty(resultData.Error) ? "操作失败" : resultData.Error));
                                    }

                                });

                            })).Start();

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                ToastUtil.ShowErrorToast(this, "操作失败");
                LoadingDialogUtil.DismissLoadingDialog();
            }
        }

        /// <summary>
        /// 删除教师信息
        /// </summary>
        private void DoDelete()
        {
            try
            {
                if (!NetUtil.CheckNetWork(CurrActivity))
                {
                    ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
                    return;
                }


                LoadingDialogUtil.ShowLoadingDialog(this, "删除中...");

                new Thread(new ThreadStart(() =>
                            {
                                var schoolId = CurrUserInfo.SchoolId;
                                var type = 2; //type = 1 助教相关身份 type = 2 教师相关身份

                                var keyword = et_teachercode.Text;
                                var modifier = CurrUserInfo.Name;
                                var rd = _meService.DeleteManagerUser(schoolId, type.ToString(), keyword, modifier);


                                RunOnUiThread(() =>
                                {
                                    LoadingDialogUtil.DismissLoadingDialog();
                                    if (rd.State == 0)
                                    {
                                        ToastUtil.ShowErrorToast(this, (string.IsNullOrEmpty(rd.Error) ? "操作失败" : rd.Error));
                                    }
                                    else
                                    {
                                        if (BaseApplication.GetInstance().teacherList != null)
                                        {
                                            BaseApplication.GetInstance().teacherList.Remove(currTeacher);
                                        }
                                        ToastUtil.ShowSuccessToast(this, "操作成功");
                                        new Handler().PostDelayed(() =>
                                            {

                                                Finish();
                                                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
                                            }, 1000);
                                    }


                                });

                            })).Start();

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                ToastUtil.ShowErrorToast(this, "操作失败");
            }
            finally
            {
                LoadingDialogUtil.DismissLoadingDialog();
            }
        }


        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Android.App.Result.Ok)
            {
                if (requestCode == 0)
                {
                    tvRoleLabel.Text = data.GetStringExtra("roleName");
                    currTeacher.Type = int.Parse(data.GetStringExtra("roleId"));

                    tvRoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                }
                else if (requestCode == 1)
                {
                    tvScoleLabel.Text = data.GetStringExtra("scopeName");
                    currTeacher.ScopeCode = int.Parse(data.GetStringExtra("scopeId"));
                    currTeacher.ScopeName = data.GetStringExtra("scopeName");

                    tvScoleLabel.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.textColorPrimary)));
                }
            }
        }
    }
}
