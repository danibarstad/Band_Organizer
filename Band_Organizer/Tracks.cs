using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Band_Organizer
{
    class Tracks
    {

        private List<string> trackList;

        public Tracks() { }

        public List<string> TrackList { get => trackList; set => trackList = value; }
    }
}
