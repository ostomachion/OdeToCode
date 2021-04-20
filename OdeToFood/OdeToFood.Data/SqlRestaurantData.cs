using System;
using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit() => db.SaveChanges();

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant is not null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id) => db.Restaurants.Find(id);

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return from r in db.Restaurants
                where r.Name.StartsWith(name) || String.IsNullOrEmpty(name)
                orderby r.Name
                select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }

        public int GetCountOfRestaurants() => db.Restaurants.Count();
    }
}