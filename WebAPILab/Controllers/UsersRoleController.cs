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
            IList<UsersRoleModel> usersRoles = null;
            foreach (var role in usersRole) {
                using (var context = new GARSupport2020Entities())
                {
                    if (role.Id_Role == 1)
                    {
                        // Valida si el existe un usuario con rol supervisor 
                        usersRoles = context.User_Role
                       .Where(usersRoleItem => usersRoleItem.id_users == role.Id_Users)
                       .Select(usersRoleItem => new UsersRoleModel()
                       {
                           Id_Role = usersRoleItem.id_role
                       }
                       ).ToList<UsersRoleModel>();
                        //Si el usuario no  es un supervisor lo agrega 
                        if (usersRole.ToString().Length <= 0)
                        {
                            context.User_Role.Add(new User_Role()
                            {
                                id = role.Id,
                                id_users = role.Id_Users,
                                id_role = role.Id_Role
                            });
                            context.SaveChanges();
                        }
                    }
                    else if (role.Id_Role == 2)
                    {
                        // Valida si el existe un usuario con rol soporte 
                        usersRoles = context.User_Role
                       .Where(usersRoleItem => usersRoleItem.id_users == role.Id_Users)
                       .Select(usersRoleItem => new UsersRoleModel()
                       {
                           Id_Role = usersRoleItem.id_role
                       }
                       ).ToList<UsersRoleModel>();
                        //Si el usuario no  es un soportista lo agrega 
                        if (usersRole.ToString().Length <= 0)
                        {
                            context.User_Role.Add(new User_Role()
                            {
                                id = role.Id,
                                id_users = role.Id_Users,
                                id_role = role.Id_Role
                            });
                            context.SaveChanges();
                        }

                    } else{  // si no esta lo elimina del role 
                        var deleteRole = context.User_Role.Where(userItem => userItem.id == role.Id).FirstOrDefault();
                        context.Entry(deleteRole).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
            }
            return Ok();
        }


        public IHttpActionResult GetById(int id)
        {
            IList<UsersRoleModel> usersRoles = null;
            IList<RoleModel> roles = null;
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
