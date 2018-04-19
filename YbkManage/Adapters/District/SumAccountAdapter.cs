
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using YbkManage.Adapters;
using static Android.Views.View;

namespace YbkManage
{
    public class SumAccountAdapter : RecyclerView.Adapter
    {
        private RecyclerView m_RecyclerView;
<<<<<<< HEAD
		private Context mContext;

		private List<PaymentSumAreaEntity> sumList;
		private List<PaymentSumBaseEntity> dynamicGradeSumList;
		private decimal avgGrowthRate;
		public int dataType = 0;
		private LinearLayout llItem;

		public SumAccountAdapter(Context context, List<PaymentSumAreaEntity> sumList, decimal avgGrowthRate,int dataType)
		{
			this.mContext = context;
			this.sumList = sumList;
			this.avgGrowthRate = avgGrowthRate;
			this.dataType = dataType;
		}

		public SumAccountAdapter(Context context, List<PaymentSumBaseEntity> gradeSumList, decimal avgGrowthRate)
		{
			this.mContext = context;
			this.dynamicGradeSumList = gradeSumList;
			this.avgGrowthRate = avgGrowthRate;
		}

		public void SetData(List<PaymentSumAreaEntity> data, decimal avgGrowthRate)
		{
			this.sumList = data;
			this.avgGrowthRate = avgGrowthRate;
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
				//预收&行课，单位万
				if (dataType != 1)
				{
					holderX.tv_currentSum.Text =(itemInfo.CurrentSum/10000).ToString("f1");
					holderX.tv_lastYearSum.Text = (itemInfo.LastYearSum/10000).ToString("f1");
				}
				else
				{
					holderX.tv_currentSum.Text = itemInfo.CurrentSum.ToString("f1");
					holderX.tv_lastYearSum.Text = itemInfo.LastYearSum.ToString("f1");
				}

				holderX.tv_growthRate.Text = (itemInfo.GrowthRate * 100).ToString("f1") + "%";

				// 比平均值
				var avgrate = itemInfo.GrowthRate - this.avgGrowthRate;
				//holderX.tv_growthRate.Text = (avgrate > 0 ? "+" : "") + (itemInfo.GrowthRate * 100).ToString("f1") + "%";
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

				}
				#endregion
			}
			else if (holder is ChildItemViewHolder)
			{
				var holderX = holder as ChildItemViewHolder;

				#region childitemview
				var itemInfo = this.dynamicGradeSumList[position];

				holderX.ItemView.SetBackgroundColor(Color.LightCyan);
				holderX.tv_name.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
				holderX.tv_currentSum.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
				holderX.tv_lastYearSum.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
				holderX.tv_growthRate.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);

				holderX.tv_name.Text = itemInfo.Name;
				//预收&行课，单位万
				if (dataType != 1)
				{
					holderX.tv_currentSum.Text =(itemInfo.CurrentSum/10000).ToString("f1");
					holderX.tv_lastYearSum.Text = (itemInfo.LastYearSum/10000).ToString("f1");
				}
				else
				{
					holderX.tv_currentSum.Text = itemInfo.CurrentSum.ToString("f1");
					holderX.tv_lastYearSum.Text = itemInfo.LastYearSum.ToString("f1");
				}
				holderX.tv_growthRate.Text = (itemInfo.GrowthRate * 100).ToString("f1") + "%";

				// 比平均值
				var avgrate = itemInfo.GrowthRate - this.avgGrowthRate;
				//holderX.tv_growthRate.Text = (avgrate > 0 ? "+" : "") + (itemInfo.GrowthRate * 100).ToString("f1") + "%";
				if (avgrate >= 0)
				{
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
				}
				else
				{
					holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
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
=======
        private Context mContext;

        private List<PaymentSumAreaEntity> sumList;
        private decimal avgGrowthRate;

        //累计：1-人次；2-预收；3-行课
        public int dataType = 1;
        private QuarterEntity searchQuarter;
        private string searchCourse = "全部科目",gradeListParam="";
        private List<string> searchGradeList = new List<string>();

        RecyclerView mRecyclerView;

        public SumAccountAdapter(Context context, List<PaymentSumAreaEntity> sumList, decimal avgGrowthRate,RecyclerView mRecyclerView)
        {
            this.mContext = context;
            this.sumList = sumList;
            this.avgGrowthRate = avgGrowthRate;

            this.mRecyclerView = mRecyclerView;
        }

        public void SetData(List<PaymentSumAreaEntity> data, decimal avgGrowthRate, QuarterEntity searchQuarter,int dataType,string searchCourse,string gradeListParam)
        {
            this.sumList = data;
            this.avgGrowthRate = avgGrowthRate;
            this.searchQuarter = searchQuarter;
            this.dataType = dataType;
            this.searchCourse = searchCourse;
            this.gradeListParam = gradeListParam;
        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            m_RecyclerView = parent as RecyclerView;
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_sumaccount, parent, false);
            return new ItemViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var holderX = holder as ItemViewHolder;
            var item = sumList[position];

            // 是否折叠
            if(item.IsFold )
            {
                holderX.mRecyclerView.Visibility =  ViewStates.Gone;
            }
            else
            {
                holderX.mRecyclerView.Visibility = ViewStates.Visible;

                if(item.GradeData.Any())
                {
                    LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this.mContext);
                    SumAccountAdapter2 mAdapter = new SumAccountAdapter2(this.mContext, item.GradeData, this.avgGrowthRate,this.searchQuarter,this.dataType,this.searchCourse,this.gradeListParam);
                    holderX.mRecyclerView.SetLayoutManager(linearLayoutManager);
                    holderX.mRecyclerView.SetAdapter(mAdapter);                    
                }
            }

            var itemInfo = this.sumList[position];

