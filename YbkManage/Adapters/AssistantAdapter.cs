using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using Square.Picasso;
using YbkManage.App;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    /// <summary>
    /// 助教组长
    /// </summary>
	public class AssistantAdapter : RecyclerView.Adapter, IOnClickListener
    {
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<AstLeaderListModel> List;

        public AssistantAdapter(Context context)
        {
            this.mContext = context;
        }

        public void SetData(List<AstLeaderListModel> list)
        {
            this.List = list;
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
            //指定Layout页面
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_assistant, parent, false);
            return new ItemViewHolder(itemView);

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
                var itemInfo = List[position];

                ((ItemViewHolder)viewHolder).Tv_Name.Text = itemInfo.Name;
                ((ItemViewHolder)viewHolder).Tv_Area.Text = itemInfo.AreaName;
            }
        }

        public override int ItemCount
        {
            get
            {
                return List == null ? 0 : List.Count;
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
            public TextView Tv_Name, Tv_Area;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Tv_Name = itemView.FindViewById<TextView>(Resource.Id.tv_name);
                Tv_Area = itemView.FindViewById<TextView>(Resource.Id.tv_area);
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
