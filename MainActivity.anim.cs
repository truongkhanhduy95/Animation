using System;
using Android.Animation;
using Android.Views.Animations;
using Xamarin.Core.Droid;

namespace Animation
{
    public partial class MainActivity
    {
		private void StartIntroAnimation()
		{
			btnCreate.TranslationY = 2 * Resources.GetDimensionPixelOffset(Resource.Dimension.btn_fab_size);

			int actionbarSize = PixelConveter.FromDp(this, 56);
			toolbar.TranslationY = -actionbarSize;
			ivLogo.TranslationY = -actionbarSize;
			inboxMenu.ActionView.TranslationY = -actionbarSize;

			toolbar.Animate()
				   .TranslationY(0)
				   .SetDuration(300)
				   .SetStartDelay(300);
			ivLogo.Animate()
				   .TranslationY(0)
				   .SetDuration(300)
				   .SetStartDelay(400);
			inboxMenu.ActionView.Animate()
				   .TranslationY(0)
				   .SetDuration(300)
				   .SetStartDelay(500)
					 .SetListener(new CustomAnimation(StartContentAnimation))
					 .Start();
		}

		private void StartContentAnimation()
		{
			btnCreate.Animate()
					 .TranslationY(0)
					 .SetInterpolator(new OvershootInterpolator(1))
					 .SetStartDelay(300)
					 .SetDuration(400)
					 .Start();
			feedAdapter.UpdateItem();
		}

	}

	public class CustomAnimation : AnimatorListenerAdapter
	{
		Action action;
		public CustomAnimation(Action action)
		{
			this.action = action;
		}

		public override void OnAnimationEnd(Animator animation)
		{
			action?.Invoke();
		}
	}

}
