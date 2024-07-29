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

        [HttpGet]
        [AllowAnonymous]
        public List<DetailGetRequest> GetAllDetail()
        {
            var ListDetails =  _detailsService.Get();
            List<DetailGetRequest> detailsGet = new List<DetailGetRequest>();
            foreach (var item in ListDetails)
            {
                detailsGet.Add(new DetailGetRequest
                {
                    DetailID = item.DetailID,
                    Amount = item.Amount,
                    Price = item.Price,
                    SubTotal = item.SubTotal,
                    Product = item.Product.Name,
                    Invoice = item.Invoice.Description
                });
            }
            return detailsGet;

        }

        [HttpGet("{DetailId}")]
        [AllowAnonymous]
        public List<DetailGetRequest> GetByFilters(int? DetailId)
        {
            var ListDetails = _detailsService.GetByFilters(DetailId);
            List<DetailGetRequest> detailsGet = new List<DetailGetRequest>();
            foreach (var item in ListDetails)
            {
                detailsGet.Add(new DetailGetRequest
                {
                    DetailID = item.DetailID,
                    Amount = item.Amount,
                    Price = item.Price,
                    SubTotal = item.SubTotal,
                    Product = item.Product.Name,
                    Invoice = item.Invoice.Description
                });
            }
            return detailsGet;
        }

        //    return query.ToList();
        //}
        //Listar todos los detalles y buscar por nombre de cliente.
        //[HttpGet]
        //public List<Detail> GetByFilters(string? customerName, string? invoiceNumber)
        //{
        //    IQueryable<Detail> query = _context.Details
        //       .Include(x => x.Product)
        //       .Include(x => x.Invoice).ThenInclude(y => y.Customer)
        //       .Where(x => x.IsActive);

        //    if (!string.IsNullOrEmpty(customerName))
        //        query = query.Where(x => x.Invoice.Customer.Name.Contains(customerName));
        //    if (!string.IsNullOrEmpty(invoiceNumber))
        //        query = query.Where(x => x.Invoice.Number.Contains(invoiceNumber));


        //    return query.ToList();
        //}


        //[HttpGet]
        //public List<DetailResponseV1> GetByInvoiceNumber(string? invoiceNumber)
        //{

        //    IQueryable<Detail> query = _context.Details
        //        .Include(x => x.Product)
        //        .Include(x => x.Invoice)
        //        .Where(x => x.IsActive);
        //    if (!string.IsNullOrEmpty(invoiceNumber))
        //        query = query.Where(x => x.Invoice.Number.Contains(invoiceNumber));

        //    //Todos los detalles del modelo
        //    var details = query.ToList();


        //    //Convertir modelo al response
        //    var response = details
        //                   .Select(x => new DetailResponseV1                            
        //                   {            
        //                    InvoiceNumber=x.Invoice.Number,
        //                    ProductName=x.Product.Name,
        //                    SubTotal=x.SubTotal
        //                    }).ToList();

        //    return response;
        //}

        //[HttpGet]
        //public List<DetailResponseV2> GetByInvoiceNumber2(string? invoiceNumber)
        //{

        //    IQueryable<Detail> query = _context.Details
        //        .Include(x => x.Product)
        //        .Include(x => x.Invoice)
        //        .Where(x => x.IsActive);
        //    if (!string.IsNullOrEmpty(invoiceNumber))
        //        query = query.Where(x => x.Invoice.Number.Contains(invoiceNumber));

        //    //Todos los detalles del modelo
        //    var details = query.ToList();


        //    //Convertir modelo al response
        //    var response = details
        //                   .Select(x => new DetailResponseV2
        //                   {
        //                       InvoiceNumber = x.Invoice.Number,
        //                       ProductName = x.Product.Name,
        //                       Amount = x.Amount,
        //                       Price=x.Price,
        //                       IGV=x.Amount*x.Price*Constants.IGV
        //                   }).ToList();

        //    return response;
        //}
    }
}
