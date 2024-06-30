using MajorTestTask.DataBase;
using MajorTestTask.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.Helpers
{
    public static class DataBaseHelper
    {
        public static async Task<List<T>> GetItemsAsync<T>() where T : class, IEntityBase, new()
        {
            using var db = new AppDbContext();
            var dbSet = GetDbSet<T>(db, typeof(T).Name);
            if (dbSet is not null)
            {
                return await dbSet.OrderBy(x => x.Name).ToListAsync();
            }
            return new List<T>();
        }
        public static async Task<bool> AddItemAsync<T>(T item) where T : class, IEntityBase, new()
        {
            using var db = new AppDbContext();
            var dbSet = GetDbSet<T>(db, typeof(T).Name);
            if (dbSet is not null)
            {
                await dbSet.AddAsync(item);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public static async Task<bool> UpdateItemAsync<T>(T item) where T : class, IEntityBase, new()
        {
            using var db = new AppDbContext();
            var dbSet = GetDbSet<T>(db, typeof(T).Name);
            if (dbSet is not null)
            {
                dbSet.Update(item);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public static async Task<bool> DeleteItemAsync<T>(Guid id) where T : class, IEntityBase, new()
        {
            using var db = new AppDbContext();
            var dbSet = GetDbSet<T>(db, typeof(T).Name);
            if (dbSet is not null)
            {
                var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
                dbSet.Remove(entity!);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private static DbSet<T>? GetDbSet<T>(AppDbContext db, string typeName) where T : class, IEntityBase, new()
        {
            var dbSet = db.GetType().GetProperties()
                .FirstOrDefault(x => x.PropertyType.IsGenericType
                && x.PropertyType.GenericTypeArguments!.Any(tp => tp.Name == typeName));
            return (DbSet<T>?)dbSet?.GetValue(db, null);
        }
    }
}