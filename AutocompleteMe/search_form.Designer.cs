namespace AutocompleteMe
{
    partial class search_form
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
            this.search_textBox = new System.Windows.Forms.TextBox();
            this.bubbleSort_button = new System.Windows.Forms.Button();
            this.mergeSort_button = new System.Windows.Forms.Button();
            this.suggestions_listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // search_textBox
            // 
            this.search_textBox.BackColor = System.Drawing.Color.White;
            this.search_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search_textBox.Location = new System.Drawing.Point(88, 12);
            this.search_textBox.Multiline = true;
            this.search_textBox.Name = "search_textBox";
            this.search_textBox.Size = new System.Drawing.Size(700, 31);
            this.search_textBox.TabIndex = 0;
            this.search_textBox.TextChanged += new System.EventHandler(this.search_textBox_TextChanged);
            this.search_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.search_textBox_KeyPress);
            // 
            // bubbleSort_button
            // 
            this.bubbleSort_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bubbleSort_button.FlatAppearance.BorderSize = 0;
            this.bubbleSort_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bubbleSort_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bubbleSort_button.ForeColor = System.Drawing.Color.Black;
            this.bubbleSort_button.Location = new System.Drawing.Point(50, 195);
            this.bubbleSort_button.Name = "bubbleSort_button";
            this.bubbleSort_button.Size = new System.Drawing.Size(101, 62);
            this.bubbleSort_button.TabIndex = 1;
            this.bubbleSort_button.Text = "Bubble Sort Suggestions";
            this.bubbleSort_button.UseVisualStyleBackColor = false;
            this.bubbleSort_button.Click += new System.EventHandler(this.bubbleSort_button_Click);
            // 
            // mergeSort_button
            // 
            this.mergeSort_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mergeSort_button.FlatAppearance.BorderSize = 0;
            this.mergeSort_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mergeSort_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mergeSort_button.ForeColor = System.Drawing.Color.Black;
            this.mergeSort_button.Location = new System.Drawing.Point(50, 272);
            this.mergeSort_button.Name = "mergeSort_button";
            this.mergeSort_button.Size = new System.Drawing.Size(101, 62);
            this.mergeSort_button.TabIndex = 2;
            this.mergeSort_button.Text = "Merge Sort Suggestions";
            this.mergeSort_button.UseVisualStyleBackColor = false;
            this.mergeSort_button.Click += new System.EventHandler(this.mergeSort_button_Click);
            // 
            // suggestions_listBox
            // 
            this.suggestions_listBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suggestions_listBox.FormattingEnabled = true;
            this.suggestions_listBox.ItemHeight = 24;
            this.suggestions_listBox.Location = new System.Drawing.Point(88, 42);
            this.suggestions_listBox.Name = "suggestions_listBox";
            this.suggestions_listBox.Size = new System.Drawing.Size(700, 292);
            this.suggestions_listBox.TabIndex = 3;
            this.suggestions_listBox.Visible = false;
            // 
            // search_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::AutocompleteMe.Properties.Resources._31_search_logo_design__2_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 503);
            this.Controls.Add(this.suggestions_listBox);
            this.Controls.Add(this.mergeSort_button);
            this.Controls.Add(this.bubbleSort_button);
            this.Controls.Add(this.search_textBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "search_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Engine";
            this.Load += new System.EventHandler(this.search_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox search_textBox;
        private System.Windows.Forms.Button bubbleSort_button;
        private System.Windows.Forms.Button mergeSort_button;
        private System.Windows.Forms.ListBox suggestions_listBox;
    }
}

