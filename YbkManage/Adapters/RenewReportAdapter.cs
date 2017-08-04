using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    public class RenewReportAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<RenewInfo> teachReportList;

        public RenewReportAdapter(Context context, List<RenewInfo> data)
        {
            this.mContext = context;
            teachReportList = data;
        }

        public void SetData(List<RenewInfo> data)
        {
            this.teachReportList = data;
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
        /// <param name="holder">View holder.</param>
        /// <param name="position">Position.</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is ItemViewHolder)
            {
                var itemInfo = teachReportList[position];

                ((ItemViewHolder)holder).Tv_Name.Text = itemInfo.Item3;
                ((ItemViewHolder)holder).Tv_Num1.Text = itemInfo.Item4.ToString("f1");
                ((ItemViewHolder)holder).Tv_Num2.Text = itemInfo.Item5.ToString("f1");

                var rate = itemInfo.Item6;
                var avgRate = this.teachReportList[this.teachReportList.Count - 1].Item6;
                ((ItemViewHolder)holder).Tv_Rate.Text = (itemInfo.Item6 * 100).ToString("f1") + "%";
                if (rate > avgRate)
                {
                    ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
                }
                else if (rate < avgRate)
                {
                    ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
                }

                if(itemInfo == teachReportList[this.teachReportList.Count - 1])
                {
					((ItemViewHolder)holder).Tv_Num1.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
                    ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
                }
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
            public TextView Tv_Name, Tv_Num1, Tv_Num2, Tv_Rate;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                Tv_Num1 = (TextView)itemView.FindViewById(Resource.Id.tv_num1);
                Tv_Num2 = (TextView)itemView.FindViewById(Resource.Id.tv_num2);
                Tv_Rate = (TextView)itemView.FindViewById(Resource.Id.tv_rate);
            }
        }

        private IRecyclerViewItemClickListener onItemClickListener;


        public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
        {
            this.onItemClickListener = listener;
        }
    }
}