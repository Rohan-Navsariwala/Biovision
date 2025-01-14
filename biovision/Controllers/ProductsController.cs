using Microsoft.AspNetCore.Mvc;
using biovision.Services;
using biovision.Models;

namespace biovision.Controllers {
	public class ProductsController : Controller {
		private readonly AssetFetchService _assetFetchService;

		public ProductsController(AssetFetchService assetFetchService) {
			_assetFetchService = assetFetchService;
		}

		public IActionResult Index(string category) {
			IEnumerable<Products> productlist = _assetFetchService.GetProductsByCategory(category);
			return View(productlist);
		}
	}
}
