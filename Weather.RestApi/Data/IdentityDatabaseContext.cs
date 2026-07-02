using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Weather.RestApi.Data;

public class IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
	: IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options) {
	protected override void OnConfiguring(DbContextOptionsBuilder options) {
		base.OnConfiguring(options);
	}

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);
	}
}