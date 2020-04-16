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
            //BandAlbumTrackDB.DropDatabase();        // FOR TESTING ONLY. Comment this line out to not drop existing database
            BandAlbumTrackDB.CreateDatabase();
            BandAlbumTrackDB.CreateTables();
            FillBandListBox();
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
                if (validate.IsPresent(txtBandName, "Band Name") && 
                    validate.IsInList(txtBandName, lbBandList))
                {
                    // the band name is added to the listbox 
                    // then clears the textbox and moves the focus to the album name textbox

                    Band newBand = new Band { BandName = txtBandName.Text };
                    BandAlbumTrackDB.InsertBand(newBand);

                    FillBandListBox();

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
                if (validate.IsPresent(txtAlbumName, "Album Name")           && 
                    validate.IsSelected(lbBandList, lblBandName.Text)   && 
                    validate.IsInList(txtAlbumName, lbAlbumList))
                {
                    // album name is added to the listbox 
                    // then the textbox is cleared and focus is moved to the track name textbox
                    
                    Album newAlbum = new Album { 
                        AlbumTitle = txtAlbumName.Text, 
                        ReleaseDate = dtReleaseDate.Value.ToLocalTime()
                    };

                    string bandName = lbBandList.SelectedItem.ToString().Trim();
                    BandAlbumTrackDB.InsertAlbum(newAlbum, bandName);
                    List<string> albumList = BandAlbumTrackDB.FetchAlbumData(bandName);

                    FillListBox(albumList, lbAlbumList);

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
                if (validate.IsPresent(txtTrackName, "Track Name")           &&
                    validate.IsSelected(lbAlbumList, lblAlbumName.Text)      &&
                    validate.IsInList(txtTrackName, lbTrackList)             &&
                    validate.IsInt(txtTrackNo))
                {
                    // track name is added to the listbox then the textbox is cleared

                    Tracks newTrack = new Tracks { 
                        TrackTitle = txtTrackName.Text,
                        TrackNumber = Int32.Parse(txtTrackNo.Text)
                    };
                    string bandName = lbBandList.SelectedItem.ToString().Trim();
                    string albumName = lbAlbumList.SelectedItem.ToString().Trim();

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
            string band = lbBandList.SelectedItem.ToString().Trim();
            string album = lbAlbumList.SelectedItem.ToString().Trim();
            Dictionary<int, string> trackList = BandAlbumTrackDB.FetchTrackData(band, album);
            string newTrackList = "";

            foreach (var track in trackList)
            {
                newTrackList += $"{track.Key}\t{track.Value}\n";
            }

            MessageBox.Show($"Band:\t{band}\n" +
                            $"Album:\t{album}\n" +
                            $"Tracks:\n" +
                            $"{newTrackList}", "Album Data");
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
                txtTrackNo.Clear();

                dtReleaseDate.ResetText();

                lbBandList.Items.Clear();
                lbAlbumList.Items.Clear();
                lbTrackList.Items.Clear();
            }
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

        private void FillBandListBox()
        {
            // TODO: is this necessary?
            List<string> bandList = BandAlbumTrackDB.FetchBandData();
            FillListBox(bandList, lbBandList);
        }

        private void FillListBox(List<string> list, ListBox listBox,
                                 ListBox secondListBox = default(ListBox))
        {
            // fills listbox with list items 

            listBox.Items.Clear();
            if (secondListBox != null)
                secondListBox.Items.Clear();

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

            if (lbBandList.SelectedItem == null)
                MessageBox.Show("whoops");
            else
            {
                string bandName = lbBandList.SelectedItem.ToString().Trim();
                List<string> albumList = BandAlbumTrackDB.FetchAlbumData(bandName);
                FillListBox(albumList, lbAlbumList, lbTrackList);
            }
        }

        private void lbAlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clears and refills albums listbox when selected index changes

            if (lbAlbumList.SelectedItem == null)
                MessageBox.Show("whoops", "Whoops");
            else
            {
                string bandName = lbBandList.SelectedItem.ToString().Trim();
                string albumName = lbAlbumList.SelectedItem.ToString().Trim();
                Dictionary<int, string> trackList = BandAlbumTrackDB.FetchTrackData(bandName, albumName);
                FillDictionary(trackList, lbTrackList);
            }
        }

        private void btnDeleteBand_Click(object sender, EventArgs e)
        {
            string band = lbBandList.SelectedItem.ToString().Trim();

            BandAlbumTrackDB.DeleteBand(band);
        }

        private void btnDeleteAlbum_Click(object sender, EventArgs e)
        {
            string band = lbBandList.SelectedItem.ToString().Trim();
            string album = lbAlbumList.SelectedItem.ToString().Trim();

            BandAlbumTrackDB.DeleteAlbum(band, album);
            FillListBox(BandAlbumTrackDB.FetchAlbumData(band), lbAlbumList);
        }

        private void btnDeleteTrack_Click(object sender, EventArgs e)
        {
            string band = lbBandList.SelectedItem.ToString().Trim();
            string album = lbAlbumList.SelectedItem.ToString().Trim();
            string[] track = lbTrackList.SelectedItem.ToString().Split('\t');

            BandAlbumTrackDB.DeleteTrack(band, album, track);
            FillDictionary(BandAlbumTrackDB.FetchTrackData(band, album), lbTrackList);
        }
    }
}
