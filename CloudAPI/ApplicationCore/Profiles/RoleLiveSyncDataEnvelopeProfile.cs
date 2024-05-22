using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Sync;
using ItemModule.ApplicationCore.DTO.Sync;
using UserRoleModule.ApplicationCore.DTO.Sync;
using CloudAPI.ApplicationCore.DTO.Sync;
using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using ItemCabinetModule.ApplicationCore.DTO.Sync;
using System;

namespace CloudAPI.ApplicationCore.Profiles
{
    [Obsolete]
    public class RoleLiveSyncDataEnvelopeProfile : Profile
    {
        public RoleLiveSyncDataEnvelopeProfile()
        {
            CreateMap<CabinetLiveSyncEnvelope, RoleLiveSyncDataEnvelope>();
            CreateMap<UserRoleLiveSyncEnvelope, RoleLiveSyncDataEnvelope>();
            CreateMap<ItemLiveSyncEnvelope, RoleLiveSyncDataEnvelope>();
            CreateMap<ItemCabinetLiveSyncEnvelope, RoleLiveSyncDataEnvelope>();
            CreateMap<UserLiveSyncDataEnvelope, RoleLiveSyncDataEnvelope>();
        }
    }
}
