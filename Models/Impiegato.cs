using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Impiegato
    {
        
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
        public string PersonalWebSite { get; set; }
        public string Photo { get; set; }
        public string AlternateText { get; set; }
    }

    public class ImpiegatoConFile
    {
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        [MiaValidazione.DueParole]
        public string FullName { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Il campo Città è obbligatorio")]
        [StringLength(10, ErrorMessage = "La città non può avere più di 10 caratteri")]
        public string City { get; set; }
        public string? EmailAddress { get; set; }
        public string? PersonalWebSite { get; set; }
        public string? Photo { get; set; }
        public string? AlternateText { get; set; }

        public IFormFile File { get; set; }
    }
}
