namespace Swiss_Tournament
{
    using Swiss_Tournament.Core;
    using Swiss_Tournament.DB;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class TieTable : Form
    {
        private int round;
        private IContainer components = null;
        private ListView lvTTable;
        private Button bClose;
        private Button bPrint;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;

        public TieTable(int round)
        {
            this.InitializeComponent();
            this.round = round;
            this.Text = this.Text + round.ToString() + " 까지";
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lvTTable = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.bClose = new Button();
            this.bPrint = new Button();
            base.SuspendLayout();
            this.lvTTable.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            ColumnHeader[] values = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6, this.columnHeader7 };
            this.lvTTable.Columns.AddRange(values);
            this.lvTTable.FullRowSelect = true;
            this.lvTTable.GridLines = true;
            this.lvTTable.Location = new Point(12, 12);
            this.lvTTable.Name = "lvTTable";
            this.lvTTable.Size = new Size(0x2a5, 0x13e);
            this.lvTTable.TabIndex = 0;
            this.lvTTable.UseCompatibleStateImageBehavior = false;
            this.lvTTable.View = View.Details;
            this.columnHeader1.Text = "순위";
            this.columnHeader2.Text = "이름";
            this.columnHeader2.Width = 0xab;
            this.columnHeader3.Text = "식별 번호";
            this.columnHeader3.Width = 0xab;
            this.columnHeader4.Text = "T0";
            this.columnHeader5.Text = "T1";
            this.columnHeader6.Text = "T2";
            this.columnHeader7.Text = "T3";
            this.bClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.bClose.Location = new Point(0x223, 0x150);
            this.bClose.Name = "bClose";
            this.bClose.Size = new Size(0x8d, 0x22);
            this.bClose.TabIndex = 1;
            this.bClose.Text = "닫기";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new EventHandler(this.bClose_Click);
            this.bPrint.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.bPrint.Enabled = false;
            this.bPrint.Location = new Point(400, 0x150);
            this.bPrint.Name = "bPrint";
            this.bPrint.Size = new Size(0x8d, 0x22);
            this.bPrint.TabIndex = 2;
            this.bPrint.Text = "출력";
            this.bPrint.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(7f, 15f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2bd, 0x17e);
            base.Controls.Add(this.bPrint);
            base.Controls.Add(this.bClose);
            base.Controls.Add(this.lvTTable);
            this.Font = new Font("맑은 고딕", 9f);
            base.Margin = new Padding(3, 4, 3, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TieTable";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "타이 테이블 - 라운드 ";
            base.Load += new EventHandler(this.TieTable_Load);
            base.ResumeLayout(false);
        }

        private void TieTable_Load(object sender, EventArgs e)
        {
            List<Member> list = MainForm.Instance.Manager.QueryMember("");
            List<History> list2 = MainForm.Instance.Manager.QueryHistory("");
            SwissTournamentRound round = new SwissTournamentRound();
            int index = 0;
            while (true)
            {
                if (index >= list2.Count)
                {
                    foreach (Member member in list)
                    {
                        round.InsertPlayer(member.Id);
                    }
                    foreach (History history in list2)
                    {
                        round.InsertRoundResult(history.Player1, history.Player2, history.Status, this.round);
                    }
                    round.Sort();
                    Dictionary<int, Tuple<int, int, int, int>> tScore = round.GetTScore();
                    round.rank.ForEach(r =>
                    {
                        string[] items = new string[] { (r.Value + 1).ToString(), (from x in list
                                where x.Id == r.Key
                                select x).First<Member>().Name, r.Key.ToString(), tScore[r.Key].Item1.ToString(), tScore[r.Key].Item2.ToString(), tScore[r.Key].Item3.ToString(), tScore[r.Key].Item4.ToString() };
                        this.lvTTable.Items.Add(new ListViewItem(items));
                    });
                    //using (List<KeyValuePair<int, int>>.Enumerator enumerator3 = round.rank.GetEnumerator())
                    //{
                    //    while (enumerator3.MoveNext())
                    //    {
                    //        KeyValuePair<int, int> r;
                    //        string[] items = new string[] { (r.Value + 1).ToString(), (from x in list
                    //            where x.Id == r.Key
                    //            select x).First<Member>().Name, r.Key.ToString(), tScore[r.Key].Item1.ToString(), tScore[r.Key].Item2.ToString(), tScore[r.Key].Item3.ToString(), tScore[r.Key].Item4.ToString() };
                    //        this.lvTTable.Items.Add(new ListViewItem(items));
                    //    }
                    //}
                    ColumnSorter.InitListView(this.lvTTable);
                    return;
                }
                if (list2[index].Round > this.round)
                {
                    index--;
                    list2.RemoveAt(index);
                }
                index++;
            }
        }
    }
}

