using DigitalMicrowave.Infrastructure.Services;
using DigitalMicrowave.Web.Model.InputModel;
using System.Web.Http;
using FluentValidation;
using System.Threading.Tasks;

namespace DigitalMicrowave.Web.Controllers
{
    public class MicrowaveController : ApiController
    {
        private IValidator<StartHeatingInputModel> _startHeatingValidator;
        private IMicrowaveService _service;

        public MicrowaveController(IValidator<StartHeatingInputModel> startHeatingValidator, IMicrowaveService microwaveService)
        {
            _startHeatingValidator = startHeatingValidator;
            _service = microwaveService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var microwave = _service.Get();
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

                _service.Start(startHeatingInput.Time, startHeatingInput.PowerLevel);
            }
            else
                _service.Start();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Stop()
        {
            _service.Stop();
            return Ok();
        }
    }
}
