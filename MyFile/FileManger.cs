using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFile
{
    public static class FileManger
    {

        /// <summary>
        /// get current directory path
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory; 
        }


        /// <summary>
        /// get root path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        public static string GetUpPath(string path, int up = 0)
        {
            if (up == 0)
                return path;

            path = System.IO.Path.GetDirectoryName(path);

            return GetUpPath(path, up - 1);
        }


        /// <summary>
        /// path//file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetDownPath(string path, string file)
        {
            return path + "\\" + file; 
        }

        public static string ReadFile(string path)
        {
            string result = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string s;
                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        result += s;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
