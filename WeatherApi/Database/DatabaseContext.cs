using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Models;

namespace WeatherApi.Database;

public class DatabaseContext : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid> {
  public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder options) {
	base.OnConfiguring(options);
  }

  protected override void OnModelCreating(ModelBuilder builder) {
	base.OnModelCreating(builder);

	builder.Entity<UserModel>(e => {
	});
  }
}