

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (ApplicationContext db = new ApplicationContext()) {
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();

            //    var company1 = new Company { Name = "Microsoft" };
            //    var company2 = new Company { Name = "Pepsi" };

            //    var user =new User { Name ="Mira",Age=17,Company=company1};
            //    var user2 = new User { Name = "Jo", Age = 71, Company = company2 };
            //    var user3 = new User { Name = "Polina", Age = 66, Company = company2 };

            //    db.Users.AddRange(user,user2,user3);
            //    db.SaveChanges();

            //}

            using (ApplicationContext db = new ApplicationContext())
            {
                //db.Database.ExecuteSqlRaw("""
                //   CREATE PROCEDURE GetUsersAndCompanies
                //   AS
                //   BEGIN
                //   SELECT u.Id as UserId,u.Name as UserName,u.Age as UserAge,u.CompanyId as CompanyId,c.Name as CompanyName
                //   From Users u
                //   INNER JOIN Companies c ON u.CompanyId=c.Id;
                //   END;
                //   """);

                //var allUsers=db.UsersWithCompany.FromSqlRaw("EXECUTE GetUsersAndCompanies").ToList();
            }


            using (ApplicationContext db = new ApplicationContext())
            {
                //db.Database.ExecuteSqlRaw("""
                //   CREATE PROCEDURE GetUsersByName
                //   @Name NVARCHAR(100)
                //   AS
                //   BEGIN
                //   SELECT *
                //   From Users 
                //   Where Name like '%'+@Name+'%';
                //   END;
                //   """);
                //SqlParameter nameParam = new SqlParameter("@Name", "Jo");
                //var allUsersByName = db.Users.FromSqlRaw("EXECUTE GetUsersByName @Name", nameParam).ToList();
            }


            using (ApplicationContext db = new ApplicationContext())
            {
                //db.Database.ExecuteSqlRaw("""
                //   CREATE PROCEDURE GetAverageUserAge
                //   @AverageAge int OUTPUT
                //   AS
                //   BEGIN
                //   SELECT @AverageAge=AVG(Age)
                //   FROM Users;
                //   END;
                //   """);
                //var ageParam = new SqlParameter("@AverageAge", System.Data.SqlDbType.Int)
                //{
                //    Direction = System.Data.ParameterDirection.Output
                //};

                //db.Database.ExecuteSqlRaw("EXECUTE GetAverageUserAge @AverageAge OUTPUT ", ageParam);
                //Console.WriteLine(ageParam.Value);
            }
        }
    }

    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company>Companies { get; set; }
        public DbSet<UserWithCompanyViewModel> UsersWithCompany { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=shop;Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWithCompanyViewModel>().HasNoKey();
            base.OnModelCreating(modelBuilder); 
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age {  get; set; }
        public int CompanyId {  get; set; }
        public Company Company { get; set; }
    }

    public class Company
    {
        public int Id {  set; get; }
        public string Name { set; get; }

        public ICollection<User> Users { get; set; }
    }

    public class UserWithCompanyViewModel
    {
        public int UserId { set; get; }
        public string UserName { set; get; }
        public int UserAge {  set; get; }
        public string CompanyName {  set; get; }
        public int CompanyId { set; get; }
    }
}
