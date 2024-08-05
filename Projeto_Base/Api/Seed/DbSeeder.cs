using Bogus;
using Domains.Models.Users;
using Domains.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Seed
{
    /// <summary>
    /// Db Seeder
    /// </summary>
    public class DbSeeder
    {
        private readonly ApiDbContext db;

        /// <summary>
        /// Db Seeder Constructor
        /// </summary>
        public DbSeeder(ApiDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Clean Data
        /// </summary>
        public async Task<(bool, string)> CleanData()
        {
            try
            {
                await db.Users.ExecuteDeleteAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Clean Data Tests
        /// </summary>
        public async Task<(bool, string)> CleanDataTests()
        {
            try
            {
                db.Users.RemoveRange(db.Users);

                await db.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Seed independent entities to context Db configuration
        /// </summary>
        public async Task<(bool seeded, string error)> SeedData()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            switch (environmentName)
            {
                case "Development":
                    return await ExecuteDevelopment();
                case "Tests":
                    return await ExecuteTests();
                default:
                    return (false, "Environment not found");
            }
        }

        /// <summary>
        /// Execute Seed Data in Development Environment
        /// </summary>
        public async Task<(bool seeded, string error)> ExecuteDevelopment()
        {
            var transaction = db.Database.BeginTransaction();

            (bool isCleaned, string error) = await CleanData();

            if (!isCleaned)
                return (isCleaned, error);

            try
            {
                UserSeed();

                await db.SaveChangesAsync();

                db.ChangeTracker.Clear();

                transaction.Commit();
                return (true, null);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return (false, $"{ex.Message} | {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Execute Seed Data in Tests Environment
        /// </summary>
        public async Task<(bool seeded, string error)> ExecuteTests()
        {
            (bool isCleaned, string error) = await CleanDataTests();

            if (!isCleaned)
                return (isCleaned, error);

            try
            {
                UserSeed();

                await db.SaveChangesAsync();

                db.ChangeTracker.Clear();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message} | {ex.StackTrace}");
            }
        }

        #region Seeds

        private void UserSeed()
        {
            HashService hashService = new HashService();
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string password = environmentName != "Tests" ? "zRd,L+13b6C0" : "Asdf1234";
            var (defaultPassword, defaultSalt) = hashService.Encrypt(password);

            List<User> admins = new List<User>
            {
                new User
                {
                    Name = "Administrador",
                    Email = "admin@email.com",
                    Cellphone = "11999999999",
                    FirstAccess = DateTime.Now,
                    PasswordSalt = defaultSalt,
                    PasswordHash = defaultPassword,
                },
            };

            var fakeUsers = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Cellphone, f => f.Person.Phone)
                .RuleFor(u => u.Profile, Domains.Enums.Profile.Common)
                .Generate(50);

            db.Users.AddRange(admins);
            db.Users.AddRange(fakeUsers);
        }
        #endregion
    }
}
