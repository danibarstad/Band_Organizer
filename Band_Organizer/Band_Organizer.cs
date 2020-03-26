using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Band_Organizer
{
    public partial class Band_Organizer : Form
    {
        public Band_Organizer()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // closes the form
            this.Close();
        }

        private void btnAddBand_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPresent(txtBandName, "Band Name"))
                {
                    // the band name is added to the listbox 
                    // then clears the textbox and moves the focus to the album name textbox

                    Band newBand = new Band { BandName = txtBandName.Text };
                    lbBandList.Items.Add(newBand.BandName);

                    BandAlbumTrackDB.InsertBandName(newBand);

                    txtBandName.Clear();
                    txtAlbumName.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }

        private void btnAddAlbum_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPresent(txtAlbumName, "Album Name") && CheckIfSelected(lbBandList, lblBandName.Text))
                {
                    // album name is added to the listbox 
                    // then the textbox is cleared and focus is moved to the track name textbox

                    Album newAlbum = new Album { 
                        AlbumTitle = txtAlbumName.Text, 
                        ReleaseDate = Convert.ToString(dtReleaseDate.Text) };                    

                    lbAlbumList.Items.Add(
                        newAlbum.AlbumTitle + "\t" + 
                        newAlbum.ReleaseDate);

                    txtAlbumName.Clear();
                    dtReleaseDate.ResetText();
                    txtTrackName.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }

        private void btnAddTrack_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPresent(txtTrackName, "Track Name") && CheckIfSelected(lbAlbumList, lblAlbumName.Text))
                {
                    Tracks newTrack = new Tracks { TrackTitle = txtTrackName.Text };
                    // track name is added to the listbox then the textbox is cleared
                    lbTrackList.Items.Add(txtTrackName.Text);

                    BandAlbumTrackDB.InsertTrackName(newTrack);

                    txtTrackName.Clear();
                    txtTrackName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (ClearAllData())
            {
                // clears all data from textboxes and listboxes
                txtBandName.Clear();
                txtAlbumName.Clear();
                txtTrackName.Clear();

                dtReleaseDate.ResetText();

                lbBandList.Items.Clear();
                lbAlbumList.Items.Clear();
                lbTrackList.Items.Clear();
            }
        }

        public bool IsPresent(TextBox textBox, string name)
        {
            // checks if a textbox is empty, notifies user if true
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", 
                    "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool ClearAllData()
        {
            // warns user when attempting to clear all data
            string message = "Are you sure you want to clear all data?";

            DialogResult button =
                MessageBox.Show(message, "Warning",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (button == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public bool CheckIfSelected(ListBox listBox, string name)
        {
            // checks to make sure a band or album is selected before entering more input
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("You must make a selection from " + name, "Entry Error");
                return false;
            }
            else
                return true;
        }
    }
}
