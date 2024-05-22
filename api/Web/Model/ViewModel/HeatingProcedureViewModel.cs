using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DigitalMicrowave.Business.Entities;

namespace DigitalMicrowave.Web.Model.ViewModel
{
    public class HeatingProcedureViewModel
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string FoodName { get; set; }
        public int Time { get; set; }
        public int PowerLevel { get; set; }
        public string Instructions { get; set; }

        public HeatingProcedureViewModel(HeatingProcedure heatingProcedure)
        {
            Id = heatingProcedure.Id;
            Name = heatingProcedure.Name;
            FoodName = heatingProcedure.FoodName;
            Time = heatingProcedure.Time;
            PowerLevel = heatingProcedure.PowerLevel;
            Instructions = heatingProcedure.Instructions;
        }
    }
}