using System;
using Android.Content;

namespace xxxxxLibrary.LoadingDialog
{
    public class LoadingDialogUtil
    {

        protected static MyLoadingDialog mLoadingDialog; // 自定义ProgressDialog

        public static void ShowLoadingDialog(Context content, String message)
        {
            if (mLoadingDialog == null)
            {
                mLoadingDialog = new MyLoadingDialog(content, message);
            }

        }

		/// <summary>
		/// 修改ProgeressDialog的文字提示
		/// </summary>
		/// <param name="message">Message.</param>
		public static void updateLoadingDialogText(String message)
        {
            if (mLoadingDialog != null && mLoadingDialog.IsShowing)
            {
                mLoadingDialog.SetMsg(message);
            }
        }

		/// <summary>
		/// 关闭自定义ProgeressDialog提示
		/// </summary>
		public static void dismissLoadingDialog()
        {
            if (mLoadingDialog != null && mLoadingDialog.IsShowing)
            {
                mLoadingDialog.Dismiss();
            }
        }

    }
}