            holderX.tv_name.Text = itemInfo.Name;
            holderX.tv_currentSum.Text = itemInfo.CurrentSum.ToString("f1");
            holderX.tv_lastYearSum.Text = itemInfo.LastYearSum.ToString("f1");
            holderX.tv_growthRate.Text = (itemInfo.GrowthRate * 100).ToString("f1") + "%";

            // 比平均值
            var avgrate = itemInfo.GrowthRate - this.avgGrowthRate;
            holderX.tv_growthRate.Text = (avgrate > 0 ? "+" : "") + (itemInfo.GrowthRate * 100).ToString("f1") + "%";
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

            if(!holderX.ll_item_wrap.HasOnClickListeners)
            {
                holderX.ll_item_wrap.Click += itemTitle_Click;
            }
        }

        private void itemTitle_Click(object sender, EventArgs e)
        {
            var aa = ((View)sender).Parent as ViewGroup;
            int position = mRecyclerView.GetChildAdapterPosition(aa);
            if(position>-1)
            {

            this.sumList[position].IsFold = !this.sumList[position].IsFold;
                NotifyDataSetChanged();
            }
            //Toast.MakeText(this.mContext, position + "", ToastLength.Long).Show();
        }

        public override int ItemCount
        {
            get
            {
                if (this.sumList != null)
                {
                    return this.sumList.Count;                 
                }
                return 0;
            }
        }

        /// <summary>
        /// Item view holder.
        /// parent item
        /// </summary>
        public class ItemViewHolder : RecyclerView.ViewHolder
        {
            public LinearLayout ll_item_wrap;
            public TextView tv_name, tv_currentSum, tv_lastYearSum, tv_growthRate;
            public RecyclerView mRecyclerView;

            public ItemViewHolder(View itemView) : base(itemView)
            {
                tv_name = (TextView)itemView.FindViewById(Resource.Id.tv_sum_name);
                tv_currentSum = (TextView)itemView.FindViewById(Resource.Id.tv_sum_currentsum);
                tv_lastYearSum = (TextView)itemView.FindViewById(Resource.Id.tv_sum_lastyearsum);
                tv_growthRate = (TextView)itemView.FindViewById(Resource.Id.tv_sum_growthrate);

                mRecyclerView = (RecyclerView)itemView.FindViewById(Resource.Id.item_recycler_view);
                ll_item_wrap = (LinearLayout)itemView.FindViewById(Resource.Id.ll_item_wrap);
            }
        }
    }

    public class SumAccountAdapter2 : RecyclerView.Adapter
    {
        private RecyclerView m_RecyclerView;
        private Context mContext;

        private List<PaymentSumBaseEntity> dynamicGradeSumList;
        private decimal avgGrowthRate;

        //累计：1-人次；2-预收；3-行课
        public int dataType = 1;
        private QuarterEntity searchQuarter;
        private string searchCourse = "全部科目",gradeListParam="";

        public SumAccountAdapter2(Context context, List<PaymentSumBaseEntity> gradeSumList, decimal avgGrowthRate, QuarterEntity searchQuarter,int dataType, string searchCourse, string gradeListParam)
        {
            this.mContext = context;
            this.dynamicGradeSumList = gradeSumList;
            this.avgGrowthRate = avgGrowthRate;

            this.searchQuarter = searchQuarter;
            this.dataType = dataType;
            this.searchCourse = searchCourse;
            this.gradeListParam = gradeListParam;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            m_RecyclerView = parent as RecyclerView;
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_sumaccount2, parent, false);
            return new ItemViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var itemInfo = dynamicGradeSumList[position];
            var holderX = holder as ItemViewHolder;

            #region childitemview

            holder.ItemView.SetBackgroundColor(Color.LightCyan);
            holderX.tv_name.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
            holderX.tv_currentSum.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
            holderX.tv_lastYearSum.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
            holderX.tv_growthRate.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);

            holderX.tv_name.Text = itemInfo.Name;
            holderX.tv_currentSum.Text = itemInfo.CurrentSum.ToString("f1");
            holderX.tv_lastYearSum.Text = itemInfo.LastYearSum.ToString("f1");
            holderX.tv_growthRate.Text = (itemInfo.GrowthRate * 100).ToString("f1") + "%";

            // 比平均值
            var avgrate = itemInfo.GrowthRate - this.avgGrowthRate;
            holderX.tv_growthRate.Text = (avgrate > 0 ? "+" : "") + (itemInfo.GrowthRate * 100).ToString("f1") + "%";
            if (avgrate >= 0)
            {
                holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
            }
            else
            {
                holderX.tv_growthRate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
            }

            if (!holder.ItemView.HasOnClickListeners)
            {
                holder.ItemView.Click += (sender, e) =>
                    {
                    Toast.MakeText(this.mContext, ((View)sender).GetY()+  "", ToastLength.Short).Show();
                        //var intent = new Intent(this.mContext, typeof(SumByTeacherFragment));
                        //intent.PutExtra("year", this.searchQuarter.Year);
                        //intent.PutExtra("quarter", this.searchQuarter.Quarter);
                        //intent.PutExtra("dataType", this.dataType);
                        //intent.PutExtra("areaCode", itemInfo.Code);
                        //intent.PutExtra("areaName", itemInfo.Name);
                        //intent.PutExtra("course", this.searchCourse);
                        //intent.PutExtra("gradeList", gradeListParam);
                        //this.mContext.StartActivity(intent);
                        //((FragmentActivity)this.mContext).OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);

                    };
            }

            #endregion
        }

        public override int ItemCount
        {
            get
            {
                if (this.dynamicGradeSumList != null)
                {
                    return this.dynamicGradeSumList.Count;
                }
                return 0;
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

    }
>>>>>>> e6d1f8b... 区域-累计
}
