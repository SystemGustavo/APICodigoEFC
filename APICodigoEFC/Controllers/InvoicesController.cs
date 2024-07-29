using Infraestructure.Context;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly CodigoContext _context;
        private InvoicesService _invoiceService;

        public InvoicesController(CodigoContext context)
        {
            _context = context;
            _invoiceService = new InvoicesService(_context);
        }

        //[HttpGet]
        //public List<Invoice> GetByFilters(string? number)
        //{
        //    IQueryable<Invoice> query = _context.Invoices.Include(x => x.Customer).Where(x => x.IsActive);

        //    if (!string.IsNullOrEmpty(number))
        //        query = query.Where(x => x.Number.Contains(number));
       

        //    return query.ToList();
        //}

        //[HttpPost]
        //public void Insert([FromBody] Invoice invoice)
        //{
        //    _context.Invoices.Add(invoice);
        //    _context.SaveChanges();
        //}

    }
}
