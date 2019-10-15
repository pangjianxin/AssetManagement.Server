using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Permission;
using Boc.Assets.Domain.Commands.Permissions;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IMemoryCache _memoryCache;

        public PermissionService(IPermissionRepository permissionRepository,
            IMapper mapper,
            IBus bus,
            IMemoryCache memoryCache)
        {
            _permissionRepository = permissionRepository;
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
            var permissions = await GetAllListAsync();
            _memoryCache.Set<IEnumerable<PermissionDto>>("permissions", permissions);

        }
    }
}