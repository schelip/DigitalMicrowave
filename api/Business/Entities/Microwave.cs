using System;
using System.Threading;
using System.Windows.Forms;

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

        public int TimeLeft { get; private set; }
        public int PowerLevel { get; private set; } = 10;
        public State CurrentState { get; private set; } = State.Idle;
        public HeatingProcedure SelectedHeatingProcedure { get; private set; }
        public string Status { get; private set; } = string.Empty;
        public char StatusChar
        {
            get => SelectedHeatingProcedure != null ?
                SelectedHeatingProcedure.HeatingString[0] :
                '.';
        }
        public string HeatingJobId { get; private set; }

        internal void AddTime() =>
            TimeLeft += 30; // TODO: Verificar se o acréscimo de tempo deveria respeitar o limite de 2 min

        public void PrepareHeating(int time, int powerLevel)
        {
            TimeLeft = time;
            PowerLevel = powerLevel;
            Status = string.Empty;
        }

        public void PrepareHeating(HeatingProcedure heatingProcedure)
        {
            SelectedHeatingProcedure = heatingProcedure;
            PrepareHeating(heatingProcedure.Time, heatingProcedure.PowerLevel);
            Status = string.Empty;
        }

        public void ContinueHeating(string heatingJobId)
        {
            HeatingJobId = heatingJobId;
            CurrentState = State.Running;
        }

        public void PauseHeating()
        {
            HeatingJobId = string.Empty;
            CurrentState = State.Paused;
        }

        public void StopHeating()
        {
            CurrentState = State.Idle;
            TimeLeft = 0;
            PowerLevel = 10;
            Status = string.Empty;
            SelectedHeatingProcedure = null;
            HeatingJobId = string.Empty;
        }

        public void TickHeating()
        {
            TimeLeft--;
            Status += new string(StatusChar, PowerLevel) + " ";
            if (TimeLeft == 0)
                Status += "Aquecimento concluído";
        }
    }
}