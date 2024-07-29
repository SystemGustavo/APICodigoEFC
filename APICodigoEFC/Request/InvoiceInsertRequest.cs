using Domain.Models;

namespace APICodigoEFC.Request
{
    public class InvoiceInsertRequest
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public int CustomerID { get; set; }
        public bool IsActive { get; set; }
    }
}
