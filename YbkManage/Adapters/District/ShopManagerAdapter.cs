using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using Square.Picasso;
using YbkManage.Adapters;
using YbkManage.App;
using static Android.Views.View;

namespace YbkManage
{
	public class ShopManagerAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
	{
		private RecyclerView m_RecyclerView;

		private Context mContext;
		private Picasso picasso;
		private List<ShopManagerList> List;

		public ShopManagerAdapter(Context context, List<ShopManagerList> data)
		{
			this.mContext = context;
			List = data;
			picasso = Picasso.With(mContext);
		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="data">Data.</param>
		public void SetData(List<ShopManagerList> data)
		{
			this.List = data;
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
			var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_shopmanager, parent, false);
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
				var data = List[position];

				((ItemViewHolder)holder).TV_Name.Text = data.Name;
				((ItemViewHolder)holder).TV_Areas.Text = data.AreaNames;

				//头像
				if (!string.IsNullOrEmpty(data.Avatar))
                {
                    picasso.Load(data.Avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
                    .Transform(new CircleImageTransformation(picasso))
					       .Into(((ItemViewHolder)holder).IV_Avatar);
                }
                else
                {
					picasso.Load(Resource.Drawable.avatar).Placeholder(Resource.Drawable.avatar).Error(Resource.Drawable.avatar)
					.Transform(new CircleImageTransformation(picasso))
					       .Into(((ItemViewHolder)holder).IV_Avatar);
                }
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
			public TextView TV_Name, TV_Areas;
			public ImageView IV_Avatar;


			public ItemViewHolder(View itemView) : base(itemView)
			{
				TV_Name = itemView.FindViewById<TextView>(Resource.Id.tv_name);
				TV_Areas = itemView.FindViewById<TextView>(Resource.Id.tv_areas);
				IV_Avatar = itemView.FindViewById<ImageView>(Resource.Id.iv_avatar);
			}
		}

		private IRecyclerViewItemClickListener onItemClickListener;


		public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
		{
			this.onItemClickListener = listener;
		}
	}
}
