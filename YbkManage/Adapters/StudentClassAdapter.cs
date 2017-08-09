using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using YbkManage.App;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    public class StudentClassAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<PureClassEntity> teachReportList;

        public StudentClassAdapter(Context context, List<PureClassEntity> data)
        {
            this.mContext = context;
            teachReportList = data;
        }

        public void SetData(List<PureClassEntity> data)
        {
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
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_student_class, parent, false);
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
                ((ItemViewHolder)holder).Tv_Teachers.Text = itemInfo.TeacherNames;
                ((ItemViewHolder)holder).Tv_Address.Text = itemInfo.PrintAddress;
                ((ItemViewHolder)holder).Tv_Date.Text = itemInfo.BeginDate + "--" + itemInfo.EndDate;
                ((ItemViewHolder)holder).Tv_Time.Text = itemInfo.PrintTime;

                ((ItemViewHolder)holder).Iv_Class_Type.SetImageResource(AppUtils.GetCourseIcon(itemInfo.ClassName));

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
            public ImageView Iv_Class_Type;
            public TextView Tv_Name, Tv_ClassCode, Tv_Teachers, Tv_Address, Tv_Date, Tv_Time;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Iv_Class_Type = (ImageView)itemView.FindViewById(Resource.Id.iv_classtype);

                Tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                Tv_ClassCode = (TextView)itemView.FindViewById(Resource.Id.tv_classcode);
                Tv_Teachers = (TextView)itemView.FindViewById(Resource.Id.tv_teachers);
                Tv_Address = (TextView)itemView.FindViewById(Resource.Id.tv_address);
                Tv_Date = (TextView)itemView.FindViewById(Resource.Id.tv_date);
                Tv_Time = (TextView)itemView.FindViewById(Resource.Id.tv_time);
            }
        }

        private IRecyclerViewItemClickListener onItemClickListener;


        public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
        {
            this.onItemClickListener = listener;
        }
    }
}