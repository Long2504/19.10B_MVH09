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
    public partial class Food_DB : Form
    {
        public Food_DB()
        {
            InitializeComponent();
              setcbb();
        }
        public Food_DB(Food F)
        {
            InitializeComponent();
            setcbb();
            loadEdit(F);
        }
        void loadEdit(Food F)
        {
            textBoxIDF.ReadOnly = true;
            textBoxIDF.Text = F.IDFood;
            textBoxTenF.Text = F.TenFood;
            comboBoxType.SelectedIndex = comboBoxType.FindString(F.TypeFood.TenType);
            numericUpDown1.Value = Int32.Parse(F.Gia.ToString());
        }
        void setcbb()
        {
            foreach (CBBitem i in BLL_Food.Instance.GetCBBAllTypeF())
            {
                comboBoxType.Items.Add(i);
            }
            comboBoxType.SelectedIndex = 0;
        }
        bool CheckEmpty()
        {
            if (string.IsNullOrWhiteSpace(textBoxIDF.Text) || string.IsNullOrWhiteSpace(textBoxTenF.Text)) return false;
            return true;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                Food FF = new Food()
                {
                    IDFood=textBoxIDF.Text,
                    TenFood=textBoxTenF.Text,
                    Gia=Int32.Parse(numericUpDown1.Value.ToString()),
                    TypeF=((CBBitem)comboBoxType.SelectedItem).id
                };
                BLL_Food.Instance.AddOrEditFood(FF);
                this.Close();
            }
            else MessageBox.Show("dien day du");
        }
    }
    
}
