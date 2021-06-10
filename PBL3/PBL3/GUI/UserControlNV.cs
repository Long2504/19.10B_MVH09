using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.BLL;
using PBL3.DAL;
namespace PBL3.GUI
{
    public partial class UserControlNV : System.Windows.Forms.UserControl
    {
        NhanVien seletedNV;
        public UserControlNV()
        {
            InitializeComponent();
               load();
        }
        void load()
        {
            dataGridView1.DataSource = BLL_NV.Instance.GetAllNV("");
        }
        void loadinf(NhanVien K)
        {

            labelIDNV.Text = K.MaNV.ToString();
            labelTenNV.Text = K.TenNV.ToString();
            labelcmnd.Text = K.CMND.ToString();
            labelDC.Text = K.DiaChi.ToString();
            labelGT.Text = (K.GioiTinh == true) ? "Nam" : " Nữ";
            labelLuong.Text = K.Luong.ToString();
            labelsdt.Text = K.sdtnv.ToString();
            labelSTT.Text = (K.Tinhtrang == true) ? "Đang Làm" : "Đã Nghỉ";
        }
        private void buttonthem_Click(object sender, EventArgs e)
        {
            NV_DB f = new NV_DB();
            f.ShowDialog();
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seletedNV = BLL_NV.Instance.GetNVbyID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            loadinf(seletedNV);
        }

        private void buttonsua_Click(object sender, EventArgs e)
        {
            if (seletedNV == null)
            {
                MessageBox.Show("chon NV");
                return;
            }
            NV_DB f = new NV_DB(seletedNV);
            f.ShowDialog();
            load();
        }

        private void buttonxoa_Click(object sender, EventArgs e)
        {
            if (seletedNV == null)
            {
                MessageBox.Show("chon NV");
                return;
            }
            BLL_NV.Instance.DeleteNV(seletedNV.MaNV);
            load();
        }

        private void buttontim_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_NV.Instance.GetAllNV(textBox1.Text);
        }
    }
}
