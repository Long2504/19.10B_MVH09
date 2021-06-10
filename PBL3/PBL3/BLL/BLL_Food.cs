using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
using PBL3.GUI;
namespace PBL3.BLL
{
    class BLL_Food
    {
        private static BLL_Food _Instance;
        public static BLL_Food Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Food();
                }
                return _Instance;
            }
            set { }
        }

        public List<Food> GetAllFood()
        {
            DBNet DB = new DBNet();
            var l = from p in DB.Foods select p;
            return l.ToList();
        }
        public List<Food> GetAllFoodByType(string idType)
        {
            DBNet DB = new DBNet();
            var l = from p in DB.Foods where p.TypeF==idType select p;
            return l.ToList();
        }
        public List<CBBitem> GetCBBAllTypeF()
        {
            DBNet DB = new DBNet();
            var l = from p in DB.TypeFoods select p;
            List<CBBitem> lb = new List<CBBitem>();
            foreach(TypeFood i in l)
            {
                lb.Add(new CBBitem() { id = i.IDType, name = i.TenType });
            }
            return lb;
        }
        public List<TypeFood> GetAllTypeF()
        {
            DBNet DB = new DBNet();
            var l = from p in DB.TypeFoods select p;
            return l.ToList();
        }
        public void AddOrEditFood(Food F)
        {
            using(DBNet DB =new DBNet())
            {
                Food FEdit = DB.Foods.Find(F.IDFood);
                if (FEdit != null)
                {
                    FEdit.TenFood = F.TenFood;
                    FEdit.TypeF = F.TypeF;
                    FEdit.Gia = F.Gia;
                }
                else
                {
                    DB.Foods.Add(F);
                }
                DB.SaveChanges();
            }
        }
        public void DeleteF(string id)
        {
            DBNet DB = new DBNet();
            DB.Foods.Remove(DB.Foods.Find(id));
            DB.SaveChanges();
        }
        public List<KHorder> GetAllKHorder()
        {
            DBNet Db = new DBNet();
            var l = from p in Db.KHorders select p;
            return l.ToList();
        }
        public Food Getfoodbyid(string id)
        {
            DBNet Db = new DBNet();
            return Db.Foods.Find(id);
        }
    }
}
