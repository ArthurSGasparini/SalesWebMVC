using Microsoft.AspNetCore.Mvc; //controlador recebe a chamada do caminho /sellers do site, e a ação Index é responsável por processar essa solicitação e retornar a view correspondente.
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;     //A view é um arquivo de interface do usuário que exibe a lista de vendedores cadastrados no sistema. No momento, o método Index retorna apenas a view,
                              //mas futuramente pode ser expandido para incluir lógica de consulta ao banco de dados e exibição dos vendedores cadastrados.

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() // Nova subpasta chamada Sellers, com a ação Index, que é a ação padrão para exibir uma lista de vendedores. Atualmente, o método retorna apenas a view correspondente, mas futuramente pode ser expandido para incluir lógica de consulta ao banco de dados e exibição dos vendedores cadastrados.
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() // A ação Create é responsável por exibir um formulário para criar um novo vendedor. Ela retorna uma view que contém o formulário de criação. No momento, o método apenas retorna a view, mas futuramente pode ser expandido para incluir lógica de preparação de dados necessários para o formulário, como a lista de departamentos disponíveis.
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // A anotação [HttpPost] indica que este método deve ser chamado quando uma solicitação HTTP POST for feita para a ação Create. Isso é típico para ações que processam formulários de criação ou edição de dados.
        [ValidateAntiForgeryToken] // A anotação [ValidateAntiForgeryToken] é usada para proteger contra ataques de falsificação de solicitação entre sites (CSRF). Ela garante que o formulário enviado para esta ação contenha um token de validação que corresponda ao token gerado pelo servidor, ajudando a prevenir ataques maliciosos.
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int? id) // A ação Delete é responsável por exibir uma confirmação de exclusão para um vendedor específico. Ela recebe um parâmetro id que representa o identificador do vendedor a ser excluído. O método verifica se o id é nulo e, em caso afirmativo, retorna uma resposta NotFound. Em seguida, ele busca o vendedor correspondente ao id usando o serviço _sellerService.FindById(id.Value). Se o vendedor não for encontrado, novamente retorna NotFound. Caso contrário, retorna a view com o objeto do vendedor para exibir a confirmação de exclusão.
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value); // O método FindById é chamado para buscar o vendedor correspondente ao id fornecido. O valor do id é passado como argumento para o método, e o resultado é armazenado na variável obj. Se o vendedor não for encontrado, obj será nulo.
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) // A ação Delete é responsável por processar a exclusão de um vendedor específico. Ela recebe um parâmetro id que representa o identificador do vendedor a ser excluído. O método chama o serviço _sellerService.Remove(id) para remover o vendedor do banco de dados. Após a exclusão, ele redireciona para a ação Index para exibir a lista atualizada de vendedores.
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) // A ação Details é responsável por exibir os detalhes de um vendedor específico. Ela recebe um parâmetro id que representa o identificador do vendedor cujos detalhes devem ser exibidos. O método verifica se o id é nulo e, em caso afirmativo, retorna uma resposta NotFound. Em seguida, ele busca o vendedor correspondente ao id usando o serviço _sellerService.FindById(id.Value). Se o vendedor não for encontrado, novamente retorna NotFound. Caso contrário, retorna a view com o objeto do vendedor para exibir os detalhes.
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id) // A ação Edit é responsável por exibir um formulário para editar um vendedor existente. Ela recebe um parâmetro id que representa o identificador do vendedor a ser editado. O método verifica se o id é nulo e, em caso afirmativo, retorna uma resposta NotFound. Em seguida, ele busca o vendedor correspondente ao id usando o serviço _sellerService.FindById(id.Value). Se o vendedor não for encontrado, novamente retorna NotFound. Caso contrário, ele prepara uma lista de departamentos usando _departmentService.FindAll() e cria um objeto SellerFormViewModel que contém o vendedor e a lista de departamentos. Por fim, retorna a view com o viewModel para exibir o formulário de edição.
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        [HttpPost] // A anotação [HttpPost] indica que este método deve ser chamado quando uma solicitação HTTP POST for feita para a ação Edit. Isso é típico para ações que processam formulários de criação ou edição de dados.
        [ValidateAntiForgeryToken]// A anotação [ValidateAntiForgeryToken] é usada para proteger contra ataques de falsificação de solicitação entre sites (CSRF). Ela garante que o formulário enviado para esta ação contenha um token de validação que corresponda ao token gerado pelo servidor, ajudando a prevenir ataques maliciosos.
        public IActionResult Edit(int id, Seller seller) // A ação Edit é responsável por exibir um formulário para editar um vendedor existente. Ela recebe um parâmetro id que representa o identificador do vendedor a ser editado. O método verifica se o id é nulo e, em caso afirmativo, retorna uma resposta NotFound. Em seguida, ele busca o vendedor correspondente ao id usando o serviço _sellerService.FindById(id.Value). Se o vendedor não for encontrado, novamente retorna NotFound. Caso contrário, ele prepara uma lista de departamentos usando _departmentService.FindAll() e cria um objeto SellerFormViewModel que contém o vendedor e a lista de departamentos. Por fim, retorna a view com o viewModel para exibir o formulário de edição.
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index)); // Após a atualização do vendedor, o método redireciona para a ação Index para exibir a lista atualizada de vendedores. O método RedirectToAction é usado para redirecionar para a ação Index, que é a ação responsável por exibir a lista de vendedores cadastrados no sistema.
            }
            catch (ApplicationException e) // A ação Edit é responsável por exibir um formulário para editar um vendedor existente. Ela recebe um parâmetro id que representa o identificador do vendedor a ser editado. O método verifica se o id é nulo e, em caso afirmativo, retorna uma resposta NotFound. Em seguida, ele busca o vendedor correspondente ao id usando o serviço _sellerService.FindById(id.Value). Se o vendedor não for encontrado, novamente retorna NotFound. Caso contrário, ele prepara uma lista de departamentos usando _departmentService.FindAll() e cria um objeto SellerFormViewModel que contém o vendedor e a lista de departamentos. Por fim, retorna a view com o viewModel para exibir o formulário de edição.
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public IActionResult Error(string message) // A ação Error é responsável por exibir uma página de erro personalizada. Ela recebe um parâmetro message que representa a mensagem de erro a ser exibida. O método cria um objeto ErrorViewModel com a mensagem de erro e retorna a view correspondente para exibir a página de erro.
        {
            var viewModel = new ErrorViewModel 
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
