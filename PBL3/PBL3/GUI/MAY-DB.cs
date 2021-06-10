using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.DAL;
using PBL3.BLL;
namespace PBL3.GUI
{
    public partial class MAY_DB : Form
    {
        public MAY_DB()
        {
            InitializeComponent();
             setcbb();
        }
        public MAY_DB(may M)
        {
            InitializeComponent();
            setcbb();
            loadedit(M);
        }
        bool Checkemty()
        {
            if (string.IsNullOrWhiteSpace(textBoxMa.Text) || string.IsNullOrWhiteSpace(textBoxTen.Text)) return false;
            return true;
        }
        void loadedit(may M)
        {
            textBoxMa.ReadOnly = true;
            textBoxMa.Text = M.MaMay.ToString();
            textBoxTen.Text = M.TenMay.ToString();
            comboBoxPhong.SelectedIndex = comboBoxPhong.FindString(M.Phong1.TenPhong.ToString());
        }
        void setcbb()
        {
            foreach (CBBitem i in BLL_MAY.Instance.getCBBitemPhong())
            {
                comboBoxPhong.Items.Add(i);
            }
            comboBoxPhong.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Checkemty())
            {
                    BLL_MAY.Instance.AddorEdit(textBoxMa.Text, textBoxTen.Text, Int32.Parse(((CBBitem)comboBoxPhong.SelectedItem).id));
                    this.Close();
            }
            else
            {
                MessageBox.Show("nhap du thong tin");
            }
        }
    }
}
