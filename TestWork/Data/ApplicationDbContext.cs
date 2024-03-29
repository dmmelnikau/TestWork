﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestWork.Models;

namespace TestWork.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Advertisement> Advertisements { get; set; }
       
            //= @"Data Source=DESKTOP-26N6QAU;Initial Catalog=N-ADV-DB;Integrated Security=True";

    }

}
