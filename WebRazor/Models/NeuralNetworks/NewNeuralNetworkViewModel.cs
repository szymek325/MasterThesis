using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.People.DTO;

namespace WebRazor.Models.NeuralNetworks
{
    public class NewNeuralNetworkViewModel
    {
        [Required] public string Name { get; set; }

        public List<PersonAsCheckbox> PeopleCheckboxes { get; set; }
    }
}