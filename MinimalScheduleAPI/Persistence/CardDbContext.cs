using Microsoft.EntityFrameworkCore;
using MinimalScheduleAPI.Model;

namespace MinimalScheduleAPI.Persistence
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
        {
            
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(c =>
            {
                c.HasKey(de => de.IdCard);

                c.Property(de => de.SDescricao)
                    .IsRequired(false)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                c.Property(de => de.SLocal)
                    .IsRequired(false);

                c.HasMany(de => de.ListToDo)
                    .WithOne()
                    .IsRequired(false)
                    .HasForeignKey(s => s.IdCard);
            });

            modelBuilder.Entity<ToDo>(t =>
            {
                t.HasKey(de => de.IdToDo);

                t.Property(de => de.SDescricao)
                    .IsRequired(false);
            });

        }

    }
}
