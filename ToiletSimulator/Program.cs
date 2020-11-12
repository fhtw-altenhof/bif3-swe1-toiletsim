using System;
using ToiletSimulator.Consumer;
using ToiletSimulator.Producer;
using ToiletSimulator.Queues;

namespace ToiletSimulator {
    class Program {
        static void Main(string[] args) {

            SimpleQueue queue = new SimpleQueue();
            // BetterQueue queue = new BetterQueue();
            // ConcurrentToiletQueue queue = new ConcurrentToiletQueue();

            int randomSeed = new Random().Next();
            Random random = new Random(randomSeed);

            PeopleGenerator[] producers = new PeopleGenerator[Parameters.Producers];
            for (int i = 0; i < producers.Length; i++) {
                producers[i] = new PeopleGenerator("People Generator " + i, queue, random);
            }

            Toilet[] consumers = new Toilet[Parameters.Consumers];
            for (int i = 0; i < consumers.Length; i++) {
                consumers[i] = new Toilet("Toilet " + i, queue);
            }

            Statistics.Reset();
            for (int i = 0; i < producers.Length; i++) {
                producers[i].Produce();
            }
            for (int i = 0; i < consumers.Length; i++) {
                consumers[i].Consume();
            }

            for (int i = 0; i < consumers.Length; i++) {
                consumers[i].Join();
            }

            Statistics.Display();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
