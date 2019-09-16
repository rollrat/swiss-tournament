namespace Swiss_Tournament
{
    using Swiss_Tournament.DB;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class CreateDB : Form
    {
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private TextBox tbFileName;
        private TextBox tbMessage;
        private Button bCreate;

        public CreateDB()
        {
            this.InitializeComponent();
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            if (this.tbMessage.Text.Contains("'"))
            {
                MessageBox.Show("설명에는 '문자가 없어야합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.tbMessage.Text.Length >= 0xff)
            {
                MessageBox.Show("설명이 너무 깁니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (!Directory.Exists("db"))
                {
                    Directory.CreateDirectory("db");
                }
                string path = "db/" + this.tbFileName.Text + ".db";
                if (File.Exists(path))
                {
                    MessageBox.Show("같은 이름의 파일이 이미 존재합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    SqliteWrapper.CreateNew(path);
                    SqliteWrapper wrapper = SqliteWrapper.Open(path);
                    wrapper.EvalNqSql("create table created (time integer, msg varchar(255))");
                    wrapper.EvalNqSql($"insert into created (time, msg) values ({DateTime.Now.Ticks}, '{this.tbMessage.Text}')");
                    wrapper.EvalNqSql("create table members (ix int, name varchar(60), id int)");
                    wrapper.EvalNqSql("create table history (ix int, p1 int, p2 int, status int, round int, desc varchar(255))");
                    base.Close();
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.tbFileName = new TextBox();
            this.tbMessage = new TextBox();
            this.bCreate = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("맑은 고딕", 12f);
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(90, 0x15);
            this.label1.TabIndex = 0;
            this.label1.Text = "파일 이름: ";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("맑은 고딕", 12f);
            this.label2.Location = new Point(50, 0x2f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x34, 0x15);
            this.label2.TabIndex = 1;
            this.label2.Text = "설명: ";
            this.tbFileName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbFileName.Font = new Font("맑은 고딕", 12f);
            this.tbFileName.Location = new Point(0x6c, 12);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new Size(0x1bc, 0x1d);
            this.tbFileName.TabIndex = 2;
            this.tbMessage.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbMessage.Font = new Font("맑은 고딕", 12f);
            this.tbMessage.Location = new Point(0x6c, 0x2c);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new Size(0x1bc, 0x1d);
            this.tbMessage.TabIndex = 3;
            this.bCreate.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.bCreate.Location = new Point(0x1a2, 0x51);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new Size(0x85, 0x24);
            this.bCreate.TabIndex = 4;
            this.bCreate.Text = "만들기";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new EventHandler(this.bCreate_Click);
            base.AutoScaleDimensions = new SizeF(7f, 15f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x234, 0x84);
            base.Controls.Add(this.bCreate);
            base.Controls.Add(this.tbMessage);
            base.Controls.Add(this.tbFileName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            this.Font = new Font("맑은 고딕", 9f);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(3, 4, 3, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CreateDB";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "데이터 베이스 생성";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

