using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RabbitMQ.Models;

public partial class RabbitMQContext : DbContext
{
    public RabbitMQContext()
    {
    }

    public RabbitMQContext(DbContextOptions<RabbitMQContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-1F0E6C0;Initial Catalog=RabbitMQ;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.BookingConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
