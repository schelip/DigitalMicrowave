using DigitalMicrowave.Web.Model.ViewModel;
using DigitalMicrowave.Business.Entities;
using DigitalMicrowave.Infrastructure.Services;
using DigitalMicrowave.Web.Hubs;
using Hangfire;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace DigitalMicrowave.Business.Services
{
    public class MicrowaveService : IMicrowaveService
    {
        private IHubContext _microwaveHubContext;
        private static Microwave microwave = new Microwave();

        public MicrowaveService()
        {
            _microwaveHubContext = GlobalHost.ConnectionManager.GetHubContext<MicrowaveHub>();
        }

        public MicrowaveViewModel Get() => new MicrowaveViewModel(microwave);

        public void Start(int time = 30, int powerLevel = 10)
        {
            if (microwave.CurrentState == Microwave.State.Running)
            {
                microwave.TimeLeft += 30; // TODO: Verificar se o acréscimo de tempo deveria respeitar o limite de 2 min
                return;
            }

            if (microwave.CurrentState == Microwave.State.Idle)
            {
                microwave.TimeLeft = time;
                microwave.PowerLevel = powerLevel;
            }

            microwave.HeatingJobId = BackgroundJob.Enqueue(() => Heat());
            microwave.CurrentState = Microwave.State.Running;
        }

        public void Stop()
        {
            if (microwave.CurrentState == Microwave.State.Running)
                microwave.CurrentState = Microwave.State.Paused;
            else
            {
                microwave.CurrentState = Microwave.State.Idle;
                microwave.TimeLeft = 0;
            }
        }

        public void Heat()
        {
            while (microwave.TimeLeft > 0)
            {
                Thread.Sleep(1000);
                if (microwave.CurrentState != Microwave.State.Running)
                    return;

                microwave.TimeLeft--;
                _microwaveHubContext.Clients.All.update(microwave);
            }

            microwave.CurrentState = Microwave.State.Idle;
            _microwaveHubContext.Clients.All.update(microwave);
        }
    }
}