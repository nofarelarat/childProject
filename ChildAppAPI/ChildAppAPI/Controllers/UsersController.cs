using System;
using System.Linq;
using System.Web.Http;

namespace ChildAppAPI.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public user getUser([FromUri]string email)
        {
            try
            {
                using (APP_DBEntities db = new APP_DBEntities())
                {
                    var user = db.users
                    .Where(b => b.email.Equals(email))
                    .FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        [HttpDelete]
        public bool deleteUser([FromUri] string email)
        {
            try
            {
                using (APP_DBEntities db = new APP_DBEntities())
                {
                    var user = db.users
                    .Where(b => b.email.Equals(email))
                    .FirstOrDefault();
                    db.users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool createUser([FromBody] user user)
        {
            try
            {
                using (APP_DBEntities db = new APP_DBEntities())
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public user[] getTeacherUsers([FromUri]string garden)
        {
            try
            {
                using (APP_DBEntities db = new APP_DBEntities())
                {
                    var users = db.users
                    .Where(b => b.gardenname.Equals(garden));
                    user[] res = users.ToArray();
                    return res;
                }
            }
            catch (Exception ex)
            {
                return (null);
            }
        }

        [HttpPatch]
        public bool UpdateUser([FromBody] Counters counter)
        {   
            try
            {
                int count = counter.countUpdate;
                string countall = counter.countYearUpdate;
                string email = counter.email;
                using (APP_DBEntities db = new APP_DBEntities())
                {
                    var userdb = db.users.FirstOrDefault(u => u.email == email);
                    userdb.count_month = count;
                    userdb.count_year = countall;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
