using DigitalMicrowave.Web.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalMicrowave.Infrastructure.Services
{
    public interface IHeatingProcedureService
    {
        Task<IEnumerable<HeatingProcedureViewModel>> GetAll();
        Task<HeatingProcedureViewModel> GetById(int id);
    }
}