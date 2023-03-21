using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext : DbContext {
    public DbSet<Categoria> Categorias {get; set;}

    public DbSet<Tarea> Tareas {get; set;}

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() {CategoriaId = Guid.Parse("edf91b0a-feee-4266-82f3-9d7a89c43522"), Nombre = "Actividades pendientes", Peso = 20});
        categoriasInit.Add(new Categoria() {CategoriaId = Guid.Parse("edf91b0a-feee-4266-82f3-9d7a89c43521"), Nombre = "Actividades Personales", Peso = 55});

        modelBuilder.Entity<Categoria>(categoria => 
        {

            categoria.ToTable("Categoria");

            categoria.HasKey(p=> p.CategoriaId);

            categoria.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p=>p.Descripcion).IsRequired(false);

            categoria.Property(p=> p.Peso);

            categoria.HasData(categoriasInit);

        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() {TareaId = Guid.Parse("edf91b0a-feee-4266-82f3-9d7a89c43523"),CategoriaId = Guid.Parse("edf91b0a-feee-4266-82f3-9d7a89c43522"), PrioridadTarea = Prioridad.Media,Titulo = "Pago de servicios publicos"});
        tareasInit.Add(new Tarea() {TareaId = Guid.Parse("edf91b0a-feee-4266-82f3-9d7a89c43524"),CategoriaId = Guid.Parse("edf91b0a-feee-4266-82f3-9d7a89c43521"), PrioridadTarea = Prioridad.Baja,Titulo = "Terminar de ver pelicula en netflix"});

        modelBuilder.Entity<Tarea>(tarea => {
            tarea.ToTable("Tarea");

            tarea.HasKey(t => t.TareaId);

            tarea.HasOne(t => t.Categoria).WithMany(t => t.Tareas).HasForeignKey(t=> t.CategoriaId);

            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(t => t.Descripcion).IsRequired(false);

            tarea.Property(t=> t.PrioridadTarea);

            tarea.Property(t => t.FechaCreacion);

            tarea.Ignore(t=>t.Resumen);

            tarea.HasData(tareasInit);
        });



    }
}