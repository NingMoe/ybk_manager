
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
	public class BudgeAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
	{
		#region Fields
		private RecyclerView m_RecyclerView;
		private IRecyclerViewItemClickListener onItemClickListener;

		private Context mContext;

		private List<PaymentEntity> paymentList;
		private PaymentEntity totalPayment;
		#endregion

		public BudgeAdapter(Context context, List<PaymentEntity> data)
		{
			this.mContext = context;
			paymentList = data;
			//合计行
			if (paymentList.Count > 0)
			{
				totalPayment = paymentList[paymentList.Count - 1];
			}
		}

		public void SetData(List<PaymentEntity> data)
		{
			this.paymentList = data;
			//合计行
			if (paymentList.Count > 0)
			{
				totalPayment = paymentList[paymentList.Count - 1];
			}
		}
		/// <summary>
		/// 指定Layout
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
		/// 获取数据总记录数
		/// </summary>
		/// <value>The item count.</value>
		public override int ItemCount
		{
			get
			{
				return paymentList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			if (holder is ItemViewHolder)
			{
				var tv_name = ((ItemViewHolder)holder).Tv_Name;
				var tv_budge = ((ItemViewHolder)holder).Tv_Num1;
				var tv_payment = ((ItemViewHolder)holder).Tv_Num2;
				var tv_rate = ((ItemViewHolder)holder).Tv_Rate;

				var itemInfo = paymentList[position];

				tv_name.Text = itemInfo.AreaName;
				tv_budge.Text = (itemInfo.Budget/10000).ToString("f1");
				tv_payment.Text = (itemInfo.Payment/10000).ToString("f1");
				//完成率
				var rate = itemInfo.CompletionRate;
				if (itemInfo.Budget != 0)
					tv_rate.Text = (itemInfo.CompletionRate * 100).ToString("f1") + "%";
				else
					tv_rate.Text = "--";

				//设置完成率字体颜色
				if (rate >= totalPayment.CompletionRate)
				{
					tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
				}
				else
				{
					tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
				}

				//总计行
				if (position == this.paymentList.Count - 1)
				{
					tv_budge.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_payment.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				}
				else
				{
					tv_budge.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					tv_payment.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
				}
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
		public void SetOnItemClickListener(IRecyclerViewItemClickListener listener)
		{
			this.onItemClickListener = listener;
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
	}
}
