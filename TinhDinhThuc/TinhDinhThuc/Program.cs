using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinhDinhThuc
{
    class Program
    {
        static int[] LayCacGiaTriThuN(int[,] arr, int n)
        {
            var ar = new int[arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                ar[i] = arr[n, i];
            }
            return ar;
        }

        static int KtViTriHangKhac0Min(int[,] arr)
        {
            var min = arr[1, 0];
            for (int i = 2; i < arr.GetLength(0); i++)
            {
                if (arr[i, 0] < min && arr[i, 0] != 0)
                    min = i;
            }
            return min;
        }

        static void DoiViTri2HangTrongMatrix(int[,] arr)
        {
            var min = KtViTriHangKhac0Min(arr);
            var an = LayCacGiaTriThuN(arr, min);
            var a0 = LayCacGiaTriThuN(arr, 0);
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                arr[0, i] = an[i];
                arr[min, i] = a0[i];
            }
        }




        static void Main(string[] args)
        {
            //var arr = new int[4, 4] { { 1, -1, 1, -2 }, { 1, 3, -1, 3 }, { -1, -1, 4, 3 }, { -3, 0, -8, -13 } };
            //var arr1=new int[3,2]{{0,1},{2,3},{1,2}};
            //DoiViTri2HangTrongMatrix(arr1);
            //var ar = LayCacGiaTriThuN(arr, 0);
            Console.ReadKey();
        }
    }
}
