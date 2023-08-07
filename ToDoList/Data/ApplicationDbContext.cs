using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ToDoList.Models.TaskList;

namespace ToDoList.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<TaskList> TaskLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TaskList>().HasData(
                new TaskList { Id=1,Name="Gym",Description="I have to go to the Gym",Category=Catgories.LifeStyle.ToString(),CreatedDate=DateTime.Now}
                
                );
        }
    }
}
