using System.Threading.Tasks;

namespace Motiv.Interfaces
{
    public interface IDatastore<T>
    {
        Task Save(T toSave);
        Task<T> Load();
        Task Clear();
    }
}
