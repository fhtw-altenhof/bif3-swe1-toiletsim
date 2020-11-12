using System;
using System.Threading;
using ToiletSimulator.Interfaces;
using ToiletSimulator.Jobs;

namespace ToiletSimulator.Producer {
    public class PeopleGenerator : IProducer {

        private int idSeed;
        private Random random;
        private int randomThreadRun;

        public string Name { get; private set; }
        public IToiletQueue Queue { get; private set; }

        private PeopleGenerator() { }

        public PeopleGenerator(string name, IToiletQueue queue, Random random) : this() {
            idSeed = 0;
            this.random = random;
            this.Name = name;
            this.Queue = queue;
            randomThreadRun = random.Next(Parameters.MinRandomValue, Parameters.MaxRandomValue);
        }


        public void Produce() {
            var thread = new Thread(new ThreadStart(Run));
            thread.Name = Name;
            thread.Start();
        }

        private void Run() {
            idSeed = 0;
            for (int i = 0; i < Parameters.JobsPerProducer; i++) {
                Thread.Sleep(randomThreadRun);
                idSeed++;
                Queue.Enqueue(new Person(random, Name + " - Person " + idSeed.ToString("00")));
            }

            Queue.CompleteAdding();
        }
    }
}
