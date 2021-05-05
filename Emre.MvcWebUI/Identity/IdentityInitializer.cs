using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;

namespace Emre.MvcWebUI.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            //Roller
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "admin", Description = "yönetici rolü" };
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }
            //Userler

            if (!context.Users.Any(i => i.Name == "emrekyck"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "emre", Surname = "kayacık", UserName = "emrekyck", Email = "emrekayacik2634@gmail.com" };
                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }
            if (!context.Users.Any(i => i.Name == "metehan"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "mehmet", Surname = "bilgin", UserName = "mbilgin", Email = "mbilgin@hotmail.com" };
                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "user");
            }
            base.Seed(context);
        }
    }
}