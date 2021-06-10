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
    public partial class TKKH_DB : Form
    {
        public TKKH_DB()
        {
            InitializeComponent();
        }
        public TKKH_DB(TaiKhoan T)
        {
            InitializeComponent();
            loadedit(T);
        }
        bool Checkempty()
        {
            if (string.IsNullOrWhiteSpace(textBoxIDTK.Text) || string.IsNullOrWhiteSpace(textBoxTenTK.Text) || string.IsNullOrWhiteSpace(textBoxMK.Text) || string.IsNullOrWhiteSpace(textBoxIDKH.Text) || string.IsNullOrWhiteSpace(textBoxTenKH.Text) || string.IsNullOrWhiteSpace(textBoxCMND.Text) || string.IsNullOrWhiteSpace(textBoxSDT.Text)) return false;
            return true;
        }
        void loadedit(TaiKhoan T)
        {
            textBoxIDTK.ReadOnly = true;
            textBoxIDKH.ReadOnly = true;
            textBoxTenTK.ReadOnly = true;
            textBoxTenTK.Text = T.TenTK;
            numericUpDownTien.Value = Int32.Parse(T.SoDu.ToString());
            numericUpDownTien.Enabled = false;
            textBoxMK.Text = BLL_TK.Instance.GetMK(T.IDTK);
            textBoxIDTK.Text = T.IDTK;
            textBoxTenKH.Text = T.KhachHang.TenKH;
            textBoxIDKH.Text = T.KhachHang.IDKH;
            textBoxCMND.Text = T.KhachHang.CMND.ToString();
            textBoxSDT.Text = T.KhachHang.SdtKH;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Checkempty())
            {
                KhachHang KH = new KhachHang()
                {
                    IDKH = textBoxIDKH.Text,
                    TenKH = textBoxTenKH.Text,
                    CMND = Int32.Parse(textBoxCMND.Text),
                    SdtKH = textBoxSDT.Text
                };
                TaiKhoan TK = new TaiKhoan()
                {
                    KhachHang = KH,
                    IDTK = textBoxIDTK.Text,
                    TenTK=textBoxTenTK.Text,
                    SoDu=Int32.Parse(numericUpDownTien.Value.ToString()),
                    MK=textBoxMK.Text,
                };
                BLL_TK.Instance.AddOrEditTK(TK, KH);
                this.Close();
            }
            else
            {
                MessageBox.Show("nhap day du thong tin");
            }
        }
    }
}
