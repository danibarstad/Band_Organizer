namespace Band_Organizer
{
    partial class Band_Organizer
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
            this.lblBandName = new System.Windows.Forms.Label();
            this.lblAlbumName = new System.Windows.Forms.Label();
            this.lblTrackName = new System.Windows.Forms.Label();
            this.txtBandName = new System.Windows.Forms.TextBox();
            this.txtAlbumName = new System.Windows.Forms.TextBox();
            this.txtTrackName = new System.Windows.Forms.TextBox();
            this.lbBandList = new System.Windows.Forms.ListBox();
            this.lbAlbumList = new System.Windows.Forms.ListBox();
            this.lbTrackList = new System.Windows.Forms.ListBox();
            this.lblReleaseDate = new System.Windows.Forms.Label();
            this.dtReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddBand = new System.Windows.Forms.Button();
            this.btnAddAlbum = new System.Windows.Forms.Button();
            this.btnAddTrack = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.txtTrackNo = new System.Windows.Forms.TextBox();
            this.lblTrackNo = new System.Windows.Forms.Label();
            this.btnViewAlbum = new System.Windows.Forms.Button();
            this.btnDeleteBand = new System.Windows.Forms.Button();
            this.btnDeleteAlbum = new System.Windows.Forms.Button();
            this.btnDeleteTrack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBandName
            // 
            this.lblBandName.AutoSize = true;
            this.lblBandName.Location = new System.Drawing.Point(12, 9);
            this.lblBandName.Name = "lblBandName";
            this.lblBandName.Size = new System.Drawing.Size(66, 13);
            this.lblBandName.TabIndex = 0;
            this.lblBandName.Text = "Band Name:";
            // 
            // lblAlbumName
            // 
            this.lblAlbumName.AutoSize = true;
            this.lblAlbumName.Location = new System.Drawing.Point(276, 9);
            this.lblAlbumName.Name = "lblAlbumName";
            this.lblAlbumName.Size = new System.Drawing.Size(70, 13);
            this.lblAlbumName.TabIndex = 1;
            this.lblAlbumName.Text = "Album Name:";
            // 
            // lblTrackName
            // 
            this.lblTrackName.AutoSize = true;
            this.lblTrackName.Location = new System.Drawing.Point(551, 9);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(69, 13);
            this.lblTrackName.TabIndex = 2;
            this.lblTrackName.Text = "Track Name:";
            // 
            // txtBandName
            // 
            this.txtBandName.Location = new System.Drawing.Point(84, 6);
            this.txtBandName.Name = "txtBandName";
            this.txtBandName.Size = new System.Drawing.Size(186, 20);
            this.txtBandName.TabIndex = 0;
            // 
            // txtAlbumName
            // 
            this.txtAlbumName.Location = new System.Drawing.Point(359, 6);
            this.txtAlbumName.Name = "txtAlbumName";
            this.txtAlbumName.Size = new System.Drawing.Size(186, 20);
            this.txtAlbumName.TabIndex = 2;
            // 
            // txtTrackName
            // 
            this.txtTrackName.Location = new System.Drawing.Point(626, 6);
            this.txtTrackName.Name = "txtTrackName";
            this.txtTrackName.Size = new System.Drawing.Size(185, 20);
            this.txtTrackName.TabIndex = 5;
            // 
            // lbBandList
            // 
            this.lbBandList.FormattingEnabled = true;
            this.lbBandList.HorizontalScrollbar = true;
            this.lbBandList.Location = new System.Drawing.Point(84, 87);
            this.lbBandList.Name = "lbBandList";
            this.lbBandList.Size = new System.Drawing.Size(186, 238);
            this.lbBandList.TabIndex = 6;
            this.lbBandList.TabStop = false;
            this.lbBandList.SelectedIndexChanged += new System.EventHandler(this.lbBandList_SelectedIndexChanged);
            // 
            // lbAlbumList
            // 
            this.lbAlbumList.FormattingEnabled = true;
            this.lbAlbumList.HorizontalScrollbar = true;
            this.lbAlbumList.Location = new System.Drawing.Point(359, 87);
            this.lbAlbumList.Name = "lbAlbumList";
            this.lbAlbumList.Size = new System.Drawing.Size(186, 238);
            this.lbAlbumList.TabIndex = 7;
            this.lbAlbumList.TabStop = false;
            this.lbAlbumList.SelectedIndexChanged += new System.EventHandler(this.lbAlbumList_SelectedIndexChanged);
            // 
            // lbTrackList
            // 
            this.lbTrackList.FormattingEnabled = true;
            this.lbTrackList.HorizontalScrollbar = true;
            this.lbTrackList.Location = new System.Drawing.Point(626, 87);
            this.lbTrackList.Name = "lbTrackList";
            this.lbTrackList.Size = new System.Drawing.Size(185, 238);
            this.lbTrackList.TabIndex = 8;
            this.lbTrackList.TabStop = false;
            // 
            // lblReleaseDate
            // 
            this.lblReleaseDate.AutoSize = true;
            this.lblReleaseDate.Location = new System.Drawing.Point(276, 38);
            this.lblReleaseDate.Name = "lblReleaseDate";
            this.lblReleaseDate.Size = new System.Drawing.Size(75, 13);
            this.lblReleaseDate.TabIndex = 9;
            this.lblReleaseDate.Text = "Release Date:";
            // 
            // dtReleaseDate
            // 
            this.dtReleaseDate.CustomFormat = "yyyy";
            this.dtReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtReleaseDate.Location = new System.Drawing.Point(359, 32);
            this.dtReleaseDate.Name = "dtReleaseDate";
            this.dtReleaseDate.ShowUpDown = true;
            this.dtReleaseDate.Size = new System.Drawing.Size(51, 20);
            this.dtReleaseDate.TabIndex = 3;
            // 
            // btnAddBand
            // 
            this.btnAddBand.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddBand.Location = new System.Drawing.Point(84, 58);
            this.btnAddBand.Name = "btnAddBand";
            this.btnAddBand.Size = new System.Drawing.Size(90, 23);
            this.btnAddBand.TabIndex = 1;
            this.btnAddBand.Text = "Add Band";
            this.btnAddBand.UseVisualStyleBackColor = true;
            this.btnAddBand.Click += new System.EventHandler(this.btnAddBand_Click);
            // 
            // btnAddAlbum
            // 
            this.btnAddAlbum.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddAlbum.Location = new System.Drawing.Point(359, 58);
            this.btnAddAlbum.Name = "btnAddAlbum";
            this.btnAddAlbum.Size = new System.Drawing.Size(90, 23);
            this.btnAddAlbum.TabIndex = 4;
            this.btnAddAlbum.Text = "Add Album";
            this.btnAddAlbum.UseVisualStyleBackColor = true;
            this.btnAddAlbum.Click += new System.EventHandler(this.btnAddAlbum_Click);
            // 
            // btnAddTrack
            // 
            this.btnAddTrack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddTrack.Location = new System.Drawing.Point(625, 58);
            this.btnAddTrack.Name = "btnAddTrack";
            this.btnAddTrack.Size = new System.Drawing.Size(90, 23);
            this.btnAddTrack.TabIndex = 7;
            this.btnAddTrack.Text = "Add Track";
            this.btnAddTrack.UseVisualStyleBackColor = true;
            this.btnAddTrack.Click += new System.EventHandler(this.btnAddTrack_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(626, 361);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(185, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(626, 331);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(185, 23);
            this.btnClearAll.TabIndex = 8;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // txtTrackNo
            // 
            this.txtTrackNo.Location = new System.Drawing.Point(626, 32);
            this.txtTrackNo.Name = "txtTrackNo";
            this.txtTrackNo.Size = new System.Drawing.Size(31, 20);
            this.txtTrackNo.TabIndex = 6;
            // 
            // lblTrackNo
            // 
            this.lblTrackNo.AutoSize = true;
            this.lblTrackNo.Location = new System.Drawing.Point(551, 35);
            this.lblTrackNo.Name = "lblTrackNo";
            this.lblTrackNo.Size = new System.Drawing.Size(48, 13);
            this.lblTrackNo.TabIndex = 11;
            this.lblTrackNo.Text = "Track #:";
            // 
            // btnViewAlbum
            // 
            this.btnViewAlbum.Location = new System.Drawing.Point(359, 331);
            this.btnViewAlbum.Name = "btnViewAlbum";
            this.btnViewAlbum.Size = new System.Drawing.Size(186, 23);
            this.btnViewAlbum.TabIndex = 12;
            this.btnViewAlbum.Text = "View Album";
            this.btnViewAlbum.UseVisualStyleBackColor = true;
            this.btnViewAlbum.Click += new System.EventHandler(this.btnViewAlbum_Click);
            // 
            // btnDeleteBand
            // 
            this.btnDeleteBand.Location = new System.Drawing.Point(180, 58);
            this.btnDeleteBand.Name = "btnDeleteBand";
            this.btnDeleteBand.Size = new System.Drawing.Size(90, 23);
            this.btnDeleteBand.TabIndex = 13;
            this.btnDeleteBand.Text = "Delete Band";
            this.btnDeleteBand.UseVisualStyleBackColor = true;
            this.btnDeleteBand.Click += new System.EventHandler(this.btnDeleteBand_Click);
            // 
            // btnDeleteAlbum
            // 
            this.btnDeleteAlbum.Location = new System.Drawing.Point(455, 58);
            this.btnDeleteAlbum.Name = "btnDeleteAlbum";
            this.btnDeleteAlbum.Size = new System.Drawing.Size(90, 23);
            this.btnDeleteAlbum.TabIndex = 14;
            this.btnDeleteAlbum.Text = "Delete Album";
            this.btnDeleteAlbum.UseVisualStyleBackColor = true;
            this.btnDeleteAlbum.Click += new System.EventHandler(this.btnDeleteAlbum_Click);
            // 
            // btnDeleteTrack
            // 
            this.btnDeleteTrack.Location = new System.Drawing.Point(721, 58);
            this.btnDeleteTrack.Name = "btnDeleteTrack";
            this.btnDeleteTrack.Size = new System.Drawing.Size(90, 23);
            this.btnDeleteTrack.TabIndex = 15;
            this.btnDeleteTrack.Text = "Delete Track";
            this.btnDeleteTrack.UseVisualStyleBackColor = true;
            this.btnDeleteTrack.Click += new System.EventHandler(this.btnDeleteTrack_Click);
            // 
            // Band_Organizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(828, 396);
            this.Controls.Add(this.btnDeleteTrack);
            this.Controls.Add(this.btnDeleteAlbum);
            this.Controls.Add(this.btnDeleteBand);
            this.Controls.Add(this.btnViewAlbum);
            this.Controls.Add(this.lblTrackNo);
            this.Controls.Add(this.txtTrackNo);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddTrack);
            this.Controls.Add(this.btnAddAlbum);
            this.Controls.Add(this.btnAddBand);
            this.Controls.Add(this.dtReleaseDate);
            this.Controls.Add(this.lblReleaseDate);
            this.Controls.Add(this.lbTrackList);
            this.Controls.Add(this.lbAlbumList);
            this.Controls.Add(this.lbBandList);
            this.Controls.Add(this.txtTrackName);
            this.Controls.Add(this.txtAlbumName);
            this.Controls.Add(this.txtBandName);
            this.Controls.Add(this.lblTrackName);
            this.Controls.Add(this.lblAlbumName);
            this.Controls.Add(this.lblBandName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Band_Organizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Band Organizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBandName;
        private System.Windows.Forms.Label lblAlbumName;
        private System.Windows.Forms.Label lblTrackName;
        private System.Windows.Forms.TextBox txtBandName;
        private System.Windows.Forms.TextBox txtAlbumName;
        private System.Windows.Forms.TextBox txtTrackName;
        private System.Windows.Forms.ListBox lbBandList;
        private System.Windows.Forms.ListBox lbAlbumList;
        private System.Windows.Forms.ListBox lbTrackList;
        private System.Windows.Forms.Label lblReleaseDate;
        private System.Windows.Forms.DateTimePicker dtReleaseDate;
        private System.Windows.Forms.Button btnAddBand;
        private System.Windows.Forms.Button btnAddAlbum;
        private System.Windows.Forms.Button btnAddTrack;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.TextBox txtTrackNo;
        private System.Windows.Forms.Label lblTrackNo;
        private System.Windows.Forms.Button btnViewAlbum;
        private System.Windows.Forms.Button btnDeleteBand;
        private System.Windows.Forms.Button btnDeleteAlbum;
        private System.Windows.Forms.Button btnDeleteTrack;
    }
}

