﻿using BooksHub.Models;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BooksHub.Extensions
{
    public static class DbContextExtension
    {
        public static void UpdateManyToMany<T, KEY>(this BookDbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, KEY> getKey) where T : class
        {
            if (currentItems != null)
            {
                db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
                db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
            }
            else
            {
                db.Set<T>().AddRange(newItems);
            }
        }


        public static IEnumerable<T> Except<T, KEY>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, KEY> getKeyFunc)
        {
            return items.GroupJoin(other, getKeyFunc, getKeyFunc, (item, templateItems) => new { item, templateItems })
             .SelectMany(t => t.templateItems.DefaultIfEmpty(), (t, tmp) => new { t, tmp })
             .Where(t => (ReferenceEquals(null, t.tmp) || t.tmp.Equals(default(T)))) //filtr
             .Select(t => t.t.item);
        }

    }
}
