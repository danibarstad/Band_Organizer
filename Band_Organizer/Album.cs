using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Band_Organizer
{
    class Album
    {

        public string albumTitle;
        private string releaseDate;

        public Album() { }

        public Album(string albumTitle)
        {
            this.AlbumTitle = AlbumTitle;
        }

        public string AlbumTitle { get => albumTitle; set => albumTitle = value; }
        public string ReleaseDate { get => releaseDate; set => releaseDate = value; }
    }
}
