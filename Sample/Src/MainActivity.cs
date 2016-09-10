using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Views;

using Com.Gordonwong.Materialsheetfab;

namespace Sample
{
    [Activity(MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : AppCompatActivity, View.IOnClickListener, ViewPager.IOnPageChangeListener
    {
        private ActionBarDrawerToggle drawerToggle;
        private DrawerLayout drawerLayout;
        private MaterialSheetFab materialSheetFab;
        public int statusBarColor;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetTitle(Resource.String.notes);
            SetContentView(Resource.Layout.activity_main);
            SetupActionBar();
            SetupDrawer();
            SetupFab();
            SetupTabs();
        }

        protected override void OnPostCreate(Bundle savedInstanceState) {
            base.OnPostCreate(savedInstanceState);
            drawerToggle.SyncState();
        }

        public override void OnBackPressed() {
            if (materialSheetFab.IsSheetVisible) {
                materialSheetFab.HideSheet();
            } else {
                base.OnBackPressed();
            }
        }

        private void SetupActionBar() {
            SetSupportActionBar((Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        private void SetupDrawer() {
            drawerLayout = (DrawerLayout) FindViewById(Resource.Id.drawer_layout);
            drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, Resource.String.opendrawer,
                Resource.String.closedrawer);
            drawerLayout.SetDrawerListener(drawerToggle);
        }

        private void SetupTabs() {
            // Setup view pager
            ViewPager viewpager = (ViewPager) FindViewById(Resource.Id.viewpager);
            viewpager.Adapter = new MainPagerAdapter(this, SupportFragmentManager);
            viewpager.OffscreenPageLimit = MainPagerAdapter.NUM_ITEMS;
            UpdatePage(viewpager.CurrentItem);

            // Setup tab layout
            TabLayout tabLayout = (TabLayout) FindViewById(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewpager);
            viewpager.AddOnPageChangeListener(this);
        }

        private void SetupFab() {

            Fab fab = (Fab) FindViewById(Resource.Id.fab);
            View sheetView = FindViewById(Resource.Id.fab_sheet);
            View overlay = FindViewById(Resource.Id.overlay);
            int sheetColor = Resources.GetColor(Resource.Color.background_card);
            int fabColor = Resources.GetColor(Resource.Color.theme_accent);

            // Create material sheet FAB
            materialSheetFab = new MaterialSheetFab(fab, sheetView, overlay, sheetColor, fabColor);

            // Set material sheet event listener
            materialSheetFab.SetEventListener(new FabListener(this));

            // Set material sheet item click listeners
            FindViewById(Resource.Id.fab_sheet_item_recording).SetOnClickListener(this);
            FindViewById(Resource.Id.fab_sheet_item_reminder).SetOnClickListener(this);
            FindViewById(Resource.Id.fab_sheet_item_photo).SetOnClickListener(this);
            FindViewById(Resource.Id.fab_sheet_item_note).SetOnClickListener(this);
        }

        private void UpdatePage(int selectedPage) {
            UpdateFab(selectedPage);
            UpdateSnackbar(selectedPage);
        }

        private void UpdateFab(int selectedPage) {
            switch (selectedPage) {
                case MainPagerAdapter.ALL_POS:
                    materialSheetFab.ShowFab();
                    break;
                case MainPagerAdapter.SHARED_POS:
                    materialSheetFab.ShowFab(0,
                        -Resources.GetDimensionPixelSize(Resource.Dimension.snackbar_height));
                    break;
                case MainPagerAdapter.FAVORITES_POS:
                default:
                    materialSheetFab.HideSheetThenFab();
                    break;
            }
        }

        private void UpdateSnackbar(int selectedPage) {
            View snackbar = FindViewById(Resource.Id.snackbar);
            switch (selectedPage) {
                case MainPagerAdapter.SHARED_POS:
                    snackbar.Visibility = ViewStates.Visible;
                    break;
                case MainPagerAdapter.ALL_POS:
                case MainPagerAdapter.FAVORITES_POS:
                default:
                    snackbar.Visibility = ViewStates.Gone;
                    break;
            }
        }

        private void ToggleDrawer() {
            if (drawerLayout.IsDrawerVisible(GravityCompat.Start)) {
                drawerLayout.CloseDrawer(GravityCompat.Start);
            } else {
                drawerLayout.OpenDrawer(GravityCompat.Start);
            }
        }

        public void OnClick(View v) {
            Toast.MakeText(this, Resource.String.sheet_item_pressed, ToastLength.Short).Show();
            materialSheetFab.HideSheet();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Android.Resource.Id.Home:
                    ToggleDrawer();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private int GetStatusBarColor() {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop) {
                return Window.StatusBarColor;
            }
            return 0;
        }

        private void SetStatusBarColor(int color) {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop) {
                Window.SetStatusBarColor(new Android.Graphics.Color(color));
            }
        }

        public void OnPageScrolled(int i, float v, int i1) {
        }

        public void OnPageSelected(int i) {
            UpdatePage(i);
        }

        public void OnPageScrollStateChanged(int i) {
        }

        class FabListener : MaterialSheetFabEventListener {

            MainActivity rootActivity;

            public FabListener(MainActivity rootActivity) {
                this.rootActivity = rootActivity;
            }

            public override void OnShowSheet() {
                // Save current status bar color
                rootActivity.statusBarColor = rootActivity.GetStatusBarColor();
                // Set darker status bar color to match the dim overlay
                rootActivity.SetStatusBarColor(rootActivity.Resources.GetColor(Resource.Color.theme_primary_dark2));
            }

            public override void OnHideSheet() {
                // Restore status bar color
                rootActivity.SetStatusBarColor(rootActivity.statusBarColor);
            }
        }
    }
}


