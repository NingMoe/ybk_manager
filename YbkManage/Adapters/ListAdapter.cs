using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace YbkManage.Adapters
{
    public class ListAdapter : RecyclerView.Adapter
    {
        private static int TYPE_ITEM_ITEM = 1;
        private static int TYPE_ITEM_FOOTER = 2;
        string[] items;

        public ListAdapter(string[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var vi = LayoutInflater.From(parent.Context);
            if (viewType == TYPE_ITEM_FOOTER)
            {
                return new FootViewHolder(vi.Inflate(Resource.Layout.listview_footer, parent, false));
            }
            else if (viewType == TYPE_ITEM_ITEM)
            {
                return new ItemViewHolder(vi.Inflate(Resource.Layout.drawer_list_item, parent, false));
            }
            return null;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            if (viewHolder is ItemViewHolder)
            {

                var words = items[position];

                ((ItemViewHolder)viewHolder).TV_Name.SetText(words, TextView.BufferType.Normal);

                if (this.onItemClickListener != null)
                {
                    //((ItemViewHolder)viewHolder).
                }
            }
        }

        public override int GetItemViewType(int position)
        {
            if (items != null && ItemCount == (position + 1))
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
                return items.Length;
            }
        }



        public class ItemViewHolder : RecyclerView.ViewHolder
        {
            public TextView TV_Name, TV_Count1, TV_Count2, TV_Rate;


            public ItemViewHolder(View itemView) : base(itemView)
            {
                TV_Name = (TextView)itemView.FindViewById(Resource.Id.tv_name);
                TV_Count1 = (TextView)itemView.FindViewById(Resource.Id.tv_num1);
                TV_Count2 = (TextView)itemView.FindViewById(Resource.Id.tv_num2);
                TV_Rate = (TextView)itemView.FindViewById(Resource.Id.tv_rate);
            }
        }

        class FootViewHolder : RecyclerView.ViewHolder
        {
            public FootViewHolder(View itemView) : base(itemView)
            {

            }
        }

        public interface OnItemClickListener
        {
            void onItemClick(View view, int position);

            void onItemLongClick(View view, int position);
        }

        private OnItemClickListener onItemClickListener;
        public void setOnItemClickListener(OnItemClickListener listener)
        {
            this.onItemClickListener = listener;
        }
    }
}
