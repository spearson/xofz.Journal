namespace xofz.Journal.UI.Forms
{
    partial class FormMainUi
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.entriesGrid = new System.Windows.Forms.DataGridView();
            this.createdTimestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedTimestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.introColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.createdTextBox = new System.Windows.Forms.TextBox();
            this.modifiedTextBox = new System.Windows.Forms.TextBox();
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.submitKey = new System.Windows.Forms.Button();
            this.newKey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // entriesGrid
            // 
            this.entriesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entriesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.createdTimestampColumn,
            this.modifiedTimestampColumn,
            this.introColumn});
            this.entriesGrid.Location = new System.Drawing.Point(236, 0);
            this.entriesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.entriesGrid.Name = "entriesGrid";
            this.entriesGrid.ReadOnly = true;
            this.entriesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entriesGrid.Size = new System.Drawing.Size(772, 222);
            this.entriesGrid.TabIndex = 2;
            this.entriesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.searchResultsViewer_CellClick);
            // 
            // createdTimestampColumn
            // 
            this.createdTimestampColumn.HeaderText = "Created Timestamp";
            this.createdTimestampColumn.Name = "createdTimestampColumn";
            this.createdTimestampColumn.ReadOnly = true;
            this.createdTimestampColumn.Width = 200;
            // 
            // modifiedTimestampColumn
            // 
            this.modifiedTimestampColumn.HeaderText = "Modified Timestamp";
            this.modifiedTimestampColumn.Name = "modifiedTimestampColumn";
            this.modifiedTimestampColumn.ReadOnly = true;
            this.modifiedTimestampColumn.Width = 200;
            // 
            // introColumn
            // 
            this.introColumn.HeaderText = "";
            this.introColumn.Name = "introColumn";
            this.introColumn.ReadOnly = true;
            this.introColumn.Width = 310;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Entry View";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Created:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 302);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Modified:";
            // 
            // createdTextBox
            // 
            this.createdTextBox.Location = new System.Drawing.Point(83, 271);
            this.createdTextBox.Name = "createdTextBox";
            this.createdTextBox.ReadOnly = true;
            this.createdTextBox.Size = new System.Drawing.Size(211, 22);
            this.createdTextBox.TabIndex = 6;
            // 
            // modifiedTextBox
            // 
            this.modifiedTextBox.Location = new System.Drawing.Point(83, 299);
            this.modifiedTextBox.Name = "modifiedTextBox";
            this.modifiedTextBox.ReadOnly = true;
            this.modifiedTextBox.Size = new System.Drawing.Size(211, 22);
            this.modifiedTextBox.TabIndex = 7;
            // 
            // contentTextBox
            // 
            this.contentTextBox.Location = new System.Drawing.Point(81, 327);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.ReadOnly = true;
            this.contentTextBox.Size = new System.Drawing.Size(915, 222);
            this.contentTextBox.TabIndex = 8;
            // 
            // submitKey
            // 
            this.submitKey.AutoSize = true;
            this.submitKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.submitKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.submitKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.submitKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitKey.Location = new System.Drawing.Point(919, 289);
            this.submitKey.Name = "submitKey";
            this.submitKey.Size = new System.Drawing.Size(77, 32);
            this.submitKey.TabIndex = 9;
            this.submitKey.Text = "Submit";
            this.submitKey.UseVisualStyleBackColor = true;
            this.submitKey.Click += new System.EventHandler(this.submitKey_Click);
            // 
            // newKey
            // 
            this.newKey.AutoSize = true;
            this.newKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.newKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.newKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.newKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newKey.Location = new System.Drawing.Point(125, 190);
            this.newKey.Name = "newKey";
            this.newKey.Size = new System.Drawing.Size(102, 32);
            this.newKey.TabIndex = 10;
            this.newKey.Text = "New Entry";
            this.newKey.UseVisualStyleBackColor = true;
            this.newKey.Click += new System.EventHandler(this.newKey_Click);
            // 
            // FormMainUi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.newKey);
            this.Controls.Add(this.submitKey);
            this.Controls.Add(this.contentTextBox);
            this.Controls.Add(this.modifiedTextBox);
            this.Controls.Add(this.createdTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.entriesGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMainUi";
            this.Text = "xofz.Journal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.entriesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView entriesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdTimestampColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedTimestampColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn introColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox createdTextBox;
        private System.Windows.Forms.TextBox modifiedTextBox;
        private System.Windows.Forms.TextBox contentTextBox;
        private System.Windows.Forms.Button submitKey;
        private System.Windows.Forms.Button newKey;
    }
}

