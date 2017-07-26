using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace xxxxxLibrary.LoadingDialog
{
    public class MyLoadingDialog : ProgressDialog
    {
        private Context mContext;
        private string message = string.Empty;
        private TextView tvMessage;

        public MyLoadingDialog(Context context, String message) : base(context)
        {
            this.mContext = context;
            this.message = message;
        }

        public MyLoadingDialog(Context context, int theme, String message) : base(context, theme)
        {
            this.mContext = context;
            this.message = message;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            init(mContext);
        }

        private void init(Context context)
        {
            SetCancelable(false);
            SetCanceledOnTouchOutside(false);

            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.loadingdialog_bg, null);
            tvMessage = (TextView)view.FindViewById(Resource.Id.tv_loading_msg);
            tvMessage.Text = this.message;

            SetContentView(view);

            // 设置对话框的宽度
            //Window dialogWindow = this.Window;
            //WindowManagerLayoutParams pars = dialogWindow.Attributes;
            //pars.Width = WindowManagerLayoutParams.WrapContent;
            //pars.Height = WindowManagerLayoutParams.WrapContent;
            //this.Window.Attributes = pars;
        }

        public override void Show()
        {
            base.Show();
        }

        public void SetMsg(string msg)
        {
            if (tvMessage != null)
            {
                tvMessage.Text = msg;
            }
        }
    }
}
