using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using YbkManage.App;
using YbkManage.Models;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    /// <summary>
    /// 教研列表的适配器
    /// </summary>
	public class TeacherAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private static int TYPE_ITEM_ITEM = 1;
        private static int TYPE_ITEM_FOOTER = 2;
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<TeacherInfoEntity> teachReportList;

        public TeacherAdapter(Context context, List<TeacherInfoEntity> data)
        {
            this.mContext = context;
            teachReportList = data;
        }

        /// <summary>
        /// Ons the create view holder.
        /// </summary>
        /// <returns>The create view holder.</returns>
        /// <param name="parent">Parent.</param>
        /// <param name="viewType">View type.</param>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            m_RecyclerView = parent as RecyclerView;
            var vi = LayoutInflater.From(parent.Context);
            if (viewType == TYPE_ITEM_FOOTER)
            {
                return new FootViewHolder(vi.Inflate(Resource.Layout.listview_footer, parent, false));
            }
            else if (viewType == TYPE_ITEM_ITEM)
            {
                var itemView = vi.Inflate(Resource.Layout.item_teacher, parent, false);
                return new ItemViewHolder(itemView);
            }
            return null;
        }

        /// <summary>
        /// Ons the bind view holder.
        /// </summary>
        /// <param name="viewHolder">View holder.</param>
        /// <param name="position">Position.</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            if (viewHolder is ItemViewHolder)
            {
                var itemInfo = teachReportList[position];

                ((ItemViewHolder)viewHolder).Tv_Name.Text = itemInfo.Name;
                ((ItemViewHolder)viewHolder).Tv_Code.Text = itemInfo.Code;

                Picasso picasso = Picasso.With(mContext);
                picasso.Load(itemInfo.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
					.Transform(new CircleImageTransformation(picasso))
                       .Into(((ItemViewHolder)viewHolder).Iv_Avatar);
            }
        }

        public override int GetItemViewType(int position)
        {
            if (teachReportList != null && ItemCount == (position + 1))
            {
            	return TYPE_ITEM_FOOTER;
            }
            else
            {
            	return TYPE_ITEM_ITEM;
            }
        }

        public override int ItemCount
        {
            get
            {
                return teachReportList.Count;
            }
		}

		public void OnClick(View v)
		{
			if (onItemClickListener != null)
			{
				int position = m_RecyclerView.GetLayoutManager().GetPosition(v);
				onItemClickListener.OnItemClick(v, position);
			}
		}

		public bool OnLongClick(View v)
		{
            if (onItemClickListener != null)
            {
                int position = m_RecyclerView.GetLayoutManager().GetPosition(v);
                onItemClickListener.OnItemLongClick(v, position);
            }
			return true;
		}



        public class ItemViewHolder : RecyclerView.ViewHolder
        {
            public ImageView Iv_Avatar;
            public TextView Tv_Name, Tv_Code,Tv_Admin;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Iv_Avatar = itemView.FindViewById<ImageView>(Resource.Id.iv_avatar);
                Tv_Name = itemView.FindViewById<TextView>(Resource.Id.tv_name);
				Tv_Code = itemView.FindViewById<TextView>(Resource.Id.tv_code);
				Tv_Admin = itemView.FindViewById<TextView>(Resource.Id.tv_admin);
            }
        }

        class FootViewHolder : RecyclerView.ViewHolder
        {
            public FootViewHolder(View itemView) : base(itemView)
            {

            }
        }

        private IRecyclerViewItemClickListener onItemClickListener;


        public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
        {
            this.onItemClickListener = listener;
        }
    }
}