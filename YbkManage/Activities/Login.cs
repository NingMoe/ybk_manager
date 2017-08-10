using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using xxxxxLibrary.Toast;
using xxxxxLibrary.LoadingDialog;
using YbkManage.App;
using System.Net;
using System.IO;
using xxxxxLibrary.Utils;
using xxxxxLibrary.Serializer;
using YbkManage.Models;
using Android.Content.PM;
using System.Collections.Generic;
using xxxxxLibrary.Network;
using System.Json;
using DataService;
using System.Threading;

namespace YbkManage.Activities
{
    /// <summary>
    /// 登录页面
    /// </summary>
    [Activity(Label = "Login", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Login : Activity
    {
        // 账户、密码
        private EditText etAccount, etPassword;
        private ImageView ivAccountClear, ivPasswordClear;
        // 登录按钮
        private Button btnLogin;
        // 遇到问题
        private TextView tvProblem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.activity_login);
            base.OnCreate(savedInstanceState);

            InitViews();
            InitEvents();
        }

        /// <summary>
        /// 页面控件
        /// </summary>
		protected void InitViews()
        {
            etAccount = (EditText)FindViewById(Resource.Id.et_account);
            etPassword = (EditText)FindViewById(Resource.Id.et_password);
            ivAccountClear = (ImageView)FindViewById(Resource.Id.iv_account_clear);
            ivPasswordClear = (ImageView)FindViewById(Resource.Id.iv_password_clear);

            btnLogin = (Button)FindViewById(Resource.Id.btn_login);
            tvProblem = (TextView)FindViewById(Resource.Id.tv_problem);

            AppUtils.HideKeyboard(this);

            //etAccount.Text = SharedPreferencesUtil.GetParam(this, AppConfig.SP_LAST_LOGIN_ACCOUNT, "").ToString();
	    
	    var lastLoginAccount = SharedPreferencesUtil.GetParam(this, AppConfig.SP_LAST_LOGIN_ACCOUNT, "");
	    etAccount.Text = lastLoginAccount != null ? (!string.IsNullOrWhiteSpace(lastLoginAccount.ToString()) ? lastLoginAccount.ToString() : "") : "";		
        }

        /// <summary>
        /// 页面事件
        /// </summary>
		protected void InitEvents()
        {
            etAccount.TextChanged += (sender, e) =>
            {
                ValidaData();
            };
            ivAccountClear.Click += (sender, e) =>
            {
                etAccount.SetText("", TextView.BufferType.Editable);
            };

            etPassword.TextChanged += (sender, e) =>
            {
                ValidaData();
            };
            ivPasswordClear.Click += (sender, e) =>
            {
                etPassword.SetText("", TextView.BufferType.Editable);
            };

            // 点击登录
            btnLogin.Click += (sender, e) =>
            {
                DoLogin();
            };

            // 遇到问题
            tvProblem.Click += (sender, e) =>
            {
                Toast.MakeText(this, "跳到问题页面", ToastLength.Short).Show();
            };
        }

        /// <summary>
        /// 验证数据合法性
        /// </summary>
        private void ValidaData()
        {
            var account = etAccount.Text.Trim();
            var password = etPassword.Text.Trim();

            if (!string.IsNullOrEmpty(account))
            {
                ivAccountClear.Visibility = ViewStates.Visible;
            }
            else
            {
                ivAccountClear.Visibility = ViewStates.Gone;
            }

            if (!string.IsNullOrEmpty(password))
            {
                ivPasswordClear.Visibility = ViewStates.Visible;
                btnLogin.Enabled = true;
            }
            else
            {
                ivPasswordClear.Visibility = ViewStates.Gone;
            }

            if (!string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(password))
            {
                btnLogin.Enabled = true;
                btnLogin.SetBackgroundResource(Resource.Drawable.button_bg);
            }
            else
            {
                btnLogin.Enabled = false;
                btnLogin.SetBackgroundResource(Resource.Drawable.button_bg_disabled);
            }
        }

        /// <summary>
        /// 登录操作
        /// </summary>
		public void DoLogin()
        {
            var account = etAccount.Text.Trim();
            if (string.IsNullOrEmpty(account))
            {
                ToastUtil.ShowWarningToast(this, "请输入您的手机号码或者邮箱");
                etAccount.RequestFocus();
                return;
            }
            if (!CheckUtil.IsValidEmail(account) && !CheckUtil.IsValidPhone(account))
            {
                ToastUtil.ShowWarningToast(this, "登录账号错误");
                etAccount.RequestFocus();
                return;
            }

            var passwrod = etPassword.Text.Trim();
            if (string.IsNullOrEmpty(passwrod))
            {
                ToastUtil.ShowWarningToast(this, "请输入您的登录密码");
                etPassword.RequestFocus();
                return;
            }

            if (!NetUtil.CheckNetWork(this))
            {
                ToastUtil.ShowWarningToast(this, "网络未连接！");
                return;
            }


            LoadingDialogUtil.ShowLoadingDialog(this, "登录中...");

            try
            {
                new Thread(new ThreadStart(() =>
                           {
                               var result = DataService.UserService.GetUser(account, passwrod);
                               RunOnUiThread(() =>
                               {

                                   LoadingDialogUtil.DismissLoadingDialog();
                                   if (result.State == 1 && result.Data != null)
                                   {
                                       var loginUserJson = Helper.ToJsonItem(result.Data);
                                       SharedPreferencesUtil.SetParam(this, AppConfig.SP_LAST_LOGIN_ACCOUNT, account);
                                       SharedPreferencesUtil.SetParam(this, AppConfig.SP_USERINFO, loginUserJson);

                                       Intent intent = new Intent(this, typeof(Main));
                                       StartActivity(intent);
                                       OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
                                       this.Finish();
                                   }
                                   else
                                   {
                                       ToastUtil.ShowWarningToast(this, result.Error ?? "登录失败");
                                   }

                               });

                           })).Start();
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                LoadingDialogUtil.DismissLoadingDialog();
                ToastUtil.ShowWarningToast(this, msg);
            }
        }
    }
}
