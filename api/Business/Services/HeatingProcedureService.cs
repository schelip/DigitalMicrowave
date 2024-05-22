using DigitalMicrowave.Infrastructure.Data.Repositories;
using DigitalMicrowave.Infrastructure.Services;
using DigitalMicrowave.Web.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DigitalMicrowave.Business.Services
{
    public class HeatingProcedureService : IHeatingProcedureService
    {
        private IHeatingProcedureRepository _repository;

        public HeatingProcedureService(IHeatingProcedureRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HeatingProcedureViewModel>> GetAll()
        {
            var heatingProcedures = await _repository.Get();
            return heatingProcedures.Select(hp => new HeatingProcedureViewModel(hp));
        }

        public async Task<HeatingProcedureViewModel> GetById(int id) => new HeatingProcedureViewModel(await _repository.GetById(id));
    }
}