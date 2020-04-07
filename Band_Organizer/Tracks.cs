using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Band_Organizer
{
    class Tracks
    {

        private string trackTitle;
        private int trackNumber;

        public Tracks() { }

        public string TrackTitle { get => trackTitle; set => trackTitle = value; }
        public int TrackNumber { get => trackNumber; set => trackNumber = value; }
    }
}
