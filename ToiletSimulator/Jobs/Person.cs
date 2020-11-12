using System;
using System.Threading;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator.Jobs {
    public class Person : IJob, IComparable {

        public string Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public TimeSpan ProcessingTime { get; private set; }
        public TimeSpan WaitingTime { get; private set; }
        public DateTime ProcessedDate { get; private set; }

        private Person() { }
        public Person(Random random, string id) {
            Id = id;
            CreationDate = DateTime.Now;
            WaitingTime = TimeSpan.MaxValue;
            ProcessedDate = DateTime.MaxValue;

            // ATTENTION: Usually the times calculated here need to be evenly distributed (NormalDistribution) to get compareable
            // results for different runs. This is known, but was omitted, as this sample should just show the handling of
            // threads and concurrent collections.

            // calculate due dat
            TimeSpan dueTime = TimeSpan.FromMilliseconds(random.Next(Parameters.MinRandomValue, Parameters.MaxRandomValue));
            DueDate = CreationDate + dueTime;

            // calculate required processing time
            ProcessingTime = TimeSpan.FromMilliseconds(random.Next(Parameters.MinRandomValue, Parameters.MaxRandomValue));
        }

        public void Process() {
            WaitingTime = DateTime.Now - CreationDate;

            if (Parameters.DisplayJobProcessing) {
                Console.WriteLine(Id + ": Processing ...   ");
            }
            Thread.Sleep(ProcessingTime);  // simulate processing

            ProcessedDate = DateTime.Now;

            if (Parameters.DisplayJobProcessing) {
                if (ProcessedDate <= DueDate) {
                    Console.WriteLine(Id + ": Ahhhhhhh, much better ...");
                } else {
                    Console.WriteLine(Id + ": OOOOh no, too late ......");
                }
            }

            Statistics.CountJob(this);
        }

        public int CompareTo(object obj) {
            if (obj == null) {
                return 1;
            }

            Person otherPerson = obj as Person;
            if (otherPerson != null) {
                return this.Id.CompareTo(otherPerson.Id);
            } else {
                throw new ArgumentException("Object is not a Person");
            }
        }
    }
}
