using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalGames.Models
{

    public class PeriodicTable
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public Record[] records { get; set; }
        public Facet_Groups[] facet_groups { get; set; }
    }

    public class Parameters
    {
        public string dataset { get; set; }
        public string timezone { get; set; }
        public int rows { get; set; }
        public int start { get; set; }
        public string format { get; set; }
        public string[] facet { get; set; }
    }

    public class Record
    {
        public string datasetid { get; set; }
        public string recordid { get; set; }
        public Fields fields { get; set; }
        public DateTime record_timestamp { get; set; }
    }

    public class Fields
    {
        public float boilingpoint { get; set; }
        public float electronegativity { get; set; }
        public float ionizationenergy { get; set; }
        public string ionradius { get; set; }
        public string name { get; set; }
        public float density { get; set; }
        public string oxidationstates { get; set; }
        public string symbol { get; set; }
        public string electronicconfiguration { get; set; }
        public string groupblock { get; set; }
        public string vandelwaalsradius { get; set; }
        public int atomicnumber { get; set; }
        public float atomicradius { get; set; }
        public float meltingpoint { get; set; }
        public string atomicmass { get; set; }
        public string standardstate { get; set; }
        public float electronaffinity { get; set; }
        public string bondingtype { get; set; }
        public string yeardiscovered { get; set; }
    }

    public class Facet_Groups
    {
        public Facet[] facets { get; set; }
        public string name { get; set; }
    }

    public class Facet
    {
        public int count { get; set; }
        public string path { get; set; }
        public string state { get; set; }
        public string name { get; set; }
    }

}
