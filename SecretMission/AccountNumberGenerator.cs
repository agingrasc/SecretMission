using System;

namespace SecretMission
{
    public interface IAccountNumberGenerator
    {
        int Generate();
    }

    public class RandomAccountNumberGenerator : IAccountNumberGenerator
    {
        private const int LowerBound = 1;
        private const int UpperBound = 10000;

        public int Generate()
        {
            var randomGenerator = new Random();
            return randomGenerator.Next(LowerBound, UpperBound);
        }
    }
}