using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BieuThucTrungToSoLonConsole
{
    public static class String_Ext
    {
        public static string[] SplitOnGroups(this string str, string pattern)
        {
            var matches = Regex.Matches(str, pattern);
            var partsList = new List<string>();
            for (var i = 0; i < matches.Count; i++)
            {
                var groups = matches[i].Groups;
                for (var j = 0; j < groups.Count; j++)
                {
                    var group = groups[j];
                    partsList.Add(group.Value);
                }
            }
            return partsList.ToArray();
        }
    }

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
            var chuoi1 = "(153613131313131231*372323526353226+323)*322323232323232323";

            string[] sep = { "+", "-", "*", ")",   "(", "^", "/" };
         


            //var list = DocFile("a");
            //foreach (var item in list)
            //{
            //    var sss = new ChuyenTrungToSangHauTo(item);
            //    GhiFile(sss.ToString());
            //}
            //var S = new List<object>();
            //var chuoi = "(153613131313131231 * 372323526353226 + 323) * 322323232323232323";
            //string[] toanTu = { "+", "-", "*", ")", "", "(", "^", "/" };
            //var so = "";
            //int i = 0;
            //var cacSoLon = chuoi.Split(toanTu, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var item in chuoi)
            //{
            //    if (item == ' ')
            //        continue;
            //    so += item;
            //    if (item == ')')
            //    {
            //        S.Add(item);
            //        so = "";
            //    }
            //    if (item == '(')
            //    {
            //        S.Add(item);
            //        so = "";
            //    }
            //  if(item=='*')
            //  {
            //      S.Add(item);
            //      so = "";
            //  }
            //    if (so == cacSoLon[i])
            //    {
            //        S.Add(so);
            //        so = "";
            //        i++;
            //    }
            //}

            //Console.WriteLine();


            Console.ReadKey();
        }
    }
}
