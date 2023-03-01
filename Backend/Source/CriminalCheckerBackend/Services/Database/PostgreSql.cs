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
        public DbSet<UserData> UserDataValues { get; set; } = null!;

        /// <inheritdoc />
        public async Task<bool> DoesUserDrinkerAsync(UserRequest user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            CheckUserInput(ref user);
            return await UserDataValues.AnyAsync(u =>
                u.UserName == user.Name 
                && u.Surname == user.Surname 
                && u.Patronymic == user.Patronymic 
                && u.BirthDay == DateOnly.FromDateTime(user.BirthDay)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task RegistrationNewUserAsync(SignUpDto data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            throw new NotImplementedException();
        }
        
        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().HasNoKey();
        }

        /// <summary>
        /// Checking <see cref="UserRequest"/> instance.
        /// </summary>
        /// <param name="user"><see cref="UserRequest"/>.</param>
        private void CheckUserInput(ref UserRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new NewUserNotValidValueException(nameof(user.Name), string.Empty);

            if (string.IsNullOrWhiteSpace(user.Surname))
                throw new NewUserNotValidValueException(nameof(user.Surname), string.Empty);
        }
    }
}
