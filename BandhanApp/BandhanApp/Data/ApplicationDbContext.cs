using BandhanApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BandhanApp.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        { }

        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasKey(sc => new { sc.SenderId, sc.ReceiverId });
            modelBuilder.Entity<Message>()
                .HasOne(s=>s.Sender)
                .WithMany(m=>m.SentMessages)
                .HasForeignKey(k=>k.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
               .HasOne(s => s.Receiver)
               .WithMany(m => m.ReceiveMessages)
               .HasForeignKey(k => k.ReceiverId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            base.OnModelCreating(modelBuilder);
        }


    }
}
