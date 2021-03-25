namespace UserMap.Services
{
    public interface ISavable
    {
        bool HasChangedElements();

        System.Threading.Tasks.Task SaveChangesAsync();
        void RestoreChanges();

        event System.EventHandler ElementChanged;
    }
}
