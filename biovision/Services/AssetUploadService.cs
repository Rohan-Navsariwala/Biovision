using biovision.Data;
using biovision.Models;

namespace biovision.Services {
	public class AssetUploadService {

		private readonly ApplicationDBContext _dbcontext;
		public AssetUploadService(ApplicationDBContext dbContext) {
			_dbcontext = dbContext;
		}

		//naming convention has changed to timestamp, this is to prevent overwriting of files
		public void AssetUpload(Products products, IFormFile image_input, IFormFile catalogue_input) {
			if (image_input != null) {
				var newFileName = $"{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(image_input.FileName)}";
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets", newFileName);
				using (var fileStream = new FileStream(path, FileMode.Create)) {
					image_input.CopyTo(fileStream);
				}
				products.Image_URL = "/assets/" + newFileName;
			}

			if (catalogue_input != null) {
				var newFileName = $"{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(catalogue_input.FileName)}";
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets", newFileName);
				using (var fileStream = new FileStream(path, FileMode.Create)) {
					catalogue_input.CopyTo(fileStream);
				}
				products.Catalogue_URL = "/assets/" + newFileName;
			}
			_dbcontext.Products.Add(products);
			_dbcontext.SaveChanges();
		}
	}
}
