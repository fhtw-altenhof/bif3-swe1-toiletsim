namespace ToiletSimulator {
    public static class Parameters {
        // number of producers
        public static int Producers => 4;

        // number of generated jobs per producer
        public static int JobsPerProducer => 40;

        // number of consumers
        public static int Consumers => 6;

        // if true, output of job processing is displayed
        public static bool DisplayJobProcessing => true;

        // minimum wait/due/processing random gen value
        public static int MinRandomValue = 100;

        // maximum wait/due/processing random gen value
        public static int MaxRandomValue = 500;
    }
}
