using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalVocIntegrator
{
    class PascalVocObject
    {
        public string name;
        public string pose;
        public int truncated;
        public int difficult;
        public int occluded;
        public int xMin;
        public int yMin;
        public int xMax;
        public int yMax;
    }
}
