using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BieuThucTrungToSoLonConsole
{
    class Program
    {
        //ghi file
        public static void GhiFile(string s)
        {

            string path = "D:\\datangay2.txt";
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
            writer.WriteLine(s);
            writer.Flush();
            fs.Close();
        }
        //đọc file
        public static List<string> DocFile(string path)
        {
            path = "D:\\data.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            string fileString = rd.ReadToEnd();
            var list = new List<string>();
            var xx = fileString.Split('\r');
            for (int i = 0; i < xx.Length; i++)
            {
                if ((xx[i]) != "\n")
                    list.Add(xx[i]);
            }
            rd.Close();
            return list;
        }

        static void Main(string[] args)
        {

            var list = DocFile("a");
            
                var sss = new ChuyenTrungToSangHauTo(list[0]);
                GhiFile(sss.ToString());

             
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}
