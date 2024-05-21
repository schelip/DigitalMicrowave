using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigitalMicrowave.Web.Model.InputModel
{
    public class StartHeatingInputModel
    {
        public int Time { get; set; } = 30;
        public int PowerLevel { get; set; } = 10;
    }
}