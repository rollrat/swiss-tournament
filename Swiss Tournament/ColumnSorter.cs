namespace Swiss_Tournament
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class ColumnSorter
    {
        public static void ColumnClickEvent(object sender, ColumnClickEventArgs e)
        {
            ListView view = sender as ListView;
            ColHeader header = (ColHeader) view.Columns[e.Column];
            header.ascending = !header.ascending;
            int count = view.Items.Count;
            view.BeginUpdate();
            ArrayList list = new ArrayList();
            int num2 = 0;
            num2 = 0;
            while (true)
            {
                if (num2 > (count - 1))
                {
                    list.Sort(0, list.Count, new SortWrapper.SortComparer(header.ascending));
                    view.Items.Clear();
                    int num3 = 0;
                    num3 = 0;
                    while (true)
                    {
                        if (num3 > (count - 1))
                        {
                            view.EndUpdate();
                            return;
                        }
                        view.Items.Add(((SortWrapper) list[num3]).sortItem);
                        num3++;
                    }
                }
                list.Add(new SortWrapper(view.Items[num2], e.Column));
                num2++;
            }
        }

        public static void ColumnToColHeader(ListView lv)
        {
            List<ColHeader> list = new List<ColHeader>();
            foreach (ColumnHeader header in lv.Columns)
            {
                list.Add(new ColHeader(header.Text, header.Width, header.TextAlign, true));
            }
            lv.Columns.Clear();
            lv.Columns.AddRange(list.ToArray());
        }

        public static int ComparePath(string addr1, string addr2) => 
            StrCmpLogicalW(addr1, addr2);

        public static void InitListView(ListView lv)
        {
            ColumnToColHeader(lv);
            lv.ColumnClick += new ColumnClickEventHandler(ColumnSorter.ColumnClickEvent);
        }

        [DllImport("shlwapi.dll", CharSet=CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);

        public class ColHeader : ColumnHeader
        {
            public bool ascending;

            public ColHeader(string text, int width, HorizontalAlignment align, bool asc)
            {
                base.Text = text;
                base.Width = width;
                base.TextAlign = align;
                this.ascending = asc;
            }
        }

        public class SortWrapper
        {
            internal ListViewItem sortItem;
            internal int sortColumn;

            public SortWrapper(ListViewItem Item, int iColumn)
            {
                this.sortItem = Item;
                this.sortColumn = iColumn;
            }

            public string Text =>
                this.sortItem.SubItems[this.sortColumn].Text;

            public class SortComparer : IComparer
            {
                private bool ascending;

                public SortComparer(bool asc)
                {
                    this.ascending = asc;
                }

                public int Compare(object x, object y)
                {
                    int num;
                    if ((x == null) | (y == null))
                    {
                        num = 0;
                    }
                    else
                    {
                        ColumnSorter.SortWrapper wrapper = (ColumnSorter.SortWrapper) x;
                        ColumnSorter.SortWrapper wrapper2 = (ColumnSorter.SortWrapper) y;
                        num = ColumnSorter.ComparePath(wrapper.sortItem.SubItems[wrapper.sortColumn].Text, wrapper2.sortItem.SubItems[wrapper2.sortColumn].Text) * (this.ascending ? 1 : -1);
                    }
                    return num;
                }
            }
        }
    }
}

