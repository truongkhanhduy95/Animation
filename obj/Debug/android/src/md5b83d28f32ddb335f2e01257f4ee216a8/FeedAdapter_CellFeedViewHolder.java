package md5b83d28f32ddb335f2e01257f4ee216a8;


public class FeedAdapter_CellFeedViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Animation.FeedAdapter+CellFeedViewHolder, Animation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FeedAdapter_CellFeedViewHolder.class, __md_methods);
	}


	public FeedAdapter_CellFeedViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == FeedAdapter_CellFeedViewHolder.class)
			mono.android.TypeManager.Activate ("Animation.FeedAdapter+CellFeedViewHolder, Animation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
