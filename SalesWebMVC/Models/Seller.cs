using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        
        public int Id { get; set; } // Propriedade para armazenar o identificador único do vendedor, geralmente usada como chave primária em bancos de dados
        public string Name { get; set; } // Propriedade para armazenar o nome do vendedor, utilizada para exibição e identificação do vendedor
        public string Email { get; set; } // Propriedade para armazenar o email do vendedor, utilizada para comunicação e identificação
        public DateTime BirthDate{ get; set; } // Propriedade para armazenar a data de nascimento do vendedor, utilizada para cálculos de idade e outras operações
        public double BaseSalary { get; set; } // Propriedade para armazenar o salário base do vendedor, utilizada para cálculos de remuneração
        public Department Department { get; set; } // Associação entre as classes
        public int DepartmentId { get; set; } // Propriedade para armazenar o identificador do departamento, utilizada para estabelecer a relação entre o vendedor e o departamento no banco de dados
        public ICollection <SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // Associação entre as classes

        public Seller() // Construtor padrão para permitir a criação de objetos sem parâmetros, necessário para algumas operações do Entity Framework
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department) // Construtor para inicializar as propriedades do vendedor, incluindo a associação com o departamento
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr) // Método para adicionar uma venda à coleção de vendas do vendedor
        {
                Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr) // Método para remover uma venda da coleção de vendas do vendedor
        {
                Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final) // Método para calcular o total de vendas do vendedor em um período específico, utilizando LINQ para filtrar as vendas por data e somar os valores
        { 
           return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); // LINQ para filtrar as vendas por data e somar os valores
        }


    }
}
