using Microsoft.EntityFrameworkCore;
using biovision.Models;

namespace biovision.Data {
	public class ApplicationDBContext : DbContext {
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {
		}

		public DbSet<Products> Products { get; set; }
	}
}
