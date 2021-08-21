using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;
using KONTAKTOR.Service.Services.DocxTemplating;
using kontaktor_network.DA.Models;
using KONTRAKTOR.DA.Models;
using KONTRAKTOR.Services.Contract.Letter;

namespace KONTAKTOR.Service.Services.ContractTempating
{
    public class ContractGenerationService
    {
        private RentFactRepository _rents;
        private CompanyRepository _companies;
        private CompartmentRepository _compartments;
        private UserInformationRepository _users;
        private TenancyRepository _tenants;
        private DocXGenerationService _generator;

        public ContractGenerationService(DocXGenerationService generator, RentFactRepository rents, TenancyRepository tenants, UserInformationRepository users, CompanyRepository companies, CompartmentRepository compartments)
        {
            _rents = rents;
            _companies = companies;
            _compartments = compartments;
            _users = users;
            _tenants = tenants;

            _generator = generator;
        }

        public async Task<byte[]> CreateContract(string rentId)
        {
            var rent = await _rents.GetAsync(rentId);
            var tenant = await _tenants.GetAsync( rent.TenantId );
            var user = await _users.GetAsync(tenant.UserInformationId);
            var company = await _companies.GetAsync(tenant.CompanyId);
            var compartment = await _compartments.GetAsync(rent.CompartmentId);

            var templateData = new ContractDataBuilder(rent, tenant, user, company, compartment).Build();
            var templateFullPath = CurrentPathHelper.GetPathToReports() +"\\Data\\contract_template.docx";
            return await _generator.CreateDocumentBodyAsync<CommonContractTemplateData>(templateFullPath, templateData);
        }
    }

    
}
