using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
namespace PBL3.BLL
{
    class BLL_TK
    {
        private static BLL_TK _Instance;
        public static BLL_TK Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_TK();
                }
                return _Instance;
            }
            set { }
        }
        public object GetAllTK(string TenTK)
        {
            DBNet Db = new DBNet();
            var l = from p in Db.TaiKhoans from k in Db.KhachHangs where p.IDKH==k.IDKH && p.TenTK.Contains(TenTK) select new {ID=p.IDTK,TenDangNhap=p.TenTK,TenKH=k.TenKH,CMND=k.CMND,SDT=k.SdtKH };
            return l.ToList();
        }
        public void AddOrEditTK(TaiKhoan T, KhachHang K)
        {
            using(DBNet DB = new DBNet() )
            {
                TaiKhoan TKEdit = DB.TaiKhoans.Find(T.IDTK);
                if (TKEdit != null)
                {
                    KhachHang KEdit = DB.KhachHangs.Find(K.IDKH);
                    KEdit.CMND = K.CMND;
                    KEdit.SdtKH = K.SdtKH;
                    KEdit.TenKH = K.TenKH;
                }
                else
                {
                    DB.KhachHangs.Add(K);
                    DB.TaiKhoans.Add(T);
                }
                DB.SaveChanges();
            }
            
        }
        public TaiKhoan GetTKbyID(string id)
        {
            DBNet DB = new DBNet();
            return DB.TaiKhoans.Find(id);
        }
        public string GetMK(string id)
        {
            DBNet DB = new DBNet();
            return DB.TaiKhoans.Find(id).MK;
        }
        public void DeleteTK(string idtk, string idkh)
        {
            DBNet DB = new DBNet();
            DB.TaiKhoans.Remove(DB.TaiKhoans.Find(idtk));
            DB.KhachHangs.Remove(DB.KhachHangs.Find(idkh));
            DB.SaveChanges();
        }
        public TaiKhoan Dangnhap(string tk, string mk)
        {
            DBNet Db = new DBNet();
            var l = from p in Db.TaiKhoans where p.TenTK == tk && p.MK == mk select p;
            return l.SingleOrDefault();
        }
        public void Nap(string id,int M)
        {
            DBNet Db = new DBNet();
            TaiKhoan N = Db.TaiKhoans.Find(id);
            N.SoDu += M;
            Db.SaveChanges();
        }
    }
}
