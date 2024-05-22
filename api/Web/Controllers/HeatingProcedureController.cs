using DigitalMicrowave.Business.Entities;
using DigitalMicrowave.Infrastructure.Services;
using DigitalMicrowave.Web.Model.InputModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DigitalMicrowave.Web.Controllers
{
    public class HeatingProcedureController : ApiController
    {
        private IHeatingProcedureService _service;

        public HeatingProcedureController(IHeatingProcedureService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var heatingProcedures =  await _service.GetAll();
            return Ok(heatingProcedures);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var heatingProcedure = await _service.GetById(id);
            return Ok(heatingProcedure);
        }
    }
}