using System.Collections.Generic;

namespace ManagerHomeAPI.Model
{
    public class supplier
    {
        public int id{set;get;}
        public string name{set;get;}
        public string address{set;get;}
        public string phoneNumber{set;get;}
        public ICollection<category> categories {get;set;}
    }
}