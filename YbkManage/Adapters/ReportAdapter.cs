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
    public class ReportAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private static int TYPE_ITEM_ITEM = 1;
        //private static int TYPE_ITEM_FOOTER = 2;
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<TeachReportEntity> teachReportList;

        public ReportAdapter(Context context, List<TeachReportEntity> data)
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
            var itemView = vi.Inflate(Resource.Layout.item_report, parent, false);
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
                var itemInfo = teachReportList[position];

                ((ItemViewHolder)viewHolder).Tv_Name.Text = itemInfo.Name;
                ((ItemViewHolder)viewHolder).Tv_Num1.Text = itemInfo.ClassSize.ToString("f1");
                ((ItemViewHolder)viewHolder).Tv_Num2.Text = itemInfo.ClassContinueSize.ToString("f1");
                ((ItemViewHolder)viewHolder).Tv_Rate.Text = (itemInfo.ContinueRate * 100).ToString("f2") + "%";

            }
        }

        public override int GetItemViewType(int position)
        {
            return TYPE_ITEM_ITEM;
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
            public TextView Tv_Name, Tv_Num1, Tv_Num2, Tv_Rate;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                Tv_Num1 = (TextView)itemView.FindViewById(Resource.Id.tv_num1);
                Tv_Num2 = (TextView)itemView.FindViewById(Resource.Id.tv_num2);
                Tv_Rate = (TextView)itemView.FindViewById(Resource.Id.tv_rate);
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