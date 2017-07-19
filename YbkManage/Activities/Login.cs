
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
using xxxxxLibrary.Toast;
using xxxxxLibrary.LoadingDialog;
using YbkManage.App;
using System.Net;
using System.IO;
using xxxxxLibrary.Utils;
using xxxxxLibrary.Serializer;
using YbkManage.Models;

namespace YbkManage.Activities
{
    /// <summary>
    /// 登录页面
    /// </summary>
    [Activity(Label = "Login")]
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
                ToastUtil.showWarningToast(this, "请输入您的手机号码或者邮箱");
                etAccount.RequestFocus();
                return;
            }
            var passwrod = etPassword.Text.Trim();
            if (string.IsNullOrEmpty(passwrod))
            {
                ToastUtil.showWarningToast(this, "请输入您的登录密码");
                etPassword.RequestFocus();
                return;
            }


            LoadingDialogUtil.ShowLoadingDialog(this, "信息验证中...");
            //测试用
            string url = "http://www.sina.com.cn/";

            //创建一个请求
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            var httpRes = (HttpWebResponse)await httpReq.GetResponseAsync();
            if (httpRes.StatusCode == HttpStatusCode.OK)
            {
                var text = new StreamReader(httpRes.GetResponseStream()).ReadToEnd();
                LoadingDialogUtil.updateLoadingDialogText("登录成功");

                UserInfoEntity CurrUserInfo = new UserInfoEntity();
                CurrUserInfo.LoginAccount = account;
                CurrUserInfo.LoginPassword = passwrod;
                string userinfoStr = JsonSerializer.ToJsonString<UserInfoEntity>(CurrUserInfo);
                SharedPreferencesUtil.SetParam(this,AppConfig.SP_USERINFO, JsonSerializer.ToJsonString<UserInfoEntity>(CurrUserInfo));

                Intent intent = new Intent(this, typeof(Main));
                StartActivity(intent);
                OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);

            }
        }

    }
}
