﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.DataBase
{
    public class AppDbContext : DbContext
    {
        private readonly string _databasePath = $"{AppContext.BaseDirectory}/DataBase/database.db";
        public AppDbContext()
        {
            SQLitePCL.Batteries.Init();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=database.db");
            //base.OnConfiguring(optionsBuilder);
        }
    }
}
