namespace Swiss_Tournament
{
    using Swiss_Tournament.DB;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class RoundControl : UserControl, Observerable
    {
        private int round;
        private IContainer components = null;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private ListView lvParty;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Button button1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button bAddGame;
        private ListView lvPrepare;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private PrintPreviewDialog printPreviewDialog1;
        private Button bViewT;
        private RadioButton rbByeLose;
        private RadioButton rbByeWin;
        private RadioButton rbDefault;
        private ContextMenuStrip contextMenuStrip1;
        private ComboBox cbPlayerIndex;
        private Label label1;
        private ToolStripMenuItem 플레이어1승리로수정ToolStripMenuItem;
        private ToolStripMenuItem 플레이어2승리로수정ToolStripMenuItem;
        private ToolStripMenuItem 무승부로수정ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 부전승으로수정ToolStripMenuItem;
        private ToolStripMenuItem 부전패로수정ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 기록없음으로수정ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem 게임삭제DToolStripMenuItem;

        public RoundControl(int round)
        {
            this.InitializeComponent();
            this.round = round;
            ColumnSorter.InitListView(this.lvPrepare);
            ColumnSorter.InitListView(this.lvParty);
            Observer.Instance.Add(this);
        }

        private void bAddGame_Click(object sender, EventArgs e)
        {
            if (this.rbDefault.Checked)
            {
                ListView.SelectedListViewItemCollection selectedItems = this.lvPrepare.SelectedItems;
                if (selectedItems.Count != 2)
                {
                    MessageBox.Show("두 참가자를 선택해 주세요!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MainForm.Instance.Manager.InsertHistory((int)selectedItems[0].Tag, (int)selectedItems[1].Tag, Status.None, this.round, "");
                    this.Refresh();
                }
            }
            else if (this.rbByeWin.Checked || this.rbByeLose.Checked)
            {
                ListView.SelectedListViewItemCollection selectedItems = this.lvPrepare.SelectedItems;
                if (selectedItems.Count != 1)
                {
                    MessageBox.Show("한 참가자를 선택해 주세요!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MainForm.Instance.Manager.InsertHistory((int)selectedItems[0].Tag, -1, this.rbByeWin.Checked ? Status.ByeWin : Status.ByeLose, this.round, "");
                    this.Refresh();
                }
            }
        }

        private void bViewT_Click(object sender, EventArgs e)
        {
            new TieTable(this.round).Show();
        }

        private void cbPlayerIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            char[] separator = new char[] { '(' };
            string str = this.cbPlayerIndex.Text.Split(separator)[0].Trim();
            this.lvParty.SelectedItems.Clear();
            foreach (ListViewItem item in this.lvParty.Items.OfType<ListViewItem>())
            {
                if ((item.SubItems[1].Text == str) || (item.SubItems[2].Text == str))
                {
                    item.Selected = true;
                    this.lvParty.Focus();
                    break;
                }
            }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoundControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPlayerIndex = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvParty = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.플레이어1승리로수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.플레이어2승리로수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.무승부로수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.부전승으로수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.부전패로수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.기록없음으로수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.게임삭제DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bViewT = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbByeLose = new System.Windows.Forms.RadioButton();
            this.rbByeWin = new System.Windows.Forms.RadioButton();
            this.rbDefault = new System.Windows.Forms.RadioButton();
            this.bAddGame = new System.Windows.Forms.Button();
            this.lvPrepare = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1078, 648);
            this.splitContainer1.SplitterDistance = 638;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbPlayerIndex);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lvParty);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 642);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "게임 목록";
            // 
            // cbPlayerIndex
            // 
            this.cbPlayerIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlayerIndex.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPlayerIndex.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPlayerIndex.FormattingEnabled = true;
            this.cbPlayerIndex.Location = new System.Drawing.Point(142, 18);
            this.cbPlayerIndex.Name = "cbPlayerIndex";
            this.cbPlayerIndex.Size = new System.Drawing.Size(484, 23);
            this.cbPlayerIndex.TabIndex = 2;
            this.cbPlayerIndex.SelectedIndexChanged += new System.EventHandler(this.cbPlayerIndex_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "플레이어로 경기 찾기: ";
            // 
            // lvParty
            // 
            this.lvParty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvParty.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvParty.ContextMenuStrip = this.contextMenuStrip1;
            this.lvParty.FullRowSelect = true;
            this.lvParty.GridLines = true;
            this.lvParty.HideSelection = false;
            this.lvParty.Location = new System.Drawing.Point(6, 47);
            this.lvParty.Name = "lvParty";
            this.lvParty.Size = new System.Drawing.Size(620, 586);
            this.lvParty.TabIndex = 0;
            this.lvParty.UseCompatibleStateImageBehavior = false;
            this.lvParty.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "인덱스";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "플레이어 1";
            this.columnHeader2.Width = 153;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "플레이어 2";
            this.columnHeader3.Width = 151;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "상태";
            this.columnHeader4.Width = 159;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.플레이어1승리로수정ToolStripMenuItem,
            this.플레이어2승리로수정ToolStripMenuItem,
            this.무승부로수정ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.부전승으로수정ToolStripMenuItem,
            this.부전패로수정ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.기록없음으로수정ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.게임삭제DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(221, 176);
            // 
            // 플레이어1승리로수정ToolStripMenuItem
            // 
            this.플레이어1승리로수정ToolStripMenuItem.Name = "플레이어1승리로수정ToolStripMenuItem";
            this.플레이어1승리로수정ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.플레이어1승리로수정ToolStripMenuItem.Text = "플레이어 1 승리로 수정 (&1)";
            this.플레이어1승리로수정ToolStripMenuItem.Click += new System.EventHandler(this.플레이어1승리로수정ToolStripMenuItem_Click);
            // 
            // 플레이어2승리로수정ToolStripMenuItem
            // 
            this.플레이어2승리로수정ToolStripMenuItem.Name = "플레이어2승리로수정ToolStripMenuItem";
            this.플레이어2승리로수정ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.플레이어2승리로수정ToolStripMenuItem.Text = "플레이어 2 승리로 수정 (&2)";
            this.플레이어2승리로수정ToolStripMenuItem.Click += new System.EventHandler(this.플레이어2승리로수정ToolStripMenuItem_Click);
            // 
            // 무승부로수정ToolStripMenuItem
            // 
            this.무승부로수정ToolStripMenuItem.Name = "무승부로수정ToolStripMenuItem";
            this.무승부로수정ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.무승부로수정ToolStripMenuItem.Text = "무승부로 수정 (&3)";
            this.무승부로수정ToolStripMenuItem.Click += new System.EventHandler(this.무승부로수정ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // 부전승으로수정ToolStripMenuItem
            // 
            this.부전승으로수정ToolStripMenuItem.Name = "부전승으로수정ToolStripMenuItem";
            this.부전승으로수정ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.부전승으로수정ToolStripMenuItem.Text = "부전승으로 수정";
            this.부전승으로수정ToolStripMenuItem.Click += new System.EventHandler(this.부전승으로수정ToolStripMenuItem_Click);
            // 
            // 부전패로수정ToolStripMenuItem
            // 
            this.부전패로수정ToolStripMenuItem.Name = "부전패로수정ToolStripMenuItem";
            this.부전패로수정ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.부전패로수정ToolStripMenuItem.Text = "부전패로 수정";
            this.부전패로수정ToolStripMenuItem.Click += new System.EventHandler(this.부전패로수정ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(217, 6);
            // 
            // 기록없음으로수정ToolStripMenuItem
            // 
            this.기록없음으로수정ToolStripMenuItem.Name = "기록없음으로수정ToolStripMenuItem";
            this.기록없음으로수정ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.기록없음으로수정ToolStripMenuItem.Text = "기록 없음으로 수정";
            this.기록없음으로수정ToolStripMenuItem.Click += new System.EventHandler(this.기록없음으로수정ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(217, 6);
            // 
            // 게임삭제DToolStripMenuItem
            // 
            this.게임삭제DToolStripMenuItem.Name = "게임삭제DToolStripMenuItem";
            this.게임삭제DToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.게임삭제DToolStripMenuItem.Text = "게임 삭제(&D)";
            this.게임삭제DToolStripMenuItem.Click += new System.EventHandler(this.게임삭제DToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.bViewT);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 639);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "게임 수정";
            // 
            // bViewT
            // 
            this.bViewT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bViewT.Location = new System.Drawing.Point(241, 605);
            this.bViewT.Name = "bViewT";
            this.bViewT.Size = new System.Drawing.Size(177, 28);
            this.bViewT.TabIndex = 2;
            this.bViewT.Text = "타이표 보기";
            this.bViewT.UseVisualStyleBackColor = true;
            this.bViewT.Click += new System.EventHandler(this.bViewT_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rbByeLose);
            this.groupBox3.Controls.Add(this.rbByeWin);
            this.groupBox3.Controls.Add(this.rbDefault);
            this.groupBox3.Controls.Add(this.bAddGame);
            this.groupBox3.Controls.Add(this.lvPrepare);
            this.groupBox3.Location = new System.Drawing.Point(6, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 541);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "게임에 배치되지 않은 참가자 목록";
            // 
            // rbByeLose
            // 
            this.rbByeLose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbByeLose.AutoSize = true;
            this.rbByeLose.Location = new System.Drawing.Point(73, 516);
            this.rbByeLose.Name = "rbByeLose";
            this.rbByeLose.Size = new System.Drawing.Size(61, 19);
            this.rbByeLose.TabIndex = 4;
            this.rbByeLose.Text = "부전패";
            this.rbByeLose.UseVisualStyleBackColor = true;
            // 
            // rbByeWin
            // 
            this.rbByeWin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbByeWin.AutoSize = true;
            this.rbByeWin.Location = new System.Drawing.Point(6, 516);
            this.rbByeWin.Name = "rbByeWin";
            this.rbByeWin.Size = new System.Drawing.Size(61, 19);
            this.rbByeWin.TabIndex = 3;
            this.rbByeWin.Text = "부전승";
            this.rbByeWin.UseVisualStyleBackColor = true;
            // 
            // rbDefault
            // 
            this.rbDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbDefault.AutoSize = true;
            this.rbDefault.Checked = true;
            this.rbDefault.Location = new System.Drawing.Point(6, 492);
            this.rbDefault.Name = "rbDefault";
            this.rbDefault.Size = new System.Drawing.Size(77, 19);
            this.rbDefault.TabIndex = 2;
            this.rbDefault.TabStop = true;
            this.rbDefault.Text = "일반 경기";
            this.rbDefault.UseVisualStyleBackColor = true;
            // 
            // bAddGame
            // 
            this.bAddGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddGame.Location = new System.Drawing.Point(235, 492);
            this.bAddGame.Name = "bAddGame";
            this.bAddGame.Size = new System.Drawing.Size(177, 43);
            this.bAddGame.TabIndex = 1;
            this.bAddGame.Text = "게임 목록에 추가";
            this.bAddGame.UseVisualStyleBackColor = true;
            this.bAddGame.Click += new System.EventHandler(this.bAddGame_Click);
            // 
            // lvPrepare
            // 
            this.lvPrepare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPrepare.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvPrepare.FullRowSelect = true;
            this.lvPrepare.GridLines = true;
            this.lvPrepare.HideSelection = false;
            this.lvPrepare.Location = new System.Drawing.Point(6, 22);
            this.lvPrepare.Name = "lvPrepare";
            this.lvPrepare.Size = new System.Drawing.Size(406, 464);
            this.lvPrepare.TabIndex = 0;
            this.lvPrepare.UseCompatibleStateImageBehavior = false;
            this.lvPrepare.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "이름";
            this.columnHeader5.Width = 123;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "식별번호";
            this.columnHeader6.Width = 134;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "임의로 게임 목록 생성";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // RoundControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RoundControl";
            this.Size = new System.Drawing.Size(1078, 648);
            this.Load += new System.EventHandler(this.RoundControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void modify_status(Status what)
        {
            ListView.SelectedListViewItemCollection selectedItems = this.lvParty.SelectedItems;
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
                        MessageBox.Show("해당 기록을 " + this.status2str(what) + "로 바꿀 수 없습니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MainForm.Instance.Manager.ModifyHistory(selectedItems[0].SubItems[0].Text.ToInt32(), what);
                        this.Refresh();
                    }
                }
            }
        }

        public override void Refresh()
        {
            List<History> list = MainForm.Instance.Manager.QueryRound(this.round);
            List<Member> list2 = MainForm.Instance.Manager.QueryMember("");
            HashSet<int> set = new HashSet<int>();
            HashSet<string> source = new HashSet<string>();
            this.lvParty.Items.Clear();
            this.lvPrepare.Items.Clear();
            this.cbPlayerIndex.Items.Clear();
            list.ForEach(r =>
            {
                set.Add(r.Player1);
                set.Add(r.Player2);
                string str = this.status2str(r.Status);
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
                this.lvParty.Items.Add(new ListViewItem(items));
            });
            foreach (Member member in list2)
            {
                if (!set.Contains(member.Id))
                {
                    string[] items = new string[] { member.Name, member.Id.ToString() };
                    this.lvPrepare.Items.Add(new ListViewItem(items) { Tag = member.Id });
                }
            }
            List<string> list3 = source.ToList<string>();
            list3.Sort(new Comparison<string>(ColumnSorter.ComparePath));
            list3.ForEach(x => this.cbPlayerIndex.Items.Add(x));
        }

        private void RoundControl_Load(object sender, EventArgs e)
        {
            this.Refresh();
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
            ListView.SelectedListViewItemCollection selectedItems = this.lvParty.SelectedItems;
            if ((selectedItems.Count > 0) && (MessageBox.Show($"{selectedItems.Count}개 항목을 삭제할까요?", "Swiss Tournament", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes))
            {
                foreach (int num in from x in selectedItems.OfType<ListViewItem>() select x.SubItems[0].Text.ToInt32())
                {
                    MainForm.Instance.Manager.RemoveHistory(num);
                }
                this.Refresh();
            }
        }

        private void 기록없음으로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modify_status(Status.None);
        }

        private void 무승부로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modify_status(Status.Draw);
        }

        private void 부전승으로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modify_status(Status.ByeWin);
        }

        private void 부전패로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modify_status(Status.ByeLose);
        }

        private void 플레이어1승리로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modify_status(Status.Player1Win);
        }

        private void 플레이어2승리로수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modify_status(Status.Player2Win);
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}

