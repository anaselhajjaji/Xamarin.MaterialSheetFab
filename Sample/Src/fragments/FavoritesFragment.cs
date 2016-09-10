namespace Sample
{
    public class FavoritesFragment : NotesListFragment
    {
        public static FavoritesFragment NewInstance()
        {
            return new FavoritesFragment();
        }

        protected override int GetLayoutResId()
        {
            return Resource.Layout.fragment_favorites;
        }

        protected override int GetNumColumns()
        {
            return 1;
        }

        protected override int GetNumItems()
        {
            return 7;
        }
    }
}