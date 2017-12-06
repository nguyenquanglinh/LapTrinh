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
        char[] mangChar;
        string chuoi;

        //hàm tạo truyền đầu vào
        public ChuyenTrungToSangHauTo(string s)
        {
            Input = s;
            ChuyenDoi = chuoi.ToCharArray();
        }

        //lấy kết quả đầu vào của hàm tạo
        string Input
        {
            get
            {
                return chuoi;
            }
            set
            {
                chuoi = value;
                //chưa biết có những lỗi gì

            }
        }

        //chuyển từ string sang char[]
        char[] ChuyenDoi
        {
            get
            {
                return mangChar;
            }
            set
            {
                mangChar = value;
                if (!IsCorrectString(value))
                    throw new Exception("bạn đã nhập sai biểu thức cần kiểm tra lại");
            }
        }

        //kiểm tra chuỗi đúng
        bool IsCorrectString(char[] mangChar)
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

        //toán tử
        bool IsOperator(char Char)
        {
            if (Char == '+' || Char == '-' || Char == '*' || Char == '/' || Char == '%' || Char == '^' || Char == '!')
                return true;
            return false;
        }

        //số đếm
        public bool IsNumber(Char Char)
        {
            if (Char == '9' || Char == '8' || Char == '7' || Char == '6' || Char == '5' || Char == '4' || Char == '3' || Char == '2' || Char == '1' || Char == '0')
                return true;
            return false;
        }

        //độ ưu tiên toán tử
        int Priority(char Char)
        {
            if (Char == '%' || Char == '^')
                return 3;
            if (Char == '*' || Char == '/')
                return 2;
            if (Char == '+' || Char == '-')
                return 1;
            return 0;
        }

        //in ra hau to
        public string PrintScreenRessults()
        {
            if (!IsCorrectString(mangChar))
                throw new Exception("nhap sai mang");
            var stack = new Stack<object>();
            var ressults = "";
            foreach (var item in mangChar)
            {
                if (item == ' ')
                    continue;
                else if (IsNumber(item))
                {
                    ressults += " " + item + " ";
                }
                else if (item == '(')
                {
                    stack.Push(item);

                }
                else if (item == ')')
                {
                    var kqTrongStack = stack.Pop();
                    while ((char)kqTrongStack != '(')
                    {
                        ressults += " " + kqTrongStack;
                        kqTrongStack = stack.Pop();
                    }
                }
                else if (IsOperator(item))
                {

                    while (stack.Count > 0 && Priority(item) <= Priority((char)stack.Peek()))
                    {
                        ressults += " " + stack.Pop();
                    }
                    stack.Push(item);
                }
            }
            while (stack.Count > 0)
            {
                ressults += " " + stack.Pop();
            }
            ressults = ChuyenVeChuoiDung(ressults);
            return ressults;
        }
        string ChuyenVeChuoiDung(string ressults)
        {
            string[] toanTu = { "+", "-", "*", "(", ")", "^", "/" };
            var s1s = chuoi.Split(toanTu, StringSplitOptions.RemoveEmptyEntries);
            string[] daucach = { "", " " };
            var ss = ressults.Split(daucach, StringSplitOptions.RemoveEmptyEntries);
            ressults = "";
            var s = "";
            int j = 0;
            foreach (var item in ss)
            {
                s += item;
                if (IsOperatorString(item))
                {
                    ressults += " " + item;
                    s = "";
                }

                else if (s == s1s[j])
                {
                    ressults += " " + s + " ";
                    s = "";
                    j++;
                }
            }
            return ressults;
        }
        // vi tri toan tu
        int TimViTriToanTu(string s, int x)
        {
            for (int i = x; i < s.Length; i++)
            {
                var sss = s[i];
                if (IsOperator(s[i]))
                    return i;
            }
            return 0;
        }
        //viết chuỗi-chưa làm
        string CongChuoi(string s, int x)
        {
            if (x == 0)
                x = chuoi.Length - 1;
            var ret = "";
            for (int i = 0; i < x; i++)
            {
                if (IsNumber(chuoi[i]))
                    ret += chuoi[i];
            }
            chuoi = chuoi.Remove(0, x + 1);

            return ret;
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
                        var x3 = x2 + x1;
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
