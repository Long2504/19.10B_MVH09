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
using Newtonsoft.Json;
namespace PBL3.GUI
{
    public partial class UserControlMAY : System.Windows.Forms.UserControl
    {
        public delegate void myDel(object[] obj, string ip);
        public myDel d { get; set; }
        public delegate void myDelLoad();

        public myDelLoad LD { get; set; }
        public myDelLoad ReloadB { get; set; }
        public delegate void myDelLoadOrder(List<ListFoodOrder> z,may m);
        public myDelLoadOrder Setorder { get; set; }
        List<DichVuKH> LDV = BLL_DV.Instance.GetAllDVKH();
            public List<may> l = new List<may>();
        public Button selected;
        public UserControlMAY()
        {
            InitializeComponent();
            load();
        }
        void load()
        {
            l.Clear();
            l.AddRange(BLL_MAY.Instance.GetListMay());
            loadP();
        }
        void setbutton(Button b,may  i)
        {
            b.Name = i.MaMay;
            string sttt;
            b.Click += B_Click;
            b.Font = new Font("arial", 9);
            b.FlatStyle = FlatStyle.Flat;
            b.Tag = i;
            b.Size = new Size(100, 100);
            if (i.TrangThai == true)
            {
                sttt = "đang hoạt động";
                b.BackColor = Color.White;
                if (i.TaiKhoan != null)
                {
                    b.Text = i.TenMay + "\n" + sttt + "\n" + i.TaiKhoan.TenTK;
                }
                else
                {
                    MessageBox.Show("Sai sot csdl");
                }
            }
            else if (i.TrangThai == false)
            {
                sttt = "không hoạt động";
                b.BackColor = Color.Red;
                b.Text = i.TenMay + "\n" + sttt + "\n";
            }
        }
        public Button findbutton2(string id)
        {
            foreach (TabPage k in tabControl1.TabPages)
            {
                foreach (FlowLayoutPanel P in k.Controls)
                {
                    foreach (Button i in P.Controls)
                    {
                        if (i.Name == id)
                        {
                            foreach (may j in l)
                            {
                                if (j.MaMay == id)
                                {
                                    return i;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        void loadP()
        {
             tabControl1.TabPages.Clear();
            foreach (Phong i in BLL_MAY.Instance.GetALLPhong())
            {
                FlowLayoutPanel f = new FlowLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Silver,
                    AutoScroll =true
                };
                TabPage T = new TabPage()
                {
                    Name = i.TenPhong,
                    Text = i.TenPhong
                };
                foreach (may j in BLL_MAY.Instance.GetAllMaybyIDP(i.IDPhong))
                {
                    Button b = new Button();
                    setbutton(b, j);
                    f.Controls.Add(b);
                }
                T.Controls.Add(f);
                tabControl1.Controls.Add(T);
            }
        }
        void reload(may m)
        {
            labelten.Text = m.TenMay;
            labelma.Text = m.MaMay;
            labelloai.Text = m.Phong1.TenPhong.ToString();
            labelstt.Text = m.TrangThai.ToString();
            labelGIA.Text = m.Phong1.GiaPhong.ToString() + "/1h";
            if((selected.Tag as may).TaiKhoan != null)
            {
                labeltk.Text = (selected.Tag as may).TaiKhoan.TenTK;
            }
            else
            {
                labeltk.Text = "";
            }
        }
        public void reloadbutton1(string Client,Button B,string idtk)
        {
            if(idtk ==null)
            {
                (B.Tag as may).TaiKhoan = new TaiKhoan()
                {
                    IDTK = (B.Tag as may).MaMay.ToString(),
                    TenTK = (B.Tag as may).MaMay.ToString()
                };
            }
            else
            {
                (B.Tag as may).TaiKhoan= BLL_TK.Instance.GetTKbyID(idtk);
            }
            (B.Tag as may).TrangThai = true;
            (B.Tag as may).IPcLient = Client;
            BLL_MAY.Instance.UpdateSTT((B.Tag as may).MaMay, true , idtk,Client);
            B.Text = (B.Tag as may).MaMay + "\n" + "đang hoạt động" + "\n" + (B.Tag as may).TaiKhoan.TenTK;
            B.BackColor = Color.White;

        }
        public void reloadbutton2(string id)
        {
            Button B = findbutton2(id);
            (B.Tag as may).TrangThai = false;
            (B.Tag as may).TaiKhoan = null;
            B.Text = (B.Tag as may).TenMay + "\n" + "không hoạt động";
            B.BackColor = Color.Red;
            (B.Tag as may).Listorder = null;
            BLL_MAY.Instance.UpdateSTT((B.Tag as may).MaMay, false, null, null);
            reload(B.Tag as may);
        }
        public void reloadbuttonF(string id, List<ListFoodOrder> order)
        {
            Button B = findbutton2(id);
            B.BackColor = Color.Green;
            (B.Tag as may).Listorder = order;         
        }
        public void MoMAY(TaiKhoan tk,Button S,bool Real)
        {
                object[] obj = new object[] { 0, LDV[0].MaDV, JsonConvert.SerializeObject(new { (S.Tag as may).TenMay, (S.Tag as may).MaMay, (S.Tag as may).IPcLient,gia= (S.Tag as may).Phong1.GiaPhong }, Formatting.Indented), JsonConvert.SerializeObject(tk, Formatting.Indented),Real };
                d(obj, null);

        }
        public void DongMay()
        {

                object[] obj = new object[] { 1, LDV[1].MaDV };
                d(obj, (selected.Tag as may).IPcLient);
                
        }
        private void B_Click(object sender, EventArgs e)
        {
            selected = sender as Button;
            reload((sender as Button).Tag as may);
        }

        private void buttonmomay_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("chon may");
                return;
            }
            if ((selected.Tag as may).TrangThai == false)
            {
                TaiKhoan tk = new TaiKhoan()
                {
                    IDTK = (selected.Tag as may).MaMay,
                    TenTK = (selected.Tag as may).TenMay,
                    SoDu = 9999999,

                };
                MoMAY(tk,selected,false);
                reload(selected.Tag as may);
            }
            else MessageBox.Show("máy đang hoạt động");
            reload(selected.Tag as may);
        }

        private void buttontinhtien_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("chon may");
                return;
            }
            if ((selected.Tag as may).TrangThai == true)
            {
                DongMay();
                reload(selected.Tag as may);
            }
            else MessageBox.Show("máy đang không hoạt động");
            reload(selected.Tag as may);
        }

        private void buttonnap_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("chon may");
                return;
            }
            if (BLL_TK.Instance.GetTKbyID((selected.Tag as may).TaiKhoan.IDTK) == null) { MessageBox.Show("khong the nap vao may may"); return; } 
            Nap F = new Nap((selected.Tag as may).TaiKhoan);
            F.S = new Nap.myDelSendClient(d);
            F.ShowDialog();
            int m = F.M();
            d(new object[] { 1, LDV[3].MaDV, m }, (selected.Tag as may).IPcLient) ;
            ReloadB();
        }

        private void buttonthem_Click(object sender, EventArgs e)
        {
            MAY_DB f = new MAY_DB();
            f.ShowDialog();
            load();
            LD();
        }

        private void buttonxoa_Click(object sender, EventArgs e)
        {
            if ((selected.Tag as may).TrangThai == true)
            {
                MessageBox.Show("May dang hoat dong");
                return;
            }
            if (selected == null)
            {
                MessageBox.Show("chon may");
                return;
            }
            BLL_MAY.Instance.DeleteMAY((selected.Tag as may).MaMay);
            load();
            LD();
        }

        private void buttonsua_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("chon may");
                return;
            }
            MAY_DB f = new MAY_DB(selected.Tag as may);
            f.ShowDialog();
            load();
            LD();
        }

        private void buttoncreate_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("chon may");
                return;
            }
            Setorder((selected.Tag as may).Listorder, selected.Tag as may);
            (selected.Tag as may).Listorder = null;
            selected.BackColor = Color.Red;
        }
    }
}
