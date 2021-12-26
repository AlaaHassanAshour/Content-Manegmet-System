using CMSystem.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CMSystem.Web.ViweModel;

namespace CMSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<PostCategory> PostCategorys { get; set; }
        public DbSet<EvantCategory> EvantCategorys { get; set; }
        public DbSet<EvantImage> EvantImages { get; set; }
        public DbSet<Orgnisation> Orgnisations { get; set; }
        public DbSet<CMSystem.Web.ViweModel.UserViweModel> UserViweModel { get; set; }
    }
}
