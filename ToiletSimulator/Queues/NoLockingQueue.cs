using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Queues {
    class NoLockingQueue : ToiletQueue {
        public NoLockingQueue() { }

        public override void Enqueue(IJob job) {
            queue.Add(job);
        }

        public override bool TryDequeue(out IJob job) {
            if (queue.Count > 0) {
                job = queue[0];
                queue.RemoveAt(0);
                return true;
            } else {
                job = null;
                return false;
            }
        }

        public override void CompleteAdding() {
            base.CompleteAdding();
        }
    }
}
