namespace CrudApi_using_interface.Model
{
    public interface ItemInterface
    {
        List<ItemClass> GetallItem();
        ItemClass Get_specific_item(int id);
        ItemClass Put_item(int id, ItemClass i1);
        void Deleteall_Item();
        ItemClass Post_item(ItemClass newItem);
    }
}
 