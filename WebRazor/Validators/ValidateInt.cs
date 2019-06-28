using System.ComponentModel.DataAnnotations;

namespace WebRazor.Validators
{
    public class ValidateInt : RequiredAttribute
    {
        public bool IsValid(int value)
        {
            return value != 0;
        }
    }
}