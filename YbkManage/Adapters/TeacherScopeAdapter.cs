using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using YbkManage.Models;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    /// <summary>
    /// 教研组列表的适配器
    /// </summary>
	public class TeacherScopeAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private static int TYPE_ITEM_ITEM = 1;
        private static int TYPE_ITEM_FOOTER = 2;
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<TeacherScopeEntity> teachReportList;

        public TeacherScopeAdapter(Context context, List<TeacherScopeEntity> data)
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
                var itemView = vi.Inflate(Resource.Layout.item_teacher_scope, parent, false);
                //itemView.SetOnClickListener(this);
                //itemView.SetOnLongClickListener(this);
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
                var scopeItem = teachReportList[position];

                ((ItemViewHolder)viewHolder).TV_ScopeName.Text = scopeItem.ScopeName;
                ((ItemViewHolder)viewHolder).TV_TeacherNum.Text = scopeItem.TeacherCount + "人";
            }
        }

        public override int GetItemViewType(int position)
        {
            return TYPE_ITEM_ITEM;
            //if (teachReportList != null && ItemCount == (position + 1))
            //{
            //	return TYPE_ITEM_FOOTER;
            //}
            //else
            //{
            //	return TYPE_ITEM_ITEM;
            //}
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
            public TextView TV_ScopeName, TV_TeacherNum;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                TV_ScopeName = itemView.FindViewById<TextView>(Resource.Id.tv_scopename);
                TV_TeacherNum = itemView.FindViewById<TextView>(Resource.Id.tv_teachernum);
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