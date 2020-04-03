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
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "IF OBJECT_ID('Bands') IS NULL\n" +
                                    "CREATE TABLE Bands (" +
                                        "id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                                        "name VARCHAR(50)" +
                                        ");" +
                                  "IF OBJECT_ID('Albums') IS NULL\n" +
                                    "CREATE TABLE Albums (" +
                                        "id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                                        "title VARCHAR(50), " +
                                        "release_date DATE, " +
                                        "band_id INT FOREIGN KEY REFERENCES Bands(id)" +
                                        ");" +
                                  "IF OBJECT_ID('Tracks') IS NULL\n" +
                                    "CREATE TABLE Tracks (" +
                                        "id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                                        "title VARCHAR(50), " +
                                        "album_id INT FOREIGN KEY REFERENCES Albums(id), " +
                                        "band_id INT FOREIGN KEY REFERENCES Bands(id)" +
                                        ")";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertBandName(Band bandName)
        {
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Bands ([Name]) VALUES (@name)";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = bandName.BandName;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertAlbumName(Album albumTitle, string bandName) 
        {
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "INSERT INTO Albums ([Title], [Release_Date], [band_id]) VALUES (@title, @releaseDate, (SELECT id FROM Bands WHERE name = @name ))";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = bandName;

                    cmd.Parameters.Add("@title", SqlDbType.NVarChar);
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
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                    cmd.Parameters["@title"].Value = trackTitle.TrackTitle;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ClearAllData() 
        {
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

        public static List<string> FetchAllData() 
        {
            List<string> bandList = new List<string>();
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "SELECT Name FROM Bands";

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

        public static List<string> FetchAlbumData(string bandName)
        {
            List<string> albumList = new List<string>();
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatement = "SELECT Title FROM Albums WHERE band_id = (SELECT id FROM Bands WHERE name = @name)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = bandName;

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

        public static List<string> FetchTrackData()
        {
            List<string> trackList = new List<string>();
            string connString = "Server=localhost;Database=BandAlbumTracks;Trusted_Connection=True;";
            string sqlStatment = "SELECT Title FROM Tracks";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStatment, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trackList.Add(reader.GetString(0));
                    }
                }
            }

            return trackList;
        }
    }
}
