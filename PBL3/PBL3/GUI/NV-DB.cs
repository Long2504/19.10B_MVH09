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
    public partial class NV_DB : Form
    {
        public NV_DB()
        {
            InitializeComponent();
        }
        public NV_DB(NhanVien K)
        {
            InitializeComponent();
            loadedit(K);
        }
        bool checkempty()
        {
            if (string.IsNullOrWhiteSpace(textBoxMaNV.Text) || string.IsNullOrWhiteSpace(textBoxTenNV.Text) || string.IsNullOrWhiteSpace(textBoxSDT.Text) || string.IsNullOrWhiteSpace(textBoxDC.Text) || string.IsNullOrWhiteSpace(textBoxCMND.Text)) return false;
            return true;
        }
        void loadedit(NhanVien K)
        {
            textBoxMaNV.ReadOnly = true;
            textBoxMaNV.Text = K.MaNV;
            textBoxTenNV.Text = K.TenNV;
            textBoxCMND.Text = K.CMND;
            textBoxDC.Text = K.DiaChi;
            textBoxSDT.Text = K.sdtnv;
            numericUpDown1.Value = Int32.Parse(K.Luong.ToString());
            checkBoxSTT.Checked = (K.Tinhtrang == true) ? true : false;
            radioButtonNam.Checked = (K.GioiTinh == true) ? true : false;
        }
        private void buttonok_Click(object sender, EventArgs e)
        {
            bool gt = (radioButtonNam.Checked == true) ? true : false;
            bool stt = (checkBoxSTT.Checked == true) ? true : false;
            int L = Int32.Parse(numericUpDown1.Value.ToString());
            if (checkempty())
            {
                NhanVien KK = new NhanVien()
                {
                    MaNV = textBoxMaNV.Text,
                    TenNV=textBoxTenNV.Text,
                    CMND=textBoxCMND.Text,
                    sdtnv=textBoxSDT.Text,
                    DiaChi=textBoxDC.Text,
                    GioiTinh=gt,
                    Tinhtrang=stt,
                    Luong=L
                };
                BLL_NV.Instance.AddOrEditNV(KK);
                this.Close();
            }
        }
    }
}
