using DigitalMicrowave.Business.Entities;
using DigitalMicrowave.Infrastructure.Data;
using DigitalMicrowave.Infrastructure.Data.Repositories;

namespace DigitalMicrowave.Business.Repositories
{
    public class HeatingProcedureRepository : BaseRepository<int, HeatingProcedure>, IHeatingProcedureRepository
    {
        public HeatingProcedureRepository(DigitalMicrowaveContext context) : base(context)
        {
        }
    }
}