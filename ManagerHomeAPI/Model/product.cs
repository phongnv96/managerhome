namespace ManagerHomeAPI.Model
{
    public class product
    {
        public int id {set;get;}
        public string name {set;get;}
        public string decription {set;get;}
        public double originPrice {set;get;}
        public double interest {set;get;}
        public double totalPrice {set;get;}
        public int categoryId {set;get;}
        public category categories{set;get;}
    }
}