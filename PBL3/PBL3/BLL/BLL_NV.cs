using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
namespace PBL3.BLL
{
    class BLL_NV
    {
        private static BLL_NV _Instance;
        public static BLL_NV Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_NV();
                    
                }
                return _Instance;
            }
            set { }
        }
        public object GetAllNV(string Name)
        {
            DBNet Db = new DBNet();
            var l = from p in Db.NhanViens where p.TenNV.Contains(Name) select new {MANV =p.MaNV , TenNV=p.TenNV ,Luong=p.Luong,GioiTinh=p.GioiTinh,CMND=p.CMND,SoDienThoai=p.sdtnv,TinhTrang=p.Tinhtrang };
            return l.ToList();
        }
        public void AddOrEditNV(NhanVien K)
        {
           using(DBNet DB = new DBNet())
            {
                NhanVien NVedit = DB.NhanViens.Find(K.MaNV);
                if (NVedit!=null)
                {
                    NVedit.TenNV = K.TenNV;
                    NVedit.GioiTinh = K.GioiTinh;
                    NVedit.CMND = K.CMND;
                    NVedit.DiaChi = K.DiaChi;
                    NVedit.Luong = K.Luong;
                    NVedit.sdtnv = K.sdtnv;
                    NVedit.Tinhtrang = K.Tinhtrang;
                }
                else
                {
                    DB.NhanViens.Add(K);
                }
                DB.SaveChanges();
            }
        }
        public NhanVien GetNVbyID(string id)
        {
            DBNet Db = new DBNet();
            return Db.NhanViens.Find(id);
        }
        public void DeleteNV(string id)
        {
            DBNet Db = new DBNet();
            NhanVien NVDEL = Db.NhanViens.Find(id);
            Db.NhanViens.Remove(NVDEL);
            Db.SaveChanges();
        }
    }
}
