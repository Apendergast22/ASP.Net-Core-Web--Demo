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
        public async Task<bool> DoesUserDrinkerAsync(int id)
        {
            return await Drinkers.AnyAsync(u => u.UserId == id).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task RegistrationNewUserAsync(NewUserInfo data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (await RegisteredUsers.AnyAsync(u => u.Email == data.Email))
                throw new UserExistsException();

            var drinker = await Drinkers.SingleOrDefaultAsync(u =>
                u.UserName == data.Name
                && u.Surname == data.Surname
                && u.Patronymic == data.Patronymic
                && u.BirthDay == DateOnly.FromDateTime(data.BirthDay)).ConfigureAwait(false);

            if (drinker != null)
                drinker.UserId = 1;
        }
        
        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drinker>().HasIndex(drinker => drinker.UserId).IsUnique();
            modelBuilder.Entity<RegisteredUser>().HasIndex(user => user.UserId).IsUnique();
            modelBuilder.Entity<RegisteredUser>().Property(f => f.UserId).ValueGeneratedOnAdd();
        }
    }
}
