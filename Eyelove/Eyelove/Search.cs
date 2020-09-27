using System;
using System.Collections.Generic;
using System.Text;

namespace Eyelove
{
    public class Search
    {
        public int ID_1 { get; set; }
        public int ID_2 { get; set; }
        public string Status { get; set; }

        public Search(int id_2)
        {
            ID_2 = id_2;
        }
    }
}
