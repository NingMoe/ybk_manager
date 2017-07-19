using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace YbkManage.Views
{
    public class TeachPopupButton : Button, PopupWindow.IOnDismissListener, View.IOnClickListener
    {
        private int normalBg;//正常状态下的背景
        private int pressBg;//按下状态下的背景
        private int normalIcon;//正常状态下的图标
        private int pressIcon;//按下状态下的图标
        private PopupWindow popupWindow;
        private Context context;
        private int screenWidth;
        private int screenHeight;
        private int paddingTop;
        private int paddingLeft;
        private int paddingRight;
        private int paddingBottom;
        private PopupButtonListener listener;
        private View popWin;

        public TeachPopupButton(Context context) : base(context)
        {
            this.context = context;
        }

        public TeachPopupButton(Context context, IAttributeSet attributeSet) : base(context, attributeSet)
        {
            this.context = context;
            initAttrs(context, attributeSet);
            initBtn(context);
        }

        public TeachPopupButton(Context context, IAttributeSet attributeSet, int defStyleAttr) : base(context, attributeSet, defStyleAttr)
        {
            this.context = context;
        }

        /// <summary>
        /// 初始化各种自定义参数
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="attrs">Attrs.</param>
        private void initAttrs(Context context, IAttributeSet attrs)
        {
            TypedArray typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.popupbtn);

            //normalBg = typedArray.GetResourceId(Resource.Styleable.popupbtn_normalBg, -1);
            //pressBg = typedArray.GetResourceId(Resource.Styleable.popupbtn_pressBg, -1);
            normalIcon = typedArray.GetResourceId(Resource.Styleable.popupbtn_normalIcon, -1);
            pressIcon = typedArray.GetResourceId(Resource.Styleable.popupbtn_pressIcon, -1);
        }

        /**
         * 初始话各种按钮样式
         */
        private void initBtn(Context context)
        {
            paddingTop = this.paddingTop;
            paddingLeft = this.paddingLeft;
            paddingRight = this.paddingRight;
            paddingBottom = this.paddingBottom;
            setNormal();

            IWindowManager wm = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            screenWidth = wm.DefaultDisplay.Width;
            screenHeight = wm.DefaultDisplay.Height;

        }

        /// <summary>
        /// 隐藏弹出框
        /// </summary>
        public void hidePopup()
        {
            if (popupWindow != null && popupWindow.IsShowing)
            {
                popupWindow.Dismiss();
            }
        }

        /**
		 * 设置自定义接口
		 * @param listener
		 */
        public void setListener(PopupButtonListener listener)
        {
            this.listener = listener;
        }


        /// <summary>
        /// 设置popupwindow的view
        /// </summary>
        /// <param name="">.</param>
        public void setPopupView(View view)
        {
            this.popWin = view;
            this.SetOnClickListener(this);
        }

        /**
     * 设置选中时候的按钮状态
     */
        private void setPress()
        {
            if (pressBg != -1)
            {
                this.SetBackgroundResource(pressBg);
                this.SetPadding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }
            if (pressIcon != -1)
            {
                Drawable drawable = Resources.GetDrawable(pressIcon);
                /// 这一步必须要做,否则不会显示.
                drawable.SetBounds(0, 0, drawable.MinimumWidth, drawable.MinimumHeight);
                this.SetCompoundDrawables(null, null, drawable, null);
            }
        }

        /**
		 * 设置正常模式下的按钮状态
		 */
        private void setNormal()
        {
            //if (normalBg != -1)
            //{
            //    this.SetBackgroundResource(normalBg);
            //    this.SetPadding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            //}
            if (normalIcon != -1)
            {
                Drawable drawable = Resources.GetDrawable(normalIcon);
                /// 这一步必须要做,否则不会显示.
                drawable.SetBounds(0, 0, drawable.MinimumWidth, drawable.MinimumHeight);
                this.SetCompoundDrawables(null, null, drawable, null);
            }
        }

        public void OnDismiss()
        {
            setNormal();
            if (listener != null)
            {
                listener.OnHide();
            }
        }

        public void OnClick(View view)
        {
            if (popupWindow == null)
            {
                LinearLayout layout = new LinearLayout(context);
                LinearLayout.LayoutParams params1 = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, (int)(screenHeight * 0.6));
                popWin.LayoutParameters = params1;

                layout.AddView(popWin);
                layout.SetBackgroundColor(Color.ParseColor("#666666"));
                popupWindow = new PopupWindow(layout, screenWidth, screenHeight);
                popupWindow.Focusable = true;
                popupWindow.SetBackgroundDrawable(new BitmapDrawable());
                popupWindow.OutsideTouchable = true;
				popupWindow.SetOnDismissListener(this);

			}

			popupWindow.ShowAsDropDown(view);
        }

        public interface PopupButtonListener
        {
            void OnShow();

            void OnHide();
        }
    }
}