using System;
using Android.Animation;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Xamarin.Core.Droid;

namespace Animation
{
    [Activity(Label = "Animation", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public partial class MainActivity : AppCompatActivity
    {
        private bool pendingIntoAnimation;
        private Toolbar toolbar;
        private Android.Widget.ImageView ivLogo;
        private RecyclerView rvFeed;
        private IMenuItem inboxMenu;
        private Android.Widget.ImageButton btnCreate;
        private FeedAdapter feedAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            if (savedInstanceState == null)
            {
                pendingIntoAnimation = true;
            }

            InitControls();
            InitEvents();

            SetUpToolbar();
            SetUpFeeds();
        }

        private void InitControls()
        {
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            rvFeed = FindViewById<RecyclerView>(Resource.Id.rvFeed);
            ivLogo = FindViewById<Android.Widget.ImageView>(Resource.Id.ivLogo);
            btnCreate = FindViewById<Android.Widget.ImageButton>(Resource.Id.btnCreate);
        }

        private void InitEvents()
        {
            btnCreate.Click += (sender, e) => 
            {
                int[] locations = new int[2];
                btnCreate.GetLocationOnScreen(locations);
                locations[0] += btnCreate.Width / 2;
                UserProfileActivity.StartUserProfileReveal(locations,this);
                OverridePendingTransition(0,0);
            };
        }

        private void SetUpFeeds()
        {
            LinearLayoutManager layoutManager = new LinearLayoutManager(this);

            rvFeed.SetLayoutManager(layoutManager);
            feedAdapter = new FeedAdapter(this);
            rvFeed.SetAdapter(feedAdapter);
        }

        private void SetUpToolbar()
        {

            toolbar.SetNavigationIcon(Resource.Drawable.ic_menu_white);
            SetSupportActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            inboxMenu = menu.FindItem(Resource.Id.action_inbox);
            inboxMenu.SetActionView(Resource.Layout.menu_item_view);
            if (pendingIntoAnimation)
            {
                pendingIntoAnimation = false;
                StartIntroAnimation();
            }
            return true;
        }
    }
}

