using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleNetCoreCookies.Tests
{
    [TestClass]
    public class RandomNumberTests
    {
        [TestMethod]
        public void CreateRandomGenerator_and_RunTests()
        {
            RandomGenerator generator = new RandomGenerator();
            int rand = generator.RandomNumber(5, 100);
            Console.WriteLine($"Random number between 5 and 100 is {rand}");

            string str = generator.RandomString(10, false);
            Console.WriteLine($"Random string of 10 chars is {str}");

            string pass = generator.RandomPassword();
            Console.WriteLine($"Random string of 6 chars is {pass}");

        }
    }
}
