using System;
using System.Collections.Generic;
using System.Threading;

namespace TeppichsAttributes.Utils
{
    public static class ListHelpers
    {
        public static void Shuffle<T>(this IList<T> list)
        {   
            for (int i = list.Count - 1; 0 < i; --i)
            {
                int swap = ThreadSafeRandom.ThisThreadsRandom.Next(i + 1);
                (list[i], list[swap]) = (list[swap], list[i]);
            }
        }

        private static class ThreadSafeRandom
        {
            [ThreadStatic] private static Random Local;

            public static Random ThisThreadsRandom
            {
                get => Local ??=
                    new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId));
            }
        }
    }
}