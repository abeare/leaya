using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace electrocalculator.Models
{
    public class components
    {
        public string Nodes { get; set; }
        public string Resistor { get; set; }
        public string Capacitor { get; set; }
        public string Inductor { get; set; }
        public int kp { get; set; }
        public int km { get; set; }
    }

    public class resistor
    {
        public List<double> ResistorsValue { get; set; }
        public List<int> ResistorsNegativeNodes { get; set; }
        public List<int> ResistorsPositiveNodes { get; set; }

    }

    public class capacitor
    {
        public List<double> CapacitorsValue { get; set; }
        public List<int> CapacitorsNegativeNodes { get; set; }
        public List<int> CapacitorsPositiveNodes { get; set; }

    }

    public class inductor
    {
        public List<double> IductorsValue { get; set; }
        public List<int> IductorsNegativeNodes { get; set; }
        public List<int> IductorsPositiveNodes { get; set; }

    }

   
}