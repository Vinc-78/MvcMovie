using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class MiaValidazione
    {
        public class DueParole : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                string fieldValue = (string)value;

                if (fieldValue == null || fieldValue.Trim().IndexOf(" ") == -1)
                {
                    return new ValidationResult("il campo deve contenere almeno due parole");
                }

                return ValidationResult.Success;
            }
        }
    }
}
