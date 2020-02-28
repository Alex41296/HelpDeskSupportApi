using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPILab.Models;

namespace WebAPILab.Controllers
{
    public class UsersRoleController : ApiController
    {
        
        public IHttpActionResult Post(IList<UsersRoleModel> usersRole)
        {
            
            int count = 0; 
            IList<UsersRoleModel> newUsersRoles = null;
            IList<UsersRoleModel> support = null;
            using (var context = new GARSupport2020Entities())
            {
                foreach (var role in usersRole)
                {


                    if (count == 0)
                    {
                        //verifica si ya existe 
                        newUsersRoles = context.User_Role
                       .Where(usersRoleItem => usersRoleItem.id_users == role.Id_Users)
                       .Select(usersRoleItem => new UsersRoleModel()
                       {
                           Id_Role = usersRoleItem.id_role,
                           Id_Users = usersRoleItem.id_users
                       }
                       ).ToList<UsersRoleModel>();
                        //Si existe lo borra
                     

                            if (newUsersRoles != null)
                            {
                            foreach (var delete in newUsersRoles)
                            {
                                var deleteRole = context.User_Role.Where(userItem => userItem.id_users == delete.Id_Users).FirstOrDefault();
                                context.Entry(deleteRole).State = System.Data.Entity.EntityState.Deleted;
                                context.SaveChanges();
                            }
                            }
                        
                        
                    }

                    count++;

                    context.User_Role.Add(new User_Role()
                    {
                        id = role.Id,
                        id_users = role.Id_Users,
                        id_role = role.Id_Role
                    });
                    context.SaveChanges();

                }

                foreach (var id_user in usersRole)
                {

                    support = context.User_Role
                    .Where(usersRoleItem => usersRoleItem.id_users == id_user.Id_Users)
                    .Select(usersRoleItem => new UsersRoleModel()
                    {
                        Id_Users = usersRoleItem.id_users,
                        Id_Role = usersRoleItem.id_role
                       
                    }).ToList<UsersRoleModel>();
                }
                //Si el valor es nulo 
                if (support == null)
                {
                    foreach (var delete in usersRole)
                    {
                        ///No es sopporte?
                      
                            // Verifica si en la tabla userIssue ya tiene un issue asignado
                            var userDeleted = context.Users_Issue
                       .Where(usersIssueItem => usersIssueItem.id_users == delete.Id_Users)
                       .Select(usersIssueItem => new UsersIssueModel()
                       {
                           Id_Users = usersIssueItem.id_users,

                       }).FirstOrDefault();

                            // Si existe entra 
                            if (userDeleted != null)
                            {
                                var deleteUser = context.Users_Issue.Where(userItem => userItem.id_users == delete.Id_Users).FirstOrDefault();
                                context.Entry(deleteUser).State = System.Data.Entity.EntityState.Deleted;
                                context.SaveChanges();
                            }
                        
                    }

                }
                else
                {
                    foreach (var role in support)
                    {
                        ///No es sopporte?
                        if (role.Id_Role != 2)
                        {
                            // Verifica si en la tabla userIssue ya tiene un issue asignado
                            var issueUser = context.Users_Issue
                       .Where(usersIssueItem => usersIssueItem.id_users == role.Id_Users)
                       .Select(usersIssueItem => new UsersIssueModel()
                       {
                           Id_Users = usersIssueItem.id_users,

                       }).FirstOrDefault();

                            // Si existe entra 
                            if (issueUser != null)
                            {
                                var deleteUser = context.Users_Issue.Where(userItem => userItem.id_users == role.Id_Users).FirstOrDefault();
                                context.Entry(deleteUser).State = System.Data.Entity.EntityState.Deleted;
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }

            return Ok();
        }

        [Route("api/usersrole/insert/{id}")]
        public IHttpActionResult GetById(int id)
        {
            IList<UsersRoleModel> usersRoles = null;
            using (var context = new GARSupport2020Entities())
            {
                usersRoles = context.User_Role
                    .Where(usersRole => usersRole.id_users == id)
                    .Select(usersRole => new UsersRoleModel()
                    {
                       Id  = usersRole.id,
                       Id_Role = usersRole.id_role,
                       Id_Users = usersRole.id_users

                    }
                ).ToList<UsersRoleModel>();
                }
            if (usersRoles == null)
            {
                return NotFound();
            }
            return Ok(usersRoles);
        }

    }
}
