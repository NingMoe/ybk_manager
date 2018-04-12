
using System;
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using Java.Lang;

namespace YbkManage
{
	/// <summary>
	/// Expandable list adapter.
	/// </summary>
	public class ExpandableListAdapter : BaseExpandableListAdapter
	{
		Activity activity;

		private List<string> groupList;
		private List<List<string>> childList;

		public ExpandableListAdapter(Activity activity, List<string> groupList, List<List<string>> childList)
		{
			this.activity = activity;

			this.groupList = groupList;
			this.childList = childList;
		}


		public override int GroupCount
		{
			get
			{
				return groupList.Count;
			}
		}

		public override bool HasStableIds
		{
			get
			{
				return true;
			}
		}

		public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
		{
			return childList[groupPosition][childPosition];
		}

		public override long GetChildId(int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override int GetChildrenCount(int groupPosition)
		{
			return childList[groupPosition].Count;
		}

		public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{

			var textView = getGenericView();
			textView.SetText(GetChild(groupPosition, childPosition).ToString(), TextView.BufferType.Normal);
			return textView;
		}

		public TextView getGenericView()
		{
			var lp = new AbsListView.LayoutParams(ViewGroup.LayoutParams.MatchParent, 64);

			var textView = new TextView(activity.ApplicationContext);
			textView.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Black));
			textView.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);
			textView.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;
			textView.SetPadding(36, 0, 0, 0);

			return textView;
		}

		public override Java.Lang.Object GetGroup(int groupPosition)
		{
			return groupList[groupPosition];
		}

		public override long GetGroupId(int groupPosition)
		{
			return groupPosition;
		}

		public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			var textView = getGenericView();
			textView.SetText(GetGroup(groupPosition).ToString(), TextView.BufferType.Normal);
			return textView;
		}

		public override bool IsChildSelectable(int groupPosition, int childPosition)
		{
			return true;
		}
	}
}
