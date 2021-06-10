using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
namespace PBL3.BLL
{
    class BLL_DV
    {
        private static BLL_DV _Instance;
        public static BLL_DV Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_DV();
                }
                return _Instance;
            }
            set { }
        }
        public List<DichVuKH> GetAllDVKH()
        {
            DBNet DB = new DBNet();
            var l = from p in DB.DichVuKHs select p;
            return l.ToList();
        }
    }
}
