using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopRepository : ICarWorkshopRepository //jest to implementacja interfejsu Domain.Interfaces.ICarWorkshopRepository
    {
        private CarWorkshopDbContext _dbContext;

        public CarWorkshopRepository(CarWorkshopDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task Create(Domain.Entities.CarWorkshop carWorkshop) //metoda Create() pochodzi z ICarWorkshopRepository
        {
            _dbContext.Add(carWorkshop); //dodaje do listy Domain.Entities.CarWorkshop podany element 
            await _dbContext.SaveChangesAsync(); //zapisuje zmiany
        }
        public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAll()//funkcja wyświetlająca wszystkie warsztaty
            => await _dbContext.CarWorkshops.ToListAsync();

        public async Task<Domain.Entities.CarWorkshop> GetByEncodedName(string encededName)
            => await _dbContext.CarWorkshops.FirstAsync(c => c.EncodedName == encededName);

        public Task Commit()
            => _dbContext.SaveChangesAsync();

        public Task<Domain.Entities.CarWorkshop?> GetByName(string name)
            => _dbContext.CarWorkshops.FirstOrDefaultAsync(cw=>cw.Name.ToLower() == name);
    }
}
