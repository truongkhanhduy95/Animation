using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Animation
{
    public class FeedAdapter : RecyclerView.Adapter
    {
        private const int ANIMATED_ITEMS_COUNT = 2;

		private Context context;
		private int lastAnimatedPosition = -1;
		private int itemsCount = 0;

		public FeedAdapter(Context context)
		{
			this.context = context;
		}

        public override int ItemCount => itemsCount;

        private void RunEnterAnimation(View view, int position)
        {
			if (position >= ANIMATED_ITEMS_COUNT - 1)
			{
				return;
			}

			if (position > lastAnimatedPosition)
			{
				lastAnimatedPosition = position;
                view.TranslationY = context.Resources.DisplayMetrics.HeightPixels;
                view.Animate()
                    .TranslationY(0)
                    .SetInterpolator(new DecelerateInterpolator(3))
                    .SetDuration(700)
                    .Start();
			}
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RunEnterAnimation(holder.ItemView,position);
			CellFeedViewHolder view = (CellFeedViewHolder)holder;
			if (position % 2 == 0)
			{
                view.ivFeedCenter.SetImageResource(Resource.Drawable.img_feed_center_1);
				view.ivFeedBottom.SetImageResource(Resource.Drawable.img_feed_bottom_1);
			}
			else
			{
				view.ivFeedCenter.SetImageResource(Resource.Drawable.img_feed_center_2);
				view.ivFeedBottom.SetImageResource(Resource.Drawable.img_feed_bottom_2);
			}
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.item_feed, parent, false);
            return new CellFeedViewHolder(view);
        }

        public void UpdateItem()
        {
            itemsCount = 10;
            NotifyDataSetChanged();
        }

		public class CellFeedViewHolder : RecyclerView.ViewHolder
		{
			public ImageView ivFeedCenter, ivFeedBottom;

            public CellFeedViewHolder(View view) : base(view)
            {
                ivFeedCenter = view.FindViewById<ImageView>(Resource.Id.ivFeedCenter);
                ivFeedBottom = view.FindViewById<ImageView>(Resource.Id.ivFeedBottom);
            }
		}
    }


}
