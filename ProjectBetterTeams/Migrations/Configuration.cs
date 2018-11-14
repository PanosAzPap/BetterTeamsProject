namespace ProjectBetterTeams.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectBetterTeams.TeamsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectBetterTeams.TeamsContext context)
        {
            UserSignUp pass = new UserSignUp();
            Users user = new Users()
            {
                Username = "Admin",
                Password = pass.EncryptPassword("Admin"),
                FirstName = "Panagiotis",
                LastName = "Papadopoulos",
                DateOFBirth = DateTime.Parse("1992/02/28").Date,
                UserType = "SuperAdmin"
            };
            using (var db = new TeamsContext())
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
            }
        }
    }
}
