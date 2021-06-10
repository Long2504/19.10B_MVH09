using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
using PBL3.GUI;
namespace PBL3.BLL
{
    class BLL_MAY
    {
        private static BLL_MAY _Instance;
        public static BLL_MAY Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_MAY();
                }
                return _Instance;
            }
            set { }
        }
        public void AddorEdit(string id, string name, int idphong)
        {
            using(DBNet DB = new DBNet())
            {
                may MEdit = DB.mays.Find(id);
                if (MEdit != null)
                {
                    MEdit.TenMay = name;
                    MEdit.Phong = idphong;
                }
                else
                {
                    DB.mays.Add(new may()
                    {
                        MaMay = id,
                        TenMay = name,
                        Phong = idphong,
                        TrangThai = false
                    });
                }
                DB.SaveChanges();
            }            
        }
        public List<may> GetListMay()
        {
            DBNet DB = new DBNet();
            var l = from p in DB.mays select p;
            return l.ToList();
        }
        public List<may> GetAllMaybyIDP(int id)
        {
            DBNet DB = new DBNet();
            var l = from p in DB.mays where p.Phong==id select p;
            return l.ToList();

        }
        public List<CBBitem> getCBBitemPhong()
        {
            List<CBBitem> l = new List<CBBitem>();


            foreach (Phong i in GetALLPhong())
            {
                l.Add(new CBBitem() { id = i.IDPhong.ToString(), name = i.TenPhong });
            }
            return l;
        }
        public List<CBBitem> getCBBitemMay()
        {
            List<CBBitem> l = new List<CBBitem>();


            foreach (may i in GetListMay())
            {
                l.Add(new CBBitem() { id = i.MaMay.ToString(), name = i.TenMay });
            }
            return l;
        }
        public List<Phong> GetALLPhong()
        {
            DBNet DB = new DBNet();
            var l = from p in DB.Phongs select p;
            return l.ToList();
        }
        public void DeleteMAY(string id)
        {
            DBNet DB = new DBNet();
            DB.mays.Remove(DB.mays.Find(id));
            DB.SaveChanges();

        }
        public void UpdateSTT(string id, bool STT, string IDTK,string ipclient)
        {
            DBNet DB = new DBNet();
            may MEdit = DB.mays.Find(id);
            MEdit.TrangThai = STT;
            MEdit.IDTK = IDTK;
            MEdit.IPcLient = ipclient;
            DB.SaveChanges();
        }
        public may GetMayByTK(string idtk)
        {
            DBNet Db = new DBNet();
            var M = from p in Db.mays where p.IDTK == idtk select p;
            return (M.SingleOrDefault() as may); 
        }
    }
}
