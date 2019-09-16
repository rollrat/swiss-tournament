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

    public class MainForm : Form
    {
        public static MainForm Instance;
        public string DBFileName;
        public SqliteWrapper DB;
        public DBManager Manager;
        public Dictionary<int, RoundControl> rounds = new Dictionary<int, RoundControl>();
        private IContainer components = null;
        private StatusStrip statusStrip1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label2;
        private Label label1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label lCreatedMessage;
        private Label lCreatedDateTime;
        private Button button2;
        private SplitContainer splitContainer1;
        private ListView lvParty;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private GroupBox groupBox1;
        private Label label7;
        private Label label6;
        private Button bInsertParty;
        private TextBox tbPartyId;
        private TextBox tbPartyName;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 삭제DToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private TabPage tabPage3;
        private TabControl tabControl2;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 수정MToolStripMenuItem;
        private ToolStripMenuItem 변경MToolStripMenuItem;

        public MainForm()
        {
            this.InitializeComponent();
            RoundControl control = new RoundControl(1) {
                Dock = DockStyle.Fill
            };
            this.tabPage4.Controls.Add(control);
            this.rounds.Add(1, control);
            Instance = this;
        }

        private void bInsertParty_Click(object sender, EventArgs e)
        {
            int num;
            if (!int.TryParse(this.tbPartyId.Text, out num))
            {
                MessageBox.Show("식별번호는 숫자여야 합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.Manager.IsExistsId(this.tbPartyId.Text.ToInt32()))
            {
                MessageBox.Show("식별번호가 이미 존재합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.tbPartyName.Text == "")
            {
                MessageBox.Show("이름이 비어있습니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.tbPartyName.Text.Length >= 30)
            {
                MessageBox.Show("이름이 너무 깁니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                this.Manager.InsertMember(this.tbPartyName.Text, this.tbPartyId.Text.ToInt32());
                this.tbPartyName.Text = "";
                this.tbPartyId.Text = $"{this.tbPartyId.Text.ToInt32() + 1}";
                this.refresh_party();
                this.refresh_round();
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
            this.components = new Container();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new ToolStripStatusLabel();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.button2 = new Button();
            this.lCreatedMessage = new Label();
            this.lCreatedDateTime = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.tabPage2 = new TabPage();
            this.splitContainer1 = new SplitContainer();
            this.groupBox3 = new GroupBox();
            this.lvParty = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.삭제DToolStripMenuItem = new ToolStripMenuItem();
            this.groupBox2 = new GroupBox();
            this.groupBox1 = new GroupBox();
            this.bInsertParty = new Button();
            this.tbPartyId = new TextBox();
            this.tbPartyName = new TextBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.tabPage3 = new TabPage();
            this.tabControl2 = new TabControl();
            this.tabPage5 = new TabPage();
            this.tabPage4 = new TabPage();
            this.tabPage6 = new TabPage();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.수정MToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripSeparator();
            this.변경MToolStripMenuItem = new ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            base.SuspendLayout();
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripStatusLabel2, this.toolStripStatusLabel3, this.toolStripStatusLabel4 };
            this.statusStrip1.Items.AddRange(toolStripItems);
            this.statusStrip1.Location = new Point(0, 0x1c4);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x410, 0x18);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(0x3e, 0x13);
            this.toolStripStatusLabel1.Text = "참가자 수:";
            this.toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel2.BorderStyle = Border3DStyle.Etched;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new Size(30, 0x13);
            this.toolStripStatusLabel2.Text = "0명";
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new Size(0x3e, 0x13);
            this.toolStripStatusLabel3.Text = "남은 작업:";
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new Size(0x22, 0x13);
            this.toolStripStatusLabel4.Text = "0 / 0";
            this.tabControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new Point(12, 13);
            this.tabControl1.Margin = new Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x3f8, 0x1b5);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabStop = false;
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.lCreatedMessage);
            this.tabPage1.Controls.Add(this.lCreatedDateTime);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new Point(4, 0x18);
            this.tabPage1.Margin = new Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3, 4, 3, 4);
            this.tabPage1.Size = new Size(0x3f0, 0x199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "요약";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button2.Enabled = false;
            this.button2.Location = new Point(0x360, 0x29);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x61, 0x16);
            this.button2.TabIndex = 8;
            this.button2.Text = "설명 수정";
            this.button2.UseVisualStyleBackColor = true;
            this.lCreatedMessage.AutoSize = true;
            this.lCreatedMessage.Font = new Font("맑은 고딕", 12f);
            this.lCreatedMessage.Location = new Point(0x9c, 40);
            this.lCreatedMessage.Name = "lCreatedMessage";
            this.lCreatedMessage.Size = new Size(0x60, 0x15);
            this.lCreatedMessage.TabIndex = 6;
            this.lCreatedMessage.Text = "0000-00-00";
            this.lCreatedDateTime.AutoSize = true;
            this.lCreatedDateTime.Font = new Font("맑은 고딕", 12f);
            this.lCreatedDateTime.Location = new Point(0x9c, 0x13);
            this.lCreatedDateTime.Name = "lCreatedDateTime";
            this.lCreatedDateTime.Size = new Size(0x60, 0x15);
            this.lCreatedDateTime.TabIndex = 5;
            this.lCreatedDateTime.Text = "0000-00-00";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("맑은 고딕", 12f);
            this.label5.Location = new Point(0x20, 0x7a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x76, 0x15);
            this.label5.TabIndex = 4;
            this.label5.Text = "남은 게임 수 : ";
            this.label4.AutoSize = true;
            this.label4.Font = new Font("맑은 고딕", 12f);
            this.label4.Location = new Point(0x10, 0x65);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x86, 0x15);
            this.label4.TabIndex = 3;
            this.label4.Text = "진행된 게임 수 : ";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("맑은 고딕", 12f);
            this.label3.Location = new Point(0x36, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x60, 0x15);
            this.label3.TabIndex = 2;
            this.label3.Text = "참가자 수 : ";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("맑은 고딕", 12f);
            this.label2.Location = new Point(0x5c, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3a, 0x15);
            this.label2.TabIndex = 1;
            this.label2.Text = "설명 : ";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("맑은 고딕", 12f);
            this.label1.Location = new Point(0x36, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x60, 0x15);
            this.label1.TabIndex = 0;
            this.label1.Text = "생성 날짜 : ";
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new Point(4, 0x18);
            this.tabPage2.Margin = new Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3, 4, 3, 4);
            this.tabPage2.Size = new Size(0x3f0, 0x199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "참가자 관리";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new Size(0x3ea, 0x191);
            this.splitContainer1.SplitterDistance = 0xf5;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            this.groupBox3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.lvParty);
            this.groupBox3.Location = new Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xef, 0x18b);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "참가자 목록";
            ColumnHeader[] values = new ColumnHeader[] { this.columnHeader1, this.columnHeader2 };
            this.lvParty.Columns.AddRange(values);
            this.lvParty.ContextMenuStrip = this.contextMenuStrip1;
            this.lvParty.Dock = DockStyle.Fill;
            this.lvParty.FullRowSelect = true;
            this.lvParty.GridLines = true;
            this.lvParty.Location = new Point(3, 0x13);
            this.lvParty.Name = "lvParty";
            this.lvParty.Size = new Size(0xe9, 0x175);
            this.lvParty.TabIndex = 0;
            this.lvParty.UseCompatibleStateImageBehavior = false;
            this.lvParty.View = View.Details;
            this.columnHeader1.Text = "이름";
            this.columnHeader1.Width = 0x69;
            this.columnHeader2.Text = "식별번호";
            this.columnHeader2.Width = 0x57;
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.변경MToolStripMenuItem, this.toolStripMenuItem2, this.삭제DToolStripMenuItem };
            this.contextMenuStrip1.Items.AddRange(itemArray2);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x76, 0x36);
            this.삭제DToolStripMenuItem.Name = "삭제DToolStripMenuItem";
            this.삭제DToolStripMenuItem.Size = new Size(0x98, 0x16);
            this.삭제DToolStripMenuItem.Text = "삭제(&D)";
            this.삭제DToolStripMenuItem.Click += new EventHandler(this.삭제DToolStripMenuItem_Click);
            this.groupBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox2.Location = new Point(3, 0x6c);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x2eb, 290);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "상세정보";
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.bInsertParty);
            this.groupBox1.Controls.Add(this.tbPartyId);
            this.groupBox1.Controls.Add(this.tbPartyName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2eb, 0x63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "추가";
            this.bInsertParty.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.bInsertParty.Location = new Point(0x299, 0x16);
            this.bInsertParty.Name = "bInsertParty";
            this.bInsertParty.Size = new Size(0x48, 0x3b);
            this.bInsertParty.TabIndex = 2;
            this.bInsertParty.Text = "추가";
            this.bInsertParty.UseVisualStyleBackColor = true;
            this.bInsertParty.Click += new EventHandler(this.bInsertParty_Click);
            this.tbPartyId.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbPartyId.Font = new Font("맑은 고딕", 12f);
            this.tbPartyId.Location = new Point(0x6d, 0x35);
            this.tbPartyId.Name = "tbPartyId";
            this.tbPartyId.Size = new Size(550, 0x1d);
            this.tbPartyId.TabIndex = 1;
            this.tbPartyId.KeyDown += new KeyEventHandler(this.tbPartyId_KeyDown);
            this.tbPartyName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbPartyName.Font = new Font("맑은 고딕", 12f);
            this.tbPartyName.Location = new Point(0x6d, 0x16);
            this.tbPartyName.Name = "tbPartyName";
            this.tbPartyName.Size = new Size(550, 0x1d);
            this.tbPartyName.TabIndex = 0;
            this.tbPartyName.KeyDown += new KeyEventHandler(this.tbPartyName_KeyDown);
            this.label7.AutoSize = true;
            this.label7.Font = new Font("맑은 고딕", 12f);
            this.label7.Location = new Point(13, 0x38);
            this.label7.Name = "label7";
            this.label7.Size = new Size(90, 0x15);
            this.label7.TabIndex = 2;
            this.label7.Text = "식별번호 : ";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("맑은 고딕", 12f);
            this.label6.Location = new Point(0x2d, 0x19);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3a, 0x15);
            this.label6.TabIndex = 1;
            this.label6.Text = "이름 : ";
            this.tabPage3.Controls.Add(this.tabControl2);
            this.tabPage3.Location = new Point(4, 0x18);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x3f0, 0x199);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "라운드 관리";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabControl2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new Point(6, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new Size(0x3e4, 0x18d);
            this.tabControl2.TabIndex = 0;
            this.tabPage5.Location = new Point(4, 0x18);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new Size(0x3dc, 0x171);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "라운드 관리자";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage4.Location = new Point(4, 0x18);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(0x3dc, 0x171);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "라운드 1";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage6.Location = new Point(4, 0x18);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(3);
            this.tabPage6.Size = new Size(0x3f0, 0x199);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "토너먼트 관리";
            this.tabPage6.UseVisualStyleBackColor = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(0x95, 6);
            this.수정MToolStripMenuItem.Name = "수정MToolStripMenuItem";
            this.수정MToolStripMenuItem.Size = new Size(0x98, 0x16);
            this.수정MToolStripMenuItem.Text = "수정(&M)";
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(0x95, 6);
            this.변경MToolStripMenuItem.Name = "변경MToolStripMenuItem";
            this.변경MToolStripMenuItem.Size = new Size(0x98, 0x16);
            this.변경MToolStripMenuItem.Text = "변경(&M)";
            this.변경MToolStripMenuItem.Click += new EventHandler(this.변경MToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(7f, 15f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x410, 0x1dc);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.statusStrip1);
            this.Font = new Font("맑은 고딕", 9f);
            base.Margin = new Padding(3, 4, 3, 4);
            this.MinimumSize = new Size(0x420, 0x203);
            base.Name = "MainForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "스위스 토너먼트 시뮬레이터";
            base.Load += new EventHandler(this.MainForm_Load);
            base.Shown += new EventHandler(this.MainForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ColumnSorter.InitListView(this.lvParty);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            new StartForm().ShowDialog();
            if (this.DBFileName != null)
            {
                this.DB = SqliteWrapper.Open(this.DBFileName);
                Tuple<DateTime, string> tuple = this.DB.Created();
                this.Manager = new DBManager(this.DB);
                this.lCreatedDateTime.Text = tuple.Item1.ToString();
                this.lCreatedMessage.Text = tuple.Item2;
                this.refresh_party();
            }
        }

        private void refresh_party()
        {
            List<ListViewItem> list2 = new List<ListViewItem>();
            foreach (Member member in this.Manager.QueryMember(""))
            {
                string[] items = new string[] { member.Name, member.Id.ToString() };
                list2.Add(new ListViewItem(items));
            }
            this.lvParty.Items.Clear();
            this.lvParty.Items.AddRange(list2.ToArray());
        }

        private void refresh_round()
        {
            this.rounds.ToList<KeyValuePair<int, RoundControl>>().ForEach(x => x.Value.Refresh());
        }

        private void tbPartyId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.bInsertParty.PerformClick();
            }
        }

        private void tbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.bInsertParty.PerformClick();
            }
        }

        private void 변경MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvParty.SelectedItems.Count == 1)
            {
                new ModifyMember(this.lvParty.SelectedItems[0].SubItems[0].Text, this.lvParty.SelectedItems[0].SubItems[1].Text.ToInt32()).ShowDialog();
                this.refresh_party();
                this.refresh_round();
            }
        }

        private void 삭제DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = this.lvParty.SelectedItems.Count;
            if ((count > 0) && (MessageBox.Show($"{count}개 항목을 삭제할까요?", "Swiss Tournament", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes))
            {
                List<History> list = this.Manager.QueryHistory("");
                HashSet<int> pp = new HashSet<int>();
                list.ForEach(delegate (History x) {
                    pp.Add(x.Player1);
                    pp.Add(x.Player2);
                });
                using (IEnumerator<ListViewItem> enumerator = this.lvParty.SelectedItems.OfType<ListViewItem>().GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ListViewItem current = enumerator.Current;
                        int item = current.SubItems[1].Text.ToInt32();
                        if (pp.Contains(item))
                        {
                            MessageBox.Show($"{current.SubItems[0].Text} 참가자의 게임 기록이 존재하기 때문에 삭제할 수 없습니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                    }
                }
                foreach (ListViewItem item2 in this.lvParty.SelectedItems.OfType<ListViewItem>())
                {
                    this.Manager.RemoveMember(item2.SubItems[1].Text.ToInt32());
                }
                this.refresh_party();
                this.refresh_round();
            }
        }
    }
}

