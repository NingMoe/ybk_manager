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
    public class ReportByTeacherAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private Context mContext;

        private RecyclerView m_RecyclerView;

        private List<Statistics_ClassRenewSummary> teachReportList;

        // 所在项目组的平均续班率
        private decimal avgRenewRateScope = 1;

        public ReportByTeacherAdapter(Context context, List<Statistics_ClassRenewSummary> data, decimal scopeRate)
        {
            this.mContext = context;
            teachReportList = data;
            avgRenewRateScope = scopeRate;
        }

        public void SetData(List<Statistics_ClassRenewSummary> data)
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
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_report_byteacher, parent, false);
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

                ((ItemViewHolder)holder).Tv_Name.Text = itemInfo.ClassName;
                ((ItemViewHolder)holder).Tv_ClassCode.Text = itemInfo.ClassCode;
                ((ItemViewHolder)holder).Tv_Area.Text = itemInfo.AreaName;
                ((ItemViewHolder)holder).Tv_Num1.Text = itemInfo.TotalStudentNum.ToString("f1") + "/" + itemInfo.RenewStudentNum.ToString("f1");
                ((ItemViewHolder)holder).Tv_Num2.Text = (itemInfo.RenewRate * 100).ToString("f1") + "%";

                // 比平均值
                if (position < teachReportList.Count - 1)
                {
                    var avgrate = itemInfo.RenewRate - avgRenewRateScope;
                    ((ItemViewHolder)holder).Tv_Rate.Text = (avgrate > 0 ? "+" : "") + (avgrate * 100).ToString("f1") + "%";
                    if (avgrate > 0)
                    {
                        ((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
                        ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
                    }
                    else if (avgrate < 0)
                    {
						((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
                        ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
                    }
                    else
                    {
						((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
						((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
                    }
                }

                //var rate = itemInfo.RenewRate;
                //var avgRate = this.teachReportList[this.teachReportList.Count - 1].RenewRate;
                //if (rate > avgRate)
                //{
                //    ((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
                //    ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
                //}
                //else if (rate < avgRate)
                //{
                //    ((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
                //    ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
                //}

                //if (position == teachReportList.Count - 1)
                //{
                //    ((ItemViewHolder)holder).Tv_Num1.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
                //    ((ItemViewHolder)holder).Tv_Num2.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
                //    ((ItemViewHolder)holder).Tv_Rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
                //}
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
            public TextView Tv_Name, Tv_Num1, Tv_Num2, Tv_Rate, Tv_ClassCode, Tv_Area;

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

        private IRecyclerViewItemClickListener onItemClickListener;


        public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
        {
            this.onItemClickListener = listener;
        }
    }
}