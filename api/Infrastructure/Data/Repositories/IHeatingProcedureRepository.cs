using DigitalMicrowave.Business.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalMicrowave.Infrastructure.Data.Repositories
{
    public interface IHeatingProcedureRepository
    {
        Task<IEnumerable<HeatingProcedure>> Get(
            Expression<Func<HeatingProcedure, bool>> filter = null,
            Func<IQueryable<HeatingProcedure>, IOrderedQueryable<HeatingProcedure>> orderBy = null,
            string includeProperties = "");
        Task<HeatingProcedure> GetById(int id);
        Task Insert(HeatingProcedure heatingProcedure);
        Task Update(HeatingProcedure heatingProcedure);
        Task Delete(int id);
        Task Delete(HeatingProcedure heatingProcedure);
    }
}