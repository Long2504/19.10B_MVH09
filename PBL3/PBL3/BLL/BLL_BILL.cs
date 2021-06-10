using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
namespace PBL3.BLL
{
    class BLL_BILL
    {

        private static BLL_BILL _Instance;
        public static BLL_BILL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_BILL();
                }
                return _Instance;
            }
            set { }
        }
        public List<bill> GetALlBill(){
            DBNet Db = new DBNet();
            var l = from p in Db.bills select p;
            return l.ToList();
        }
        public bill GetBillByid(string id)
        {
            DBNet Db = new DBNet();
            return Db.bills.Find(id);
        }
        public object GetALlBilltoShow()
        {
            DBNet Db = new DBNet();
            var l = from p in Db.bills select new {p.IDBill,p.TaiKhoan.TenTK,p.MaNV,p.DichVuKH.TenDV,p.IDOrder,p.TongTien };
            return l.ToList();
        }
        public object GetListFoodtoshowbyid(string id)
        {
            DBNet Db = new DBNet();
            var l = from p in Db.ListFoodOrders where p.IDOrder == id select new { p.IDOrder, p.Food.TenFood, p.Soluong };
            return l.ToList();
        }
        public List<KHorder> GetAllKHorder()
        {
            DBNet Db = new DBNet();
            var l = from p in Db.KHorders select p;
            return l.ToList();
        }
        public void AddKHorder(string idm, List<ListFoodOrder> l)
        {
            int tongtien=0;
            KHorder KHor = new KHorder()
            {
                IDMay = idm,
                IDOrder = "K" + (GetAllKHorder().Count + 1).ToString(),


            };
            DBNet Db = new DBNet();
            
                Db.KHorders.Add(KHor);
                foreach (ListFoodOrder i in l)
                {
                tongtien += Int32.Parse(i.TongTien.ToString());
                    Db.ListFoodOrders.Add(new ListFoodOrder() { TongTien = i.TongTien, IDFood = i.IDFood, Soluong = i.Soluong, IDOrder = KHor.IDOrder });
                }
                Db.SaveChanges();

            AddBill(KHor, Db.DichVuKHs.Find(3), null, null, tongtien);
            ;
        }
        public void AddBill(KHorder KHOr,DichVuKH DV,NhanVien NV,TaiKhoan TK,int Tien)
        {
            DBNet Db = new DBNet();
            bill B = new bill()
            {
                IDBill = "K" + (GetALlBill().Count + 1).ToString(),
                MaDV = DV.MaDV,
                TongTien=Tien
            };
            if (TK != null) B.IDTK = TK.IDTK;
            if (KHOr != null) B.IDOrder = KHOr.IDOrder;
            Db.bills.Add(B);
            Db.SaveChanges();
        }
    }
}
