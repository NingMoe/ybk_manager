
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
	public class SumByTeacherAdapter : RecyclerView.Adapter, IOnClickListener, IOnLongClickListener
	{
		#region Fields
		private RecyclerView m_RecyclerView;
		private IRecyclerViewItemClickListener onItemClickListener;

		private Context mContext;
		//最后一行是合计行
		private List<PaymentSumTeacherEntity> paymentList;
		private PaymentSumTeacherEntity totalPayment;
		#endregion

		public SumByTeacherAdapter(Context context, List<PaymentSumTeacherEntity> data)
		{
			this.mContext = context;
			paymentList = data;
			//合计行
			if (paymentList.Count > 0)
			{
				totalPayment = paymentList[paymentList.Count - 1];
			}
		}

		public void SetData(List<PaymentSumTeacherEntity> data)
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
			var itemView = vi.Inflate(Resource.Layout.item_sumbyteacher, parent, false);
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
				var itemInfo = paymentList[position];

				var tv_name = ((ItemViewHolder)holder).Tv_Name;
				var tv_course = ((ItemViewHolder)holder).Tv_course;
				var tv_total = ((ItemViewHolder)holder).Tv_total;
				var tv_classcount = ((ItemViewHolder)holder).Tv_classcount;
				var tv_classavg = ((ItemViewHolder)holder).Tv_classavg;
				var tv_renewrate = ((ItemViewHolder)holder).Tv_renewrate;
				var tv_refundrate = ((ItemViewHolder)holder).Tv_refundrate;

				//内容赋值
				tv_name.Text = itemInfo.TeacherName;
				tv_course.Text = itemInfo.CourseName;
				tv_total.Text = itemInfo.Total.ToString("f0");
				tv_classcount.Text = itemInfo.ClassCount.ToString("f1");
				tv_classavg.Text = itemInfo.ClassAvg.ToString("f1");
				tv_renewrate.Text = (itemInfo.RenewRate * 100).ToString("f1") + "%";
				tv_refundrate.Text = (itemInfo.RefundRate * 100).ToString("f1") + "%";

				//总计行
				if (position == this.paymentList.Count - 1)
				{
					//字体颜色
					tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_course.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_total.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_classcount.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_classavg.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_renewrate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_refundrate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				}
				else
				{
					//字体颜色
					tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
					tv_course.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					tv_total.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					tv_classcount.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					tv_classavg.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					tv_renewrate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
					tv_refundrate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
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
			public TextView Tv_Name, Tv_course, Tv_total, Tv_classcount, Tv_classavg, Tv_renewrate, Tv_refundrate;


			public ItemViewHolder(View itemView) : base(itemView)
			{
				Tv_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
				Tv_course = (TextView)itemView.FindViewById(Resource.Id.tv_course);
				Tv_total = (TextView)itemView.FindViewById(Resource.Id.tv_total);
				Tv_classcount = (TextView)itemView.FindViewById(Resource.Id.tv_classcount);
				Tv_classavg = (TextView)itemView.FindViewById(Resource.Id.tv_classavg);
				Tv_renewrate=(TextView)itemView.FindViewById(Resource.Id.tv_renewrate);
				Tv_refundrate=(TextView)itemView.FindViewById(Resource.Id.tv_refundrate);
			}
		}
	}
}
