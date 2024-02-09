using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WebApi.Services;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(Name = "signup")]
        public string Signup([FromForm] UserViewModel newUser)
        {
            if (newUser.IsUserValid())
            {
                string path = Path.Combine(@"F:\fanap-bootcamp-c#\7 - api\WebApi\WebApi\Images", newUser.UserName + "." + newUser.Image.FileName);
                try
                {
                    DataAccess.UserDataAccess.AddUser(new DataModels.User
                    {
                        UserName = newUser.UserName,
                        Password = newUser.Password,
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email,
                        Age = newUser.Age,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        ImageDirectory = path
                    });
                }
                catch
                {
                    return "An error occurred when add new user";
                }
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    newUser.Image.CopyTo(stream);
                }
                return "User successfuly added";
            }
            else
            {
                return "error";
            }
        }

        [HttpGet(Name = "getuser")]
        public object GetUser(string userName)
        {
            var user = DataAccess.UserDataAccess.ReadUser(userName);
            if (user == null)
            {
                return new
                {
                    message = "No user found!"
                };
            }

            var filePath = Path.Combine(user.ImageDirectory);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "text/html";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);


            return new
            {
                UserName = userName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
                Image = File(bytes, contentType, Path.GetFileName(filePath))
            };
        }
    }
}
