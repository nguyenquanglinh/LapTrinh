using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeDoThiLienThong
{
    public partial class DoThiLienThongAbc : Form
    {
        DoThi dt;
        int x;
        int y;

        public DoThiLienThongAbc()
        {
            InitializeComponent();
            dt = new DoThi(this);
        }

        private void DoThiLienThong_MouseUp(object sender, MouseEventArgs e)
        {
            dt.ThemDinh(e.Location.X, e.Location.Y);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            dt.DocFile();
            MessageBox.Show("file đã được đọc");
        }

        private void btnVe_Click(object sender, EventArgs e)
        {
            dt.VeLaiFile();

            MessageBox.Show("hoàn thành");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            dt.LuuFile();

            MessageBox.Show("hoàn thành");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var list = dt.ThuTuDinhDuyet(1);
            foreach (var item in list)
            {
                MessageBox.Show(item.ToString());
            }
        }

        private void btnDem_Click(object sender, EventArgs e)
        {
            var soDothi = dt.DemDoThi();
            MessageBox.Show("canh cua do thi la "+(soDothi.Count.ToString()));
        }

        private void btnSoCanh_Click(object sender, EventArgs e)
        {
            dt.DemCanhCuaDoThi();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dt.VeLaiFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.ToMau();
        }
    }
}

