
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using YbkManage.Adapters;
using YbkManage.Fragments;

namespace YbkManage
{
	/// <summary>
	/// 区域-招新
	/// </summary>
	public class NewStudentFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		#region UIField
		private LayoutInflater layoutInflater;
		#endregion


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_increase, container, false);


			return view;
		}
		public void OnClick(View v)
		{
			throw new NotImplementedException();
		}

		public void OnItemClick(View itemView, int position)
		{
			throw new NotImplementedException();
		}

		public void OnItemLongClick(View itemView, int position)
		{
			throw new NotImplementedException();
		}

		public void OnRefresh()
		{
			throw new NotImplementedException();
		}


	}
}
