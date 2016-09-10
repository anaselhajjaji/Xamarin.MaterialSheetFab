using Android.Support.V4.App;
using Android.Views;
using Android.OS;
using Android.Support.V7.Widget;

namespace Sample
{
    public abstract class NotesListFragment : Fragment
    {
        protected abstract int GetLayoutResId();

        protected abstract int GetNumColumns();

        protected abstract int GetNumItems();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(GetLayoutResId(), container, false);

            // Setup list
            RecyclerView recyclerView = (RecyclerView)view.FindViewById(Resource.Id.notes_list);
            recyclerView.SetLayoutManager(new StaggeredGridLayoutManager(GetNumColumns(),
                    StaggeredGridLayoutManager.Vertical));
            recyclerView.SetAdapter(new NotesAdapter(Activity, GetNumItems()));

            return view;
        }
    }
}