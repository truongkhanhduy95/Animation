using System;
using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Animation
{
    
    public class RevealBackgroundView : View
    {

        private static AccelerateInterpolator INTERPOLATOR = new AccelerateInterpolator();
        private const int FILL_TIME = 400;

        private AnimationState State = AnimationState.NotStarted;

        private Paint fillPaint;
        private int currentRadius;
        private ObjectAnimator revealAnimator;

        private int startLocationX, startLocationY;

        private OnStateChangeListener onStateChanged;


        protected RevealBackgroundView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
		
        public RevealBackgroundView(Context context): base(context)
        {
            Initialize();
        }

		public RevealBackgroundView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Initialize();
		}

		public RevealBackgroundView(Context context, IAttributeSet attrs, int def) : base(context, attrs, def)
		{
			Initialize();
		}

		public RevealBackgroundView(Context context, IAttributeSet attrs, int def, int res) : base(context, attrs, def, res)
		{
			Initialize();
		}

        private void Initialize()
        {
            fillPaint = new Paint();
            fillPaint.SetStyle(Paint.Style.Fill);
            fillPaint.Color = Color.White;
        }

        public void StartFromLocation(int[] tapLocations)
        {
            State = AnimationState.FillStarted;
            startLocationX = tapLocations[0];
            startLocationY = tapLocations[1];
            revealAnimator = ObjectAnimator.OfInt(this, "currentRadius", 0, Width + Height);
            revealAnimator.SetDuration(FILL_TIME);
            revealAnimator.SetInterpolator(INTERPOLATOR);
            revealAnimator.AddListener(new CustomAnimation(()=> ChangeState(AnimationState.Finished)));
            revealAnimator.Start();
        }

        public void SetToFinishFrame()
        {
            ChangeState(AnimationState.Finished);
            Invalidate();
        }

        public void SerCurrentRadius(int radius)
        {
            this.currentRadius = radius;
            Invalidate();
        }

        public void ChangeState( AnimationState state)
        {
            if (this.State == state)
                return;

            this.State = state;
            if (onStateChanged != null)
            {
                onStateChanged.OnStateChanged(this.State);
            }

        }

        public void SetOnStateChangeListener(OnStateChangeListener onStateChangeListener)
        {
            this.onStateChanged = onStateChangeListener;
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (State == AnimationState.Finished)
            {
                canvas.DrawRect(0, 0, Width, Height, fillPaint);
            }
            else
            {
                canvas.DrawCircle(startLocationX, startLocationY, currentRadius, fillPaint);
            }
        }
    }

    public interface OnStateChangeListener
    {
        void OnStateChanged(AnimationState state);
    }

    public enum AnimationState
    {
        NotStarted,
        FillStarted,
        Finished
    }
}
