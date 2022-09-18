using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Device.Location;
using System.Security.Cryptography;

namespace LinqLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(File.ReadAllText("../../../data.json"));

            #region FirstTask

            var northernmost = persons.MaxBy(p => p.Latitude);
            var southernmost = persons.MinBy(p => p.Latitude);
            var easternmost = persons.MaxBy(p => p.Longitude);
            var westernmost = persons.MinBy(p => p.Longitude);
            Console.WriteLine("The name of the northernmost one: " + northernmost.Name);
            Console.WriteLine("The name of the southernmost one: " + southernmost.Name);
            Console.WriteLine("The name of the easternmost one: " + easternmost.Name);
            Console.WriteLine("The name of the westernmost one: " + westernmost.Name);

            #endregion

            #region SecondTask

            var coupleMax = persons.SelectMany
                (p1 => persons.Select
                (p2 => new
                {
                    Person1 = p1, Person2 = p2, distance = GetDistance(p1, p2)
                }))
                .Where(g => g.Person1 != g.Person2)
                .MaxBy(g => g.distance);
                
            
            var coupleMin = persons.SelectMany
                (p1 => persons.Select
                (p2 => new
                {
                    Person1 = p1, Person2 = p2, distance = GetDistance(p1, p2)
                }))
                .Where(g => g.Person1 != g.Person2)
                .MinBy(g => g.distance);
                

            Console.WriteLine("Couple with max distance: " + coupleMax.Person1.Name + " - " + coupleMax.Person2.Name);
            Console.WriteLine("Couple with min distance: " + coupleMin.Person1.Name + " - " + coupleMin.Person2.Name);

            #endregion

            #region ThirdTask

            var coupleWithSameWords = persons.SelectMany
                (p1 => persons.Select
                (p2 => new
                {
                    Person1 = p1, Person2 = p2, Same = p1.About.Split(' ').Intersect(p2.About.Split(' ')).Count()
                }))
                .Where(g => g.Person1 != g.Person2)
                .MaxBy(g => g.Same);

            Console.WriteLine("Couple with the the most count of same words in about: " +
                              $"{coupleWithSameWords.Person1.Name} - {coupleWithSameWords.Person2.Name}");

            #endregion

            #region FourthTask

            var friends = persons.SelectMany(person => person.Friends, (person, friend) => new
                {
                    Person = person.Name, Friend = friend.Name
                })
                .GroupBy(p => p.Friend)
                .Where(p => p.Count() > 1)
                .Select(p => new
                {
                    Friend = p.Key, People = p.Select(p => p.Person).ToList()
                });

            foreach (var friend in friends)
            {
                Console.WriteLine($"Friends with {friend.Friend}: ");
                foreach (var person in friend.People)
                {
                    Console.WriteLine(person);
                }

                Console.WriteLine();
            }

            #endregion
            
        }

        static double GetDistance(Person person1, Person person2)
        {
            var geo1 = new GeoCoordinate(person1.Latitude, person1.Longitude);
            var geo2 = new GeoCoordinate(person2.Latitude, person2.Longitude);
            return geo1.GetDistanceTo(geo2);
        }
    }
}
//checked
