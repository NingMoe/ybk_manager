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
    public class ReportByTeacherAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private static int TYPE_ITEM_ITEM = 1;
        //private static int TYPE_ITEM_FOOTER = 2;
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<RenewInfoEntity> teachReportList;

        public ReportByTeacherAdapter(Context context, List<RenewInfoEntity> data)
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
            var itemView = vi.Inflate(Resource.Layout.item_report_byteacher, parent, false);
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

                ((ItemViewHolder)viewHolder).Tv_Name.Text = itemInfo.ClassName;
                ((ItemViewHolder)viewHolder).Tv_ClassCode.Text = itemInfo.ClassCode;
                ((ItemViewHolder)viewHolder).Tv_Area.Text = itemInfo.AreaName;
                ((ItemViewHolder)viewHolder).Tv_Num1.Text = itemInfo.TotalStudentNum.ToString("f1")+"/"+itemInfo.RenewStudentNum.ToString("f1");
                ((ItemViewHolder)viewHolder).Tv_Num2.Text = (itemInfo.RenewRate * 100).ToString("f2") + "%";
                ((ItemViewHolder)viewHolder).Tv_Rate.Text = (itemInfo.RenewRate * 100).ToString("f2") + "%";

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
            public TextView Tv_Name, Tv_Num1, Tv_Num2, Tv_Rate,Tv_ClassCode,Tv_Area;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                Tv_Num1 = (TextView)itemView.FindViewById(Resource.Id.tv_num1);
                Tv_Num2 = (TextView)itemView.FindViewById(Resource.Id.tv_num2);
				Tv_Rate = (TextView)itemView.FindViewById(Resource.Id.tv_rate);
				Tv_ClassCode = (TextView)itemView.FindViewById(Resource.Id.tv_classCode);
                Tv_Area = (TextView)itemView.FindViewById(Resource.Id.tv_area);
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