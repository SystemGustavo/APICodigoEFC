using Infraestructure.Context;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using APICodigoEFC.Request;
using APICodigoEFC.Response;

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

        [HttpGet]
        public List<InvoiceResponse> Get()
        {
            var query = _invoiceService.Get()
                                    .Select(x => new InvoiceResponse
                                    {
                                        InvoiceID = x.InvoiceID,
                                        Number = x.Number,
                                        Description = x.Description,
                                        CustomerName = x.Customer.Name,
                                    }).ToList();
            return query;
        }

        [HttpGet]
        public List<InvoiceResponse> GetByFilters(string? number)
        {
            var query = _invoiceService.GetByFilters(number)
                                                     .Select(x => new InvoiceResponse
                                                     {
                                                         InvoiceID = x.InvoiceID,
                                                         Number = x.Number,
                                                         Description = x.Description,
                                                         CustomerName = x.Customer.Name,
                                                     }).ToList();
            return query;
        }

        [HttpPost]
        public void Insert([FromBody] InvoiceInsertRequest request)
        {
            var invoice = new Invoice
            {
                Number = request.Number,
                Description = request.Description,
                CustomerID = request.CustomerID,
                IsActive = true,
            };

            _invoiceService.Insert(invoice);
        }

        [HttpPut]
        public void Update([FromBody] InvoiceUpdateRequest request)
        {
            var invoice = new Invoice
            {
                InvoiceID = request.InvoiceID,
                Number = request.Number,
                Description = request.Description,
                CustomerID = request.CustomerID,
                IsActive = true,
            };

            _invoiceService.Update(invoice);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _invoiceService.Delete(id);
        }

    }
}
