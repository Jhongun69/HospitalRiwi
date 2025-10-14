using HospitalRiwi.Models;
using Microsoft.EntityFrameworkCore;


namespace HospitalRiwi.Data;

public class AppDbContext : DbContext //heredamos dbcontext que es propio de entity
{
    //Constructor vacio para recibir "options" desde program.cs y conectarse a la database
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    //Aqui le digo a entity cuales seran las tablas en la database
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<MedicalDate> MedicalDates { get; set; }
    
}