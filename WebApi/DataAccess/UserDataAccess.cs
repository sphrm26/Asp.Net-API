using WebApi.Context;
using WebApi.DataModels;

namespace WebApi.DataAccess
{
    public class UserDataAccess
    {
        public static void AddUser(User user)
        {
            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static User ReadUser(string userName)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.FirstOrDefault(u => u.UserName == userName);
            }
        }
    }
}
