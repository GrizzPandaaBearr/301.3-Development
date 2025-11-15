using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // User-Patient relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Admin)
            .WithOne(a => a.User)
            .HasForeignKey<Admin>(a => a.AdminID)
            .OnDelete(DeleteBehavior.Cascade);

        // --- USER <-> DOCTOR (1:1 shared PK) ---
        modelBuilder.Entity<User>()
            .HasOne(u => u.Doctor)
            .WithOne(d => d.User)
            .HasForeignKey<Doctor>(d => d.DoctorID)
            .OnDelete(DeleteBehavior.Cascade);

        // --- USER <-> PATIENT (1:1 shared PK) ---
        modelBuilder.Entity<User>()
            .HasOne(u => u.Patient)
            .WithOne(p => p.User)
            .HasForeignKey<Patient>(p => p.PatientID)
            .OnDelete(DeleteBehavior.Cascade);

        // PATIENT has optional assigned doctor
        modelBuilder.Entity<Patient>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.DoctorID)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.ToTable("ActivityLogs");

            entity.HasKey(e => e.LogID);

            entity.Property(e => e.LogID)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserID)
                .IsRequired();

            entity.Property(e => e.Action)
                .IsRequired();

            entity.Property(e => e.Timestamp)
                .IsRequired();

            // Relationship → Many logs belong to one user
            entity.HasOne(e => e.User)
                .WithMany(u => u.ActivityLogs)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        });


        
    }
}