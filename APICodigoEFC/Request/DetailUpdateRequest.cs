namespace APICodigoEFC.Request
{
    public class DetailUpdateRequest
    {
        public int DetailID { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public int ProductID { get; set; }
        public int InvoiceID { get; set; }
    }
}
