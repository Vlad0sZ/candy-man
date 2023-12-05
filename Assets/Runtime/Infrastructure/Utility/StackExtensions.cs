using System.Collections.Generic;
using System.Linq;
using Runtime.Infrastructure.RandomCore.Impl;
using Runtime.Infrastructure.RandomCore.Interfaces;

namespace Runtime.Infrastructure.Utility
{
    public static class StackExtensions
    {
        public static Stack<T> ToShuffleStack<T>(this IEnumerable<T> list, IRandom random)
        {
            List<T> listCopy = list.ToList();
            int n = listCopy.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(0, n + 1);
                (listCopy[k], listCopy[n]) = (listCopy[n], listCopy[k]);
            }

            return new Stack<T>(listCopy);
        }

        public static Stack<T> ToShuffleStack<T>(this IEnumerable<T> list) => 
            ToShuffleStack(list, new UnityRandom());

        public static List<T> ToShuffleList<T>(this IEnumerable<T> list, IRandom random) => 
            list.ToShuffleStack(random).ToList();
    }
}