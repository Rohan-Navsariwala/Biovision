using Microsoft.AspNetCore.Mvc;

namespace biovision.Controllers {
	public class AboutController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}


