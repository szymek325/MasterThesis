using System.Collections.Generic;
using Domain.People.DTO;

namespace WebRazor.Models.People
{
    public class PeopleViewModel
    {
        public IEnumerable<PersonOutput> People { get; set; }
    }
}