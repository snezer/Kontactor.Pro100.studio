using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Repository
{
    public interface IOccupationRepository
    {
        Task<IEnumerable<Occupation>> GetOccupationsAsync();

        Task<Occupation> CreateAsync(Occupation model);

        Task<bool> UpdateAsync(Occupation model);
    }
}
