using System;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace xxxxxLibrary.Toast
{
    /// <summary>
    /// 自定义Toast
    /// </summary>
    public class MyToast : Android.Widget.Toast
    {
        public MyToast(Context context) : base(context)
        {

        }

        public static MyToast Create(Context context, int iconResId,string msg, ToastLength duration)
        {
            View wrap = LayoutInflater.FromContext(context).Inflate(Resource.Layout.toast_bg, null);
            TextView tvMsg = (TextView)wrap.FindViewById(Resource.Id.tv_msg);
            tvMsg.SetText(msg, TextView.BufferType.Normal);
            ImageView ivIcon = (ImageView)wrap.FindViewById(Resource.Id.iv_icon);
            ivIcon.SetImageResource(iconResId);

            MyToast toast = new MyToast(context);
            toast.View = wrap;
            toast.SetGravity(GravityFlags.CenterVertical, 0, 0);
            toast.Duration = duration;
            return toast;
        }
    }
}
