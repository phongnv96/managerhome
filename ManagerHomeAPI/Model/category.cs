using System.Collections.Generic;

namespace ManagerHomeAPI.Model
{
    public class category
    {
        public int id{set;get;}
        public string name {set;get;}
        public int supplierId {set;get;}
        public supplier supplier {set;get;} 
        public ICollection<product> products {set;get;}
    }
}