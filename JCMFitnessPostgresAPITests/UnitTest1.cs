using JCMFitnessPostgresAPI.Models;
using NUnit.Framework;

namespace JCMFitnessPostgresAPITests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var user1 = new User { FirstName = "Pete" };
            var workout1 = new Workout { Name = "Running" };
            var workout2 = new Workout { Name = "Jogging" };

            var userworkout1 = new UserWorkout
            {
                User = user1,
                Workout = workout1,
                IsPublic = false
            };

            var userworkout2 = new UserWorkout
            {
                User = user1,
                Workout = workout2,
                IsPublic = true
            };



            
        }
    }
}