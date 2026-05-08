using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index() // Nova subpasta chamada Sellers, com a ação Index, que é a ação padrão para exibir uma lista de vendedores. Atualmente, o método retorna apenas a view correspondente, mas futuramente pode ser expandido para incluir lógica de consulta ao banco de dados e exibição dos vendedores cadastrados.
        {
            return View();
        }
    }
}
