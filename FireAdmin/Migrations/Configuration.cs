using FireAdmin.Models;
using System.Data.Entity.Migrations;

namespace FireAdmin.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


        protected override void Seed(ApplicationDbContext context)
        {
            this.AddUserAndRoles();
        }


        bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("OPCS", "God-level Access");
            if (!success == true) return success;

            success = idManager.CreateRole("Admin", "Global Access");
            if (!success == true) return success;

            success = idManager.CreateRole("Manager", "Edit existing records");
            if (!success == true) return success;

            success = idManager.CreateRole("User", "Restricted to business domain activity");
            if (!success) return success;


            var newUser = new ApplicationUser()
            {
                UserName = "OPCS",
                FirstName = "Steve",
                LastName = "Humby",
                Email = "steve@opcsdesign.co.uk"
            };

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            success = idManager.CreateUser(newUser, "Letmein2:BACKDOOR");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "OPCS");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Manager");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "User");
            if (!success) return success;

            return success;
        }
    }
}
