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
    public partial class UserControlF : System.Windows.Forms.UserControl
    {
        public delegate void myDel(object[] obj, string ip);
        public myDel F { get; set; }

        public delegate void myDelB();
        public myDelB B { get; set; }
        public List<Food> l = new List<Food>();
        List<DichVuKH> LDV = BLL_DV.Instance.GetAllDVKH();
        public Food SelectedF;
        public List<ListFoodOrder> ListOrder = new List<ListFoodOrder>();
        public UserControlF()
        {
            InitializeComponent();
            load();
            loadcomboboxMay();
        }
        public void setOrder(List<ListFoodOrder> ListOrderMay,may M)
        {
            this.BringToFront();
            comboBoxMay.SelectedIndex = comboBoxMay.FindString(M.TenMay);
            ListOrder = ListOrderMay;
            foreach(ListFoodOrder i in ListOrder)
            {
                AddPanelorder(BLL_Food.Instance.Getfoodbyid(i.IDFood));
            }
        }
        void setbutton(Button b, Food i)
        {
            b.Size = new Size(100, 100);
            b.BackgroundImageLayout = ImageLayout.Zoom;
            b.Tag = i;
            b.Click += B_Click;

        }
        public void loadcomboboxMay()
        {
            comboBoxMay.Items.Clear();
            foreach (CBBitem i in BLL_MAY.Instance.getCBBitemMay())
            {
                comboBoxMay.Items.Add(i);
            }
        }

        void LoadtypeForm()
        {
            FlowLayoutPanel fall = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Silver,
            };
            TabPage Tall = new TabPage()
            {
                Name = "ALL",
                Text = "ALL"
            };
            foreach (Food j in l)
            {
                Panel a = new Panel();
                loaddv(j, a);
                fall.Controls.Add(a);
            }
            Tall.Controls.Add(fall);
            tabControl1.Controls.Add(Tall);

            foreach (TypeFood i in BLL_Food.Instance.GetAllTypeF())
            {
                FlowLayoutPanel f = new FlowLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Silver,
                };
                TabPage T = new TabPage()
                {
                    Name = i.TenType,
                    Text = i.TenType
                };
                foreach (Food j in BLL_Food.Instance.GetAllFoodByType(i.IDType))
                {
                    Panel a = new Panel();
                    loaddv(j, a);
                    f.Controls.Add(a);
                }
                T.Controls.Add(f);
                tabControl1.Controls.Add(T);
            }
        }
        void loaddv(Food i, Panel a)
        {
            a.Size = new Size(100, 120);
            Button b = new Button();
            Label c = new Label();
            c.Font = new Font("arial", 7);
            c.Text = i.TenFood;
            a.Controls.Add(b);
            a.Controls.Add(c);
            c.Dock = DockStyle.Bottom;
            b.Dock = DockStyle.Fill;
            setbutton(b, i);
        }
        void load()
        {
            l = BLL_Food.Instance.GetAllFood();
            tabControl1.TabPages.Clear();
            LoadtypeForm();
        }
        void loadinf(Food F)
        {
            labelMa.Text = F.IDFood;
            labelTen.Text = F.TenFood;
            labelGia.Text = F.Gia.ToString();
            labelType.Text = F.TypeFood.TenType.ToString();
        }
        void AddPanelorder(Food O)
        {
            Panel P = new Panel();
            Panel P1 = new Panel();
            P1.Dock = DockStyle.Right;
            P1.Size = new Size(100, 100);
            P1.BackColor = Color.Yellow;
            Label TenF = new Label();
            TenF.Font = new Font("arial", 7);
            TenF.ForeColor = Color.Red;
            TenF.Text = O.TenFood + "\n" + O.Gia.ToString() + "\n so luong : " + numericUpDown1.Value.ToString();
            TenF.Dock = DockStyle.Left;
            P1.Controls.Add(TenF);
            P.BackColor = Color.Blue;
            P.Size = new Size(180, 50);
            P.Controls.Add(P1);
            flowLayoutPanel1.Controls.Add(P);
        }
        void AddOrder()
        {
            ListOrder.Add(new ListFoodOrder() { IDFood = SelectedF.IDFood, Soluong = Int32.Parse(numericUpDown1.Value.ToString()), TongTien = Int32.Parse(numericUpDown1.Value.ToString()) * SelectedF.Gia });
            AddPanelorder(SelectedF);
        }
        void CreateOrder()
        {
            BLL_BILL.Instance.AddKHorder((comboBoxMay.SelectedItem as CBBitem).id, ListOrder);
            B();
        }
        private void B_Click(object sender, EventArgs e)
        {
            SelectedF = (sender as Button).Tag as Food;
            loadinf(SelectedF);
        }

        private void buttoncreate_Click(object sender, EventArgs e)
        {
            CreateOrder();
            ListOrder.Clear();
            flowLayoutPanel1.Controls.Clear();
        }

        private void buttonthem_Click(object sender, EventArgs e)
        {
            Food_DB f = new Food_DB();
            f.ShowDialog();
            load();
        }

        private void buttonsua_Click(object sender, EventArgs e)
        {
            if (SelectedF == null) { MessageBox.Show("chon mon an"); return; }
            Food_DB f = new Food_DB(SelectedF);
            f.ShowDialog();
            load();
        }

        private void buttonxoa_Click(object sender, EventArgs e)
        {
            if (SelectedF == null) { MessageBox.Show("chon mon an"); return; }
            BLL_Food.Instance.DeleteF(SelectedF.IDFood);
            load();
        }

        private void buttonAddorder_Click(object sender, EventArgs e)
        {
            if (SelectedF == null) { MessageBox.Show("chon mon an"); return; }
            AddOrder();
        }
    }
}

