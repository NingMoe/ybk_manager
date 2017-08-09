using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    /// <summary>
    /// 教研组列表的适配器
    /// </summary>
	public class TeacherScopeAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<ScopeModel> teacherScopeList;

        public TeacherScopeAdapter(Context context, List<ScopeModel> data)
        {
            this.mContext = context;
            teacherScopeList = data;
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">Data.</param>
        public void SetData(List<ScopeModel> data)
        {
            this.teacherScopeList = data;
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
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_teacher_scope, parent, false);
            return new ItemViewHolder(itemView);
        }

        /// <summary>
        /// Ons the bind view holder.
        /// </summary>
        /// <param name="holder">Vholder.</param>
        /// <param name="position">Position.</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is ItemViewHolder)
            {
                var scopeItem = teacherScopeList[position];

                ((ItemViewHolder)holder).TV_ScopeName.Text = scopeItem.Name;
                ((ItemViewHolder)holder).TV_TeacherNum.Text = (scopeItem.TeacherCount ?? 0) + "人";
            }
        }

        public override int ItemCount
        {
            get
            {
                return teacherScopeList.Count;
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

        private IRecyclerViewItemClickListener onItemClickListener;


        public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
        {
            this.onItemClickListener = listener;
        }
    }
}