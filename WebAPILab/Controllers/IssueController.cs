using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPILab.Models;

namespace WebAPILab.Controllers
{
    public class IssueController: ApiController
    {

        public IHttpActionResult Post(IssueModel issue)
        {
            using (var context = new GARSupport2020Entities())
            {
                context.Issue.Add(new Issue()
                {
                    id = issue.Id,
                    id_client = issue.Id_Client,
                    description = issue.Description,
                    time_stamp = issue.Time_Stamp,
                    contact_phone = issue.Contact_Phone,
                    contact_email = issue.Contact_Email,
                    classification = issue.Classification,
                    status = issue.Status,
                    service_type = issue.Service_Type,
                    name  = issue.Name,
                    second_name = issue.Second_Name,
                    first_name = issue.First_Name,
                    address = issue.Address,
                    phone = issue.Phone,
                    second_contact = issue.Second_contact,
                    email = issue.Email
                });
                context.SaveChanges();
            }
            return Ok();
        }


        public IHttpActionResult GetById(int id)
        {
            IList<IssueModel> issue = null;
            using (var context = new GARSupport2020Entities())
            {
                issue = context.Issue
                    .Where(issueItem => issueItem.id_client == id)
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
                    }
                ).ToList<IssueModel>();


            }
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }
    }
}