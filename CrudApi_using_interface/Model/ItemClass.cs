using System.ComponentModel.DataAnnotations;

namespace CrudApi_using_interface.Model
{
    public class ItemClass
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
