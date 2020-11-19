using System;
using ToiletSimulator.Interfaces;

namespace ToiletSimulator {
    public static class Statistics {
        private static object locker = new object();
        private static int jobs = 0;
        private static int starvedJobs = 0;
        private static TimeSpan totalWaitingTime = TimeSpan.Zero;

        public static void Reset() {
            lock (locker) {
                jobs = 0;
                starvedJobs = 0;
                totalWaitingTime = TimeSpan.Zero;
            }
        }

        public static void CountJob(IJob job) {
            lock (locker) {
                jobs++;
                if (job.ProcessedDate > job.DueDate) {
                    starvedJobs++;
                }
                totalWaitingTime += job.WaitingTime;
            }
        }

        public static void Display() {
            lock (locker) {
                Console.WriteLine();
                Console.WriteLine("Statistics:");
                Console.WriteLine("---------");
                Console.WriteLine("Jobs:                      " + jobs);
                Console.WriteLine("Starved Jobs:              " + starvedJobs);
                Console.WriteLine("Starvation Ratio:          " + starvedJobs / ((double)jobs));
                Console.WriteLine("Total Waiting Time:        " + totalWaitingTime);
                Console.WriteLine("Mean Waiting Time:         " + TimeSpan.FromMilliseconds(totalWaitingTime.TotalMilliseconds / jobs));
            }
        }
    }
}
