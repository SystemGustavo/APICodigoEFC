using Domain.Models;

namespace APICodigoEFC.Response
{
    public class InvoiceResponse
    {
        public int InvoiceID { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
    }
}
