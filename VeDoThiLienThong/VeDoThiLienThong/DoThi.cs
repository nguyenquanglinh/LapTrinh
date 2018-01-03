using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeDoThiLienThong
{
    class DoThi
    {
        #region biến toàn cục
        Form form;
        List<HinhTron> tapDinh;
        List<Canh> tapCanh;

        int[,] arr;
        List<int> list;
        HinhTron lastedClick = null;
        #endregion

        #region hàm tạo
        //các hàm ngoài có thể dùng
        // 1 hàm tạo đồ thị từ form
        public DoThi(Form f1)
        {
            form = f1;
            tapCanh = new List<Canh>();
            tapDinh = new List<HinhTron>();
            f1.Paint += f1_Paint;
        }
        //kiem tra hinh tron da co
        bool OverLapHt(HinhTron ht)
        {
            foreach (var item in this.tapDinh)
                if (ht.Overlap(item))
                    return true;
            return false;
        }
        //vẽ hình tròn bằng tọa độ x,y
        public void ThemDinh(int x, int y)
        {
            var ht = VeHinhTron(x, y);
            //....
            if (OverLapHt(ht))
                return;
            if (!tapDinh.Contains(ht))
            {
                tapDinh.Add(ht);
                form.Controls.Add(ht);
            }
            ht.Click += a_Click;
            ht.DoubleClick += ht_DoubleClick;
        }

        //thêm cạnh vào tập cạnh từ ht a ht b
        public void ThemCanh(Canh canh)
        {
            foreach (var item in tapCanh)
            {
                if (item.Equals(canh))
                    return;
            }
            foreach (var item in tapDinh)
            {
                if (item.Equals(canh.DiemDau) || item.Equals(canh.DiemCuoi))
                    continue;
            }

            if (!tapCanh.Contains(canh))
            {

                tapCanh.Add(canh);
                canh.DiemDau.Color = Color.Blue;
                canh.DiemCuoi.Color = Color.Blue;
            }
            form.Invalidate();
        }


        int demDinh;
        //vẽ ht trên form liên tục
        public void Draw()
        {
            GhiTenDinhLenHinhTron();

            foreach (var item in tapCanh)
            {
                VeCanh(item);
            }

        }

        #endregion

        #region vẽ hình cạnh thêm đỉnh

        void GhiTenDinhLenHinhTron()
        {
            demDinh = 0;
            foreach (var ht in tapDinh)
            {
                ht.Ten = demDinh.ToString();
                demDinh++;
            }
        }

        //hàm vẽ
        void f1_Paint(object sender, PaintEventArgs e)
        {
            this.Draw();

        }

        //vẽ hình tròn =ht truyền vào
        HinhTron VeHinhTron(int x, int y)
        {
            var ht = new HinhTron() { Location = new Point(x, y) };
            ht.Color = Color.Blue;
            return ht;

        }

        //xóa hình tròn khi double.click
        void ht_DoubleClick(object sender, EventArgs e)
        {
            demDinh--;
            var ht = sender as HinhTron;
            for (int i = 0; i < tapDinh.Count; i++)
            {
                if (ht.Equals(tapDinh[i]))
                {
                    tapDinh.RemoveAt(i);
                }
            }
            XoaCanh(ht);
            lastedClick = null;
            form.Controls.Remove(ht);

            form.Invalidate();
        }


        //xóa cạnh của hình tròn khi xóa 1 đỉnh của hình tròn
        void XoaCanh(HinhTron a)
        {
            for (int i = tapCanh.Count - 1; i >= 0; i--)
            {
                var dDau = tapCanh[i].DiemDau;
                var dCuoi = tapCanh[i].DiemCuoi;
                if (tapCanh[i].DiemDau.Equals(a) || tapCanh[i].DiemCuoi.Equals(a))
                {
                    tapCanh.RemoveAt(i);
                }
            }
        }

        //vẽ hình tròn từ sự kiện click
        void a_Click(object sender, EventArgs e)
        {
            var ht = sender as HinhTron;
            ht.Color = Color.Red;

            if (lastedClick == null)
            {
                lastedClick = ht;
                return;
            }
            if (ht.Equals(lastedClick))
                return;
            var canh = new Canh(lastedClick, ht);
            ThemCanh(canh);
            lastedClick = null;
        }
        //nối các 2 định của 1 cạnh trong tập cạnh
        private void VeCanh(Canh canh)
        {
            var dDau = canh.DiemDau;
            var dCuoi = canh.DiemCuoi;
            var color = dCuoi.Color;

            using (Graphics g = form.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen p = new Pen(color, 2))
                {
                    g.DrawLine(p, dDau.Center, dCuoi.Center);
                }
            }

        }

        #endregion

        #region dfs

        private bool DinhDaDuocDuyet(List<int> a, int dinh)
        {
            if (a == null)
                return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == dinh)
                    return true;
            }
            return false;
        }

        internal List<List<int>> DemDoThi()
        {
            var soDothi = new List<List<int>>();
            var caDinhDangDuyet = ThuTuDinhDuyet(0);
            if (caDinhDangDuyet != null)
                soDothi.Add(caDinhDangDuyet);
            for (int i = 1; i < arr.GetLength(0); i++)
            {
                if (!KiemTraDinhDaDuyet(soDothi, i))
                {
                    caDinhDangDuyet = ThuTuDinhDuyet(i);
                    if (caDinhDangDuyet != null)
                        soDothi.Add(caDinhDangDuyet);
                }
            }
            return soDothi;

        }

        bool KiemTraDinhDaDuyet(List<List<int>> a, int DinhCanKiemTra)
        {
            foreach (var item in a)
            {
                foreach (var Dinh in item)
                {
                    if (Dinh == DinhCanKiemTra)
                        return true;
                }
            }
            return false;
        }

        internal void DemCanhCuaDoThi()
        {
            var soDoThi = DemDoThi();
            foreach (var item in soDoThi)
            {
                MessageBox.Show(item.Count.ToString());
            }
        }

        public List<int> ThuTuDinhDuyet(int dinhDuyet)
        {
            int dinh = dinhDuyet;
            var stack = new Stack<int>();
            list = new List<int>();
            var dinhDangDuyet = new int[arr.GetLength(0)];
            stack.Push(dinh);
            while (stack.Count != 0)
            {
                var dinhRa = stack.Pop();
                list.Add(dinhRa);
                for (int i = 0; i < arr.GetLength(0); i++)
                    if (arr[dinhRa, i] == 1 && dinhDangDuyet[i] == 0)
                    {
                        dinhDangDuyet[dinhRa] = 11;
                        stack.Push(i);
                    }
            }
            if (list.Count > 1)
                return list;
            return null;
        }

        #endregion

        #region chưa làm được

        private void GhiFileArr()
        {
            string path = "D:\\Output.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            if (tapDinh.Count > 0)
            {
                string appendText = tapDinh.Count + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
            foreach (var item in tapCanh)
            {
                var dDau = (item.DiemDau.Ten);
                var dCuoi = (item.DiemCuoi.Ten);
                var s = dDau + " " + dCuoi;
                string appendText = s + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
        }

        //đọc và thêm dữ liệu lên arr-chưa dùng
        public void DocFile()
        {
            string path = "D:\\Output.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            string fileString = rd.ReadToEnd();
            if (string.IsNullOrEmpty(fileString))
            {
                MessageBox.Show("file rỗng");
                return;
            }
            var xx = fileString.Split('\r');
            var soDinh = 0;
            if (xx[0] != "\t")
                soDinh = int.Parse(xx[0]);
            arr = new int[soDinh + 1, soDinh + 1];
            for (int i = 1; i < xx.Length; i++)
            {
                if ((xx[i]) != "\n")
                {
                    var dong = xx[i].Split(' ');
                    arr[int.Parse(dong[0]), int.Parse(dong[1])] = 1;
                    arr[int.Parse(dong[1]), int.Parse(dong[0])] = 1;
                    var canh = ThemCanhTuTenDinh(int.Parse(dong[0]), int.Parse(dong[1]));
                    ThemCanh(canh);
                }
            }
            rd.Close();
        }
        //hinh tron có đinh thu 
        Canh ThemCanhTuTenDinh(int d1, int d2)
        {
            var canh = new Canh();
            GhiTenDinhLenHinhTron();
            foreach (var item in tapDinh)
            {
                if (int.Parse(item.Ten) == d1)
                    canh.DiemDau = item;
                if (int.Parse(item.Ten) == d2)
                    canh.DiemCuoi = item;
            }
            return canh;
        }

        //chưa làm dk
        public void ThemDinhTuFile()
        {
            var x = 0;
            var y = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] != 0)
                    {
                        ThemDinh(0 + x + 15, 0 + y + 30);
                        arr[j, i] = 0;
                        x = tapDinh[tapDinh.Count - 1].Center.X;
                        y = tapDinh[tapDinh.Count - 1].Center.Y;
                    }
                }
            }

        }

        #endregion

        #region LuuFile LayFile

        //ghi lại caic cạnh
        //////private void GhiCanhVaoFile()
        //////{
        //////    if (tapCanh.Count > 0)
        //////    {
        //////        string path = "D:\\OutputCanhLocation.txt";
        //////        if (File.Exists(path))
        //////            File.Delete(path);
        //////        foreach (var item in tapCanh)
        //////        {
        //////            var s = item.ToaDoCanh();
        //////            string appendText = s + Environment.NewLine;
        //////            File.AppendAllText(path, appendText);
        //////        }
        //////    }
        //////}

        //ghi lại các đỉnh
        void GhiDinhVaoFile()
        {
            if (tapDinh.Count > 0)
            {
                string path = "D:\\OutputLocation.txt";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                foreach (var item in tapDinh)
                {
                    var x = item.Center.X;
                    var y = item.Center.Y;
                    var s = x + ";" + y;
                    string appendText = s + Environment.NewLine;
                    File.AppendAllText(path, appendText);

                }
            }
        }

        //ghi cạnh lên form
        private void VeLaiCanh()
        {
            string path = "D:\\OutputCanhLocation.txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("file rỗng hoặc chưa được tạo");
                return;
            }
            var xx = File.ReadAllLines(path);
            foreach (var item in xx)
            {
                var s = item.Split(';');
                if (s.Length == 4)
                {
                    var canh = new Canh();
                    canh.DiemDau = VeHinhTron(int.Parse(s[0]), int.Parse(s[1]));
                    canh.DiemCuoi = VeHinhTron(int.Parse(s[2]), int.Parse(s[3]));
                    ThemCanh(canh); ;
                }
            }

        }

        // vẽ lại  ht lên form từ file
        void VeLaiDinh()
        {
            string path = "D:\\OutputLocation.txt";

            if (!File.Exists(path))
            {
                MessageBox.Show("file không tồn tại");
                return;
            }

            var xx = File.ReadAllLines(path);

            foreach (var item in xx)
            {
                var s = item.Split(';');
                ThemDinh(int.Parse(s[0]), int.Parse(s[1]));

            }
        }

        #endregion

        #region Thao TacTren file text
        //lưu file có 2 hàm tọa độ và arr
        public void LuuFile()
        {
            GhiDinhVaoFile();
            //     GhiCanhVaoFile();
            GhiFileArr();
        }

        public void VeLaiFile()
        {
            VeLaiDinh();
            VeLaiCanh();
        }
        #endregion

        static Random rd = new Random();
        internal void ToMau()
        {
            VeLaiDinh();
            DocFile();
            var xxxxx = tapDinh[0];
            var yyyyy = tapCanh[0];
            //VeLaiFile();
            Random rd = new Random();
            var soDoThi = DemDoThi();
            var list = new List<Color>();
            list.Add(Color.Blue);
            list.Add(Color.Red);
            list.Add(Color.PeachPuff);
            list.Add(Color.Pink);
            list.Add(Color.Purple);
            list.Add(Color.PowderBlue);
            list.Add(Color.BurlyWood);
            VeLaiDinh();
            
            foreach (var item in soDoThi)
            {

                var xx = list[rd.Next(0, list.Count)];
                VeCacCanhTren1DoThi(item, xx);
                foreach (var Dinh in item)
                {
                    var kt = KiemTraDinh(Dinh);
                    kt.Color = xx;
                }
            }
        }
        void VeCacCanhTren1DoThi(List<int> doThi, Color color)
        {
            foreach (var dinh in doThi)
            {
                foreach (var diem in tapCanh)
                {
                    if (int.Parse(diem.DiemCuoi.Ten) == dinh || int.Parse(diem.DiemDau.Ten) == dinh)
                        VeCanh(diem);
                }
            }

        }
        HinhTron KiemTraDinh(int a)
        {
            for (int i = 0; i < tapDinh.Count; i++)
            {
                if (int.Parse(tapDinh[i].Ten) == a)
                    return tapDinh[i];
            }
            return null;
        }
    }
}
