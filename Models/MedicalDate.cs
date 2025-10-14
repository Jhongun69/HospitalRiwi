using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalRiwi.Models;

public class MedicalDate
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int PatientId { get; set; }
    [Required]
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "La fecha y hora de la cita son obligatorias.")]
    public DateTime Appointment { get; set; }
    [Required]
    public string Status { get; set; } = "Scheduled";//le pongo "agendada" por defecto para que al crear una nueva cita tenga este estado

    //   Propiedades de navegacion
    // commo se relacionan los modelos
    [ForeignKey("PatientId")]
    public virtual Patient? Patient { get; set;}
    [ForeignKey("DoctorId")]
    public virtual Doctor? Doctor { get; set;}

}