namespace Swiss_Tournament
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ModifyMember : Form
    {
        private string name;
        private int id;
        private IContainer components = null;
        private TextBox tbPartyId;
        private TextBox tbPartyName;
        private Label label7;
        private Label label6;
        private Button bInsertParty;

        public ModifyMember(string name, int id)
        {
            this.InitializeComponent();
            this.tbPartyId.Text = id.ToString();
            this.tbPartyName.Text = name;
            this.name = name;
            this.id = id;
        }

        private void bInsertParty_Click(object sender, EventArgs e)
        {
            int num;
            if (!int.TryParse(this.tbPartyId.Text, out num))
            {
                MessageBox.Show("식별번호는 숫자여야 합니다!", "Swiss Tournament", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (MainForm.Instance.Manager.IsExistsId(this.tbPartyId.Text.ToInt32()) && (this.id != this.tbPartyId.Text.ToInt32()))
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
                MainForm.Instance.Manager.ModifyMember(this.tbPartyId.Text.ToInt32(), this.tbPartyName.Text);
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
            this.tbPartyId = new TextBox();
            this.tbPartyName = new TextBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.bInsertParty = new Button();
            base.SuspendLayout();
            this.tbPartyId.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbPartyId.Font = new Font("맑은 고딕", 12f);
            this.tbPartyId.Location = new Point(0x6c, 0x2b);
            this.tbPartyId.Name = "tbPartyId";
            this.tbPartyId.Size = new Size(0x1f3, 0x1d);
            this.tbPartyId.TabIndex = 4;
            this.tbPartyName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbPartyName.Font = new Font("맑은 고딕", 12f);
            this.tbPartyName.Location = new Point(0x6c, 12);
            this.tbPartyName.Name = "tbPartyName";
            this.tbPartyName.Size = new Size(0x1f3, 0x1d);
            this.tbPartyName.TabIndex = 3;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("맑은 고딕", 12f);
            this.label7.Location = new Point(12, 0x2e);
            this.label7.Name = "label7";
            this.label7.Size = new Size(90, 0x15);
            this.label7.TabIndex = 6;
            this.label7.Text = "식별번호 : ";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("맑은 고딕", 12f);
            this.label6.Location = new Point(0x2c, 15);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3a, 0x15);
            this.label6.TabIndex = 5;
            this.label6.Text = "이름 : ";
            this.bInsertParty.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.bInsertParty.Location = new Point(0x265, 12);
            this.bInsertParty.Name = "bInsertParty";
            this.bInsertParty.Size = new Size(0x48, 0x3b);
            this.bInsertParty.TabIndex = 7;
            this.bInsertParty.Text = "수정";
            this.bInsertParty.UseVisualStyleBackColor = true;
            this.bInsertParty.Click += new EventHandler(this.bInsertParty_Click);
            base.AutoScaleDimensions = new SizeF(7f, 15f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b9, 0x57);
            base.Controls.Add(this.bInsertParty);
            base.Controls.Add(this.tbPartyId);
            base.Controls.Add(this.tbPartyName);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            this.Font = new Font("맑은 고딕", 9f);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "ModifyMember";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "참가자 수정";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

