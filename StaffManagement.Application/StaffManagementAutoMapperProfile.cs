using AutoMapper;
using StaffManagement.Staffs;

namespace StaffManagement.Application;
public class StaffManagementAutoMapperProfile : Profile
{
    public StaffManagementAutoMapperProfile()
    {
        MapStaffs();
    }
    void MapStaffs()
    {
        CreateMap<CreateStaffDto, Staff>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x=>Guid.NewGuid()));
        CreateMap<UpdateStaffDto, Staff>();
        CreateMap<Staff, StaffDto>();
    }
}