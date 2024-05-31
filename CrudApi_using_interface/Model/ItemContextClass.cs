using Microsoft.EntityFrameworkCore;

namespace CrudApi_using_interface.Model
{
    public class ItemContextClass: DbContext
    {
        public ItemContextClass(DbContextOptions<ItemContextClass> options) : base(options)
        {

        }
        public DbSet<ItemClass> ItemClass { get; set; }
    }
}
