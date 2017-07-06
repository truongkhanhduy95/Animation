using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Animation
{
    [Activity(Label = "UserProfileActivity", Theme = "@style/AppTheme.TransparentActivity")]
    public class UserProfileActivity : AppCompatActivity, OnStateChangeListener
    {
        public const string ARG_REVEAL_START_LOCATION = "reveal_start_location";

        private RevealBackgroundView vRevealBackground;
        private RecyclerView rvUserProfile;
        private TabLayout tlProfileTab;
        private LinearLayout vUserProfileRoot;
        private ImageView ivUserProfilePhoto;
        private View vUserStats;
        private View vUserDetails,rootView;

        private DecelerateInterpolator INTERPOLATOR = new DecelerateInterpolator();

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
            SetContentView(Resource.Layout.activity_user_profile);

			InitControls();
            InitTabs();

            SetupProfileGrid();
            SetupRevealBackground(savedInstanceState);
        }

        private void InitControls()
        {
            vRevealBackground = FindViewById<RevealBackgroundView>(Resource.Id.vRevealBackground);
            rvUserProfile = FindViewById<RecyclerView>(Resource.Id.rvUserProfile);
            tlProfileTab = FindViewById<TabLayout>(Resource.Id.tlUserProfileTabs);
            vUserProfileRoot = FindViewById<LinearLayout>(Resource.Id.vUserProfileRoot);
            vUserStats = FindViewById(Resource.Id.vUserStats);
            vUserDetails = FindViewById(Resource.Id.vUserDetails);
            ivUserProfilePhoto = FindViewById<ImageView>(Resource.Id.ivUserProfilePhoto);
            //rootView = FindViewById(Resource.Id.rootView);
        }

        private void InitTabs()
        {
            tlProfileTab.AddTab(tlProfileTab.NewTab().SetIcon(Resource.Drawable.ic_grid_on_white));
            tlProfileTab.AddTab(tlProfileTab.NewTab().SetIcon(Resource.Drawable.ic_list_white));
            tlProfileTab.AddTab(tlProfileTab.NewTab().SetIcon(Resource.Drawable.ic_place_white));
            tlProfileTab.AddTab(tlProfileTab.NewTab().SetIcon(Resource.Drawable.ic_label_white));
		}

		private void SetupProfileGrid()
        {
            StaggeredGridLayoutManager layoutManager = new StaggeredGridLayoutManager(3, StaggeredGridLayoutManager.Vertical);
            rvUserProfile.SetLayoutManager(layoutManager);
        }

        private void SetupRevealBackground(Bundle savedInstanceState)
        {
            vRevealBackground.SetOnStateChangeListener(this);
            if (savedInstanceState == null)
            {
				startingLocation = this.Intent.GetIntArrayExtra(ARG_REVEAL_START_LOCATION);
				predrawListener = new CustomPredrawListener(OnPreDrawListener);
                //vRevealBackground.ViewTreeObserver.PreDraw += ViewTreeObserver_PreDraw;
                //rootView.Visibility = ViewStates.Invisible;
                vRevealBackground.ViewTreeObserver.AddOnPreDrawListener(predrawListener);

            }
            else
            {
                vRevealBackground.SetToFinishFrame();
            }
        }

        private void OnPreDrawListener()
        {
            vRevealBackground.ViewTreeObserver.RemoveOnPreDrawListener(predrawListener);
            vRevealBackground.StartFromLocation(startingLocation);
        }

        public void OnStateChanged(AnimationState state)
        {
            if (state == AnimationState.Finished)
            {
                rvUserProfile.Visibility =ViewStates.Visible;
                tlProfileTab.Visibility = ViewStates.Visible;
                vUserProfileRoot.Visibility = ViewStates.Visible;
                vRevealBackground.RootView.Visibility = ViewStates.Visible;
                AnimateUserProfileOptions();
                AnimateUserProfileHeader();
            }
            else
            {
                rvUserProfile.Visibility = ViewStates.Invisible;
                tlProfileTab.Visibility = ViewStates.Invisible;
                vUserProfileRoot.Visibility = ViewStates.Invisible;
            }
        }

        private void AnimateUserProfileOptions()
        {
            tlProfileTab.TranslationY = -tlProfileTab.Height;
            tlProfileTab.Animate()
                        .TranslationY(0)
                        .SetDuration(300)
                        .SetStartDelay(300)
                        .SetInterpolator(INTERPOLATOR)
                        .Start();
        }

        private void AnimateUserProfileHeader()
        {
            vUserProfileRoot.TranslationY = -vUserProfileRoot.Height;
            ivUserProfilePhoto.TranslationY = -ivUserProfilePhoto.Height;
			vUserDetails.TranslationY = -vUserDetails.Height;
            vUserStats.Alpha = 0;

            vUserProfileRoot.Animate()
                            .TranslationY(0)
                            .SetDuration(300)
                            .SetInterpolator(INTERPOLATOR)
                            .Start();
			ivUserProfilePhoto.Animate()
                              .TranslationY(0)
                              .SetDuration(300)
                              .SetStartDelay(100)
                              .SetInterpolator(INTERPOLATOR)
                              .Start();
			vUserDetails.Animate()
                        .TranslationY(0)
                        .SetDuration(300)
                        .SetStartDelay(200)
                        .SetInterpolator(INTERPOLATOR)
                        .Start();
			vUserStats.Animate()
                      .Alpha(1)
                      .SetDuration(300)
                      .SetStartDelay(400)
                      .SetInterpolator(INTERPOLATOR)
                      .Start();
		}
    }

    public class CustomPredrawListener : Java.Lang.Object,ViewTreeObserver.IOnPreDrawListener
    {
        Action action;

        public CustomPredrawListener(Action action)
        {
            this.action = action;
        }

        public bool OnPreDraw()
        {
            action?.Invoke();
            return true;
        }
    }
}
