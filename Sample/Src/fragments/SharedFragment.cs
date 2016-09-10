namespace Sample
{
    public class SharedFragment : NotesListFragment
    {
        public static SharedFragment NewInstance()
        {
            return new SharedFragment();
        }

        protected override int GetLayoutResId()
        {
            return Resource.Layout.fragment_shared;
        }

        protected override int GetNumColumns()
        {
            return 2;
        }

        protected override int GetNumItems()
        {
            return 10;
        }
    }
}