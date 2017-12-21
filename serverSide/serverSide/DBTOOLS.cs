using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Xml.Linq;

namespace serverSide
{
    [WebService(Namespace = "http://localhost/serverSide")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    class DBTOOLS : System.Web.Services.WebService
    {
        [WebMethod]
        public bool AddUser(string email, string firstname, string lastname)
        {
            try
            {
                using (APP_DBEntities2 db = new APP_DBEntities2())
                {
                    user user = new user
                    {
                        email = email,
                        firstname = firstname,
                        lastname = lastname
                    };
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
        [WebMethod]
        public user FindByMail (string email)
        {
            try
            {
                using (APP_DBEntities2 db = new APP_DBEntities2())
                {
                    var user = db.users
                    .Where(b => b.email.Equals(email))
                    .FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {
                return(null);
            }
        }
    }
}
