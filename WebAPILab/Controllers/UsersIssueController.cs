using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPILab.Models;

namespace WebAPILab.Controllers
{
    public class UsersIssueController : ApiController
    {
        public IHttpActionResult Post(UsersIssueModel usersIssue)
        {
            using (var context = new GARSupport2020Entities())
            {
                context.Users_Issue.Add(new Users_Issue()
                {
                    id = usersIssue.Id,
                    id_users = usersIssue.Id_Users,
                    id_issue = usersIssue.Id_Users
                });
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
