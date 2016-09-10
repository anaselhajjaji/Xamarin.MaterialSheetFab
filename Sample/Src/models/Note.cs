using System;
using System.Collections.Generic;

using Android.Content;
using Android.Text;

using Java.Util;
using Android.Text.Format;

namespace Sample
{
    public class Note
    {
        private static String[] ACTIONS_PEOPLE =
        { "call", "email", "meet up with",
            "hang out with"
        };
        private static String[] ACTIONS_OBJECTS = { "clean", "buy", "sell", "fix" };
        private static String[] NAMES =
        { "Sherry", "Gordon", "Tom", "Kevin", "Brian", "Naomi",
            "Ali", "Jennifer"
        };
        private static String[] OBJECTS = { "desk", "car", "motorcycle", "computer", "laptop" };
        private static String WORDS = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.";
        private static String[] CITIES =
        { "San Francisco", "Campbell", "Lincoln", "New York",
            "Silverton", "Scarface", "King Salmon"
        };

        private static String[] LIST_TITLES =
        { "shopping", "to bring", "on sale", "look for",
            "buy", "get rid of"
        };
        private static String[] LIST_DELIMITERS = { "•", "-" };
        private static String[] LIST_GROCERIES =
        { "almond milk", "coconut water", "cucumber",
            "green apples"
        };
        private static String[] LIST_CAMPING =
        { "lantern", "smores", "extra blankets",
            "warm socks", "first aid kit", "tent"
        };

        private static int NUM_WORDS = 4;
        private static int DATE_RANGE = 60;

        private String title;
        private String note;
        private String info;
	
        private int infoImage;
        private int color;

        private Note(String title, String note, String info, int infoImage, int color)
        {
            this.title = title;
            this.note = note;
            this.info = info;
            this.infoImage = infoImage;
            this.color = color;
        }

        public String GetTitle()
        {
            return title;
        }

        public void SetTitle(String title)
        {
            this.title = title;
        }

        public String GetNote()
        {
            return note;
        }

        public void SetNote(String note)
        {
            this.note = note;
        }

        public String GetInfo()
        {
            return info;
        }

        public void SetInfo(String info)
        {
            this.info = info;
        }

        public int GetInfoImage()
        {
            return infoImage;
        }

        public void SetInfoImage(int infoImage)
        {
            this.infoImage = infoImage;
        }

        public int GetColor()
        {
            return color;
        }

        public void SetColor(int color)
        {
            this.color = color;
        }

        public static Note RandomNote(Context context)
        {
            double rand = Java.Lang.Math.Random();
            String title = "";
            String note = "";
            NoteInfo info = new NoteInfo("", 0);
            int color = GetRandomColor(context);

            // Title only
            if (rand >= 0.65)
            {
                title = GetRandomActivity();
                if (Java.Lang.Math.Random() >= 0.7)
                {
                    info = GetRandomDate(context);
                }
            }
		// Title and note
		else if (rand >= 0.3)
            {
                title = GetRandomActivity();
                note = GetRandomWords();
                if (Java.Lang.Math.Random() >= 0.7)
                {
                    info = GetRandomInfo(context);
                }
            }
		// Lists
		else
            {
                title = GetRandomListTitle();
                note = GetRandomList();
                if (Java.Lang.Math.Random() >= 0.7)
                {
                    info = GetRandomLocation();
                }
            }

            return new Note(Capitalize(title), note, info.info, info.infoImage, color);
        }

        private static String GetRandomActivity()
        {
            if (Java.Lang.Math.Random() >= 0.5)
            {
                return GetRandomString(false, ACTIONS_PEOPLE) + " " + GetRandomString(false, NAMES);
            }
            else
            {
                return GetRandomString(false, ACTIONS_OBJECTS) + " " + GetRandomString(false, OBJECTS);
            }
        }

        private static String GetRandomWords()
        {
            int rand = (int)(Java.Lang.Math.Random() * NUM_WORDS) + 1;
            String words = "";
            for (int i = 0; i < rand; i++)
            {
                words += WORDS;
                if (i != rand - 1)
                {
                    words += " ";
                }
            }
            return words;
        }

        private static String GetRandomListTitle()
        {
            String title = GetRandomString(true, LIST_TITLES);
            if (!TextUtils.IsEmpty(title))
            {
                title += ":";
            }
            return title;
        }

        private static String GetRandomList()
        {
            String[] list = (String[])GetRandomItem(new Object[] { LIST_GROCERIES, LIST_CAMPING });
            String delimiter = GetRandomString(true, LIST_DELIMITERS);
            if (!TextUtils.IsEmpty(delimiter))
            {
                delimiter += " ";
            }
            String listStr = "";
            for (int i = 0; i < list.Length; i++)
            {
                listStr += delimiter + list[i];
                if (i != list.Length - 1)
                {
                    listStr += "\n";
                }
            }
            return listStr;
        }

        private static NoteInfo GetRandomInfo(Context context)
        {
            NoteInfo[] infos = new NoteInfo[] { GetRandomDate(context), GetRandomLocation() };
            return (NoteInfo)GetRandomItem(infos);
        }

        private static NoteInfo GetRandomDate(Context context)
        {
            Calendar cal = Calendar.GetInstance(Java.Util.TimeZone.Default);
            cal.Add(Calendar.Date, (int)(Java.Lang.Math.Random() * DATE_RANGE));
            String date = DateFormat.GetMediumDateFormat(context).Format(cal.Time);
            return new NoteInfo(date, Resource.Drawable.ic_event_white_24dp);
        }

        private static NoteInfo GetRandomLocation()
        {
            String location = GetRandomString(false, CITIES);
            return new NoteInfo(location, Resource.Drawable.ic_place_white_24dp);
        }

        private static int GetRandomColor(Context context)
        {
            int[] colors;
            if (Java.Lang.Math.Random() >= 0.6)
            {
                colors = context.Resources.GetIntArray(Resource.Array.note_accent_colors);
            }
            else
            {
                colors = context.Resources.GetIntArray(Resource.Array.note_neutral_colors);
            }
            return colors[((int)(Java.Lang.Math.Random() * colors.Length))];
        }

        private static String Capitalize(String str)
        {
            if (TextUtils.IsEmpty(str))
            {
                return str;
            }
            return Java.Lang.Character.ToUpperCase(str.ToCharArray()[0]) + str.Substring(1);
        }

        private static String GetRandomString(bool includeEmpty, String[] strings)
        {
            if (includeEmpty)
            {
                List<String> stringsWithEmpty = new List<String>(strings);
                stringsWithEmpty.Add("");
                return (String)GetRandomItem(stringsWithEmpty.ToArray());
            }
            return (String)GetRandomItem(strings);
        }

        private static Object GetRandomItem(Object[] objs)
        {
            return objs[((int)(Java.Lang.Math.Random() * objs.Length))];
        }

        private class NoteInfo
        {

            public String info;
            public int infoImage;

            public NoteInfo(String info, int infoImage)
            {
                this.info = info;
                this.infoImage = infoImage;
            }

        }
    }
}