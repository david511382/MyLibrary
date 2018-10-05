using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommand
{
    public static class Commander
    {

        /// <summary>
        /// execute cmd on path
        /// </summary>
        public static void exc(string path)
        {
            System.Diagnostics.Process.Start(path);
        }
    }
}
