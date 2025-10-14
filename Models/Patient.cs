using System.ComponentModel.DataAnnotations;

namespace HospitalRiwi.Models;

public class Patient
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del paciente es obligatorio.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "El documento del paciente es obligatorio.")]
    public string Document { get; set; }
    
    [Range(0, 120, ErrorMessage = "La edad debe ser un valor válido.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "El Celular del paciente es obligatorio.")]
    public string PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string Email { get; set; }

    // Propiedades de navegacion
    // Un paciente puede tener muchas citas
    public virtual ICollection<MedicalDate> MedicalDates { get; set; } = new HashSet<MedicalDate>();
    //asegura que cada vez que se cree un nuevo objeto Patient, su propiedad MedicalDates sea una lista vacía en lugar de ser null. Esto es todo lo que necesitamos para que ModelState.IsValid sea true.
}