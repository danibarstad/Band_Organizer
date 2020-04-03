using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Band_Organizer
{
    class Album
    {

        private string albumTitle;
        private DateTime releaseDate;

        public Album() { }

        public Album(string albumTitle)
        {
            this.AlbumTitle = AlbumTitle;
        }

        public string AlbumTitle { get => albumTitle; set => albumTitle = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
    }
}
