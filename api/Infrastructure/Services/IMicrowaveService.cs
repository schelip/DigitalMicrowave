using DigitalMicrowave.Web.Model.InputModel;
using DigitalMicrowave.Web.Model.ViewModel;

namespace DigitalMicrowave.Infrastructure.Services
{
    public interface IMicrowaveService
    {
        MicrowaveViewModel Get();
        void Start(int time = 30, int powerLevel = 10);
        void Stop();
    }
}
