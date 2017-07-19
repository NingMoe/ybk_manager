using System;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace xxxxxLibrary.LoadingDialog
{
    public class MyLoadingDialog : ProgressDialog
    {
        private string message = string.Empty;
        private TextView tvMessage;

        public MyLoadingDialog(Context context, String message):base(context)
        {
            this.message = message;

            init(context);
        }

        private void init(Context context)
        {
            SetCancelable(false);
            SetCanceledOnTouchOutside(false);


			Show();
			View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.loadingdialog_bg, null);
			tvMessage = (TextView)view.FindViewById(Resource.Id.tv_loading_msg);
            tvMessage.SetText(this.message, TextView.BufferType.Normal);

            SetContentView(view);


   //         Window.Attributes.Width = windowma

   //         Android.Views.WindowManagerLayoutParams params = Window.Attributes;

			//WindowManager windowManager = this.getWindowManager();
			//Display display = windowManager.getDefaultDisplay();
			//WindowManager.LayoutParams lParams = alertWindow.getAttributes();
			//lParams.alpha = 0.9f;
			//lParams.width = (int)(display.getWidth() * 0.4);
			//lParams.height = lParams.width;
			//alertWindow.setAttributes(lParams);
		}

        public void SetMsg(string msg)
        {
            if(tvMessage!=null)
			{
				tvMessage.SetText(msg, TextView.BufferType.Normal);
			}
        }
    }
}
