using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DetailsService
    {
        private readonly CodigoContext _context;
        public DetailsService(CodigoContext context)
        {
            _context = context;
        }

        public void Insert(Detail detail)
        {
            //verificar la Existencia de Products y Invoices
            var product = _context.Products.Find(detail.ProductID);
            var invoice = _context.Invoices.Find(detail.InvoiceID);

            //Logica de Negocio
            if (product != null && invoice != null) 
            {
                _context.Details.Add(detail);
                _context.SaveChanges();
            }
        }

        public void Update(Detail detail)
        {
            //verificar la Existencia de Details,Products y Invoices
            var details = _context.Details.Find(detail.DetailID);
            var products = _context.Products.Find(detail.ProductID);
            var invoices = _context.Invoices.Find(detail.InvoiceID);

            //Logica de Negocio
            if(details != null)
            {
                //mapear 
                details.DetailID = detail.DetailID;
                details.IsActive = detail.IsActive;
                details.Amount = detail.Amount;
                details.Price = detail.Price;
                details.SubTotal = detail.SubTotal;
                details.ProductID = detail.ProductID;
                details.InvoiceID = detail.InvoiceID;

                if (products != null && invoices != null)
                {
                    _context.Entry(details).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            var details = _context.Details.Find(id);
            if (details != null)
            {
                details.IsActive = false;
                _context.Entry(details).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Detail> Get()
        {
            IQueryable<Detail> query = _context.Details
                                     .Include(x => x.Product)
                                     .Include(x => x.Invoice)
                                     .Where(x => x.IsActive);
            //Logica de Negocio
            if (!query.Any())
                return Enumerable.Empty<Detail>();
            else
                return query.ToList();
        }

        public IEnumerable<Detail> GetByFilters(int? DetailId)
        {
            IQueryable<Detail> query = _context.Details
                                     .Include(x => x.Product)
                                     .Include(x => x.Invoice)
                                     .Where(x => x.DetailID == DetailId);
            //Logica de Negocio
            if (!query.Any())
                return Enumerable.Empty<Detail>();
            else
                return query.ToList();
        }

    }
}
