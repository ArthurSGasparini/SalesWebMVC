using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;

namespace SalesWebMVC.Models
{
    public class Seller
    {

        public int Id { get; set; } // Propriedade para armazenar o identificador único do vendedor, geralmente usada como chave primária em bancos de dados

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters")]
        public string Name { get; set; } // Propriedade para armazenar o nome do vendedor, utilizada para exibição e identificação do vendedor

        [DataType(DataType.EmailAddress)] // Atributo para especificar o tipo de dados da propriedade, útil para formatação e validação de endereços de email
        [Required(ErrorMessage = "{0} is required")] // Atributo para indicar que a propriedade é obrigatória, útil para validação de dados
        [EmailAddress(ErrorMessage = "Enter a valid email")] // Atributo para validar o formato do email, útil para garantir que o valor inserido seja um endereço de email válido
        public string Email { get; set; } // Propriedade para armazenar o email do vendedor, utilizada para comunicação e identificação

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Birth Date")] // Atributo para personalizar o nome da propriedade na exibição, útil para interfaces de usuário
        [DataType(DataType.Date)] // Atributo para especificar o tipo de dados da propriedade, útil para formatação e validação de datas
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // Atributo para especificar o formato de exibição da propriedade, útil para formatação de datas 0 para exibir apenas o dia, mês e ano
        public DateTime BirthDate { get; set; } // Propriedade para armazenar a data de nascimento do vendedor, utilizada para cálculos de idade e outras operações

        [Required(ErrorMessage = "{0} is required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // Atributo para validar que o valor da propriedade esteja dentro de um intervalo específico, útil para garantir que o salário base seja razoável
        [Display(Name = "Base Salary")]  // Atributo para personalizar o nome da propriedade na exibição, útil para interfaces de usuário
        [DisplayFormat(DataFormatString = "{0:F2}")] // Atributo para especificar o formato de exibição da propriedade, útil para formatação de valores monetários
        public double BaseSalary { get; set; } // Propriedade para armazenar o salário base do vendedor, utilizada para cálculos de remuneração

        public Department Department { get; set; } // Associação entre as classes
        public int DepartmentId { get; set; } // Propriedade para armazenar o identificador do departamento, utilizada para estabelecer a relação entre o vendedor e o departamento no banco de dados
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // Associação entre as classes

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
