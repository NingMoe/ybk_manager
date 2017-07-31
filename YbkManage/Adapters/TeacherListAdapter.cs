using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using Square.Picasso;
using YbkManage.App;
using YbkManage.Models;
using static Android.Views.View;

namespace YbkManage.Adapters
{
    /// <summary>
    /// 教研列表的适配器
    /// </summary>
	public class TeacherListAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
    {
        private static int TYPE_ITEM_ITEM = 1;
        private static int TYPE_ITEM_FOOTER = 2;
        private RecyclerView m_RecyclerView;

        private Context mContext;

        private List<TeacherListModel> teacherList;

        private Picasso picasso;

        private bool hideFooter = false;

        // 页面类型 1=教师列表 2=教学主管列表
        private int pageType = 1;

        public TeacherListAdapter(Context context, int pagetype)
        {
            mContext = context;
            pageType = pagetype;
            picasso = Picasso.With(mContext);
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
        /// <param name="holder">View holder.</param>
        /// <param name="position">Position.</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is ItemViewHolder)
            {
                var itemInfo = teacherList[position];

                ((ItemViewHolder)holder).Tv_Name.Text = itemInfo.Name;
                ((ItemViewHolder)holder).Tv_Code.Text = itemInfo.Code;


                if (pageType == 1)
                {
                    ((ItemViewHolder)holder).Tv_Job.Visibility = ViewStates.Gone;
                    if (itemInfo.Type == 22)
                    {
                        ((ItemViewHolder)holder).Tv_Job.Text = "教学区长";
                        ((ItemViewHolder)holder).Tv_Job.Visibility = ViewStates.Visible;
                    }
                    else if (itemInfo.Type == 23)
                    {
                        ((ItemViewHolder)holder).Tv_Job.Text = "教学主管";
                        ((ItemViewHolder)holder).Tv_Job.Visibility = ViewStates.Visible;
                    }
                }
                else
                {
                    ((ItemViewHolder)holder).Tv_Job.Visibility = ViewStates.Visible;
                    ((ItemViewHolder)holder).Tv_Job.Text = itemInfo.ScopeName;
                }


                if (!string.IsNullOrEmpty(itemInfo.Avatar))
                {
                    picasso.Load(itemInfo.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
                    .Transform(new CircleImageTransformation(picasso))
                       .Into(((ItemViewHolder)holder).Iv_Avatar);
                }
            }
        }

        public override int GetItemViewType(int position)
        {
            if (teacherList != null && ItemCount == (position + 1) && !this.hideFooter)
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
                return teacherList != null ? teacherList.Count : 0;
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

        public void HideFootere(bool isHide)
        {
            this.hideFooter = isHide;
        }


        public class ItemViewHolder : RecyclerView.ViewHolder
        {
            public ImageView Iv_Avatar;
            public TextView Tv_Name, Tv_Code, Tv_Job;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                Iv_Avatar = itemView.FindViewById<ImageView>(Resource.Id.iv_avatar);
                Tv_Name = itemView.FindViewById<TextView>(Resource.Id.tv_name);
                Tv_Code = itemView.FindViewById<TextView>(Resource.Id.tv_code);
                Tv_Job = itemView.FindViewById<TextView>(Resource.Id.tv_job);
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

        public void SetData(List<TeacherListModel> data)
        {
            this.teacherList = data;
        }
    }
}