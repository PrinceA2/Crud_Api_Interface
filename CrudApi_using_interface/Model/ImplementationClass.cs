
namespace CrudApi_using_interface.Model
{
    public class ImplementationClass : ItemInterface
    {

        private readonly ItemContextClass _context;

        public ImplementationClass(ItemContextClass context)
        {
            _context = context;
        }

        public void Deleteall_Item()
        {
            //Retrieve all items from the database
            var items = _context.ItemClass.ToList();

            //Delete all items from the database
            _context.ItemClass.RemoveRange(items);

            //Save the Changes to the database
            _context.SaveChanges();
        }

        public List<ItemClass> GetallItem()
        {
            return _context.ItemClass.ToList(); 
        }

        public ItemClass Get_specific_item(int id)
        {
            var item = _context.ItemClass.FirstOrDefault(i => i.ID == id);

            return item;
        }

        public ItemClass Post_item(ItemClass newItem)
        {
            // Add the new_item to the item.
            _context.ItemClass.Add(newItem);

            //Saving the changes
            _context.SaveChanges();
            return newItem;
        }

        public ItemClass Put_item(int id, ItemClass i1)
        {
            var existing_item = _context.ItemClass.FirstOrDefault(i => i.ID == id);
            if (existing_item != null)
            {
                existing_item.Name = i1.Name;
                existing_item.Description = i1.Description;
            }
            _context.SaveChanges();
            return existing_item;
        }
    }
}