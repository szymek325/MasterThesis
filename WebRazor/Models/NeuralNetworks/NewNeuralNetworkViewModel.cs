using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.People.DTO;
using WebRazor.Validators;

namespace WebRazor.Models.NeuralNetworks
{
    public class NewNeuralNetworkViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [ValidatePeople(ErrorMessage = "You have to pick at leat 2 people")]
        public List<PersonAsCheckbox> PeopleCheckboxes { get; set; }
    }
}