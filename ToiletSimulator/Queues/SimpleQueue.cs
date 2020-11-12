using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Queues {
    public class SimpleQueue : ToiletQueue {

        public SimpleQueue() { }

        public override void Enqueue(IJob job) {
            lock (queue) {
                queue.Add(job);
            }
        }

        public override bool TryDequeue(out IJob job) {
            lock (queue) {
                if (queue.Count > 0) {
                    job = queue[0];
                    queue.RemoveAt(0);
                    return true;
                } else {
                    job = null;
                    return false;
                }
            }
        }

        public override void CompleteAdding() {
            base.CompleteAdding();
        }
    }
}
