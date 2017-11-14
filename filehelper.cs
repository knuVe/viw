using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontomanager_0._3
{
    public class filehelper
    {
        public filehelper(string myPath)
        {
            myPath = MyPath;
        }

        private string MyPath { get; set; }

        public byte[] ReadByteString()
        {
            return File.ReadAllBytes(MyPath);
        }

        public void WriteAllBytes(byte[] arrayToWrite)
        {
            File.WriteAllBytes(MyPath, arrayToWrite);
        }

        internal static byte[] ReadAllBytesStatic(string myPath)
        {
            return File.ReadAllBytes(myPath);
        }


    }
}
