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
            //BandAlbumTrackDB.DropDatabase();        // FOR TESTING ONLY. Comment this line out to not drop existing database
            BandAlbumTrackDB.CreateDatabase();
            BandAlbumTrackDB.CreateTables();
            FillListBox();
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
                    BandAlbumTrackDB.InsertBandName(newBand);

                    FillListBox();

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
                    
                    string bandName = lbBandList.SelectedItem.ToString().Trim();
                    Album newAlbum = new Album { 
                        AlbumTitle = txtAlbumName.Text, 
                        ReleaseDate = dtReleaseDate.Value.ToLocalTime()
                    };

                    BandAlbumTrackDB.InsertAlbumName(newAlbum, bandName);
                    lbAlbumList.Items.Clear();
                    List<string> albumList = BandAlbumTrackDB.FetchAlbumData(bandName);
                    foreach (string album in albumList)
                    {
                        lbAlbumList.Items.Add(album);
                    }

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
                    // track name is added to the listbox then the textbox is cleared

                    Tracks newTrack = new Tracks { TrackTitle = txtTrackName.Text };
                    string albumName = lbAlbumList.SelectedItem.ToString().Trim();

                    BandAlbumTrackDB.InsertTrackName(newTrack, albumName);
                    lbTrackList.Items.Clear();
                    List<string> trackList = BandAlbumTrackDB.FetchTrackData(albumName);
                    foreach (string track in trackList)
                    {
                        lbTrackList.Items.Add(track);
                    }

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
                BandAlbumTrackDB.ClearAllData();

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

        private bool IsPresent(TextBox textBox, string name)
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

        private bool ClearAllData()
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

        private bool CheckIfSelected(ListBox listBox, string name)
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

        private void FillListBox()
        {
            lbBandList.Items.Clear();
            List<string> bandList = BandAlbumTrackDB.FetchAllData();
            foreach (string band in bandList)
            {
                lbBandList.Items.Add(band);
            }
        }

        private void lbBandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bandName = lbBandList.SelectedItem.ToString().Trim();
            lbAlbumList.Items.Clear();
            List<string> albumList = BandAlbumTrackDB.FetchAlbumData(bandName);
            foreach (string album in albumList)
            {
                lbAlbumList.Items.Add(album);
            }
        }

        private void lbAlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAlbumList.SelectedItem == null)
            {
                MessageBox.Show("whoops");
            }
            else
            {
                string albumName = lbAlbumList.SelectedItem.ToString().Trim();
                lbTrackList.Items.Clear();
                List<string> trackList = BandAlbumTrackDB.FetchTrackData(albumName);
                foreach (string track in trackList)
                {
                    lbTrackList.Items.Add(track);
                }
            }
        }
    }
}
