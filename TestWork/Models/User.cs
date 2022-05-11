using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestWork.Models
{
    public class User:IdentityUser
    {
    }
    public enum Roles
    {
        Admin,
        User,
        Manager
    }
}
