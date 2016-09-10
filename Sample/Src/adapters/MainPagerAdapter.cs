using System;
using Android.Support.V4.App;
using Android.Content;
using Android.Runtime;

namespace Sample
{
    public class MainPagerAdapter : FragmentPagerAdapter
    {
        public override int Count
        {
            get
            {
                return NUM_ITEMS;
            }
        }

        public const int NUM_ITEMS = 3;
        public const int ALL_POS = 0;
        public const int SHARED_POS = 1;
        public const int FAVORITES_POS = 2;

        private Context context;

        public MainPagerAdapter(Context context, FragmentManager fm)
            : base(fm)
        {
            this.context = context;
        }

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case ALL_POS:
                    return AllFragment.NewInstance();
                case SHARED_POS:
                    return SharedFragment.NewInstance();
                case FAVORITES_POS:
                    return FavoritesFragment.NewInstance();
                default:
                    return null;
            }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            switch (position) {
                case ALL_POS:
                    return new Java.Lang.String(context.GetString(Resource.String.all));
                case SHARED_POS:
                    return new Java.Lang.String(context.GetString(Resource.String.shared));
                case FAVORITES_POS:
                    return new Java.Lang.String(context.GetString(Resource.String.favorites));
                default:
                    return new Java.Lang.String("");
            }
        }
    }

}

