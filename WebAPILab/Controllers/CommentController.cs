using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPILab.Models;

namespace WebAPILab.Controllers
{
    public class CommentController : ApiController
    {

        public IHttpActionResult Post(CommentModel comment)
        {
            using (var context = new GARSupport2020Entities())
            {
                context.Comment.Add(new Comment()
                {
                    id = comment.Id,
                    id_issue = comment.Id_Issue,
                    name = comment.Name,
                    description = comment.Description,
                    time_stamp = comment.Time_Stamp

                });
                context.SaveChanges();
            }
            return Ok(comment);
        }


        public IHttpActionResult GetById(int id)
        {
            IList<CommentModel> comment = null;
            using (var context = new GARSupport2020Entities())
            {
                comment = context.Comment
                    .Where(commentItem => commentItem.id_issue == id)
                    .Select(commentItem => new CommentModel()
                    {
                        Id = commentItem.id,
                        Id_Issue = commentItem.id_issue,
                        Name = commentItem.name,
                        Description = commentItem.description,
                        Time_Stamp = commentItem.time_stamp
                    }
                ).ToList<CommentModel>();


            }
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
    }
}
