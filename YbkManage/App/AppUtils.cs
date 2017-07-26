using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using xxxxxLibrary.Utils;

namespace YbkManage.App
{
    public class AppUtils
    {
        /*
         * 隐藏键盘
         */
        public static void HideKeyboard(Activity context)
        {
            InputMethodManager imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
            if (imm != null)
            {
                imm.HideSoftInputFromWindow(context.Window.DecorView.WindowToken, 0);
            }
        }

        public delegate void ShowDialogClick(int type);

        /// <summary>
        /// 公共弹窗.
        /// </summary>
        /// <param name="context">Context.传入当前调用该方法的activity实例</param>
        /// <param name="title">Title.</param>
        /// <param name="content">Content.</param>
        /// <param name="type">Type. 显示类型1：仅为确定，2：有“确定”、“取消”两个操作</param>
        /// <param name="cbFunc">Cb func.</param>
        public static void ShowDialog(Context context, String title, String content, int type, ShowDialogClick cbFunc)
        {
            Dialog dialog = new Dialog(context, Resource.Style.myDialog);
            // 设置像是内容模板
            LayoutInflater inflater = LayoutInflater.From(context);
            View view = inflater.Inflate(Resource.Layout.dialog_global, null);
            Button confirmButton = (Button)view.FindViewById(Resource.Id.setting_account_bind_confirm);// 确认
            Button cancelButton = (Button)view.FindViewById(Resource.Id.setting_account_bind_cancel);// 取消
            TextView dialogTitle = (TextView)view.FindViewById(Resource.Id.global_dialog_title);// 标题
            View line_hori_center = view.FindViewById(Resource.Id.line_hori_center);// 中竖线
            confirmButton.Visibility = ViewStates.Gone;// 默认隐藏取消按钮
            line_hori_center.Visibility = ViewStates.Gone;
            TextView textView = (TextView)view.FindViewById(Resource.Id.setting_account_bind_text);

            // 设置对话框的宽度
            Window dialogWindow = dialog.Window;
            WindowManagerLayoutParams lp = dialogWindow.Attributes;
            lp.Width = (int)(context.Resources.DisplayMetrics.Density * 288);
            dialogWindow.Attributes = lp;

            // 设置显示类型
            if (type != 1 && type != 2)
            {
                type = 1;
            }
            dialogTitle.SetText(title, TextView.BufferType.Normal);// 设置标题
            textView.SetText(content, TextView.BufferType.Normal); // 设置提示内容

            // 确认按钮操作
            if (type == 1 || type == 2)
            {
                confirmButton.Visibility = ViewStates.Visible;
                confirmButton.Click += (sender, e) =>
                {
                    if (cbFunc != null)
                    {
                        cbFunc(1);
                    }
                    dialog.Dismiss();
                };
            }
            // 取消按钮事件
            if (type == 2)
            {
                cancelButton.Visibility = ViewStates.Visible;
                line_hori_center.Visibility = ViewStates.Visible;
                cancelButton.Click += (sender, e) =>
                {
                    if (cbFunc != null)
                    {
                        cbFunc(0);
                    }
                    dialog.Dismiss();
                };
            }
            dialog.AddContentView(view, new ViewGroup.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent));
            dialog.SetCancelable(true);// 点击返回键关闭
            dialog.SetCanceledOnTouchOutside(true);// 点击外部关闭
            dialog.Show();
            dialog.SetCancelable(false);
            dialog.SetCanceledOnTouchOutside(false);
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetSign(Dictionary<string, string> param)
        {
            string paramList = string.Empty;
            foreach (var key in param.Keys)
            {
                if (!String.IsNullOrEmpty(param[key]))
                {
                    paramList += key + "=" + param[key] + "&";
                }
            }
            paramList += "appkey=" + AppConfig.APP_KEY;
            //LogHelper.Info("1.paramList:"+ paramList);
            paramList = paramList.ToLower();
            //LogHelper.Info("2.paramList:" + paramList);
            var sign = EncryptUtil.MD5(paramList).ToUpper(); //签名
                                                   //LogHelper.Info("3.sign:" + sign);
            return sign;
        }

		/// <summary>
		/// 将dip或dp值转换为px值，保证尺寸大小不变
		/// </summary>
		/// <returns>The dip2px.</returns>
		/// <param name="context">Context.</param>
		/// <param name="dipValue">Dip value.</param>
		public static int dip2px(Context context, float dipValue)
        {
            float scale = context.Resources.DisplayMetrics.Density;
            return (int)(dipValue * scale + 0.5f);
        }

		/// <summary>
		/// 将px值转换为sp值，保证文字大小不变
		/// </summary>
		/// <returns>The px2sp.</returns>
		/// <param name="context">Context.</param>
		/// <param name="pxValue">Px value.</param>
		public static int px2sp(Context context, float pxValue)
        {
            float fontScale = context.Resources.DisplayMetrics.ScaledDensity;
            return (int)(pxValue / fontScale + 0.5f);
        }

		/// <summary>
		/// 将sp值转换为px值，保证文字大小不变 
		/// </summary>
		/// <returns>The sp2px.</returns>
		/// <param name="context">Context.</param>
		/// <param name="spValue">Sp value.</param>
		public static int sp2px(Context context, float spValue)
        {
            float fontScale = context.Resources.DisplayMetrics.ScaledDensity;
            return (int)(spValue * fontScale + 0.5f);
        }
    }
}
