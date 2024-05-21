namespace DigitalMicrowave.Business.Entities
{
    public class Microwave
    {
        public enum State
        {
            Running,
            Paused,
            Idle
        }

        public int TimeLeft { get; set; }
        public int PowerLevel { get; set; } = 10;
        public State CurrentState { get; set; } = State.Idle;
        public string Status { get; set; } = string.Empty;
        public string HeatingJobId { get; set; }
    }
}