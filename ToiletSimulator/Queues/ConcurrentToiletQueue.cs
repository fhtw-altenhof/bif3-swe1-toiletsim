using System.Collections.Concurrent;
using System.Threading;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Queues {
    public class ConcurrentToiletQueue : IToiletQueue {

        private ConcurrentQueue<IJob> concurrentQueue;
        private int producersCompleted;

        public int Count => concurrentQueue.Count;

        public bool IsCompleted => (concurrentQueue.Count) == 0 && (producersCompleted == Parameters.Producers);

        public ConcurrentToiletQueue() {
            concurrentQueue = new ConcurrentQueue<IJob>();
        }

        public void CompleteAdding() {
            Interlocked.Increment(ref producersCompleted);
        }

        public void Enqueue(IJob job) {
            concurrentQueue.Enqueue(job);
        }

        public bool TryDequeue(out IJob job) {
            return concurrentQueue.TryDequeue(out job);
        }
    }
}
