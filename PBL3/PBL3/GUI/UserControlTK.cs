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
    public partial class UserControlTK : System.Windows.Forms.UserControl
    {
        TaiKhoan  SelectedTK;
        public delegate void myDelLoad();
        public myDelLoad ReloadB { get; set; }
        public UserControlTK()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            dataGridView1.DataSource = BLL_TK.Instance.GetAllTK("");
        }
        void loadinf()
        {

        }

        private void buttonthem_Click(object sender, EventArgs e)
        {
            TKKH_DB f = new TKKH_DB();
            f.ShowDialog();
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedTK = BLL_TK.Instance.GetTKbyID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            loadinf();
        }

        private void buttonsua_Click(object sender, EventArgs e)
        {
            if (SelectedTK == null)
            {
                MessageBox.Show("chon TK");
                return;
            }
            TKKH_DB f = new TKKH_DB(SelectedTK);
            f.ShowDialog();
            load();
        }

        private void buttonxoa_Click(object sender, EventArgs e)
        {
            if (SelectedTK == null)
            {
                MessageBox.Show("chon TK");
                return;
            }
            BLL_TK.Instance.DeleteTK(SelectedTK.IDTK, SelectedTK.KhachHang.IDKH);
            load();
        }

        private void buttontim_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_TK.Instance.GetAllTK(textBox1.Text);
        }

        private void buttonnap_Click(object sender, EventArgs e)
        {
            if (SelectedTK == null)
            {
                MessageBox.Show("chon TK");
                return;
            }
            Nap F = new Nap(SelectedTK);
            F.ReloadB = new Nap.myDelLoad(this.ReloadB);
            F.ShowDialog();
        }
    }
}
