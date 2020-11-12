using System;
using System.Linq;
using System.Threading;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Queues {
    public class BetterQueue : ToiletQueue {

        Semaphore sem = new Semaphore(0, Parameters.Producers * Parameters.JobsPerProducer);

        public BetterQueue() { }

        public override void Enqueue(IJob job) {
            lock (queue) {
                queue.Add(job);
            }

            sem.Release();
        }

        public override bool TryDequeue(out IJob job) {
            sem.WaitOne();

            lock (queue) {
                if (queue.Count > 0) {

                    queue = (from e in queue
                             orderby DateTime.Now.Subtract(e.DueDate), e.ProcessingTime
                             select e)
                             .ToList();

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

            if (producersCompleted == Parameters.Producers) {
                sem.Release(Parameters.Consumers - 1);
            }
        }
    }
}
