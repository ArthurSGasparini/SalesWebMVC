using SalesWebMVC.Models.ViewModels.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; } // Propriedade para armazenar o identificador único da venda, geralmente usada como chave primária em bancos de dados
        [DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}")] // Atributo para formatar a exibição da data no formato dia/mês/ano, útil para interfaces de usuário
        public DateTime Date { get; set; } //   Propriedade para armazenar a data da venda, utilizada para cálculos de tempo e exibição
        [DisplayFormat (DataFormatString = "{0:F2}")] // Atributo para formatar a exibição do valor no formato de duas casas decimais, útil para interfaces de usuário
        public double Amount { get; set; } // Propriedade para armazenar o valor da venda, utilizada para cálculos de total e exibição
        public SaleStatus Status { get; set; } // Associação entre as classes - Classe Enum
        public Seller Seller { get; set; } // Associação entre as classes - Uma venda tem um vendedor

        public SalesRecord() // Construtor padrão para permitir a criação de objetos sem parâmetros, necessário para algumas operações do Entity Framework
        {
        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller) // Construtor para inicializar as propriedades da venda, incluindo a associação com o vendedor e o status da venda
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }

    }
}
