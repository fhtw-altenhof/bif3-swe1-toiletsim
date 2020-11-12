using System.Threading;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Consumer {
    class Toilet : IConsumer {

        public string Name { get; private set; }
        public IToiletQueue Queue { get; private set; }

        private Thread thread;

        private Toilet() { }
        public Toilet(string name, IToiletQueue queue) {
            Name = name;
            Queue = queue;
        }

        public void Consume() {
            thread = new Thread(Run);
            thread.Start();
        }

        public void Run() {
            while (!Queue.IsCompleted) {
                IJob job;
                if (Queue.TryDequeue(out job)) {
                    job.Process();
                }
            }
        }

        public void Join() {
            thread.Join();
        }
    }
}
