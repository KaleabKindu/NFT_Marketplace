using Domain;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.UnitTest.Mocks
{
    public class MockUserManager
    {
        public static  Mock<UserManager<AppUser>> GetUserManager()
        {
            var Users = new List<AppUser> {
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "myUserName1",
                    Email = "email1@gmail.com",
                    Password = "password",
                    FullName  = "this is may name "
                },
                new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "myUserName2",
                    Email = "email2@gmail.com",
                    Password = "password",
                    FullName  = "this is may name "
                }
            };
                
            var store = new Mock<IUserStore<AppUser>>();
            var mgr = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<AppUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser, string>((x, y) => Users.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
            // var userManagerMock = new Mock<UserManager<AppUser>>();
            // var user = new AppUser();
            // userManagerMock.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // userManagerMock.Setup(u => u.Users).Returns(Users.AsQueryable<AppUser>);

            // return userManagerMock;

        }
    }
}
