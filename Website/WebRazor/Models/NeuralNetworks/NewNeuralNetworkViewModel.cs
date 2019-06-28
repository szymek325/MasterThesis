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

        [Range(2, 20, ErrorMessage = "Can only be between 2 .. 20")]
        public int PhotosPerPerson { get; set; }
    }
}