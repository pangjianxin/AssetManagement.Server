using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Permission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAllListAsync();
        Task ModifyRolePermission(ModifyPermission model);
    }
}