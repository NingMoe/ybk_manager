using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

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


		/// <summary>
		/// 公共弹窗.
		/// </summary>
		/// <param name="context">Context.传入当前调用该方法的activity实例</param>
		/// <param name="title">Title.</param>
		/// <param name="content">Content.</param>
		/// <param name="type">Type. 显示类型1：仅为确定，2：有“确定”、“取消”两个操作</param>
		/// <param name="handler">Handler. 传入的需要回调的handler信息，可作为回调方法是用，msg.what = 1时为操作完成状态符</param>
		public static void ShowDialog(Context context, String title, String content, int type, Handler handler)
        {
            Dialog dialog = new Dialog(context, Resource.Style.CustomDialog);
            dialog.SetCancelable(true);
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
                    if (handler != null)
                    {
                        Message msg = handler.ObtainMessage();
                        msg.What = 1;
                        handler.SendMessage(msg);
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
                    if (handler != null)
                    {
                        Message msg = handler.ObtainMessage();
                        msg.What = 0;
                        handler.SendMessage(msg);
                    }
                    dialog.Dismiss();
                };
            }
            dialog.AddContentView(view, new ViewGroup.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.MatchParent));
            dialog.SetCancelable(true);// 点击返回键关闭
            dialog.SetCanceledOnTouchOutside(true);// 点击外部关闭
            dialog.Show();
        }
    }
}
