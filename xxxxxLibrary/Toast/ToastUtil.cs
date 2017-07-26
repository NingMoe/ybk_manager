using System;
using Android.Content;
using Android.OS;

namespace xxxxxLibrary.Toast
{
    public class ToastUtil
    {
    	private static MyToast mToast; // 自定义Toast

		/// <summary>
		/// Toast提示(成功提示)
		/// </summary>
		/// <param name="mContext">M context.</param>
		/// <param name="words">Words.</param>
		public static void ShowSuccessToast(Context mContext,String words)
		{
            ShowMyToast(mContext, Resource.Drawable.icn_toast_success, words);
		}

		/// <summary>
		/// Toast提示(笑脸提示)
		/// </summary>
		/// <param name="mContext">M context.</param>
		/// <param name="words">Words.</param>
		public static void ShowSmileToast(Context mContext, String words)
		{
			ShowMyToast(mContext, Resource.Drawable.icn_toast_smile, words);
		}

		/// <summary>
		/// Toast提示(警告提示)
		/// </summary>
		/// <param name="mContext">M context.</param>
		/// <param name="words">Words.</param>
		public static void ShowWarningToast(Context mContext, String words)
		{
			ShowMyToast(mContext, Resource.Drawable.icn_toast_warning, words);
		}

		/// <summary>
		/// Toast提示(错误提示)
		/// </summary>
		/// <param name="mContext">M context.</param>
		/// <param name="words">Words.</param>
		public static void ShowErrorToast(Context mContext, String words)
		{
			ShowMyToast(mContext, Resource.Drawable.icn_toast_error, words);
		}

		/// <summary>
		/// 显示自定义Toast提示
		/// </summary>
		/// <param name="mContext">M context.</param>
		/// <param name="iconResId">Icon res identifier.</param>
		/// <param name="words">Words.</param>
		private static void ShowMyToast(Context mContext, int iconResId, String words)
		{
			if (mToast != null)
			{
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.IceCreamSandwich)
				{
					mToast.Cancel();
				}
			}
			else
			{
                mToast = MyToast.Create(mContext,iconResId,words,Android.Widget.ToastLength.Long);
			}
			mToast.Show();
		}
    }
}
