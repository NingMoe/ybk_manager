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
		public async void DoLogin()
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

            LoadingDialogUtil.ShowLoadingDialog(this, "信息验证中...");
            try
            {
                Dictionary<string, string> requstParams = new Dictionary<string, string>();
                requstParams.Add("appId", AppConfig.APP_ID);
                requstParams.Add("method", "GetManagementLoginUser");
                requstParams.Add("encodeUser", EncryptUtil.Encode(account, AppConfig.EncodeKey));
                requstParams.Add("encodePwd", EncryptUtil.Encode(passwrod, AppConfig.EncodeKey));
                //requstParams.Add("encodeUser", "wb5dHl6OxCEjjJRhtXUsn1%2FkRAJb2rmALjX9fa%2F%2BEcU%3D");
                //requstParams.Add("encodePwd", "XCmS4wVZwUlCjfFdruS0Sg%3D%3D");
                requstParams.Add("sign", AppUtils.GetSign(requstParams));
                var result = await HttpRequestUtil.SendPostRequestBasedOnHttpClient(AppConfig.API_USER_INDEX, requstParams);

                //            var data = (JsonObject)result;
                //var state = int.Parse(data["State"].ToString());
                //if (state == 1)
                //{
                //	LoadingDialogUtil.UpdateLoadingDialogText("登录成功");

                //	UserInfoEntity CurrUserInfo = new UserInfoEntity();
                //	CurrUserInfo.LoginAccount = account;
                //	CurrUserInfo.LoginPassword = passwrod;
                //	CurrUserInfo.UserId = data["Data"]["UserId"].ToString().Replace("\"", "");
                //	CurrUserInfo.Name = data["Data"]["Name"].ToString().Replace("\"", "");
                //                CurrUserInfo.SchoolId = int.Parse(data["Data"]["SchoolId"].ToString().Replace("\"", ""));
                //	CurrUserInfo.SchoolName = data["Data"]["SchoolName"].ToString().Replace("\"", "");
                //	CurrUserInfo.Grade = int.Parse(data["Data"]["Grade"].ToString().Replace("\"", ""));

                //	string userinfoStr = JsonSerializer.ToJsonString<UserInfoEntity>(CurrUserInfo);
                //	SharedPreferencesUtil.SetParam(this, AppConfig.SP_USERINFO, JsonSerializer.ToJsonString<UserInfoEntity>(CurrUserInfo));

                //	Intent intent = new Intent(this, typeof(Main));
                //	StartActivity(intent);
                //	this.Finish();
                //	OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
                //}
                //else{
                //    LoadingDialogUtil.DismissLoadingDialog();
                //    ToastUtil.showErrorToast(this,"账号或密码错误");
                //}


                UserInfoEntity CurrUserInfo = new UserInfoEntity();
                CurrUserInfo.LoginAccount = account;
                CurrUserInfo.LoginPassword = passwrod;
                CurrUserInfo.UserId = "xdf003579687";
                CurrUserInfo.Name = "教学经理高中";
                CurrUserInfo.SchoolId = 1;
                CurrUserInfo.SchoolName = "北京新东方";
                CurrUserInfo.Grade = 2;

                string userinfoStr = JsonSerializer.ToJsonString<UserInfoEntity>(CurrUserInfo);
                SharedPreferencesUtil.SetParam(this, AppConfig.SP_USERINFO, JsonSerializer.ToJsonString<UserInfoEntity>(CurrUserInfo));

                Intent intent = new Intent(this, typeof(Main));
                StartActivity(intent);
                this.Finish();
                OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                LoadingDialogUtil.DismissLoadingDialog();
                ToastUtil.ShowErrorToast(this, msg);
            }
        }

    }
}
