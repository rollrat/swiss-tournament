namespace Swiss_Tournament
{
    using Swiss_Tournament.DB;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class StartForm : Form
    {
        private bool safe_close = false;
        private IContainer components = null;
        private ListView lvFiles;
        private Button bCreate;
        private Button bOpen;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;

        public StartForm()
        {
            this.InitializeComponent();
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            new CreateDB().ShowDialog();
            this.refresh();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            if (this.lvFiles.SelectedItems.Count <= 0)
            {
                MessageBox.Show("데이터 베이스를 선택해주세요!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MainForm.Instance.DBFileName = $"db/{this.lvFiles.SelectedItems[0].SubItems[0].Text}";
                this.safe_close = true;
                base.Close();
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
            this.lvFiles = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.bCreate = new Button();
            this.bOpen = new Button();
            base.SuspendLayout();
            this.lvFiles.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            ColumnHeader[] values = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 };
            this.lvFiles.Columns.AddRange(values);
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.Location = new Point(12, 12);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new Size(0x2a9, 0x105);
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = View.Details;
            this.columnHeader1.Text = "파일 이름";
            this.columnHeader1.Width = 0x100;
            this.columnHeader2.Text = "설명";
            this.columnHeader2.Width = 0xea;
            this.columnHeader3.Text = "생성된 날짜";
            this.columnHeader3.Width = 0xa1;
            this.bCreate.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.bCreate.Location = new Point(12, 0x117);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new Size(160, 0x24);
            this.bCreate.TabIndex = 1;
            this.bCreate.Text = "새로 만들기";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new EventHandler(this.bCreate_Click);
            this.bOpen.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.bOpen.Location = new Point(0x215, 0x117);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new Size(160, 0x24);
            this.bOpen.TabIndex = 2;
            this.bOpen.Text = "열기";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new EventHandler(this.bOpen_Click);
            base.AutoScaleDimensions = new SizeF(7f, 15f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c1, 0x147);
            base.Controls.Add(this.bOpen);
            base.Controls.Add(this.bCreate);
            base.Controls.Add(this.lvFiles);
            this.Font = new Font("맑은 고딕", 9f);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(3, 4, 3, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StartForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "열기";
            base.FormClosing += new FormClosingEventHandler(this.StartForm_FormClosing);
            base.Load += new EventHandler(this.StartForm_Load);
            base.ResumeLayout(false);
        }

        private void refresh()
        {
            if (Directory.Exists("db"))
            {
                this.lvFiles.Items.Clear();
                foreach (string str in Directory.GetFiles("db"))
                {
                    Tuple<DateTime, string> tuple = SqliteWrapper.Open(str).Created();
                    string[] items = new string[] { Path.GetFileName(str), tuple.Item2, tuple.Item1.ToString() };
                    this.lvFiles.Items.Add(new ListViewItem(items));
                }
            }
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.safe_close)
            {
                Application.Exit();
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            this.refresh();
            ColumnSorter.InitListView(this.lvFiles);
        }
    }
}

