using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace Animation
{
    [Activity(Label = "UserProfileActivity")]
    public class UserProfileActivity : AppCompatActivity, OnStateChangeListener
    {
        public const string ARG_REVEAL_START_LOCATION = "reveal_start_location";

        private RevealBackgroundView vRevealBackground;
        private RecyclerView rvUserProfile;

        private CustomPredrawListener predrawListener;
        private int[] startingLocation;

        public static void StartUserProfileReveal(int[] startingLocations, Activity activity)
        {
            Intent intent = new Intent(activity, typeof(UserProfileActivity));
            intent.PutExtra(ARG_REVEAL_START_LOCATION,startingLocations);
            activity.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTheme(Resource.Style.AppTheme);
            SetContentView(Resource.Layout.activity_user_profile);


			InitControls();

            SetupProfileGrid();
            SetupRevealBackground();
        }

        private void InitControls()
        {
            vRevealBackground = FindViewById<RevealBackgroundView>(Resource.Id.vRevealBackground);
            rvUserProfile = FindViewById<RecyclerView>(Resource.Id.rvUserProfile);
        }

		private void SetupProfileGrid()
        {
            StaggeredGridLayoutManager layoutManager = new StaggeredGridLayoutManager(3, StaggeredGridLayoutManager.Vertical);
            rvUserProfile.SetLayoutManager(layoutManager);
        }

        private void SetupRevealBackground()
        {
            vRevealBackground.SetOnStateChangeListener(this);
            startingLocation = this.Intent.GetIntArrayExtra(ARG_REVEAL_START_LOCATION);
            predrawListener = new CustomPredrawListener(aaa);
            vRevealBackground.ViewTreeObserver.PreDraw += (sender, e) => 
            {
               vRevealBackground.StartFromLocation(startingLocation);
            };
        }

        private void aaa()
        {
            vRevealBackground.ViewTreeObserver.RemoveOnPreDrawListener(predrawListener);

        }

        public void OnStateChanged(AnimationState state)
        {
            if (state == AnimationState.Finished)
            {
                rvUserProfile.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                rvUserProfile.Visibility = Android.Views.ViewStates.Invisible;
            }
        }
    }

    public class CustomPredrawListener : ViewTreeObserver.IOnPreDrawListener
    {
        Action action;

        public CustomPredrawListener(Action action)
        {
            this.action = action;
        }

        public IntPtr Handle => new IntPtr();

        public void Dispose()
        {
           
        }

        public bool OnPreDraw()
        {
            action?.Invoke();
            return true;
        }
    }
}
