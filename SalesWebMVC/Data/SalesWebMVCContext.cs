using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Models
{
    public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext (DbContextOptions<SalesWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } // Propriedade para acessar a tabela de departamentos no banco de dados, permitindo operações de consulta e manipulação de dados relacionados aos departamentos
        public DbSet<Seller> Seller { get; set; }  // Propriedade para acessar a tabela de vendedores no banco de dados, permitindo operações de consulta e manipulação de dados relacionados aos vendedores
        public DbSet<SalesRecord> SalesRecord { get; set; } // Propriedade para acessar a tabela de registros de vendas no banco de dados, permitindo operações de consulta e manipulação de dados relacionados às vendas
    }

}
