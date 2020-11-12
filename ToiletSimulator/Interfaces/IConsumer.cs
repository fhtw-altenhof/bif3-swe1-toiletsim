namespace ToiletSimulator.Interfaces {
    public interface IConsumer {
        string Name { get; }
        public IToiletQueue Queue { get; }
        void Consume();
        void Run();
        void Join();
    }
}
