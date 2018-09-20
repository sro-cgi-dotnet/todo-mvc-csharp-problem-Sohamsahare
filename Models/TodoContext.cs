using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models{
    public class TodoContext : DbContext{
        public DbSet<Note> Notes {get; set;}
        public DbSet<CheckListItem> CheckLists {get; set;}
        
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Filename=./Notes.db");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=NoteDB3;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Note>().HasMany(n => n.CheckList).WithOne().HasForeignKey(c => c.NoteId);
        }    
    }
}