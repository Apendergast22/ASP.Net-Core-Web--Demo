using CriminalCheckerBackend.Model.Exceptions;
using CriminalCheckerBackend.Services.Password;

namespace CriminalCheckerBackend.Tests.Services
{
    using NUnit.Framework;

    public class PasswordServiceTest
    {
        private const string GoodPassword = "qwerty123";
        private static readonly string PathToTestFolder =
            Path.Combine(
                Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) ??
                throw new ArgumentNullException(), "TestItems");

        private static readonly IPassword PasswordService = new PasswordService(Path.Combine(PathToTestFolder, "TestSalt.txt"), 80);

        [Test]
        [Description("Checking constructor arguments")]
        public void BadConstructorArgumentsThrowTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PasswordService(string.Empty, 100));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PasswordService(Path.GetRandomFileName(), 10));
            
            var badPathToSaltService = new PasswordService(Path.GetRandomFileName(), 100);
            Assert.Throws<FileNotFoundException>(() => badPathToSaltService.Hash(GoodPassword));
        }

        [Test]
        [Description("Checking methods arguments")]
        public void BadPasswordThrowTest()
        {
            Assert.Throws<BadPasswordException>(() => PasswordService.Hash(null));
            Assert.Throws<BadPasswordException>(() => PasswordService.Hash(string.Empty));

            var passHash = new byte[] { 1, 2 };
            Assert.Throws<BadPasswordException>(() => PasswordService.VerifyPassword(null, passHash));
            Assert.Throws<BadPasswordException>(() => PasswordService.VerifyPassword(string.Empty, passHash));

            Assert.Throws<ArgumentNullException>(() => PasswordService.VerifyPassword(GoodPassword, null));
            Assert.Throws<ArgumentNullException>(() => PasswordService.VerifyPassword(GoodPassword, Array.Empty<byte>()));
        }

        [Test]
        [Description("Testing verify passwords")]
        public void CheckingVerifyPasswordTest()
        {
            var allPasswords = new List<string>();
            for (var i = 0; i < 10; i++)
                allPasswords.Add(Path.GetRandomFileName());
            
            var passwords = allPasswords
                .Distinct()
                .ToDictionary(password => password, _ => new List<byte[]>(10));

            foreach (var password in passwords.Keys)
                passwords[password] = passwords[password].Select(_ => PasswordService.Hash(password)).ToList();

            foreach (var password in passwords.Keys)
                passwords[password].Select(hash => PasswordService.VerifyPassword(password, hash)).ToList().ForEach(Assert.True);
        }

        [Test]
        [Description("Testing bad passwords")]
        public void CheckingBadPasswordTest()
        {
            var allPasswords = new List<string>();
            for (var i = 0; i < 10; i++)
                allPasswords.Add(Path.GetRandomFileName());
            
            var passwords = allPasswords
                .Distinct()
                .ToDictionary(password => password, _ => new List<byte[]>(10));

            foreach (var password in passwords.Keys)
                passwords[password] = passwords[password].Select(_ => PasswordService.Hash(password)).ToList();

            var badPasswords = passwords.Keys.ToDictionary(key => key, key => key + Path.GetRandomFileName());
            foreach (var password in passwords.Keys)
                passwords[password].Select(hash => PasswordService.VerifyPassword(badPasswords[password], hash)).ToList().ForEach(Assert.False);
        }
    }
}