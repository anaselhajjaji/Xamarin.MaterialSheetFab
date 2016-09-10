namespace Sample
{
    public class AllFragment : NotesListFragment
    {
        public static AllFragment NewInstance()
        {
            return new AllFragment();
        }

        protected override int GetLayoutResId()
        {
            return Resource.Layout.fragment_all;
        }

        protected override int GetNumColumns()
        {
            return 2;
        }

        protected override int GetNumItems()
        {
            return 20;
        }
    }
}
