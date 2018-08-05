using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.NeuralNetwork.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebRazor.Validators;

namespace WebRazor.Models.Recognition
{
    public class NewRecognitionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [ValidateFile(ErrorMessage = "Please select a PNG/JPG/JPEG Image")]
        public IFormFile File { get; set; }

        public int NeuralNetworkId { get; set; }
        public List<SelectListItem> NeuralNetworks { get; set; }
    }
}