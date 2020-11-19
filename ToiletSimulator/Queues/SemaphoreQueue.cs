using System;
using System.Linq;
using System.Threading;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Queues {
    public class SemaphoreQueue : ToiletQueue {

        Semaphore hodor = new Semaphore(1, 1);

        public SemaphoreQueue() { }

        public override void Enqueue(IJob job) {
            hodor.WaitOne();

            queue.Add(job);

            hodor.Release();
        }

        public override bool TryDequeue(out IJob job) {
            hodor.WaitOne();

            if (queue.Count > 0) {

                queue = (from e in queue
                        orderby DateTime.Now.Subtract(e.DueDate), e.ProcessingTime
                        select e)
                        .ToList();

                job = queue[0];
                queue.RemoveAt(0);
                hodor.Release();
                return true;
            } else {
                job = null;
                hodor.Release();
                return false;
            }
        }

        public override void CompleteAdding() {
            base.CompleteAdding();
        }
    }
}
