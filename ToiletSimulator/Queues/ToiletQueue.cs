using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Queues {
    public abstract class ToiletQueue : IToiletQueue {

        protected IList<IJob> queue;
        protected int producersCompleted;

        public int Count {
            get {
                lock (queue) {
                    return queue.Count;
                }
            }
        }

        protected ToiletQueue() {
            queue = new List<IJob>();
        }

        public abstract void Enqueue(IJob job);


        public abstract bool TryDequeue(out IJob job);


        public virtual void CompleteAdding() {
            Interlocked.Increment(ref producersCompleted);
        }

        public bool IsCompleted {
            get {
                lock (queue) {
                    return queue.Count == 0
                        && producersCompleted == Parameters.Producers;
                }
            }
        }
    }
}
