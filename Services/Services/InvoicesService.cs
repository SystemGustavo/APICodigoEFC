using Infraestructure.Context;
using Domain.Models;
using Infraestructure.Context;
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
    }
}
