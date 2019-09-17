/*

   Copyright (C) 2019. rollrat All Rights Reserved.

   Author: Jeong HyunJun

*/

using Swiss_Tournament.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Swiss_Tournament
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            RoundControl control = new RoundControl(1) { Dock = DockStyle.Fill };
            tabPage4.Controls.Add(control);
            rounds.Add(1, control);
            Instance = this;
        }

        private void bInsertParty_Click(object sender, EventArgs e)
        {
            int num;
            if (!int.TryParse(tbPartyId.Text, out num))
            {
                MessageBox.Show("식별번호는 숫자여야 합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (Manager.IsExistsId(tbPartyId.Text.ToInt32()))
            {
                MessageBox.Show("식별번호가 이미 존재합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (tbPartyName.Text == "")
            {
                MessageBox.Show("이름이 비어있습니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (tbPartyName.Text.Length >= 30)
            {
                MessageBox.Show("이름이 너무 깁니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                Manager.InsertMember(tbPartyName.Text, tbPartyId.Text.ToInt32());
                tbPartyName.Text = "";
                tbPartyId.Text = $"{tbPartyId.Text.ToInt32() + 1}";
                refresh_party();
                refresh_round();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ColumnSorter.InitListView(lvParty);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            new StartForm().ShowDialog();
            if (DBFileName != null)
            {
                DB = SqliteWrapper.Open(DBFileName);
                Tuple<DateTime, string> tuple = DB.Created();
                Manager = new DBManager(DB);
                lCreatedDateTime.Text = tuple.Item1.ToString();
                lCreatedMessage.Text = tuple.Item2;
                refresh_party();
            }
        }

        private void refresh_party()
        {
            List<ListViewItem> list2 = new List<ListViewItem>();
            foreach (Member member in Manager.QueryMember(""))
            {
                string[] items = new string[] { member.Name, member.Id.ToString() };
                list2.Add(new ListViewItem(items));
            }
            lvParty.Items.Clear();
            lvParty.Items.AddRange(list2.ToArray());
        }

        private void refresh_round()
        {
            rounds.ToList().ForEach(x => x.Value.Refresh());
        }

        private void tbPartyId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bInsertParty.PerformClick();
            }
        }

        private void tbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bInsertParty.PerformClick();
            }
        }

        private void 변경MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvParty.SelectedItems.Count == 1)
            {
                new ModifyMember(lvParty.SelectedItems[0].SubItems[0].Text, lvParty.SelectedItems[0].SubItems[1].Text.ToInt32()).ShowDialog();
                refresh_party();
                refresh_round();
                Observer.Instance.Refresh();
            }
        }

        private void 삭제DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = lvParty.SelectedItems.Count;
            if ((count > 0) && (MessageBox.Show($"{count}개 항목을 삭제할까요?", "Swiss Tournament", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes))
            {
                List<History> list = Manager.QueryHistory("");
                HashSet<int> pp = new HashSet<int>();
                list.ForEach(delegate (History x) {
                    pp.Add(x.Player1);
                    pp.Add(x.Player2);
                });
                foreach (var xx in lvParty.SelectedItems.OfType<ListViewItem>())
                {
                    int item = xx.SubItems[1].Text.ToInt32();
                    if (pp.Contains(item))
                    {
                        MessageBox.Show($"{xx.SubItems[0].Text} 참가자의 게임 기록이 존재하기 때문에 삭제할 수 없습니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                }

                foreach (var item2 in lvParty.SelectedItems.OfType<ListViewItem>())
                {
                    Manager.RemoveMember(item2.SubItems[1].Text.ToInt32());
                }
                refresh_party();
                refresh_round();
                Observer.Instance.Refresh();
            }
        }

        private void BNewRound_Click(object sender, EventArgs e)
        {
            var nt = new TabPage();
            nt.Location = new Point(4, 24);
            nt.Padding = new Padding(3);
            nt.Size = new Size(988, 369);
            nt.TabIndex = 0;
            nt.Text = "라운드 " + tabControl2.TabPages.Count;
            nt.UseVisualStyleBackColor = true;
            tabControl2.TabPages.Add(nt);

            RoundControl control = new RoundControl(1)
            {
                Dock = DockStyle.Fill
            };
            nt.Controls.Add(control);
            rounds.Add(tabControl2.TabPages.Count - 1, control);
        }
    }
}

