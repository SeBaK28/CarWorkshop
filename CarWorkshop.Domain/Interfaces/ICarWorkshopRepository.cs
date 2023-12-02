using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopRepository
    {
        Task Create(Domain.Entities.CarWorkshop carWorkshop); //tworzenie encji typu carWorkshop
        Task <Domain.Entities.CarWorkshop?> GetByName(string name);//wyszukaj po Nazwie 
        Task <IEnumerable<Domain.Entities.CarWorkshop>> GetAll();//metoda zwraca litę CarWorkshop dla usera
        Task <Domain.Entities.CarWorkshop> GetByEncodedName (string encededName);
        Task Commit();
    }
}
