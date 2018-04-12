
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using YbkManage.Adapters;
using static Android.Views.View;

namespace YbkManage
{
	public class SumAccountAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
	{
        private RecyclerView m_RecyclerView;
		private Context mContext;

		private List<PaymentSumAreaEntity> sumList;
		private List<PaymentSumBaseEntity> dynamicGradeSumList;
		private decimal avgGrowthRate;

		private LinearLayout llItem;

		public SumAccountAdapter(Context context, List<PaymentSumAreaEntity> sumList, decimal avgGrowthRate)
		{
			this.mContext = context;
			this.sumList = sumList;
			this.avgGrowthRate = avgGrowthRate;
		}

		public SumAccountAdapter(Context context, List<PaymentSumBaseEntity> gradeSumList, decimal avgGrowthRate)
		{
			this.mContext = context;
			this.dynamicGradeSumList = gradeSumList;
			this.avgGrowthRate = avgGrowthRate;
		}

		public void SetData(List<PaymentSumAreaEntity> data)
		{
			this.sumList = data;
		}


		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemView = new View(this.mContext);
			m_RecyclerView = parent as RecyclerView;
			var vi = LayoutInflater.From(parent.Context);

			if (this.sumList == null) viewType = 2;


			switch (viewType)
			{
				case 1: //parent
					itemView = vi.Inflate(Resource.Layout.item_sumaccount, parent, false);
					return new ItemViewHolder(itemView);
				case 2: //child
					itemView = vi.Inflate(Resource.Layout.item_sumaccount, parent, false);
					return new ChildItemViewHolder(itemView);
				default:
					itemView = vi.Inflate(Resource.Layout.item_sumaccount, parent, false);
					return new ItemViewHolder(itemView);
			}
			//m_RecyclerView = parent as RecyclerView;
			//var vi = LayoutInflater.From(parent.Context);
			//var itemView = vi.Inflate(Resource.Layout.item_sumaccount, parent, false);
			//return new ItemViewHolder(itemView);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			if (this.sumList == null)
				holder = holder as ChildItemViewHolder;
			
			if (holder is ItemViewHolder)
			{
				var holderX = holder as ItemViewHolder;

				#region itemview
				var itemInfo = this.sumList[position];

				holderX.tv_name.Text = itemInfo.Name;
				holderX.tv_currentSum.Text = itemInfo.CurrentSum.ToString("f1");
				holderX.tv_lastYearSum.Text = itemInfo.LastYearSum.ToString("f1");
				holderX.tv_growthRate.Text = (itemInfo.GrowthRate * 100).ToString("f1") + "%";

				// 比平均值
				var avgrate = itemInfo.GrowthRate - this.avgGrowthRate;
				holderX.tv_growthRate.Text = (avgrate > 0 ? "+" : "") + (avgrate * 100).ToString("f1") + "%";
				if (avgrate >= 0)
				{
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
				}
				else
				{
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
				}

				if (position == this.sumList.Count - 1)
				{
					holderX.tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					holderX.tv_currentSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					holderX.tv_lastYearSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				}
				else
				{
					holderX.tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					holderX.tv_currentSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					holderX.tv_lastYearSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
				}

				#endregion
			}
			else if (holder is ChildItemViewHolder)
			{
				var holderX = holder as ChildItemViewHolder;

				#region childitemview
				var itemInfo = this.dynamicGradeSumList[position];

				holderX.tv_name.Text = itemInfo.Name;
				holderX.tv_currentSum.Text = itemInfo.CurrentSum.ToString("f1");
				holderX.tv_lastYearSum.Text = itemInfo.LastYearSum.ToString("f1");
				holderX.tv_growthRate.Text = (itemInfo.GrowthRate * 100).ToString("f1") + "%";

				// 比平均值
				var avgrate = itemInfo.GrowthRate - this.avgGrowthRate;
				holderX.tv_growthRate.Text = (avgrate > 0 ? "+" : "") + (avgrate * 100).ToString("f1") + "%";
				if (avgrate >= 0)
				{
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
				}
				else
				{
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
				}

				if (position == this.dynamicGradeSumList.Count - 1)
				{
					holderX.tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					holderX.tv_currentSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					holderX.tv_lastYearSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				}
				else
				{
					holderX.tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					holderX.tv_currentSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					holderX.tv_lastYearSum.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
				}

				#endregion
			}
		}

		public override int ItemCount
		{
			get
			{
				if (this.sumList != null)
					return this.sumList.Count;
				else
					return this.dynamicGradeSumList.Count;
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

		/// <summary>
		/// parent view holder.
		/// 折叠子项的item
		/// </summary>
		public class ChildItemViewHolder : RecyclerView.ViewHolder
		{
			public TextView tv_name, tv_currentSum, tv_lastYearSum, tv_growthRate;

			public ChildItemViewHolder(View childItemView) : base(childItemView)
			{
				tv_name = (TextView)childItemView.FindViewById(Resource.Id.tv_sum_name);
				tv_currentSum = (TextView)childItemView.FindViewById(Resource.Id.tv_sum_currentsum);
				tv_lastYearSum = (TextView)childItemView.FindViewById(Resource.Id.tv_sum_lastyearsum);
				tv_growthRate = (TextView)childItemView.FindViewById(Resource.Id.tv_sum_growthrate);
			}
		}

		/// <summary>
		/// Item view holder.
		/// parent item
		/// </summary>
		public class ItemViewHolder : RecyclerView.ViewHolder
		{
			public TextView tv_name, tv_currentSum, tv_lastYearSum, tv_growthRate;


			public ItemViewHolder(View itemView) : base(itemView)
			{
				tv_name = (TextView)itemView.FindViewById(Resource.Id.tv_sum_name);
				tv_currentSum = (TextView)itemView.FindViewById(Resource.Id.tv_sum_currentsum);
				tv_lastYearSum = (TextView)itemView.FindViewById(Resource.Id.tv_sum_lastyearsum);
				tv_growthRate = (TextView)itemView.FindViewById(Resource.Id.tv_sum_growthrate);
			}
		}

		private IRecyclerViewItemClickListener onItemClickListener;


		public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
		{
			this.onItemClickListener = listener;
		}


		//public interface ItemClickListener
		//{
		//	/** 
		//	 * 展开子Item 
		//	 * @param  
		//	 */
		//	void onExpandChildren(DataBean bean);

		//	/** 
		//	 * 隐藏子Item 
		//	 * @param  
		//	 */
		//	void onHideChildren(DataBean bean);
		//}
	}
}
