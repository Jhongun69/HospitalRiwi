using HospitalRiwi.Data;
using HospitalRiwi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRiwi.Controllers;

public class MedicalDatesController : Controller
{
    private readonly AppDbContext _context;

    public MedicalDatesController(AppDbContext context)
    {
        _context = context;
    }

    // metodito GET: /MedicalDate o 
    // este método maneja tanto la lista completa como los filtros
    public async Task<IActionResult> Index(int? patientId, int? doctorId)
    {
        // Preparamos la consulta base incluyendo los datos relacionados
        var query = _context.MedicalDates
                            .Include(m => m.Patient)
                            .Include(m => m.Doctor)
                            .AsQueryable();

        // Si el usuario seleccionó un paciente en el filtro...
        if (patientId.HasValue)
        {
            // ...se añade una condición a la consulta para filtrar por ese paciente
            query = query.Where(m => m.PatientId == patientId.Value);
        }

        // Hacemos lo mismo si seleccionó un médico
        if (doctorId.HasValue)
        {
            query = query.Where(m => m.DoctorId == doctorId.Value);
        }

        // se preparan las listas para los menus desplegables del filtro
        // Se las pasamos a la vista a través del ViewBag
        ViewBag.PatientsList = new SelectList(await _context.Patients.ToListAsync(), "Id", "Name");
        ViewBag.DoctorsList = new SelectList(await _context.Doctors.ToListAsync(), "Id", "Name");

        // Finalmente, ejecutamos la consulta (con los filtros si los hubo) y la pasamos a la vista.
        var appointments = await query.ToListAsync();
        return View(appointments);
    }
    //------------------------------------------------------------------------------------------------------------------------------

    // metodito GET: MedicalDate/Create
    // Muestra el formulario para agendar una nueva cita
    public IActionResult Create()
    {
        // Preparamos los datos para los dropdowns de pacientes y mdicos
        // Pasamos la lista de pacientes y doctores a la vista a través del ViewBag
        ViewBag.PatientsList = new SelectList(_context.Patients, "Id", "Name");
        ViewBag.DoctorsList = new SelectList(_context.Doctors, "Id", "Name");
        return View();
    }

    // metodito POST: MedicalDate/Create
    // Recibe los datos del formulario y agenda la cita
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MedicalDate medicalDate)
    {
        //  REGLAS DE NEGOCIO
        // 1. Validar que un médico no tenga otra cita a la misma hora
        var doctorIsBusy = await _context.MedicalDates
            .AnyAsync(m => m.DoctorId == medicalDate.DoctorId && m.Appointment == medicalDate.Appointment);

        if (doctorIsBusy)
        {
            // Si el doctor está ocupado, mandamos un error general al formulario
            ModelState.AddModelError(string.Empty, "El médico seleccionado ya tiene una cita agendada a esa misma fecha y hora.");
        }

        // 2. Validar que un paciente no tenga otra cita a la misma hora
        var patientIsBusy = await _context.MedicalDates
            .AnyAsync(m => m.PatientId == medicalDate.PatientId && m.Appointment == medicalDate.Appointment);

        if (patientIsBusy)
        {
            ModelState.AddModelError(string.Empty, "El paciente seleccionado ya tiene una cita agendada a esa misma fecha y hora.");
        }


        if (ModelState.IsValid)
        {
            // El estado por defecto es "Scheduled", ya lo definimos en el modelo así que no hay que asignarlo
            _context.Add(medicalDate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //  Si el modelo no es válido hay que volver a cargar los dropdowns
        // antes de mostrar el formulario otra vez
        ViewBag.PatientsList = new SelectList(_context.Patients, "Id", "Name", medicalDate.PatientId);
        ViewBag.DoctorsList = new SelectList(_context.Doctors, "Id", "Name", medicalDate.DoctorId);
        return View(medicalDate);
    }
    //-------------------------------------------------------------------------------------------------------------------------------------

    // metodito POST: MedicalDate/Cancel/5
    // Esta acción no tiene vista, solo cambia el estado y redirige
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var medicalDate = await _context.MedicalDates.FindAsync(id);
        if (medicalDate != null)
        {
            medicalDate.Status = "Canceled"; // Cambiamos el estado a "Cancelada"
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    //-------------------------------------------------------------------------------------------------------------------------------------

    // metodito POST: MedicalDate/Attend/5
    // Similar a Cancelar, solo cambia el estado a "Atendida".
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Attend(int id)
    {
        var medicalDate = await _context.MedicalDates.FindAsync(id);
        if (medicalDate != null)
        {
            medicalDate.Status = "Attended"; // Cambiamos el estado a "Atendida"
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}