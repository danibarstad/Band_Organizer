﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Band_Organizer
{
    class BandAlbumTrackDB
    {

        private static void CreateDatabase()
        {
            string str;
            SqlConnection conn = new SqlConnection("Server=localhost, Integrated Security=SSPI, database=master");

            str = "IF NOT EXISTS CREATE DATABASE BandAlbumTracks ON PRIMARY " +
                "(NAME = BandAlbumTracks_Data, " +
                "FILENAME = 'C:\\BandAlbumTracks.mdf";

            SqlCommand myCommand = new SqlCommand(str, conn);
            try
            {
                conn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Database is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void InsertBandName(Band bandName)
        {
            //BandAlbumTracksDataSetTableAdapters.BandsTableAdapter bandsTableAdapter =
            //    new BandAlbumTracksDataSetTableAdapters.BandsTableAdapter();

            //bandsTableAdapter.Insert(1, $"{bandName.BandName}");


            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Bands ([Name]) VALUES (@name)";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NText);
                    cmd.Parameters["@name"].Value = bandName.BandName;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertAlbumName(Album albumTitle) 
        {
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Albums ([Title], [ReleaseDate]) VALUES (@title, @releaseDate)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@title", SqlDbType.NText);
                    cmd.Parameters["@title"].Value = albumTitle.AlbumTitle;

                    cmd.Parameters.Add("@releaseDate", SqlDbType.Date);
                    cmd.Parameters["@releaseDate"].Value = albumTitle.ReleaseDate;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertTrackName(Tracks trackTitle) 
        {
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Tracks ([Title]) VALUES (@title)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@title", SqlDbType.NText);
                    cmd.Parameters["@title"].Value = trackTitle.TrackTitle;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void ClearAllData() { }

        private static void LoadData() { }

    }
}
