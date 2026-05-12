using Microsoft.AspNetCore.Mvc; //controlador recebe a chamada do caminho /sellers do site, e a ação Index é responsável por processar essa solicitação e retornar a view correspondente.
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;     //A view é um arquivo de interface do usuário que exibe a lista de vendedores cadastrados no sistema. No momento, o método Index retorna apenas a view,
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

        

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            } 
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
