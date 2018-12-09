using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace FireAdmin.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		// Add an instance IDbSet using the 'new' keyword:
		new public virtual IDbSet<ApplicationRole> Roles { get; set; }

		public ApplicationDbContext()
				: base(nameOrConnectionString: "DefaultConnection")
		{

		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			if (modelBuilder == null)
			{
				throw new ArgumentNullException(paramName: "modelBuilder");
			}

			// Keep this:
			modelBuilder.Entity<IdentityUser>().ToTable(tableName: "AspNetUsers");

			// Change TUser to ApplicationUser everywhere else - IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
			EntityTypeConfiguration<ApplicationUser> table =
			modelBuilder.Entity<ApplicationUser>().ToTable(tableName: "AspNetUsers");

			table.Property((ApplicationUser u) => u.UserName).IsRequired();

			// EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
			modelBuilder.Entity<ApplicationUser>().HasMany((ApplicationUser u) => u.Roles);
			modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
					new { r.UserId, r.RoleId }).ToTable(tableName: "AspNetUserRoles");

			// Leave this alone:
			EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
					modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
							new { l.UserId, l.LoginProvider, l.ProviderKey }).ToTable(tableName: "AspNetUserLogins");

			entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
			EntityTypeConfiguration<IdentityUserClaim> table1 = modelBuilder.Entity<IdentityUserClaim>().ToTable(tableName: "AspNetUserClaims");
			table1.HasRequired((IdentityUserClaim u) => u.User);

			// Add this, so that IdentityRole can share a table with ApplicationRole:
			modelBuilder.Entity<IdentityRole>().ToTable(tableName: "AspNetRoles");

			// Change these from IdentityRole to ApplicationRole:
			EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 = modelBuilder.Entity<ApplicationRole>().ToTable(tableName: "AspNetRoles");
			entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();
		}
	}
}