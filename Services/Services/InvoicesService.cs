using Infraestructure.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class InvoicesService
    {
        private readonly CodigoContext _context;
        public InvoicesService(CodigoContext context)
        {
            _context = context;
        }

        public void Insert(Invoice invoice)
        {
            //verificar la Existencia de Customers
            var customer = _context.Customers.Find(invoice.CustomerID);

            //Logica de Negocio
            if (customer != null)
            {
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
            }
        }

        public void Update(Invoice invoice)
        {
            //verificar la Existencia de Invoices y Customers
            var invoices = _context.Invoices.Find(invoice.InvoiceID);
            var customers = _context.Customers.Find(invoice.CustomerID);

            //Logica de Negocio
            if (invoices != null && customers != null)
            {
                //mapear 
                invoices.InvoiceID = invoice.InvoiceID;
                invoices.Number = invoice.Number;
                invoices.Description = invoice.Description;
                invoices.CustomerID = invoice.CustomerID;
                invoices.IsActive = invoice.IsActive;

                _context.Entry(invoices).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var invoices = _context.Invoices.Find(id);
            if (invoices != null)
            {
                invoices.IsActive = false;
                _context.Entry(invoices).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Invoice> GetByFilters(string? number)
        {
            IQueryable<Invoice> query = _context.Invoices.Include(x => x.Customer).Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(number))
                query = query.Where(x => x.Number.Contains(number));
            return query.ToList();
        }

        public IEnumerable<Invoice> Get()
        {
            IQueryable<Invoice> query = _context.Invoices.Where(x => x.IsActive == true);
            return query.ToList();
        }
    }
}
