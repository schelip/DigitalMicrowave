using DigitalMicrowave.Business.Entities;
using DigitalMicrowave.Web.Model.ViewModel;
using System.Threading.Tasks;

namespace DigitalMicrowave.Infrastructure.Services
{
    public interface IMicrowaveService
    {
        MicrowaveViewModel Get();
        void Start(int time = 30, int powerLevel = 10);
        void StartHeatingProcedure(HeatingProcedure heatingProcedure);
        void Stop();
    }
}
