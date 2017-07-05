package md5b83d28f32ddb335f2e01257f4ee216a8;


public class CustomAnimation
	extends android.animation.AnimatorListenerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationEnd:(Landroid/animation/Animator;)V:GetOnAnimationEnd_Landroid_animation_Animator_Handler\n" +
			"";
		mono.android.Runtime.register ("Animation.CustomAnimation, Animation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomAnimation.class, __md_methods);
	}


	public CustomAnimation () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CustomAnimation.class)
			mono.android.TypeManager.Activate ("Animation.CustomAnimation, Animation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationEnd (android.animation.Animator p0)
	{
		n_onAnimationEnd (p0);
	}

	private native void n_onAnimationEnd (android.animation.Animator p0);

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
