using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Repository
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(string id, IDbTransaction transaction = null);
        Task<Company> CreateAsync(Company company);
        Task<bool> UpdateAsync(Company company);
    }
}
