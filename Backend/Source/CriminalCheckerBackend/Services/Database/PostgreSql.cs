using CriminalCheckerBackend.Model;
using CriminalCheckerBackend.Model.DataBase;
using CriminalCheckerBackend.Model.DataBase.Exceptions;
using CriminalCheckerBackend.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace CriminalCheckerBackend.Services.Database
{
    /// <summary>
    /// Service to interaction with PostgreSQL database server.
    /// </summary>
    public class PostgreSql : DbContext, IDataBase
    {

        /// <summary>
        /// Instancing new object of <see cref="PostgreSql"/>.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        public PostgreSql(DbContextOptions options) : base(options)
        {
        }
        
        /// <inheritdoc />
        public DbSet<Drinker> Drinkers { get; set; } = null!;

        /// <inheritdoc />
        public DbSet<RegisteredUser> RegisteredUsers { get; set; } = null!;

        /// <inheritdoc />
        public void RecreateDataBase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <inheritdoc />
        public async Task<bool> DoesUserDrinkerAsync(DrinkerDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            return await Drinkers.AnyAsync(u =>
                u.UserName == user.Name
                && u.Surname == user.Surname
                && u.Patronymic == user.Patronymic
                && u.BirthDay == DateOnly.FromDateTime(user.BirthDay)).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public async Task AddNewDrinkerAsync(string name, string surname, string patronymic, DateOnly birthDay)
        {
            try
            {
                var registeredUser = await RegisteredUsers.SingleOrDefaultAsync(user =>
                    user.Name == name && user.Surname == surname && user.Patronymic == patronymic &&
                    user.BirthDay == birthDay).ConfigureAwait(false);

                Drinkers.Add(new Drinker(registeredUser?.UserId ?? - 1, name, name, name, birthDay));
                await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task RegistrationNewUserAsync(NewUserInfo data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (await RegisteredUsers.AnyAsync(u => u.Email == data.Email))
                throw new UserExistsException();

            var user = await RegisteredUsers.AddAsync(new RegisteredUser(data)).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            
            var drinker = await Drinkers.SingleOrDefaultAsync(u =>
                u.UserName == data.Name
                && u.Surname == data.Surname
                && u.Patronymic == data.Patronymic
                && u.BirthDay == DateOnly.FromDateTime(data.BirthDay)).ConfigureAwait(false);

            if (drinker == null)
                return;

            drinker.UserId = user.Entity.UserId;
            await SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drinker>().HasIndex(user => user.Index).IsUnique();
            modelBuilder.Entity<Drinker>().Property(f => f.Index).ValueGeneratedOnAdd();

            modelBuilder.Entity<Drinker>().Property(f => f.UserId).IsRequired();
            modelBuilder.Entity<Drinker>().Property(f => f.UserName).IsRequired();
            modelBuilder.Entity<Drinker>().Property(f => f.Patronymic).IsRequired();
            modelBuilder.Entity<Drinker>().Property(f => f.Surname).IsRequired();
            modelBuilder.Entity<Drinker>().Property(f => f.BirthDay).IsRequired();

            modelBuilder.Entity<RegisteredUser>().HasIndex(user => user.UserId).IsUnique();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.UserId).ValueGeneratedOnAdd();

            modelBuilder.Entity<RegisteredUser>().Property(f => f.Address).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.City).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.BirthDay).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.Email).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.Hash).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.Name).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.Patronymic).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.SaltPosition).IsRequired();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.Surname).IsRequired();
        }
    }
}
