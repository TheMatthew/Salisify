namespace salsa_beats
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ClaveBox = new System.Windows.Forms.CheckBox();
            this.GuiroBox = new System.Windows.Forms.CheckBox();
            this.TimbaleBox = new System.Windows.Forms.CheckBox();
            this.BongoBox = new System.Windows.Forms.CheckBox();
            this.CowbellBox = new System.Windows.Forms.CheckBox();
            this.ClaveVol = new System.Windows.Forms.HScrollBar();
            this.GuiroVol = new System.Windows.Forms.HScrollBar();
            this.TimbaleVol = new System.Windows.Forms.HScrollBar();
            this.BongoVol = new System.Windows.Forms.HScrollBar();
            this.CowbellVol = new System.Windows.Forms.HScrollBar();
            this.ClaveSelect = new System.Windows.Forms.ComboBox();
            this.MaracasBox = new System.Windows.Forms.CheckBox();
            this.MaracasVol = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 219);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(821, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(733, 193);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "120";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ClaveBox
            // 
            this.ClaveBox.AutoSize = true;
            this.ClaveBox.Location = new System.Drawing.Point(13, 13);
            this.ClaveBox.Name = "ClaveBox";
            this.ClaveBox.Size = new System.Drawing.Size(53, 17);
            this.ClaveBox.TabIndex = 2;
            this.ClaveBox.Text = "Clave";
            this.ClaveBox.UseVisualStyleBackColor = true;
            this.ClaveBox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // GuiroBox
            // 
            this.GuiroBox.AutoSize = true;
            this.GuiroBox.Location = new System.Drawing.Point(12, 36);
            this.GuiroBox.Name = "GuiroBox";
            this.GuiroBox.Size = new System.Drawing.Size(51, 17);
            this.GuiroBox.TabIndex = 3;
            this.GuiroBox.Text = "Guiro";
            this.GuiroBox.UseVisualStyleBackColor = true;
            this.GuiroBox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // TimbaleBox
            // 
            this.TimbaleBox.AutoSize = true;
            this.TimbaleBox.Location = new System.Drawing.Point(12, 59);
            this.TimbaleBox.Name = "TimbaleBox";
            this.TimbaleBox.Size = new System.Drawing.Size(63, 17);
            this.TimbaleBox.TabIndex = 4;
            this.TimbaleBox.Text = "Timbale";
            this.TimbaleBox.UseVisualStyleBackColor = true;
            this.TimbaleBox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // BongoBox
            // 
            this.BongoBox.AutoSize = true;
            this.BongoBox.Location = new System.Drawing.Point(12, 82);
            this.BongoBox.Name = "BongoBox";
            this.BongoBox.Size = new System.Drawing.Size(57, 17);
            this.BongoBox.TabIndex = 5;
            this.BongoBox.Text = "Bongo";
            this.BongoBox.UseVisualStyleBackColor = true;
            this.BongoBox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // CowbellBox
            // 
            this.CowbellBox.AutoSize = true;
            this.CowbellBox.Location = new System.Drawing.Point(12, 105);
            this.CowbellBox.Name = "CowbellBox";
            this.CowbellBox.Size = new System.Drawing.Size(63, 17);
            this.CowbellBox.TabIndex = 6;
            this.CowbellBox.Text = "Cowbell";
            this.CowbellBox.UseVisualStyleBackColor = true;
            this.CowbellBox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // ClaveVol
            // 
            this.ClaveVol.LargeChange = 20;
            this.ClaveVol.Location = new System.Drawing.Point(90, 13);
            this.ClaveVol.Maximum = 254;
            this.ClaveVol.Name = "ClaveVol";
            this.ClaveVol.Size = new System.Drawing.Size(80, 17);
            this.ClaveVol.SmallChange = 20;
            this.ClaveVol.TabIndex = 7;
            this.ClaveVol.Value = 200;
            // 
            // GuiroVol
            // 
            this.GuiroVol.LargeChange = 20;
            this.GuiroVol.Location = new System.Drawing.Point(90, 36);
            this.GuiroVol.Maximum = 254;
            this.GuiroVol.Name = "GuiroVol";
            this.GuiroVol.Size = new System.Drawing.Size(80, 17);
            this.GuiroVol.SmallChange = 20;
            this.GuiroVol.TabIndex = 8;
            this.GuiroVol.Value = 200;
            // 
            // TimbaleVol
            // 
            this.TimbaleVol.LargeChange = 20;
            this.TimbaleVol.Location = new System.Drawing.Point(90, 59);
            this.TimbaleVol.Maximum = 254;
            this.TimbaleVol.Name = "TimbaleVol";
            this.TimbaleVol.Size = new System.Drawing.Size(80, 17);
            this.TimbaleVol.SmallChange = 20;
            this.TimbaleVol.TabIndex = 9;
            this.TimbaleVol.Value = 200;
            // 
            // BongoVol
            // 
            this.BongoVol.LargeChange = 20;
            this.BongoVol.Location = new System.Drawing.Point(90, 82);
            this.BongoVol.Maximum = 254;
            this.BongoVol.Name = "BongoVol";
            this.BongoVol.Size = new System.Drawing.Size(80, 17);
            this.BongoVol.SmallChange = 20;
            this.BongoVol.TabIndex = 10;
            this.BongoVol.Value = 200;
            // 
            // CowbellVol
            // 
            this.CowbellVol.LargeChange = 20;
            this.CowbellVol.Location = new System.Drawing.Point(90, 105);
            this.CowbellVol.Maximum = 254;
            this.CowbellVol.Name = "CowbellVol";
            this.CowbellVol.Size = new System.Drawing.Size(80, 17);
            this.CowbellVol.SmallChange = 20;
            this.CowbellVol.TabIndex = 11;
            this.CowbellVol.Value = 200;
            // 
            // ClaveSelect
            // 
            this.ClaveSelect.FormattingEnabled = true;
            this.ClaveSelect.Items.AddRange(new object[] {
            "3 - 2",
            "2 - 3"});
            this.ClaveSelect.Location = new System.Drawing.Point(194, 13);
            this.ClaveSelect.Name = "ClaveSelect";
            this.ClaveSelect.Size = new System.Drawing.Size(121, 21);
            this.ClaveSelect.TabIndex = 12;
            // 
            // MaracasBox
            // 
            this.MaracasBox.AutoSize = true;
            this.MaracasBox.Location = new System.Drawing.Point(12, 128);
            this.MaracasBox.Name = "MaracasBox";
            this.MaracasBox.Size = new System.Drawing.Size(67, 17);
            this.MaracasBox.TabIndex = 13;
            this.MaracasBox.Text = "Maracas";
            this.MaracasBox.UseVisualStyleBackColor = true;
            this.MaracasBox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // MaracasVol
            // 
            this.MaracasVol.LargeChange = 20;
            this.MaracasVol.Location = new System.Drawing.Point(90, 128);
            this.MaracasVol.Maximum = 254;
            this.MaracasVol.Name = "MaracasVol";
            this.MaracasVol.Size = new System.Drawing.Size(80, 17);
            this.MaracasVol.SmallChange = 20;
            this.MaracasVol.TabIndex = 14;
            this.MaracasVol.Value = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 254);
            this.Controls.Add(this.MaracasVol);
            this.Controls.Add(this.MaracasBox);
            this.Controls.Add(this.ClaveSelect);
            this.Controls.Add(this.CowbellVol);
            this.Controls.Add(this.BongoVol);
            this.Controls.Add(this.TimbaleVol);
            this.Controls.Add(this.GuiroVol);
            this.Controls.Add(this.ClaveVol);
            this.Controls.Add(this.CowbellBox);
            this.Controls.Add(this.BongoBox);
            this.Controls.Add(this.TimbaleBox);
            this.Controls.Add(this.GuiroBox);
            this.Controls.Add(this.ClaveBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.progressBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ClaveBox;
        private System.Windows.Forms.CheckBox GuiroBox;
        private System.Windows.Forms.CheckBox TimbaleBox;
        private System.Windows.Forms.CheckBox BongoBox;
        private System.Windows.Forms.CheckBox CowbellBox;
        private System.Windows.Forms.HScrollBar ClaveVol;
        private System.Windows.Forms.HScrollBar GuiroVol;
        private System.Windows.Forms.HScrollBar TimbaleVol;
        private System.Windows.Forms.HScrollBar BongoVol;
        private System.Windows.Forms.HScrollBar CowbellVol;
        private System.Windows.Forms.ComboBox ClaveSelect;
        private System.Windows.Forms.CheckBox MaracasBox;
        private System.Windows.Forms.HScrollBar MaracasVol;
    }
}

