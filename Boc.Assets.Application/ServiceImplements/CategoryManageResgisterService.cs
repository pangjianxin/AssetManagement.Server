using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class CategoryManageResgisterService : ICategoryManageResgisterService
    {
        private readonly ICategoryManageRegisterRepository _categoryManageRegisterRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public CategoryManageResgisterService(ICategoryManageRegisterRepository categoryManageRegisterRepository,
            IOrganizationRepository organizationRepository,
            IMapper mapper)
        {
            _categoryManageRegisterRepository = categoryManageRegisterRepository;
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrgDto>> GetOrgByCategoryAndOrg2(Guid categoryId, string org2)
        {

            var organizations = from org in _organizationRepository.GetAll()
                                join registrations in _categoryManageRegisterRepository.GetAll(it =>
                                        it.AssetCategoryId == categoryId && it.Org2 == org2)
                                    on org.Id equals registrations.ManagerId
                                select org;
            //var organizations = _organizationRepository.GetAll()
            //    .Where(it => _categoryOrgRegistrationRepository.GetAll(temp => temp.AssetCategoryId == categoryId && it.Org2 == org2).Select(that => that.OrganizationId).Contains(it.Id));
            return await _mapper.ProjectTo<OrgDto>(organizations).ToListAsync();
        }
    }
}