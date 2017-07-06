//using System;
//using System.Collections.Generic;
//using Android.Content;
//using Android.Support.V7.Widget;
//using Android.Views;
//using Android.Views.Animations;
//using Android.Widget;

//namespace Animation
//{
//	public class UserProfileAdapter : RecyclerView.Adapter
//	{
//        private const int PHOTO_ANIMATE_DELAY = 900;
//        private DecelerateInterpolator INTERPOLATOR = new DecelerateInterpolator();

//		private Context context;
//        private int cellSize;

//        private List<string> photos;
//        private int lastAnimation = -1;

//		public UserProfileAdapter(Context context)
//		{
//			this.context = context;
//            photos = context.Resources.GetStringArray()
//		}

//		public override int ItemCount => itemsCount;

//		private void RunEnterAnimation(View view, int position)
//		{
//			if (position >= ANIMATED_ITEMS_COUNT - 1)
//			{
//				return;
//			}

//			if (position > lastAnimatedPosition)
//			{
//				lastAnimatedPosition = position;
//				view.TranslationY = context.Resources.DisplayMetrics.HeightPixels;
//				view.Animate()
//					.TranslationY(0)
//					.SetInterpolator(new DecelerateInterpolator(3))
//					.SetDuration(700)
//					.Start();
//			}
//		}

//		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
//		{
//			RunEnterAnimation(holder.ItemView, position);
//			UserProfileViewHolder view = (UserProfileViewHolder)holder;
//			if (position % 2 == 0)
//			{
//				view.ivFeedCenter.SetImageResource(Resource.Drawable.img_feed_center_1);
//				view.ivFeedBottom.SetImageResource(Resource.Drawable.img_feed_bottom_1);
//			}
//			else
//			{
//				view.ivFeedCenter.SetImageResource(Resource.Drawable.img_feed_center_2);
//				view.ivFeedBottom.SetImageResource(Resource.Drawable.img_feed_bottom_2);
//			}
//		}

//		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
//		{
//			View view = LayoutInflater.From(context).Inflate(Resource.Layout.item_feed, parent, false);
//			return new UserProfileViewHolder(view);
//		}

//		public void UpdateItem()
//		{
//			itemsCount = 10;
//			NotifyDataSetChanged();
//		}
//	}

//	public class UserProfileViewHolder : RecyclerView.ViewHolder
//	{
//		public ImageView ivFeedCenter, ivFeedBottom;

//		public UserProfileViewHolder(View view) : base(view)
//		{
//			ivFeedCenter = view.FindViewById<ImageView>(Resource.Id.ivFeedCenter);
//			ivFeedBottom = view.FindViewById<ImageView>(Resource.Id.ivFeedBottom);
//		}
//	}

//}
