using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalVocIntegrator
{
    class PascalVocFormat
    {
        public string folder;
        public string filename;
        public string path;
        public string database;
        public int width;
        public int height;
        public int depth;
        public string segmented;

        public List<PascalVocObject> objectList;
    }
}
