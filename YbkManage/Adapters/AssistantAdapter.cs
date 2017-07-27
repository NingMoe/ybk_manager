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
	public class AssistantAdapter : RecyclerView.Adapter, IOnClickListener
	{
		private static int TYPE_ITEM_ITEM = 1;
		private static int TYPE_ITEM_FOOTER = 2;
		private RecyclerView m_RecyclerView;

		private Context mContext;

		private List<AstLeaderListModel> List;

		public AssistantAdapter(Context context, List<AstLeaderListModel> data)
		{
			this.mContext = context;
			List = data;
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
				//指定Layout页面
				var itemView = vi.Inflate(Resource.Layout.item_assistant, parent, false);
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
				var itemInfo = List[position];

				((ItemViewHolder)viewHolder).Tv_Name.Text = itemInfo.Name;
				((ItemViewHolder)viewHolder).Tv_Area.Text = itemInfo.AreaName;

		
			}
		}

		public override int GetItemViewType(int position)
		{
			if (List != null && ItemCount == (position + 1))
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
				return List.Count;
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
				Tv_Name = itemView.FindViewById<TextView>(Resource.Id.astitem_tv_name);
				Tv_Area = itemView.FindViewById<TextView>(Resource.Id.astitem_tv_area);
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
