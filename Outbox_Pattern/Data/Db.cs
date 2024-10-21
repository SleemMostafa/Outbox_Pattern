using Microsoft.EntityFrameworkCore;
using Outbox_Pattern.Data.Entities;

namespace Outbox_Pattern.Data;

public class Db:DbContext
{
    public Db(DbContextOptions<Db> options):base(options)
    {
        
    }
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
}