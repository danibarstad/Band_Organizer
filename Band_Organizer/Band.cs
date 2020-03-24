using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Band_Organizer
{
    class Band
    {
        private string bandName;

        public Band() { }

        public Band(string bandName)
        {
            this.BandName = bandName;
        }

        public string BandName { get => bandName; set => bandName = value; }
    }
}
