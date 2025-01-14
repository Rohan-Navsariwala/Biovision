using Microsoft.AspNetCore.Mvc;
using biovision.Models;
using biovision.Services;


namespace biovision.Controllers {
	public class AdminController : Controller {
		private readonly AuthService _authService;
		private readonly AssetUploadService _assetUploadService;
		private readonly AssetFetchService _assetFetchService;
		private readonly DeleteEntryService _deleteEntryService;

		public AdminController(AuthService authService, AssetUploadService assetUploadService, AssetFetchService assetFetchService, DeleteEntryService deleteEntryService) {
			_authService = authService;
			_assetUploadService = assetUploadService;
			_assetFetchService = assetFetchService;
			_deleteEntryService = deleteEntryService;
		}

		public IActionResult Index() {
			return View();
		}

		[HttpPost]
		public IActionResult Login(string username, string password) {

			if (_authService.Authenticate(username, password)) {

				HttpContext.Session.SetString("Admin", "true");
				return RedirectToAction("AdminDash");
			}

			ModelState.AddModelError("", "Invalid credentials");
			return RedirectToAction("Index");
		}

		public IActionResult Logout() {
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}

		public IActionResult AddProduct() {

			if (!_authService.IsAuthenticated())
				return RedirectToAction("Index");

			return View("AddProduct");
		}

		public IActionResult AdminDash() {
			if (!_authService.IsAuthenticated())
				return RedirectToAction("Index");

			return View("AdminDash");
		}


		public IActionResult UpdateList(Products products, IFormFile image_input, IFormFile catalogue_input) {
			_assetUploadService.AssetUpload(products, image_input, catalogue_input);
			return RedirectToAction("AdminDash");
		}

		public IActionResult DisplayList(string category) {
			IEnumerable<Products> products = _assetFetchService.GetProductsByCategory(category);

			if (!_authService.IsAuthenticated())
				return RedirectToAction("Index");

			return View("AdminDash", products);
		}

		public IActionResult DeleteEntry(int Id) {
			if(_authService.IsAuthenticated())
				_deleteEntryService.Delete(Id);

			return RedirectToAction("AdminDash");
		}
	}
}
