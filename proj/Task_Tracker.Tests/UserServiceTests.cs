using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_Tracker.Logic;

namespace Task_Tracker.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService userService;

        [TestInitialize]
        public void Setup()
        {
            userService = new UserService();
        }

        [TestMethod]
        public void Register_NewUser_ReturnsTrue()
        {
            var result = userService.Register("Zaman Ali", "zaman123");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Register_ExistingUser_ReturnsFalse()
        {
            userService.Register("Zaman Ali", "zaman123");
            var result = userService.Register("Zaman Ali", "zaman12345");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsTrue()
        {
            userService.Register("Zaman Ali", "zaman123");
            var result = userService.Login("Zaman Ali", "zaman123");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsFalse()
        {
            userService.Register("Zaman Ali", "zaman123");
            var result = userService.Login("Zaman Ali", "zaman12345");
            Assert.IsFalse(result);
        }
    }
}
