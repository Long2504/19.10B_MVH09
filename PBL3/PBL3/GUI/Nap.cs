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
    public partial class Nap : Form
    {
        TaiKhoan TK;
        public delegate void myDelLoad();
        public delegate void myDelSendClient(object[] obj, string ip);
        public myDelLoad ReloadB { get; set; }
        public myDelSendClient S {get;set;}
        public Nap(TaiKhoan T)
        {
            InitializeComponent();
            TK = T;
            label1.Text = T.TenTK;
        }
        public Nap(TaiKhoan T,may B)
        {
            InitializeComponent();
            TK = T;
            label1.Text = T.TenTK;
        }
        public int M()
        {
            return Int32.Parse(numericUpDown1.Value.ToString());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BLL_TK.Instance.Nap(TK.IDTK, Int32.Parse(numericUpDown1.Value.ToString()));
            BLL_BILL.Instance.AddBill(null, BLL_DV.Instance.GetAllDVKH()[3], null, TK, Int32.Parse(numericUpDown1.Value.ToString()));
            this.Close();

        }
    }
}
