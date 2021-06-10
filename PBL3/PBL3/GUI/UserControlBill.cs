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
    public partial class UserControlBill : System.Windows.Forms.UserControl
    {
        bill SelectedB;
        public UserControlBill()
        {
            InitializeComponent();
            load();
        }
        public void load()
        {
            dataGridView2.DataSource = BLL_BILL.Instance.GetALlBilltoShow();
        }
        void looadinfbill()
        {
        }


        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedB = BLL_BILL.Instance.GetBillByid(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            if (SelectedB.IDOrder != null)
            {
                dataGridView1.DataSource = BLL_BILL.Instance.GetListFoodtoshowbyid(SelectedB.KHorder.IDOrder);
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }
    }
}
