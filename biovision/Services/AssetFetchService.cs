using biovision.Data;
using biovision.Models;

namespace biovision.Services {
	public class AssetFetchService {

		private readonly ApplicationDBContext _dbContext;

		public AssetFetchService(ApplicationDBContext applicationDBContext) {
			_dbContext = applicationDBContext;
		}

		public IEnumerable<Products> GetProductsByCategory(string category) {
			// Use LINQ to query the database
			var products = _dbContext.Products
				.Where(p => p.Category == category)
				.ToList();

			return products;
		}

	}
}
