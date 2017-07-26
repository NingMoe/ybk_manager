using System;
using Android.Content;

namespace xxxxxLibrary.LoadingDialog
{
    public class LoadingDialogUtil
    {
        // 自定义ProgressDialog
        protected static MyLoadingDialog mLoadingDialog;

        public static void ShowLoadingDialog(Context content, String message)
        {
            if (mLoadingDialog == null)
            {
                mLoadingDialog = new MyLoadingDialog(content, Resource.Style.myDialog, message);
                mLoadingDialog.Show();
            }

        }

        /// <summary>
        /// 修改ProgeressDialog的文字提示
        /// </summary>
        /// <param name="message">Message.</param>
        public static void UpdateLoadingDialogText(String message)
        {
            if (mLoadingDialog != null && mLoadingDialog.IsShowing)
            {
                mLoadingDialog.SetMsg(message);
            }
        }

        /// <summary>
        /// 关闭自定义ProgeressDialog提示
        /// </summary>
        public static void DismissLoadingDialog()
        {
            if (mLoadingDialog != null && mLoadingDialog.IsShowing)
            {
                mLoadingDialog.Dismiss();
            }
        }

    }
}
