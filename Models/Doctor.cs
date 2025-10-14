using System.ComponentModel.DataAnnotations;

namespace HospitalRiwi.Models;

public class Doctor
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El documento del médico es obligatorio.")]
    public string Document { get; set; }

    [Required(ErrorMessage = "La especialidad es obligatoria.")]
    public string Speciality { get; set; }
    public string PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string Email { get; set; }

    // Propiedad de navegacion, Un médico puede tener muchas citas médicas.
    public virtual ICollection<MedicalDate> MedicalDates { get; set; } = new HashSet<MedicalDate>();
}

