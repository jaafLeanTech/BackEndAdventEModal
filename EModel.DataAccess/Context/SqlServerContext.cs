using EModel.Entities.Entities;
using Microsoft.EntityFrameworkCore;


namespace EModel.DataAccess.Context
{
    public class SqlServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly string _connectionString = string.Empty;

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

        public SqlServerContext() => _connectionString = @"Data Source = DESKTOP-V89KSU6\SQLEXPRESS; Initial Catalog = Advent; Integrated Security = true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
