using System;

using Android.Support.V7.Widget;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Text;

namespace Sample
{
    public class NotesAdapter : RecyclerView.Adapter
    {
        #region implemented abstract members of Adapter

        public override void OnBindViewHolder(RecyclerView.ViewHolder holderOrigin, int position)
        {
            Note noteModel = notes[position];
            String title = noteModel.GetTitle();
            String note = noteModel.GetNote();
            String info = noteModel.GetInfo();
            int infoImage = noteModel.GetInfoImage();
            int color = noteModel.GetColor();

            ViewHolder holder = holderOrigin as ViewHolder;

            // Set text
            holder.titleTextView.Text = title;
            holder.noteTextView.Text = note;
            holder.infoTextView.Text = info;

            // Set image
            if (infoImage != 0)
            {
                holder.infoImageView.SetImageResource(infoImage);
            }

            // Set visibilities
            holder.titleTextView.Visibility = TextUtils.IsEmpty(title) ? ViewStates.Gone : ViewStates.Visible;
            holder.noteTextView.Visibility = TextUtils.IsEmpty(note) ? ViewStates.Gone : ViewStates.Visible;
            holder.infoLayout.Visibility = TextUtils.IsEmpty(info) ? ViewStates.Gone : ViewStates.Visible;

            // Set padding
            int paddingTop = (holder.titleTextView.Visibility != ViewStates.Visible) ? 0
                : holder.itemView.Context.Resources
                .GetDimensionPixelSize(Resource.Dimension.note_content_spacing);
            holder.noteTextView.SetPadding(holder.noteTextView.PaddingLeft, paddingTop,
                holder.noteTextView.PaddingRight, holder.noteTextView.PaddingBottom);

            // Set background color
            ((CardView)holder.itemView).SetCardBackgroundColor(color);
        }

        public override int ItemCount
        {
            get
            {
                return notes.Length;
            }
        }

        #endregion

        private Note[] notes;

        public NotesAdapter(Context context, int numNotes)
        {
            notes = GenerateNotes(context, numNotes);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_note, parent,
                         false);
            return new ViewHolder(v);
        }

        private Note[] GenerateNotes(Context context, int numNotes)
        {
            Note[] notes = new Note[numNotes];
            for (int i = 0; i < notes.Length; i++)
            {
                notes[i] = Note.RandomNote(context);
            }
            return notes;
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView titleTextView;
            public TextView noteTextView;
            public LinearLayout infoLayout;
            public TextView infoTextView;
            public ImageView infoImageView;
            public View itemView;

            public ViewHolder(View itemView)
                : base(itemView)
            {
                titleTextView = (TextView)itemView.FindViewById(Resource.Id.note_title);
                noteTextView = (TextView)itemView.FindViewById(Resource.Id.note_text);
                infoLayout = (LinearLayout)itemView.FindViewById(Resource.Id.note_info_layout);
                infoTextView = (TextView)itemView.FindViewById(Resource.Id.note_info);
                infoImageView = (ImageView)itemView.FindViewById(Resource.Id.note_info_image);
                this.itemView = itemView;
            }
        }
    }
}