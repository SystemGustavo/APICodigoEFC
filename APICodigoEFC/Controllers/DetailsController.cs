using Infraestructure.Context;
using Domain.Models;
using APICodigoEFC.Response;
using APICodigoEFC.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Services;
using APICodigoEFC.Request;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly CodigoContext _context;
        private DetailsService _detailsService;

        public DetailsController(CodigoContext context)
        {
            _context = context;
            _detailsService = new DetailsService(_context);
        }

        [HttpPost]
        public void Insert([FromBody] DetailInsertRequest request)
        {
            var detail = new Detail
            {
                IsActive = true,
                Amount = request.Amount,
                Price = request.Price,
                SubTotal = request.SubTotal,
                ProductID = request.ProductID,
                InvoiceID = request.InvoiceID,
            };

            _detailsService.Insert(detail);
        }

        [HttpPut]
        public void Update([FromBody] DetailUpdateRequest request)
        {
            var detail = new Detail
            {
                DetailID = request.DetailID,
                IsActive = true,
                Amount = request.Amount,
                Price = request.Price,
                SubTotal = request.SubTotal,
                ProductID = request.ProductID,
                InvoiceID = request.InvoiceID,
            };

            _detailsService.Update(detail);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _detailsService.Delete(id);
        }

        //Listar todos los detalles y buscar por nombre de cliente.
        [HttpGet]
        public List<DetailGetRequest> GetByFilters(string? customerName, string? invoiceNumber)
        {
            var ListDetailsResponse = _detailsService.GetByFilters(customerName, invoiceNumber)
                                                     .Select(x => new DetailGetRequest
                                                     {
                                                         DetailID = x.DetailID,
                                                         Amount = x.Amount,
                                                         Price = x.Price,
                                                         SubTotal = x.SubTotal,
                                                         CustomerName = x.Invoice.Customer.Name,
                                                         Product = x.Product.Name,
                                                         Invoice = x.Invoice.Description
                                                     }).ToList();
            return ListDetailsResponse;
        }


        [HttpGet]
        public List<DetailResponseV1> GetByInvoiceNumber(string? invoiceNumber)
        {
            //Convertir modelo al response
            var response = _detailsService.GetByInvoiceNumber(invoiceNumber)
                                          .Select(x => new DetailResponseV1
                                          {
                                              InvoiceNumber = x.Invoice.Number,
                                              ProductName = x.Product.Name,
                                              SubTotal = x.SubTotal
                                          }).ToList();
            return response;
        }

        [HttpGet]
        public List<DetailResponseV2> GetByInvoiceNumber2(string? invoiceNumber)
        {
            //Convertir modelo al response
            var response = _detailsService.GetByInvoiceNumber2(invoiceNumber)
                                          .Select(x => new DetailResponseV2
                                          {
                                              InvoiceNumber = x.Invoice.Number,
                                              ProductName = x.Product.Name,
                                              Amount = x.Amount,
                                              Price = x.Price,
                                              IGV = x.Amount * x.Price * Constants.IGV
                                          }).ToList();
            return response;
        }
    }
}
