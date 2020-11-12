using System;
using System.Collections.Generic;
using System.Text;

namespace ToiletSimulator.Interfaces {
    public interface IToiletQueue {
        int Count { get; }                     // number of queued jobs
        void Enqueue(IJob job);                // enqueue a new job
        bool TryDequeue(out IJob job); // fetch next job
        void CompleteAdding();
        bool IsCompleted { get; }
    }
}
