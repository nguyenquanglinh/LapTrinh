using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BieuThucTrungToSoLonConsole
{
    public class ChuyenTrungToSangHauTo
    {
        List<object> items = new List<object>();
        private static List<object> SplitItem(string str)
        {
            string[] sep = { "+", "-", "*", ")", "(", "^", "/" };

            foreach (var item in sep)
            {
                str = str.Replace(item, " " + item + " ");
            }
            var arr = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<object>();
            foreach (var item in arr)
            {
                if ('0' <= item[0] && item[0] <= '9')
                    result.Add(new SoLon(item));
                else
                    result.Add(item);
            }
            return result;
        }

        //hàm tạo truyền đầu vào
        public ChuyenTrungToSangHauTo(string s)
        {
            if (!IsCorrectString(s))
                throw new ArgumentException("Chuoi khong hop le");
            items = SplitItem(s);
        }


        //kiểm tra chuỗi đúng
        static bool IsCorrectString(string mangChar)
        {
            int mo = 0, dong = 0;
            for (int i = 0; i < mangChar.Length; i++)
            {
                if (mangChar[i] == '(')
                    mo++;
                if (mangChar[i] == ')')
                    dong++;
                if (mo < dong)
                    return false;
            }
            return mo - dong == 0;
        }

        //độ ưu tiên toán tử
        static int Priority(string Char)
        {
            if (Char == "%" || Char == "^")
                return 3;
            if (Char == "*" || Char == "/")
                return 2;
            if (Char == "+" || Char == "-")
                return 1;
            return 0;
        }

        //in ra hau to
        public List<object> PrintScreenRessults()
        {
            List<object> result = new List<object>();
            var stack = new Stack<object>();
            var ressults = "";
            foreach (var item in items)
            {
                if (item == "(")
                {
                    stack.Push(item);

                }
                else if (item == ")")
                {
                    var kqTrongStack = stack.Pop();
                    while (kqTrongStack != "(")
                    {
                        result.Add(kqTrongStack);
                        kqTrongStack = stack.Pop();
                    }
                }
                else if (IsOperator(item))
                {

                    while (stack.Count > 0 && Priority(item) <= Priority((char)stack.Peek()))
                    {
                        ressults += stack.Pop();
                    }
                    stack.Push(item);
                }
            }
            while (stack.Count > 0)
            {
                ressults += stack.Pop();
            }
            var s = ressults.ToCharArray();
            ressults = ChuyenVeChuoiDung(s);
            return ressults;
        }

        string ChuyenVeChuoiDung(char[] ressults)
        {
            string[] toanTu = { "+", "-", "*", "(", ")", "^", "/" };
            var cacSoLon = chuoi.Split(toanTu, StringSplitOptions.RemoveEmptyEntries);
            var ressults1 = "";
            var s = "";
            int j = 0;
            foreach (var item in ressults)
            {
                s += item;
                if (IsOperator(item))
                {
                    ressults1 += " " + item;
                    s = "";
                }
                else if (s == cacSoLon[j])
                {
                    ressults1 += " " + s + " ";
                    s = "";
                    j++;
                }
            }
            return ressults1;
        }

        ////phep toan ket qua
        public string Expressions()
        {
            var s = PrintScreenRessults();
            var stack = new Stack<object>();
            foreach (var item in s.Split(' '))
            {
                if (item == "")
                    continue;
                if (IsOperatorString(item))
                {
                    if (item == "+")
                    {
                        var x1 = (SoLon)stack.Pop();
                        var x2 = (SoLon)stack.Pop();
                        var x3 = x1 + x2;
                        stack.Push(x3);
                    }
                    else if (item == "-")
                    {
                        var x1 = (SoLon)stack.Pop();
                        var x2 = (SoLon)stack.Pop();
                        var x3 = x2 - x1;
                        stack.Push(x3);
                    }
                    else if (item == "*")
                    {
                        var x1 = (SoLon)stack.Pop();
                        var x2 = (SoLon)stack.Pop();
                        var x3 = x1 * x2;
                        stack.Push(x3);
                    }
                    else if (item == "^")
                    {
                        var x1 = (SoLon)stack.Pop();
                        var x2 = (SoLon)stack.Pop();
                        var x3 = x2 ^ x1;
                        stack.Push(x3);
                    }
                }
                else if (item != "/")
                    stack.Push(new SoLon(item));
            }
            return stack.Pop().ToString();
        }

        //la toaan hang string
        bool IsOperatorString(string s)
        {
            return (s == "+" || s == "-" || s == "*" || s == "^");
        }
        //in ket quả
        public override string ToString()
        {
            return Input.ToString() + "= " + PrintScreenRessults() + "=" + Expressions().ToString();
        }
    }
}
