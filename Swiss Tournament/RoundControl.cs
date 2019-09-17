/*

   Copyright (C) 2019. rollrat All Rights Reserved.

   Author: Jeong HyunJun

*/

using Swiss_Tournament.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Swiss_Tournament
{
    public partial class RoundControl : UserControl, Observerable
    {
        int round;

        public RoundControl(int round)
        {
            InitializeComponent();
            this.round = round;
            ColumnSorter.InitListView(lvPrepare);
            ColumnSorter.InitListView(lvParty);
            Observer.Instance.Add(this);
        }

        private void bAddGame_Click(object sender, EventArgs e)
        {
            if (rbDefault.Checked)
            {
                ListView.SelectedListViewItemCollection selectedItems = lvPrepare.SelectedItems;
                if (selectedItems.Count != 2)
                {
                    MessageBox.Show("두 참가자를 선택해 주세요!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MainForm.Instance.Manager.InsertHistory((int)selectedItems[0].Tag, (int)selectedItems[1].Tag, Status.None, round, "");
                    Refresh();
                }
            }
            else if (rbByeWin.Checked || rbByeLose.Checked)
            {
                ListView.SelectedListViewItemCollection selectedItems = lvPrepare.SelectedItems;
                if (selectedItems.Count != 1)
                {
                    MessageBox.Show("한 참가자를 선택해 주세요!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MainForm.Instance.Manager.InsertHistory((int)selectedItems[0].Tag, -1, rbByeWin.Checked ? Status.ByeWin : Status.ByeLose, round, "");
                    Refresh();
                }
            }
        }

        private void bViewT_Click(object sender, EventArgs e)
        {
            new TieTable(round).Show();
        }

        private void cbPlayerIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            char[] separator = new char[] { '(' };
            string str = cbPlayerIndex.Text.Split(separator)[0].Trim();
            lvParty.SelectedItems.Clear();
            foreach (ListViewItem item in lvParty.Items.OfType<ListViewItem>())
            {
                if ((item.SubItems[1].Text == str) || (item.SubItems[2].Text == str))
                {
                    item.Selected = true;
                    lvParty.Focus();
                    break;
                }
            }
        }

        private void modify_status(Status what)
        {
            ListView.SelectedListViewItemCollection selectedItems = lvParty.SelectedItems;
            if (selectedItems.Count > 0)
            {
                if (selectedItems.Count != 1)
                {
                    MessageBox.Show("한 참가자를 선택해 주세요!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    bool flag1;
                    if (((what == Status.ByeWin) || (what == Status.ByeLose)) && (selectedItems[0].SubItems[2].Text != ""))
                    {
                        flag1 = true;
                    }
                    else if ((what == Status.ByeWin) || (what == Status.ByeLose))
                    {
                        flag1 = false;
                    }
                    else
                    {
                        flag1 = selectedItems[0].SubItems[2].Text == "";
                    }
                    if (flag1)
                    {
                        MessageBox.Show("해당 기록을 " + status2str(what) + "로 바꿀 수 없습니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MainForm.Instance.Manager.ModifyHistory(selectedItems[0].SubItems[0].Text.ToInt32(), what);
                        Refresh();
                    }
                }
            }
        }

        public override void Refresh()
        {
            List<History> list = MainForm.Instance.Manager.QueryRound(round);
            List<Member> list2 = MainForm.Instance.Manager.QueryMember("");
            HashSet<int> set = new HashSet<int>();
            HashSet<string> source = new HashSet<string>();
            lvParty.Items.Clear();
            lvPrepare.Items.Clear();
            cbPlayerIndex.Items.Clear();
            list.ForEach(r =>
            {
                set.Add(r.Player1);
                set.Add(r.Player2);
                string str = status2str(r.Status);
                string name = (from x in list2
                               where x.Id == r.Player1
                               select x).First<Member>().Name;
                string str3 = "";
                source.Add($"{name} ({r.Player1})");
                if (r.Player2 >= 0)
                {
                    str3 = (from x in list2
                            where x.Id == r.Player2
                            select x).First<Member>().Name;
                    source.Add($"{str3} ({r.Player2})");
                }
                string[] items = new string[] { r.Index.ToString(), name, str3, str };
                lvParty.Items.Add(new ListViewItem(items));
            });
            foreach (Member member in list2)
            {
                if (!set.Contains(member.Id))
                {
                    string[] items = new string[] { member.Name, member.Id.ToString() };
                    lvPrepare.Items.Add(new ListViewItem(items) { Tag = member.Id });
                }
            }
            List<string> list3 = source.ToList<string>();
            list3.Sort(new Comparison<string>(ColumnSorter.ComparePath));
            list3.ForEach(x => cbPlayerIndex.Items.Add(x));
        }

        private void RoundControl_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private string status2str(Status status)
        {
            string str;
            switch (status)
            {
                case Status.None:
                    str = "기록없음";
                    break;

                case Status.Player1Win:
                    str = "플레이어1 승리";
                    break;

                case Status.Player2Win:
                    str = "플레이어2 승리";
                    break;

                case Status.Draw:
                    str = "무승부";
                    break;

                case Status.ByeWin:
                    str = "부전승";
                    break;

                case Status.ByeLose:
                    str = "부전패";
                    break;

                case Status.Invalid:
                    str = "오류!";
                    break;

                default:
                    str = "";
                    break;
            }
            return str;
        }

        private void 게임삭제DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = lvParty.SelectedItems;
            if ((selectedItems.Count > 0) && (MessageBox.Show($"{selectedItems.Count}개 항목을 삭제할까요?", "Swiss Tournament", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes))
            {
                foreach (int num in from x in selectedItems.OfType<ListViewItem>() select x.SubItems[0].Text.ToInt32())
                {
                    MainForm.Instance.Manager.RemoveHistory(num);
                }
                Refresh();
            }
        }

        private void 기록없음으로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modify_status(Status.None);
        }

        private void 무승부로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modify_status(Status.Draw);
        }

        private void 부전승으로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modify_status(Status.ByeWin);
        }

        private void 부전패로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modify_status(Status.ByeLose);
        }

        private void 플레이어1승리로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modify_status(Status.Player1Win);
        }

        private void 플레이어2승리로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modify_status(Status.Player2Win);
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}

