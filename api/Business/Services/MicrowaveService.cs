using DigitalMicrowave.Web.Model.ViewModel;
using DigitalMicrowave.Business.Entities;
using DigitalMicrowave.Infrastructure.Services;
using Hangfire;
using System.Threading;
using System;

namespace DigitalMicrowave.Business.Services
{
    public class MicrowaveService : IMicrowaveService
    {
        private static Microwave microwave = new Microwave();

        public MicrowaveViewModel Get() => new MicrowaveViewModel(microwave);

        public void Start(int time = 30, int powerLevel = 10)
        {
            if (microwave.CurrentState == Microwave.State.Running)
            {
                if (microwave.SelectedHeatingProcedure != null)
                    // TODO: Exceções customizadas
                    throw new Exception("Não é permitido adicionar tempo durante programas de aquecimento");

                microwave.AddTime();
            }
            
            if (microwave.CurrentState == Microwave.State.Idle)
                microwave.PrepareHeating(time, powerLevel);

            var heatingJobId = BackgroundJob.Enqueue(() => HeatJob());
            microwave.ContinueHeating(heatingJobId);
        }

        public void StartHeatingProcedure(HeatingProcedure heatingProcedure)
        {
            if (microwave.CurrentState == Microwave.State.Running)
                // TODO: Exceções customizadas
                throw new Exception("Já existe um programa de aquecimento ativo");

            if (microwave.CurrentState == Microwave.State.Idle)
                microwave.PrepareHeating(heatingProcedure);

            var heatingJobId = BackgroundJob.Enqueue(() => HeatJob());
            microwave.ContinueHeating(heatingJobId);
        }

        public void Stop()
        {
            if (microwave.CurrentState == Microwave.State.Running)
                microwave.PauseHeating();
            else
                microwave.StopHeating();
        }

        public void HeatJob()
        {
            while (microwave.TimeLeft > 0)
            {
                Thread.Sleep(1000);
                if (microwave.CurrentState != Microwave.State.Running)
                    return;
                microwave.TickHeating();
            }
            Thread.Sleep(1000);
            microwave.StopHeating();
        }
    }
}