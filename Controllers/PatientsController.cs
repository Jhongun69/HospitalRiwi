using HospitalRiwi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalRiwi.Models;

namespace HospitalRiwi.Controllers;

public class PatientsController : Controller //heredo controller que es propio de ef para los controladores
{
    private readonly AppDbContext _context;//creo variable para acceder a base de datos
    //private Protegido de modificaciones externas.
    //readonly Protegido de modificaciones internas después de la inicialización

    public PatientsController(AppDbContext context)//Constructor para recibir el contexto (Inyección de Dependencias)
    {
        _context = context;//(propio de entity)
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------
    //metodito get, se encarga de obtener la lista de todos los pacientes
    //y pasarla a la vista para que sea mostrada
    public async Task<IActionResult> Index() //el metodo que se ejecuta cuando visitan /Patients/index 
    {
        //le decimos a _context ve a patients y trae los datos, guardalos en Patients y espera
        //                _context ->patients ->tolistasync
        var Patients = await _context.Patients.ToListAsync();
        return View(Patients);//esto le dice a mvc busca un index en la carpeta views, archivo patients y pasale la variable Patients(la de arriba)

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------
    // metodito GET: Patient/Create
    // Este metodo muestra el formulario para crear un nuevo paciente
    public IActionResult Create()
    {
        return View();
    }

    // metodito POST: Patient/Create
    // Este metodo recibe los datos del formulario cuando el usuario hace click en "Guardar"
    [HttpPost]// Le dice a MVC que este método solo debe responder a peticiones POST
    [ValidateAntiForgeryToken] // genera un token secreto y lo envia en cada formulario que envia el usuario, se usa por ciberseguridad
    public async Task<IActionResult> Create(Patient patient)
    {
        // Verificamos si ya existe un paciente con el mismo documento
        bool documentExists = await _context.Patients.AnyAsync(p => p.Document == patient.Document);//anysync pregunta existe un paciente p en la tabla Patients?
        if (documentExists)
        {
            // Si el documento ya existe se añade un error al ModelState
            // Este error se mostrara junto al campo "Document"
            ModelState.AddModelError("Document", "Ya existe un paciente con este número de documento.");
        }

        // ModelState.IsValid comprueba si el modelo cumple con todas las validaciones
        // (los [Required] [EmailAddress] etc...
        if (ModelState.IsValid)
        {
            // Si todo es válido añade el nuevo paciente al contexto de Entity
            _context.Add(patient);
            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();
            // Redirigimos al usuario a la página de la lista (Index).
            return RedirectToAction(nameof(Index));
        }

        // Si el modelo no es valido volvemos a mostrar el formulario de creacion
        // con los datos que el usuario ya había ingresado y los mensajes de error.
        return View(patient);
    }
    //------------------------------------------------------------------------------------------------------------------------------------------
    // metodito GET: Patient/Details/ + el id
    public async Task<IActionResult> Details(int? id)
    {
        // Verificamos si nos pasaron un id si no devolvemos un error
        if (id == null)
        {
            return NotFound();
        }

        // Buscamos el paciente en la base de datos cuyo Id coincida con el que nos pasaron
        // FirstOrDefaultAsync encuentra el primero que coincida o devuelve null si no hay ninguno
        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.Id == id);

        // Si no se encontro ningún paciente con ese id, devolvemos un error
        if (patient == null)
        {
            return NotFound();
        }

        // Si encontramos el paciente lo pasamos a la vista Details.cshtml
        return View(patient);
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    // metodito GET Patient/Edit/id
    // Este método busca al paciente y muestra sus datos en un formulario
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // FindAsync es una forma  de buscar un item por su clave primaria (Id)
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return View(patient);
    }

    // metodito POST: Patient/Edit/id
    // Este método recibe los datos modificados del formulario y los guarda
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        // Verificamos que el id de la URL coincida con el id del paciente que nos llega
        if (id != patient.Id)
        {
            return NotFound();
        }

        // Validamos que el documento no esté duplicado, excluyendo al paciente actual
        bool documentExists = await _context.Patients.AnyAsync(p => p.Document == patient.Document && p.Id != patient.Id);
        if (documentExists)
        {
            ModelState.AddModelError("Document", "Ya existe otro paciente con este número de documento.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Le decimos al contexto que este 'patient' ha sido modificado
                _context.Update(patient);
                // Guardamos los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Esto es un control de errores por si alguien borró el paciente
                // mientras se estaba editando.
                return NotFound();
            }
            // Si todo sale bien, redirigimos a la lista de pacientes.
            return RedirectToAction(nameof(Index));
        }
        // Si hay errores de validación, volvemos a mostrar el formulario con los errores.
        return View(patient);
    }
    //------------------------------------------------------------------------------------------------------------------------------
    //metodito GET: Patient/Delete/id
    // Muestra una vista de confirmación antes de borrar.
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // metodito POST: Patient/Delete/id
    // Esta acción se ejecuta cuando el usuario confirma la eliminación.
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

