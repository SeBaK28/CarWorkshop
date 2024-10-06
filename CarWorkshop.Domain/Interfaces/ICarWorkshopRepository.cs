using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopRepository
    {
        Task Create(Domain.Entities.CarWorkshop carWorkshop); //create entity carWorkshop
        Task <Domain.Entities.CarWorkshop?> GetByName(string name);//search by name
        Task <IEnumerable<Domain.Entities.CarWorkshop>> GetAll();
        Task <Domain.Entities.CarWorkshop> GetByEncodedName (string encededName);
        Task Commit();
    }
}
