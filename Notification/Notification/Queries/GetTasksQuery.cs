using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.DTOs;

namespace Notification.Queries
{
    public class GetTasksQuery : IRequest<List<TaskDto>> { }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
