using DigitalMicrowave.Business.Entities;
using System;

namespace DigitalMicrowave.Web.Model.ViewModel
{
    public class MicrowaveViewModel
    {
        public string TimeLeft { get; set; }
        public int PowerLevel { get; set; }
        public string CurrentState { get; set; }
        public string Status { get; set; }

        public MicrowaveViewModel(Microwave microwave)
        {
            TimeLeft = FormatTimeLeft(microwave.TimeLeft);
            PowerLevel = microwave.PowerLevel;
            CurrentState = microwave.CurrentState.ToString();
            Status = microwave.Status;
        }

        private string FormatTimeLeft(int seconds)
        {
            if (60 < seconds && seconds < 100) // TODO: Validar se a formatação deve realmente respeitar o limite superior de 100 segundos
            {
                var span = TimeSpan.FromSeconds(seconds);
                return $"{span.Minutes}:{span.Seconds:D2}";
            }
            return $"{seconds} seg";
        }
    }
}