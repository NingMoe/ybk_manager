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
    public class StudentClassAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private static int TYPE_ITEM_ITEM = 1;
        //private static int TYPE_ITEM_FOOTER = 2;
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<ClassEntity> teachReportList;

        public StudentClassAdapter(Context context, List<ClassEntity> data)
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
            var itemView = vi.Inflate(Resource.Layout.item_student_class, parent, false);
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
				((ItemViewHolder)viewHolder).Tv_Teachers.Text = itemInfo.TeacherNames;
				((ItemViewHolder)viewHolder).Tv_Address.Text = itemInfo.PrintAddress;
                ((ItemViewHolder)viewHolder).Tv_Date.Text = itemInfo.BeginDate+"--"+itemInfo.EndDate;
                ((ItemViewHolder)viewHolder).Tv_Time.Text = itemInfo.PrintTime;

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
            public ImageView Iv_Class_Type;
            public TextView Tv_Name, Tv_ClassCode, Tv_Teachers, Tv_Address,Tv_Date,Tv_Time;


            public ItemViewHolder(View itemView) : base(itemView)
			{
				Iv_Class_Type = (ImageView)itemView.FindViewById(Resource.Id.iv_classtype);

				Tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                Tv_ClassCode = (TextView)itemView.FindViewById(Resource.Id.tv_classcode);
                Tv_Teachers = (TextView)itemView.FindViewById(Resource.Id.tv_teachers);
				Tv_Address= (TextView)itemView.FindViewById(Resource.Id.tv_address);
				Tv_Date = (TextView)itemView.FindViewById(Resource.Id.tv_date);
                Tv_Time = (TextView)itemView.FindViewById(Resource.Id.tv_time);
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