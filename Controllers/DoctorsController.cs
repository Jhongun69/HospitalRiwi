using HospitalRiwi.Data;
using HospitalRiwi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRiwi.Controllers;

public class DoctorsController : Controller
{
    // El contexto de la base de datos
    private readonly AppDbContext _context;

    public DoctorsController(AppDbContext context)
    {
        _context = context;
    }

    // metodito GET: /Doctor
    // Muestra la lista de médicos 
    public async Task<IActionResult> Index(string speciality)
    {
        // Preparamos la consulta para traer los medicos. Aún no se ejecuta
        var doctors = from d in _context.Doctors select d;

        // Si el usuario usó el filtro (speciality no está vacio)...
        if (!string.IsNullOrEmpty(speciality))
        {
            // ...añadimos una condición a la consulta para filtrar por esa especialidad
            doctors = doctors.Where(s => s.Speciality == speciality);
        }

        // Para el dropdown del filtro, necesitamos una lista de todas las especialidades unicas
        var specialities = await _context.Doctors
                                        .Select(d => d.Speciality)
                                        .Distinct()
                                        .ToListAsync();
        
        // Usamos ViewBag para pasarle la lista de especialidades a la Vista
        ViewBag.Specialities = new SelectList(specialities);

        // ejecutamos la consulta y pasamos la lista de médicos a la Vista
        return View(await doctors.ToListAsync());
    }

    // metodito GET: Doctor/Details/id
    // Muestra los datos de un solo médico
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.Id == id);

        if (doctor == null) return NotFound();

        return View(doctor);
    }
    //-----------------------------------------------------------------------------------------------------------------------

    // metodito GET: Doctor/Create
    //  muestra el formulario para registrar un nuevo médico
    public IActionResult Create()
    {
        return View();
    }

    // metodito POST: Doctor/Create
    // Recibe los datos del formulario y crea el registro
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Doctor doctor)
    {
        // Verificamos si ya existe un médico con ese documento
        if (await _context.Doctors.AnyAsync(d => d.Document == doctor.Document))
        {
            ModelState.AddModelError("Document", "Este documento ya está registrado.");
        }

        // Si el modelo es válido (pasa las validaciones y nuestra verificación etc)...
        if (ModelState.IsValid)
        {
            _context.Add(doctor);
            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Si no es válido, volvemos a mostrar el formulario con los errores
        return View(doctor);
    }
    //-----------------------------------------------------------------------------------------------------------------------


    // metodito GET: Doctor/Edit/5
    // Busca al medico por su id y muestra sus datos en el formulario de edicion
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var doctor = await _context.Doctors.FindAsync(id);
        
        if (doctor == null) return NotFound();
        
        return View(doctor);
    }

    // metodito POST: Doctor/Edit/5
    // Recibe los datos modificados y actualiza la base de datos
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Doctor doctor)
    {
        if (id != doctor.Id) return NotFound();

        // buscamos un doctor con el mismo documento,
        // PERO que no sea el que estamos editando actualmente (Id diferente)
        if (await _context.Doctors.AnyAsync(d => d.Document == doctor.Document && d.Id != doctor.Id))
        {
            ModelState.AddModelError("Document", "Este documento ya pertenece a otro médico.");
        }

        if (ModelState.IsValid)
        {
            _context.Update(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(doctor);
    }
    //-----------------------------------------------------------------------------------------------------------------------


    // metodito GET: Doctor/Delete/5
    // Muestra una página de confirmación antes de eliminar
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.Id == id);
        
        if (doctor == null) return NotFound();

        return View(doctor);
    }

    // metodito POST: Doctor/Delete/5
    // La acción que se ejecuta cuando el usuario confirma la eliminación
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor != null)
        {
            _context.Doctors.Remove(doctor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}