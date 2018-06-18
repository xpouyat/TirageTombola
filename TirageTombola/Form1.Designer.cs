namespace TirageTombola
{
    partial class Tombola
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
            this.buttonTirage = new System.Windows.Forms.Button();
            this.buttonLots = new System.Windows.Forms.Button();
            this.buttonElevesProgrammes = new System.Windows.Forms.Button();
            this.xlspath = new System.Windows.Forms.Label();
            this.infolabellots = new System.Windows.Forms.Label();
            this.xlspath_Eleves = new System.Windows.Forms.Label();
            this.infolabel_Programmes = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonTirage
            // 
            this.buttonTirage.Enabled = false;
            this.buttonTirage.Location = new System.Drawing.Point(25, 207);
            this.buttonTirage.Name = "buttonTirage";
            this.buttonTirage.Size = new System.Drawing.Size(114, 39);
            this.buttonTirage.TabIndex = 0;
            this.buttonTirage.Text = "Tirage !";
            this.buttonTirage.UseVisualStyleBackColor = true;
            this.buttonTirage.Click += new System.EventHandler(this.buttonTirage_Click);
            // 
            // buttonLots
            // 
            this.buttonLots.Location = new System.Drawing.Point(25, 24);
            this.buttonLots.Name = "buttonLots";
            this.buttonLots.Size = new System.Drawing.Size(190, 37);
            this.buttonLots.TabIndex = 1;
            this.buttonLots.Text = "Fichier Excel des lots...";
            this.buttonLots.UseVisualStyleBackColor = true;
            this.buttonLots.Click += new System.EventHandler(this.buttonLots_Click);
            // 
            // buttonElevesProgrammes
            // 
            this.buttonElevesProgrammes.Enabled = false;
            this.buttonElevesProgrammes.Location = new System.Drawing.Point(25, 108);
            this.buttonElevesProgrammes.Name = "buttonElevesProgrammes";
            this.buttonElevesProgrammes.Size = new System.Drawing.Size(190, 37);
            this.buttonElevesProgrammes.TabIndex = 2;
            this.buttonElevesProgrammes.Text = "Fichier Excel des Programmes/Eleves (optionnel)...";
            this.buttonElevesProgrammes.UseVisualStyleBackColor = true;
            this.buttonElevesProgrammes.Click += new System.EventHandler(this.buttonElevesProgrammes_Click);
            // 
            // xlspath
            // 
            this.xlspath.AutoSize = true;
            this.xlspath.Location = new System.Drawing.Point(278, 24);
            this.xlspath.Name = "xlspath";
            this.xlspath.Size = new System.Drawing.Size(35, 13);
            this.xlspath.TabIndex = 3;
            this.xlspath.Text = "label1";
            // 
            // infolabellots
            // 
            this.infolabellots.AutoSize = true;
            this.infolabellots.Location = new System.Drawing.Point(278, 48);
            this.infolabellots.Name = "infolabellots";
            this.infolabellots.Size = new System.Drawing.Size(35, 13);
            this.infolabellots.TabIndex = 4;
            this.infolabellots.Text = "label1";
            // 
            // xlspath_Eleves
            // 
            this.xlspath_Eleves.AutoSize = true;
            this.xlspath_Eleves.Location = new System.Drawing.Point(278, 108);
            this.xlspath_Eleves.Name = "xlspath_Eleves";
            this.xlspath_Eleves.Size = new System.Drawing.Size(35, 13);
            this.xlspath_Eleves.TabIndex = 5;
            this.xlspath_Eleves.Text = "label1";
            // 
            // infolabel_Programmes
            // 
            this.infolabel_Programmes.AutoSize = true;
            this.infolabel_Programmes.Location = new System.Drawing.Point(278, 135);
            this.infolabel_Programmes.Name = "infolabel_Programmes";
            this.infolabel_Programmes.Size = new System.Drawing.Size(35, 13);
            this.infolabel_Programmes.TabIndex = 6;
            this.infolabel_Programmes.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(245, 223);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(470, 23);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // Tombola
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 276);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.infolabel_Programmes);
            this.Controls.Add(this.xlspath_Eleves);
            this.Controls.Add(this.infolabellots);
            this.Controls.Add(this.xlspath);
            this.Controls.Add(this.buttonElevesProgrammes);
            this.Controls.Add(this.buttonLots);
            this.Controls.Add(this.buttonTirage);
            this.Name = "Tombola";
            this.Text = "Tombola";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tombola_FormClosing);
            this.Load += new System.EventHandler(this.Tombola_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTirage;
        private System.Windows.Forms.Button buttonLots;
        private System.Windows.Forms.Button buttonElevesProgrammes;
        private System.Windows.Forms.Label xlspath;
        private System.Windows.Forms.Label infolabellots;
        private System.Windows.Forms.Label xlspath_Eleves;
        private System.Windows.Forms.Label infolabel_Programmes;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

