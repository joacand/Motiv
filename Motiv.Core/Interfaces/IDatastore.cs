using System.Threading.Tasks;

namespace Motiv.Core.Interfaces
{
    public interface IDatastore<T>
    {
        Task Save(T toSave);
        Task<T> Load();
        Task Clear();
    }
}
