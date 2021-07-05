
namespace RegReader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rwDrop = new System.Windows.Forms.ComboBox();
            this.rwLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.regDrop = new System.Windows.Forms.ComboBox();
            this.Status = new System.Windows.Forms.Label();
            this.conStat = new System.Windows.Forms.Label();
            this.strtAdd = new System.Windows.Forms.Label();
            this.sAddress = new System.Windows.Forms.NumericUpDown();
            this.numParam = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.stats = new System.Windows.Forms.Label();
            this.createParam = new System.Windows.Forms.Button();
            this.sCmd = new System.Windows.Forms.Button();
            this.replyBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam)).BeginInit();
            this.SuspendLayout();
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(89, 12);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 0;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start:";
            // 
            // rwDrop
            // 
            this.rwDrop.Enabled = false;
            this.rwDrop.FormattingEnabled = true;
            this.rwDrop.Items.AddRange(new object[] {
            "Read",
            "Write"});
            this.rwDrop.Location = new System.Drawing.Point(89, 53);
            this.rwDrop.Name = "rwDrop";
            this.rwDrop.Size = new System.Drawing.Size(97, 24);
            this.rwDrop.TabIndex = 2;
            this.rwDrop.SelectedIndexChanged += new System.EventHandler(this.rwDrop_ValueChanged);
            // 
            // rwLabel
            // 
            this.rwLabel.AutoSize = true;
            this.rwLabel.Location = new System.Drawing.Point(22, 56);
            this.rwLabel.Name = "rwLabel";
            this.rwLabel.Size = new System.Drawing.Size(39, 17);
            this.rwLabel.TabIndex = 3;
            this.rwLabel.Text = "R/W:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reg:";
            // 
            // regDrop
            // 
            this.regDrop.Enabled = false;
            this.regDrop.FormattingEnabled = true;
            this.regDrop.Items.AddRange(new object[] {
            "DN",
            "FN"});
            this.regDrop.Location = new System.Drawing.Point(89, 98);
            this.regDrop.Name = "regDrop";
            this.regDrop.Size = new System.Drawing.Size(97, 24);
            this.regDrop.TabIndex = 4;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(180, 15);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(52, 17);
            this.Status.TabIndex = 6;
            this.Status.Text = "Status:";
            // 
            // conStat
            // 
            this.conStat.AutoSize = true;
            this.conStat.Enabled = false;
            this.conStat.Location = new System.Drawing.Point(229, 15);
            this.conStat.Name = "conStat";
            this.conStat.Size = new System.Drawing.Size(0, 17);
            this.conStat.TabIndex = 7;
            // 
            // strtAdd
            // 
            this.strtAdd.AutoSize = true;
            this.strtAdd.Location = new System.Drawing.Point(22, 151);
            this.strtAdd.Name = "strtAdd";
            this.strtAdd.Size = new System.Drawing.Size(98, 34);
            this.strtAdd.TabIndex = 8;
            this.strtAdd.Text = "Start Address:\r\n      (HEX)";
            // 
            // sAddress
            // 
            this.sAddress.Enabled = false;
            this.sAddress.Hexadecimal = true;
            this.sAddress.Location = new System.Drawing.Point(126, 160);
            this.sAddress.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.sAddress.Name = "sAddress";
            this.sAddress.Size = new System.Drawing.Size(60, 22);
            this.sAddress.TabIndex = 9;
            // 
            // numParam
            // 
            this.numParam.Enabled = false;
            this.numParam.Hexadecimal = true;
            this.numParam.Location = new System.Drawing.Point(126, 221);
            this.numParam.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numParam.Name = "numParam";
            this.numParam.Size = new System.Drawing.Size(60, 22);
            this.numParam.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 34);
            this.label3.TabIndex = 10;
            this.label3.Text = "Num Params:\r\n      (HEX)";
            // 
            // stats
            // 
            this.stats.AutoSize = true;
            this.stats.Location = new System.Drawing.Point(232, 15);
            this.stats.Name = "stats";
            this.stats.Size = new System.Drawing.Size(94, 17);
            this.stats.TabIndex = 12;
            this.stats.Text = "Disconnected";
            // 
            // createParam
            // 
            this.createParam.Enabled = false;
            this.createParam.Location = new System.Drawing.Point(192, 220);
            this.createParam.Name = "createParam";
            this.createParam.Size = new System.Drawing.Size(75, 23);
            this.createParam.TabIndex = 13;
            this.createParam.Text = "Set";
            this.createParam.UseVisualStyleBackColor = true;
            this.createParam.Click += new System.EventHandler(this.createParam_Click);
            // 
            // sCmd
            // 
            this.sCmd.BackColor = System.Drawing.Color.Lime;
            this.sCmd.Enabled = false;
            this.sCmd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sCmd.Location = new System.Drawing.Point(520, 66);
            this.sCmd.Name = "sCmd";
            this.sCmd.Size = new System.Drawing.Size(197, 85);
            this.sCmd.TabIndex = 14;
            this.sCmd.Text = "Send Command";
            this.sCmd.UseVisualStyleBackColor = false;
            this.sCmd.Click += new System.EventHandler(this.sCmd_Click);
            // 
            // replyBox
            // 
            this.replyBox.Location = new System.Drawing.Point(25, 445);
            this.replyBox.Multiline = true;
            this.replyBox.Name = "replyBox";
            this.replyBox.Size = new System.Drawing.Size(750, 191);
            this.replyBox.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 663);
            this.Controls.Add(this.replyBox);
            this.Controls.Add(this.sCmd);
            this.Controls.Add(this.createParam);
            this.Controls.Add(this.stats);
            this.Controls.Add(this.numParam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sAddress);
            this.Controls.Add(this.strtAdd);
            this.Controls.Add(this.conStat);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.regDrop);
            this.Controls.Add(this.rwLabel);
            this.Controls.Add(this.rwDrop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox rwDrop;
        private System.Windows.Forms.Label rwLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox regDrop;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label conStat;
        private System.Windows.Forms.Label strtAdd;
        private System.Windows.Forms.NumericUpDown sAddress;
        private System.Windows.Forms.NumericUpDown numParam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label stats;
        private System.Windows.Forms.Button createParam;
        private System.Windows.Forms.Button sCmd;
        private System.Windows.Forms.TextBox replyBox;
    }
}

