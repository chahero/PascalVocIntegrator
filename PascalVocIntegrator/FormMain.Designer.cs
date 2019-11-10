namespace PascalVocIntegrator
{
    partial class FormMain
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
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.buttonSaveAsIntegratedPascalVocFormat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllowDrop = true;
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Location = new System.Drawing.Point(11, 11);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(639, 507);
            this.listViewFiles.TabIndex = 6;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            this.listViewFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragDrop);
            this.listViewFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFiles_DragEnter);
            this.listViewFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewFiles_KeyDown);
            // 
            // buttonSaveAsIntegratedPascalVocFormat
            // 
            this.buttonSaveAsIntegratedPascalVocFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveAsIntegratedPascalVocFormat.Location = new System.Drawing.Point(11, 529);
            this.buttonSaveAsIntegratedPascalVocFormat.Name = "buttonSaveAsIntegratedPascalVocFormat";
            this.buttonSaveAsIntegratedPascalVocFormat.Size = new System.Drawing.Size(639, 31);
            this.buttonSaveAsIntegratedPascalVocFormat.TabIndex = 5;
            this.buttonSaveAsIntegratedPascalVocFormat.Text = "Integrated PascalVoc Files and Save";
            this.buttonSaveAsIntegratedPascalVocFormat.UseVisualStyleBackColor = true;
            this.buttonSaveAsIntegratedPascalVocFormat.Click += new System.EventHandler(this.buttonSaveAsIntegratedPascalVocFormat_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 570);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.buttonSaveAsIntegratedPascalVocFormat);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMain";
            this.Text = "PascalVocIntegratorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.Button buttonSaveAsIntegratedPascalVocFormat;
    }
}