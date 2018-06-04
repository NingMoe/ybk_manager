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
	public class NewStudentAdapter : RecyclerView.Adapter
	{
		private RecyclerView m_RecyclerView;
		private Context mContext;

		private List<NewStudentSumAreaEntity> newStudentList;
		private decimal avgGrowthRate;

		//累计：1-人次；2-预收；3-行课
		//public int dataType = 1;
		//private QuarterEntity searchQuarter;
		//private string searchCourse = "全部科目";
		//private List<string> searchGradeList = new List<string>();

		RecyclerView mRecyclerView;

		public NewStudentAdapter(Context context, List<NewStudentSumAreaEntity> newStudentList, decimal avgGrowthRate, RecyclerView mRecyclerView)
		{
			this.mContext = context;
            this.newStudentList = newStudentList;
			this.avgGrowthRate = avgGrowthRate;
			this.mRecyclerView = mRecyclerView;
		}

		public void SetData(List<NewStudentSumAreaEntity> data, decimal avgGrowthRate)
		{
            this.newStudentList = data;
			this.avgGrowthRate = avgGrowthRate;

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
			var item = newStudentList[position];

			// 是否折叠
			if (item.IsFold)
			{
				holderX.mRecyclerView.Visibility = ViewStates.Gone;
			}
			else
			{
				holderX.mRecyclerView.Visibility = ViewStates.Visible;

				if (item.GradeData.Any())
				{
					LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this.mContext);
					NewStudentAdapter2 mAdapter = new NewStudentAdapter2(this.mContext, item.GradeData, this.avgGrowthRate);
					holderX.mRecyclerView.SetLayoutManager(linearLayoutManager);
					holderX.mRecyclerView.SetAdapter(mAdapter);
				}
			}

			var itemInfo = this.newStudentList[position];

			holderX.tv_name.Text = itemInfo.Name;
			holderX.tv_total.Text = itemInfo.Total.ToString("f1");
			holderX.tv_new.Text = itemInfo.StudentCount.ToString("f1");
			holderX.tv_rate.Text = (itemInfo.Rate * 100).ToString("f1") + "%";

			// 比平均值
			var avgrate = itemInfo.Rate - this.avgGrowthRate;
			if (avgrate >= 0)
			{
				holderX.tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
			}
			else
			{
				holderX.tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
			}

			if (position == this.newStudentList.Count - 1)
			{
				holderX.tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				holderX.tv_total.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				holderX.tv_new.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
				holderX.tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorPrimary)));
			}
			else
			{
				holderX.tv_name.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
				holderX.tv_total.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
				holderX.tv_new.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorSecond)));
			}

			if (!holderX.ll_item_wrap.HasOnClickListeners)
			{
				holderX.ll_item_wrap.Click += itemTitle_Click;
			}
		}

		private void itemTitle_Click(object sender, EventArgs e)
		{
			var aa = ((View)sender).Parent as ViewGroup;
			int position = mRecyclerView.GetChildAdapterPosition(aa);
			if (position > -1)
			{

                this.newStudentList[position].IsFold = !this.newStudentList[position].IsFold;
				NotifyDataSetChanged();
			}
		}

		public override int ItemCount
		{
			get
			{
				if (this.newStudentList != null)
				{
					return this.newStudentList.Count;
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
			public TextView tv_name, tv_total, tv_new, tv_rate;
			public RecyclerView mRecyclerView;

			public ItemViewHolder(View itemView) : base(itemView)
			{
				tv_name = (TextView)itemView.FindViewById(Resource.Id.tv_sum_name);
				tv_total = (TextView)itemView.FindViewById(Resource.Id.tv_sum_currentsum);
				tv_new = (TextView)itemView.FindViewById(Resource.Id.tv_sum_lastyearsum);
				tv_rate = (TextView)itemView.FindViewById(Resource.Id.tv_sum_growthrate);

				mRecyclerView = (RecyclerView)itemView.FindViewById(Resource.Id.item_recycler_view);
				ll_item_wrap = (LinearLayout)itemView.FindViewById(Resource.Id.ll_item_wrap);
			}
		}
	}

	public class NewStudentAdapter2 : RecyclerView.Adapter
	{
		private RecyclerView m_RecyclerView;
		private Context mContext;

		private List<NewStudentSumBaseEntity> dynamicGradeSumList;
		private decimal avgGrowthRate;

		public NewStudentAdapter2(Context context, List<NewStudentSumBaseEntity> gradeSumList, decimal avgGrowthRate)
		{
			this.mContext = context;
			this.dynamicGradeSumList = gradeSumList;
			this.avgGrowthRate = avgGrowthRate;

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
			holderX.tv_total.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
			holderX.tv_new.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);
			holderX.tv_rate.SetTextSize(Android.Util.ComplexUnitType.Dip, 12);

			holderX.tv_name.Text = itemInfo.Name;
				holderX.tv_total.Text = itemInfo.Total.ToString("f1");
				holderX.tv_new.Text = itemInfo.StudentCount.ToString("f1");
			holderX.tv_rate.Text = (itemInfo.Rate * 100).ToString("f1") + "%";

			// 比平均值
			var avgrate = itemInfo.Rate - this.avgGrowthRate;
			if (avgrate >= 0)
			{
				holderX.tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorHigh)));
			}
			else
			{
				holderX.tv_rate.SetTextColor(new Color(ContextCompat.GetColor(mContext, Resource.Color.textColorRed)));
			}

			if (!holder.ItemView.HasOnClickListeners)
			{
				//holder.ItemView.Click += (sender, e) =>
				//	{
					
				//		var intent = new Intent(this.mContext, typeof(SumByTeacherActivity));
				//		intent.PutExtra("year", this.searchQuarter.Year);
				//		intent.PutExtra("quarter", this.searchQuarter.Quarter);
				//		intent.PutExtra("dataType", this.dataType);
				//		intent.PutExtra("areaCode", this.areaCode);
				//		intent.PutExtra("areaName", areaName);
				//		intent.PutExtra("course", this.searchCourse);
				//		intent.PutExtra("gradeList", itemInfo.Name);
				//		this.mContext.StartActivity(intent);
				//		((FragmentActivity)this.mContext).OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);

				//	};
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
			public TextView tv_name, tv_total, tv_new, tv_rate;


			public ItemViewHolder(View itemView) : base(itemView)
			{
				tv_name = (TextView)itemView.FindViewById(Resource.Id.tv_sum_name);
				tv_total = (TextView)itemView.FindViewById(Resource.Id.tv_sum_currentsum);
				tv_new = (TextView)itemView.FindViewById(Resource.Id.tv_sum_lastyearsum);
				tv_rate = (TextView)itemView.FindViewById(Resource.Id.tv_sum_growthrate);
			}
		}

	}
}
