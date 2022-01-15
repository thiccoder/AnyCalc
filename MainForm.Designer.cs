
namespace AnyCalc
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Out = new System.Windows.Forms.TextBox();
            this.FullString = new System.Windows.Forms.Label();
            this.BaseUpDown = new System.Windows.Forms.NumericUpDown();
            this.BaseGroupBox = new System.Windows.Forms.GroupBox();
            this.AcceptBaseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BaseUpDown)).BeginInit();
            this.BaseGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Out
            // 
            this.Out.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Out.BackColor = System.Drawing.SystemColors.Control;
            this.Out.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Out.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Out.Font = new System.Drawing.Font("OCR A Extended", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Out.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Out.HideSelection = false;
            this.Out.Location = new System.Drawing.Point(9, 41);
            this.Out.Margin = new System.Windows.Forms.Padding(0);
            this.Out.MaxLength = 10;
            this.Out.Name = "Out";
            this.Out.ReadOnly = true;
            this.Out.Size = new System.Drawing.Size(426, 77);
            this.Out.TabIndex = 0;
            this.Out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FullString
            // 
            this.FullString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FullString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FullString.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FullString.Font = new System.Drawing.Font("OCR A Extended", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.FullString.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FullString.Location = new System.Drawing.Point(9, 9);
            this.FullString.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.FullString.Name = "FullString";
            this.FullString.Size = new System.Drawing.Size(426, 22);
            this.FullString.TabIndex = 1;
            this.FullString.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BaseUpDown
            // 
            this.BaseUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BaseUpDown.Cursor = System.Windows.Forms.Cursors.Default;
            this.BaseUpDown.Font = new System.Drawing.Font("OCR A Extended", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BaseUpDown.Location = new System.Drawing.Point(0, 16);
            this.BaseUpDown.Margin = new System.Windows.Forms.Padding(0);
            this.BaseUpDown.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.BaseUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.BaseUpDown.Name = "BaseUpDown";
            this.BaseUpDown.Size = new System.Drawing.Size(55, 30);
            this.BaseUpDown.TabIndex = 2;
            this.BaseUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BaseUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // BaseGroupBox
            // 
            this.BaseGroupBox.Controls.Add(this.AcceptBaseButton);
            this.BaseGroupBox.Controls.Add(this.BaseUpDown);
            this.BaseGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BaseGroupBox.Font = new System.Drawing.Font("OCR A Extended", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BaseGroupBox.Location = new System.Drawing.Point(380, 126);
            this.BaseGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.BaseGroupBox.Name = "BaseGroupBox";
            this.BaseGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.BaseGroupBox.Size = new System.Drawing.Size(55, 65);
            this.BaseGroupBox.TabIndex = 1;
            this.BaseGroupBox.TabStop = false;
            this.BaseGroupBox.Text = "BASE";
            // 
            // AcceptBaseButton
            // 
            this.AcceptBaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AcceptBaseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AcceptBaseButton.Location = new System.Drawing.Point(0, 45);
            this.AcceptBaseButton.Margin = new System.Windows.Forms.Padding(0);
            this.AcceptBaseButton.Name = "AcceptBaseButton";
            this.AcceptBaseButton.Size = new System.Drawing.Size(55, 17);
            this.AcceptBaseButton.TabIndex = 3;
            this.AcceptBaseButton.Text = "CHANGE";
            this.AcceptBaseButton.UseVisualStyleBackColor = true;
            this.AcceptBaseButton.Click += new System.EventHandler(this.ChangeBase_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(444, 541);
            this.Controls.Add(this.BaseGroupBox);
            this.Controls.Add(this.FullString);
            this.Controls.Add(this.Out);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnyCalc";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.BaseUpDown)).EndInit();
            this.BaseGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Out;
        private System.Windows.Forms.Label FullString;
        private System.Windows.Forms.NumericUpDown BaseUpDown;
        private System.Windows.Forms.GroupBox BaseGroupBox;
        private System.Windows.Forms.Button AcceptBaseButton;
    }
}

