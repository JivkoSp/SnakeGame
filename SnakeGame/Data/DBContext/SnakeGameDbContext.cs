using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SnakeGame.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Data.DBContext
{
    public class SnakeGameDbContext : DbContext
    {
        private static string ConnectionString = @"Server=.; Database=SnakeGame; Trusted_Connection=True; MultipleActiveResultSets=True;";

        public SnakeGameDbContext()
        {

        }

        public DbSet<UserScore> UserScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SnakeGameDbContext.ConnectionString);
        }
    }
}
