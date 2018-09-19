using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models{
    public class TodoContext : DbContext{
        public DbSet<Note> Notes {get; set;}
        
        public TodoContext(DbContextOptions<TodoContext> options):base(options){

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Filename=./Notes.db");
    }
}