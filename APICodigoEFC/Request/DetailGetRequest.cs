using Domain.Models;

namespace APICodigoEFC.Request
{
    public class DetailGetRequest
    {
        public int DetailID { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public string Invoice { get; set; }
    }
}
