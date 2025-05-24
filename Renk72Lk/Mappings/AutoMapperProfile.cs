using AutoMapper;
using Renk72Lk.DataAccess.Entities;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();
        CreateMap<BidAttachmentsEntity, BidAttachmentsModel>().ReverseMap();
        CreateMap<AuthHistoryEntity, AuthHistoryModel>().ReverseMap();
        CreateMap<BidEntity, BidModel>().ReverseMap();
        CreateMap<BidPersonalInfoEntity, BidPersonalInfoModel>().ReverseMap();
        CreateMap<BidRepresentativeInfoEntity, BidRepresentativeInfoModel>().ReverseMap();
        CreateMap<BidConnectionObjectInfoEntity, BidConnectionObjectInfoModel>().ReverseMap();
        CreateMap<BidTechnicalSpecificationsEntity, BidTechnicalSpecificationsModel>().ReverseMap();
        CreateMap<AttachmentsPointEntity, AttachmentsPointModel>().ReverseMap();
        CreateMap<AttachmentsStageEntity, AttachmentsStageModel>().ReverseMap();
        CreateMap<AddressEntity, AddressModel>().ReverseMap();

        CreateMap<MessageEntity, MessageModel>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)).ReverseMap();

        CreateMap<AttachmentFileEntity, AttachmentFileModel>().ReverseMap();
    }
}
