using AutoMapper;
using Notification.Commands;
using Notification.DTOs;
using Notification.Entities;

namespace Notification.Mapping
{
    public class TaskProfile:Profile
    {

        public TaskProfile()
        {
            CreateMap<TaskItem, TaskDto>();

            CreateMap<CreateTaskCommand, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
        }

    }
}
