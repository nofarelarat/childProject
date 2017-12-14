using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
