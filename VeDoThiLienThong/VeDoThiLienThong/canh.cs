using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeDoThiLienThong
{
    /// <summary>
    /// Mô tả 1 cạnh của đồ thị
    /// </summary>
    public class Canh
    {

        public HinhTron DiemDau { get; set; }
        public HinhTron DiemCuoi { get; set; }

        public Canh() { }

        public Canh(HinhTron lastedClick, HinhTron ht)
        {
            this.DiemDau = lastedClick;
            this.DiemCuoi = ht;
        }
        public string ToaDoCanh()
        {
            var dDau = this.DiemDau;
            var dCuoi = this.DiemCuoi;
            int x1 = dDau.Center.X;
            int y1 = dDau.Center.Y;
            int x2 = dCuoi.Center.X;
            int y2 = dCuoi.Center.Y;
            return x1 + ";" + y1 + ";" + x2 + ";" + y2;
        }


        public override bool Equals(object obj)
        {
            var canh = obj as Canh;
            if (canh == null)
                return false;
            if (this.DiemDau.Equals(canh.DiemDau) && this.DiemCuoi.Equals(canh.DiemCuoi) || (this.DiemDau.Equals(canh.DiemCuoi) && this.DiemCuoi.Equals(canh.DiemDau)))
                return true;
            return false;
        }
    }
}
