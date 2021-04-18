using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Foo", Location = "Indiana", Cuisine = CuisineType.Indian },
                new Restaurant { Id = 3, Name = "Bar", Location = "Arizona", Cuisine = CuisineType.Mexican },
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id) => restaurants.SingleOrDefault(r => r.Id == id);

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant is not null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant is not null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int Commit() => 0;
    }
}