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
    public partial class Form1 : Form
    {

        List<DichVuKH> LDV = BLL_DV.Instance.GetAllDVKH();
        connectSERVER SERVER = new connectSERVER();
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            loadUsercontrol();
            SERVER.connect();
            load();
        }
        void loadUsercontrol()
        {

            this.userControlf1 = new PBL3.GUI.UserControlF();
            this.userControlmay1 = new PBL3.GUI.UserControlMAY();
            this.userControlnv1 = new PBL3.GUI.UserControlNV();
            this.userControltk1 = new PBL3.GUI.UserControlTK();
            this.userControlBill1 = new PBL3.GUI.UserControlBill();

            // userControldv1
            // 
            this.userControlf1.AutoScroll = true;
            this.userControlf1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlf1.F = null;
            this.userControlf1.Location = new System.Drawing.Point(0, 0);
            this.userControlf1.Name = "userControldv1";
            this.userControlf1.Size = new System.Drawing.Size(776, 373);
            this.userControlf1.TabIndex = 3;
            // 
            // userControlmay1
            // 
            this.userControlmay1.d = null;
            this.userControlmay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlmay1.Location = new System.Drawing.Point(0, 0);
            this.userControlmay1.Name = "userControlmay1";
            this.userControlmay1.Size = new System.Drawing.Size(776, 373);
            this.userControlmay1.TabIndex = 2;
            // 
            // userControlnv1
            // 
            this.userControlnv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlnv1.Location = new System.Drawing.Point(0, 0);
            this.userControlnv1.Name = "userControlnv1";
            this.userControlnv1.Size = new System.Drawing.Size(776, 373);
            this.userControlnv1.TabIndex = 0;
           
            // 
            // userControltk1
            // 
            this.userControltk1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControltk1.Location = new System.Drawing.Point(0, 0);
            this.userControltk1.Name = "userControltk1";
            this.userControltk1.Size = new System.Drawing.Size(776, 373);
            this.userControltk1.TabIndex = 1;
            // userControlBill1
            // 
            this.userControlBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlBill1.Location = new System.Drawing.Point(0, 0);
            this.userControlBill1.Name = "userControlBill1";
            this.userControlBill1.Size = new System.Drawing.Size(665, 323);
            this.userControlBill1.TabIndex = 0;
            
            //
            //Add Panel3
            //
            this.panel3.Controls.Add(userControlf1);
            this.panel3.Controls.Add(userControlmay1);
            this.panel3.Controls.Add(userControlnv1);
            this.panel3.Controls.Add(userControltk1);
            this.panel3.Controls.Add(userControlBill1);
        }
        void load()
        {
            userControlmay1.BringToFront();
            userControlmay1.d = new UserControlMAY.myDel(SERVER.send);
            SERVER.SerVerDel = new connectSERVER.ServerDel2(THDV);
            userControlf1.F = new UserControlF.myDel(SERVER.send);
            userControlmay1.LD = new UserControlMAY.myDelLoad(userControlf1.loadcomboboxMay);
            userControlf1.B = new UserControlF.myDelB(userControlBill1.load);
            userControlmay1.Setorder = new UserControlMAY.myDelLoadOrder(userControlf1.setOrder);
            userControlmay1.ReloadB = new UserControlMAY.myDelLoad(userControlBill1.load);
            userControltk1.ReloadB = new UserControlTK.myDelLoad(userControlBill1.load);
        }
        public void test(string a)
        {
            label1.Text = a;
        }
        void THDV(object[] dv, string ip)
        {

            int m = Int32.Parse(dv[1].ToString());
            if (m == -1) loadclient(ip);
            else if (m == -2) loadclientDV(ip);
            else if (m == 0) Dangnhap(dv[2].ToString(), dv[3].ToString(), dv[4].ToString());
            else if (m == Int32.Parse(LDV[0].MaDV.ToString())) {
                if (dv[2] != null)
                {
                    userControlmay1.reloadbutton1(ip, userControlmay1.findbutton2(dv[2].ToString()),dv[3].ToString());
                }
                else
                {
                    userControlmay1.reloadbutton1(ip, userControlmay1.selected,null);
                }
            } 
            else if (m == Int32.Parse(LDV[1].MaDV.ToString())) userControlmay1.reloadbutton2(dv[2].ToString());
            else if (m == Int32.Parse(LDV[2].MaDV.ToString())) userControlmay1.reloadbuttonF(dv[2].ToString(), (List<ListFoodOrder>)JsonConvert.DeserializeObject<List<ListFoodOrder>>(dv[3].ToString()));
        }
        void Dangnhap(string TK,string MK,string IDmay)
        {
          TaiKhoan T =  BLL_TK.Instance.Dangnhap(TK, MK);
           if (T!=null)
           {
                userControlmay1.MoMAY(new TaiKhoan() {TenTK=T.TenTK,IDTK=T.IDTK,SoDu=T.SoDu },userControlmay1.findbutton2(IDmay),true);
           }
        }
        void loadclientDV(string ip)
        {
            List<TypeFood> l = BLL_Food.Instance.GetAllTypeF();
          string lt=  JsonConvert.SerializeObject(from p in l select new { p.IDType, p.TenType }, Formatting.Indented);
               SERVER.send(new object[] { 1, 0, JsonConvert.SerializeObject(from p in userControlf1.l select new { p.IDFood, p.TenFood,p.TypeF,p.Gia }),lt }, ip);
        }
        void loadclient(string ip)
        {
            SERVER.send(new object[] { 0, 0, JsonConvert.SerializeObject(from p in userControlmay1.l select new { p.MaMay, p.TenMay }) }, ip);
        }
      
        private void buttonTK_Click(object sender, EventArgs e)
        {
            userControltk1.BringToFront();
        }

        private void buttonNV_Click(object sender, EventArgs e)
        {
            userControlnv1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userControlmay1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            userControlf1.BringToFront();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
              SERVER.send(new object[] { 1, LDV[1].MaDV }, null);
        }

        private void buttonBill_Click(object sender, EventArgs e)
        {
            userControlBill1.BringToFront();
        }

        private void buttonChat_Click(object sender, EventArgs e)
        {

        }
    }

}

