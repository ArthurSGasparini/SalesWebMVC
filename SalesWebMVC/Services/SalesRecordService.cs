using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) // o metodo FindByDateAsync tem a função de buscar os registros de vendas por data, podendo ser filtrado por data mínima e/ou data máxima. O resultado é uma lista de SalesRecord ordenada por data de forma decrescente, incluindo as informações do vendedor e do departamento do vendedor.
        { 
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate) // a diferença entre o método FindByDateAsync e o método FindByDateGroupingAsync é que o segundo método retorna os registros de vendas agrupados por departamento do vendedor, enquanto o primeiro método retorna uma lista simples de registros de vendas. O método FindByDateGroupingAsync utiliza a função GroupBy para agrupar os registros de vendas por departamento do vendedor, permitindo que os resultados sejam organizados por departamento. O resultado é uma lista de grupos, onde cada grupo contém os registros de vendas correspondentes a um departamento específico.
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            /* return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department) // agrupa os registros de vendas por departamento do vendedor, permitindo que os resultados sejam organizados por departamento.
                .ToListAsync();*/
            var list = await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return list.GroupBy(x => x.Seller.Department).ToList();

        }



    }
}
