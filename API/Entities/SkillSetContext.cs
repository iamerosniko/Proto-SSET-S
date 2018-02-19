using System.Data.Entity;

namespace API.Entities
{
    public class SkillSetContext : DbContext
    {
        public SkillSetContext() : base("name=SkillsetContext")
        {
        }

        public DbSet<SetUser> SetUsers { get; set; }
        public DbSet<SetGroup> SetGroups { get; set; }
        public DbSet<SetUserAccess> SetUserAccesses { get; set; }
        public DbSet<SetModule> SetModules { get; set; }
        public DbSet<SetGroupAccess> SetGroupAccesses { get; set; }
        public DbSet<Associate> Associates { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Skillset> Skillsets { get; set; }
        public DbSet<DepartmentSkillset> DepartmentSkillsets { get; set; }
        public DbSet<AssociateDepartmentSkillset> AssociateDepartmentSkillsets { get; set; }
    }
}
