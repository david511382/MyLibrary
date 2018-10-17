using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReflectionTests
{
    class TestClass
    {
        public int i { get; set; }
        public string s { get; set; }
        public bool b { get; set; }
        public float f { get; set; }
        public double d { get; set; }
        public DateTime dt { get; set; }
        public long l { get; set; }

        public TestClass()
        {
            i = 11;
            s = "fd";
            b = false;
            f = 1.12f;
            d = 23.4324343d;
            dt = new DateTime(1999, 11, 23, 3, 32, 44);
            l = 2343435353535;
        }
    }
}
