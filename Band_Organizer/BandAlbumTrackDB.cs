using System;
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
        public static void DropDatabase()
        {
            // drops database
            // TESTING PURPOSES ONLY
            // TODO: ... make it work

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "DROP DATABASE IF EXISTS BandAlbumTracks;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateDatabase()
        {
            // creates database if it doesn't already exist

            string connString = "Server = localhost; Integrated Security = SSPI; database = master;";
            string sqlStatement = "IF NOT EXISTS ( SELECT * FROM sys.databases WHERE name = N'BandAlbumTracks')" +
                                  "CREATE DATABASE BandAlbumTracks";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTables()
        {
            // creats tables for database

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "IF OBJECT_ID('Bands') IS NULL\n" +
                                    "CREATE TABLE Bands (" +
                                        "id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                                        "name NVARCHAR(MAX)" +
                                        ");" +
                                  "IF OBJECT_ID('Albums') IS NULL\n" +
                                    "CREATE TABLE Albums (" +
                                        "id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                                        "title NVARCHAR(MAX), " +
                                        "release_date DATE, " +
                                        "band_id INT FOREIGN KEY REFERENCES Bands(id)" +
                                        ");" +
                                  "IF OBJECT_ID('Tracks') IS NULL\n" +
                                    "CREATE TABLE Tracks (" +
                                        "id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                                        "track_no INT, " +
                                        "title NVARCHAR(MAX), " +
                                        "band_id INT FOREIGN KEY REFERENCES Bands(id), " +
                                        "album_id INT FOREIGN KEY REFERENCES Albums(id)" +
                                        ");";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertBand(Band band)
        {
            // inserts band into Bands table

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Bands ([Name]) VALUES (@name)";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = band.BandName;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertAlbum(Album album, string band) 
        {
            // inserts album in Albums table

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Albums ([Title], [Release_Date], [band_id]) " +
                                  "VALUES (@title, @releaseDate, (SELECT id FROM Bands WHERE name = @name ))";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = band;

                    cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                    cmd.Parameters["@title"].Value = album.AlbumTitle;

                    cmd.Parameters.Add("@releaseDate", SqlDbType.Date);
                    cmd.Parameters["@releaseDate"].Value = album.ReleaseDate;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertTrack(Tracks track, string band, string album) 
        {
            // inserts track into Tracks table

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Tracks ([Track_No], [Title], [band_id], [album_id]) " +
                                  "VALUES (@trackNo, @title, " +
                                  "(SELECT id FROM Bands WHERE name = @bandName), " +
                                  "(SELECT id FROM Albums WHERE title = @albumName))";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@trackNo", SqlDbType.Int);
                    cmd.Parameters["@trackNo"].Value = track.TrackNumber;

                    cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                    cmd.Parameters["@title"].Value = track.TrackTitle;

                    cmd.Parameters.Add("@bandName", SqlDbType.NVarChar);
                    cmd.Parameters["@bandName"].Value = band;

                    cmd.Parameters.Add("@albumName", SqlDbType.NVarChar);
                    cmd.Parameters["@albumName"].Value = album;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<string> FetchBandData() 
        {
            // returns list of bands

            List<string> bandList = new List<string>();

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "SELECT Name FROM Bands ORDER BY Name ASC";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, conn);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bandList.Add(reader.GetString(0));
                    }
                }
            }
            return bandList;
        }

        public static List<string> FetchAlbumData(string band)
        {
            // returns list of albums based on band name

            List<string> albumList = new List<string>();

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "SELECT Title FROM Albums " +
                                  "WHERE band_id = (SELECT id FROM Bands WHERE name = @name) " +
                                  "ORDER BY release_date DESC";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = band;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            albumList.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return albumList;
        }

        public static Dictionary<int, string> FetchTrackData(string band, string album)
        {
            // return dictionary with track number and track name based on album name

            Dictionary<int, string> trackDict = new Dictionary<int, string>();

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatment = "SELECT Track_No, Title FROM Tracks " +
                                 "WHERE band_id = (SELECT id FROM Bands WHERE name = @bandName) AND " +
                                 "album_id = (SELECT id FROM Albums WHERE title = @albumName) " +
                                 "ORDER BY track_no ASC";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatment, conn))
                {
                    cmd.Parameters.Add("@bandName", SqlDbType.NVarChar);
                    cmd.Parameters["@bandName"].Value = band;

                    cmd.Parameters.Add("@albumName", SqlDbType.NVarChar);
                    cmd.Parameters["@albumName"].Value = album;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trackDict.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
            }
            return trackDict;
        }

        public static void ClearAllData()
        {
            // clears everything from all tables and resets auto-increment primary keys

            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "DELETE FROM Tracks; DELETE FROM Albums; DELETE FROM Bands;" +
                                  "DBCC CHECKIDENT ('?', RESEED, 0);";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool DeleteBand(string band)
        {
            // TODO: finish this
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_COnnection=True;";
            string sqlStatement = "DELETE FROM Bands " +
                                  "WHERE id = (SELECT id FROM Bands WHERE name = @bandName);";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@bandName", SqlDbType.NVarChar);
                    cmd.Parameters["@bandName"].Value = band;

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static bool DeleteAlbum(string band, string album)
        {
            // TODO: finish this
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "DELETE FROM Tracks WHERE album_id = " +
                                        "(SELECT id FROM Albums WHERE title = @albumName)" +
                                    "AND band_id = " +
                                        "(SELECT id FROM Bands WHERE name = @bandName);" +
                                  "DELETE FROM Albums WHERE title = @albumName;";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@albumName", SqlDbType.NVarChar);
                    cmd.Parameters["@albumName"].Value = album;

                    cmd.Parameters.Add("@bandName", SqlDbType.NVarChar);
                    cmd.Parameters["@bandName"].Value = band;

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static bool DeleteTrack(string band, string album, string[] track)
        {
            // TODO: fix this
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "DELETE FROM Tracks WHERE title = @trackName AND band_id = " +
                                  "(SELECT id FROM Bands WHERE name = @bandName);";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("trackName", SqlDbType.NVarChar);
                    cmd.Parameters["trackName"].Value = track[1];

                    cmd.Parameters.Add("bandName", SqlDbType.NVarChar);
                    cmd.Parameters["bandName"].Value = band;

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
