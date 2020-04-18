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
        Validation validate = new Validation();

        public Band_Organizer()
        {
            InitializeComponent();
            BandAlbumTrackDB.CreateDatabase();                              // Create database
            BandAlbumTrackDB.CreateTables();                                // Create tables
            FillListBox(BandAlbumTrackDB.FetchBandData(), lbBandList);      // Fill band listbox
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
                if (validate.IsInList(txtBandName, lbBandList)      &&
                    validate.IsPresent(txtBandName, "Band Name"))
                {
                    // the band name is added to the listbox 
                    // then clears the textbox and moves the focus to the album name textbox

                    Band newBand = new Band { 
                        BandName = txtBandName.Text };
                    BandAlbumTrackDB.InsertBand(newBand);

                    List<string> bandList = BandAlbumTrackDB.FetchBandData();                    
                    FillListBox(bandList, lbBandList, lbAlbumList, lbTrackList);

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
                if (validate.IsInList(txtAlbumName, lbAlbumList)            &&
                    validate.IsPresent(txtAlbumName, "Album Name")          && 
                    validate.IsSelected(lbBandList, lblBandName.Text))
                {
                    // album name is added to the listbox 
                    // then the textbox is cleared and focus is moved to the track name textbox
                    
                    Album newAlbum = new Album { 
                        AlbumTitle = txtAlbumName.Text, 
                        ReleaseDate = dtReleaseDate.Value.ToLocalTime()};
                    string bandName = TrimString(lbBandList);
                    BandAlbumTrackDB.InsertAlbum(newAlbum, bandName);

                    List<string> albumList = BandAlbumTrackDB.FetchAlbumData(bandName);
                    FillListBox(albumList, lbAlbumList, lbTrackList);

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
                if (validate.IsInt(txtTrackNo)                              &&
                    validate.IsInList(txtTrackName, lbTrackList)            &&
                    validate.IsPresent(txtTrackName, "Track Name")          &&
                    validate.IsSelected(lbAlbumList, lblAlbumName.Text))
                {
                    // track name is added to the listbox then the textbox is cleared

                    Tracks newTrack = new Tracks { 
                        TrackTitle = txtTrackName.Text,
                        TrackNumber = Int32.Parse(txtTrackNo.Text)};
                    string bandName = TrimString(lbBandList);
                    string albumName = TrimString(lbAlbumList);
                    BandAlbumTrackDB.InsertTrack(newTrack, bandName, albumName);

                    Dictionary<int, string> trackList =
                        BandAlbumTrackDB.FetchTrackData(bandName, albumName);
                    FillDictionary(trackList, lbTrackList);

                    txtTrackName.Clear();
                    txtTrackNo.Clear();
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

        private void btnViewAlbum_Click(object sender, EventArgs e)
        {
            // displays windows with all data for the selected album

            if (validate.IsSelected(lbAlbumList, "Album List"))
            {
                string newTrackList = "";
                string band = TrimString(lbBandList);
                string album = TrimString(lbAlbumList);
                Dictionary<int, string> trackList = BandAlbumTrackDB.FetchTrackData(band, album);

                foreach (var track in trackList)
                {
                    newTrackList += $"{track.Key}\t{track.Value}\n";
                }

                MessageBox.Show($"Band:\t{band}\n" +
                                $"Album:\t{album}\n" +
                                $"Tracks:\n" +
                                $"{newTrackList}", "Album Data");
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (ClearAllData())
            {
                // clears data from database, input boxes and listboxes

                BandAlbumTrackDB.ClearAllData();
                ClearInput();
                ClearListBoxes();
            }
        }

        private bool ClearAllData()
        {
            // warns user when attempting to clear all data

            DialogResult button =
                MessageBox.Show("Are you sure you want to clear all data? " +
                                "This will delete all data from the database.", 
                                "Warning",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (button == DialogResult.Yes)
                return true;
            else
                return false;
        }

        private void FillListBox(List<string> list, ListBox listBox,
                                 ListBox ListBox2 = default(ListBox),
                                 ListBox ListBox3 = default(ListBox))
        {
            // fills listbox with list items 

            listBox.Items.Clear();
            if (ListBox2 != null)
                ListBox2.Items.Clear();
            if (ListBox3 != null)
                ListBox3.Items.Clear();

            foreach (string item in list)
                listBox.Items.Add(item);
        }

        private void FillDictionary(Dictionary<int, string> tracks, ListBox listBox)
        {
            // fills listbox with dictionary key-value pair

            listBox.Items.Clear();

            foreach (var item in tracks)
                listBox.Items.Add(item.Key.ToString() + "\t" + item.Value);
        }

        private void lbBandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clears and refills band listbox when selected index changes

            ClearInput();

            if (lbBandList.SelectedItem == null)
                MessageBox.Show("Please select an item from the list.", 
                                "Whoops");
            else
            {
                string bandName = TrimString(lbBandList);
                List<string> albumList = BandAlbumTrackDB.FetchAlbumData(bandName);
                FillListBox(albumList, lbAlbumList, lbTrackList);
            }
        }

        private void lbAlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clears and refills albums listbox when selected index changes

            ClearInput();

            if (lbAlbumList.SelectedItem == null)
                MessageBox.Show("Please select an item from the list.", 
                                "Whoops");
            else
            {
                string bandName = TrimString(lbBandList);
                string albumName = TrimString(lbAlbumList);
                Dictionary<int, string> trackList = 
                    BandAlbumTrackDB.FetchTrackData(bandName, albumName);

                FillDictionary(trackList, lbTrackList);
            }
        }

        private void btnDeleteBand_Click(object sender, EventArgs e)
        {
            // deletes band (and associated albums and tracks) from database

            if (validate.IsSelected(lbBandList, "Band List"))
            {
                string band = TrimString(lbBandList);
                BandAlbumTrackDB.DeleteBand(band);

                List<string> bandList = BandAlbumTrackDB.FetchBandData();
                FillListBox(bandList, lbBandList, lbAlbumList, lbTrackList);
            }
        }

        private void btnDeleteAlbum_Click(object sender, EventArgs e)
        {
            // deletes album (and associated tracks) from database

            if (validate.IsSelected(lbAlbumList, "Album List"))
            {
                string band = TrimString(lbBandList);
                string album = TrimString(lbAlbumList);
                BandAlbumTrackDB.DeleteAlbum(band, album);

                List<string> albumList = BandAlbumTrackDB.FetchAlbumData(band);                
                FillListBox(albumList, lbAlbumList, lbTrackList);
            }
        }

        private void btnDeleteTrack_Click(object sender, EventArgs e)
        {
            // deletes track from database

            if (validate.IsSelected(lbTrackList, "Track List"))
            {
                string band = TrimString(lbBandList);
                string album = TrimString(lbAlbumList);
                string[] track = lbTrackList.SelectedItem.ToString().Split('\t');
                BandAlbumTrackDB.DeleteTrack(band, album, track);

                Dictionary<int, string> trackDict = BandAlbumTrackDB.FetchTrackData(band, album);                
                FillDictionary(trackDict, lbTrackList);
            }
        }

        private string TrimString(ListBox listBox)
        {
            // gets selected item from listBox and trim any space

            return listBox.SelectedItem.ToString().Trim();
        }

        private void ClearInput()
        {
            // clears all input boxes

            txtTrackNo.Clear();
            txtBandName.Clear();
            txtAlbumName.Clear();
            txtTrackName.Clear();

            dtReleaseDate.ResetText();
        }

        private void ClearListBoxes()
        {
            // clears all listboxes

            lbBandList.Items.Clear();
            lbAlbumList.Items.Clear();
            lbTrackList.Items.Clear();
        }
    }
}
