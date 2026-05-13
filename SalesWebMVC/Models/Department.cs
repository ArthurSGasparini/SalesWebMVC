using System.Collections.Generic;
using System.Linq;
using System;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; } // Propriedade para armazenar o identificador único do departamento, geralmente usada como chave primária em bancos de dados
        public string Name { get; set; } // Propriedade para armazenar o nome do departamento, utilizada para exibição e identificação do departamento
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); // Associação entre as classes - Um departamento tem vários vendedores

        public Department() // Construtor padrão para permitir a criação de objetos sem parâmetros, necessário para algumas operações do Entity Framework
        {
        }

        public Department(int id, string name) // Construtor para inicializar as propriedades do departamento, incluindo a associação com os vendedores
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller) => Sellers.Add(seller); // Método para adicionar um vendedor à coleção de vendedores do departamento

        public double TotalSales(DateTime initial, DateTime final) => Sellers.Sum(seller => seller.TotalSales(initial, final)); // Método para calcular o total de vendas do departamento em um período específico, utilizando LINQ para somar as vendas de todos os vendedores do departamento // LINQ para somar as vendas de todos os vendedores do departamento
    }
}
