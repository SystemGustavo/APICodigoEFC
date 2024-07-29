namespace APICodigoEFC.Request
{
    public class DetailInsertRequest
    {
        public int Amount { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public int ProductID { get; set; }
        public int InvoiceID { get; set; }
    }
}
