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
                    id_issue = usersIssue.Id_Issue
                });
                context.SaveChanges();
            }
            return Ok();
        }

        public IHttpActionResult GetAll()
        {
            IList<UsersIssueModel> issues= null;

            using (var context = new GARSupport2020Entities())
            {
                issues = context.Users_Issue
                .Select(issueItem => new UsersIssueModel()
                {
                    Id = issueItem.id,
                    Id_Users = issueItem.id_users,
                    Id_Issue = issueItem.id_issue
                   
                }).ToList<UsersIssueModel>();

            }
            if (issues.Count == 0)
            {
                return NotFound();
            }
            return Ok(issues);
        }


        public IHttpActionResult GetById(int id)
        {
            IList<IssueModel> issue = null;
            IList<UsersIssueModel> usersIssue = null;
            using (var context = new GARSupport2020Entities())
            {
                usersIssue = context.Users_Issue
                    .Where(usersIssueItem => usersIssueItem.id_users == id)
                    .Select(usersIssueItem => new UsersIssueModel()
                    {
                        
                        Id_Issue = usersIssueItem.id_issue
                    }
                ).ToList<UsersIssueModel>();
            }

            using (var context = new GARSupport2020Entities())
            {
                foreach (var id_issue in usersIssue)
                {
                    issue = context.Issue
                   .Where(issueItem => issueItem.id == id_issue.Id_Issue)
                  .Select(issueItem => new IssueModel()
                  {
                      Id = issueItem.id,
               Id_Client = issueItem.id_client,
               Description = issueItem.description,
               Time_Stamp = issueItem.time_stamp,
               Contact_Phone = issueItem.contact_phone,
               Contact_Email = issueItem.contact_email,
               Classification = issueItem.classification,
               Status = issueItem.status,
               Service_Type = issueItem.service_type,
               Name = issueItem.name,
               First_Name = issueItem.first_name,
               Second_Name = issueItem.second_name,
               Address = issueItem.address,
               Phone = issueItem.phone,
               Second_contact = issueItem.second_contact,
               Email = issueItem.email
                  }).ToList<IssueModel>(); 
    }
            }
            if (issue== null)
            {
                return NotFound();
            }
            return Ok(issue);
        }


    }
}
