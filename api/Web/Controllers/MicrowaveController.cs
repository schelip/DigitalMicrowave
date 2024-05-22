using DigitalMicrowave.Infrastructure.Services;
using DigitalMicrowave.Web.Model.InputModel;
using System.Web.Http;
using FluentValidation;
using System.Threading.Tasks;
using DigitalMicrowave.Infrastructure.Data.Repositories;

namespace DigitalMicrowave.Web.Controllers
{
    public class MicrowaveController : ApiController
    {
        private IValidator<StartHeatingInputModel> _startHeatingValidator;
        private IMicrowaveService _microwaveService;
        private IHeatingProcedureRepository _heatingProcedureRepository;

        public MicrowaveController(
            IValidator<StartHeatingInputModel> startHeatingValidator,
            IMicrowaveService microwaveService,
            IHeatingProcedureRepository heatingProcedureService)
        {
            _startHeatingValidator = startHeatingValidator;
            _microwaveService = microwaveService;
            _heatingProcedureRepository = heatingProcedureService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var microwave = _microwaveService.Get();
            return Ok(microwave);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Start([FromBody] StartHeatingInputModel startHeatingInput)
        {
            if (startHeatingInput != null)
            {
                var result = await _startHeatingValidator.ValidateAsync(startHeatingInput);

                if (!result.IsValid)
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        return BadRequest(ModelState);
                    }

                _microwaveService.Start(startHeatingInput.Time, startHeatingInput.PowerLevel);
            }
            else
                _microwaveService.Start();

            return Ok();
        }

        [HttpPost]
        [Route("api/microwave/start-proc/{heatingProcedureId}")]
        public async Task<IHttpActionResult> StartHeatingProcedure([FromUri] int heatingProcedureId)
        {
            // TODO: GAMBIARRA!! Não foi possível injetar o HeatingProcedureRepository no MicrowaveService, mas o controller não deveria acessar o Repository
            var heatingProcedure = await _heatingProcedureRepository.GetById(heatingProcedureId);

            if (heatingProcedure == null)
                return NotFound();

            _microwaveService.StartHeatingProcedure(heatingProcedure);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Stop()
        {
            _microwaveService.Stop();
            return Ok();
        }
    }
}
