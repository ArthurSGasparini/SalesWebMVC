using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;
using System.Threading.Tasks;
using System;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() => await _context.Seller.ToListAsync(); // funcao responsavel por buscar todos os vendedores no banco de dados. O método é assíncrono, o que significa que ele pode ser executado de forma não bloqueante, permitindo que outras operações sejam realizadas enquanto a busca está em andamento. O método retorna uma lista de objetos do tipo Seller, representando todos os vendedores encontrados no banco de dados. Dentro do método, é feita uma consulta ao contexto do banco de dados usando o método ToListAsync para obter todos os vendedores e retornar a lista resultante.

        public async Task InsertAsync(Seller obj) // funcao responsavel por inserir um novo vendedor no banco de dados. O método é assíncrono, o que significa que ele pode ser executado de forma não bloqueante, permitindo que outras operações sejam realizadas enquanto a inserção está em andamento. O método recebe um objeto do tipo Seller como parâmetro, que contém as informações do vendedor a ser inserido. Dentro do método, o objeto é adicionado ao contexto do banco de dados usando o método Add, e em seguida, as alterações são salvas no banco de dados usando o método SaveChangesAsync. O uso do await garante que o método aguarde a conclusão da operação de salvamento antes de continuar a execução.
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id) => await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // funcao responsavel por buscar um vendedor no banco de dados com base em seu ID. O método é assíncrono, o que significa que ele pode ser executado de forma não bloqueante, permitindo que outras operações sejam realizadas enquanto a busca está em andamento. O método recebe um inteiro id como parâmetro, que representa o ID do vendedor a ser buscado. Dentro do método, é feita uma consulta ao contexto do banco de dados usando o método FirstOrDefaultAsync para encontrar o primeiro vendedor que corresponda ao ID fornecido. A consulta inclui a propriedade Department do vendedor usando o método Include, garantindo que as informações do departamento também sejam carregadas. O resultado da consulta é retornado como um objeto do tipo Seller.

        public async Task RemoveAsync(int id) // funcao responsavel por remover um vendedor do banco de dados com base em seu ID. O método é assíncrono, o que significa que ele pode ser executado de forma não bloqueante, permitindo que outras operações sejam realizadas enquanto a remoção está em andamento. O método recebe um inteiro id como parâmetro, que representa o ID do vendedor a ser removido. Dentro do método, é feita uma consulta ao contexto do banco de dados usando o método FindAsync para encontrar o vendedor correspondente ao ID fornecido. Em seguida, o vendedor encontrado é removido do contexto usando o método Remove, e as alterações são salvas no banco de dados usando o método SaveChangesAsync. O uso do await garante que o método aguarde a conclusão da operação de salvamento antes de continuar a execução.
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj) // funcao responsavel por atualizar as informações de um vendedor no banco de dados. O método é assíncrono, o que significa que ele pode ser executado de forma não bloqueante, permitindo que outras operações sejam realizadas enquanto a atualização está em andamento. O método recebe um objeto do tipo Seller como parâmetro, que contém as informações atualizadas do vendedor. Dentro do método, é feita uma verificação para garantir que o ID do vendedor a ser atualizado exista no banco de dados usando o método AnyAsync. Se o ID não for encontrado, uma exceção NotFoundException é lançada. Caso contrário, o objeto é atualizado no contexto do banco de dados usando o método Update, e as alterações são salvas no banco de dados usando o método SaveChangesAsync. O uso do await garante que o método aguarde a conclusão da operação de salvamento antes de continuar a execução. Se ocorrer uma exceção de concorrência durante a atualização, uma exceção personalizada DbConcurrencyException é lançada com a mensagem da exceção original.
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        internal Seller FindById(int value) => throw new NotImplementedException();

        // await: palavra-chave usada para aguardar a conclusão de uma operação assíncrona, permitindo que outras operações sejam realizadas enquanto a operação está em andamento. O uso do await garante que o método aguarde a conclusão da operação antes de continuar a execução, evitando bloqueios e melhorando a eficiência do aplicativo.
        // async: palavra-chave usada para indicar que um método é assíncrono, permitindo que ele seja executado de forma não bloqueante. Um método assíncrono pode conter operações que levam tempo para serem concluídas, como acesso a banco de dados ou chamadas de rede, e o uso do async permite que outras operações sejam realizadas enquanto essas operações estão em andamento, melhorando a responsividade do aplicativo.
    }


}