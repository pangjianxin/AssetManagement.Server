using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Permission;
using Boc.Assets.Domain.Commands.Permissions;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IOrgRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IMemoryCache _memoryCache;

        public PermissionService(IPermissionRepository permissionRepository,
            IOrgRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IBus bus,
            IMemoryCache memoryCache)
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bus = bus;
            _memoryCache = memoryCache;
        }

        public async Task<List<PermissionDto>> GetAllListAsync()
        {
            var list = await _permissionRepository.GetAllListAsync();
            return _mapper.Map<List<PermissionDto>>(list);
        }

        public async Task ModifyRolePermission(ModifyPermission model)
        {
            _memoryCache.Remove("permissions");
            var command = _mapper.Map<ModifyPermissionCommand>(model);
            await _bus.SendCommandAsync(command);
            _memoryCache.Set("permissions", await _permissionRepository.GetAllListAsync());

        }
    }
}