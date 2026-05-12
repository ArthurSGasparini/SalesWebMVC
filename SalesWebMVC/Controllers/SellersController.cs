using Microsoft.AspNetCore.Mvc; //controlador recebe a chamada do caminho /sellers do site, e a ação Index é responsável por processar essa solicitação e retornar a view correspondente.
using SalesWebMVC.Services;     //A view é um arquivo de interface do usuário que exibe a lista de vendedores cadastrados no sistema. No momento, o método Index retorna apenas a view,
                                //mas futuramente pode ser expandido para incluir lógica de consulta ao banco de dados e exibição dos vendedores cadastrados.

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index() // Nova subpasta chamada Sellers, com a ação Index, que é a ação padrão para exibir uma lista de vendedores. Atualmente, o método retorna apenas a view correspondente, mas futuramente pode ser expandido para incluir lógica de consulta ao banco de dados e exibição dos vendedores cadastrados.
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
