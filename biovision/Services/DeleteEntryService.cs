using biovision.Data;
using biovision.Models;

namespace biovision.Services {
	public class DeleteEntryService {

		private readonly ApplicationDBContext _dbcontext;
		public DeleteEntryService(ApplicationDBContext dbContext) {
			_dbcontext = dbContext;
		}


		public void Delete(int Id) {

			var product = _dbcontext.Products.Find(Id);

			DeleteFiles(product);
			DeleteEntry(product);

		}

		public void DeleteFiles(Products product) {

			var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

			if (!string.IsNullOrEmpty(product.Image_URL)) {
				var imagePath = Path.Combine(basePath, product.Image_URL.TrimStart('/'));
				if (File.Exists(imagePath)) {
					File.Delete(imagePath);
				}
			}

			if (!string.IsNullOrEmpty(product.Catalogue_URL)) {
				var cataloguePath = Path.Combine(basePath, product.Catalogue_URL.TrimStart('/'));
				if (File.Exists(cataloguePath)) {
					File.Delete(cataloguePath);
				}
			}
		}


		public void DeleteEntry(Products product) {
			_dbcontext.Products.Remove(product);
			_dbcontext.SaveChanges();

		}
	}
}
