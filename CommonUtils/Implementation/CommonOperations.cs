using AutoMapper;
using MyFirstWebAPIProject.Database.DatabaseContext;
using WebAPITrainingProject.Services.Implementation;

namespace WebAPITrainingProject.CommonUtils;

public sealed class CommonOperations<T> : ICommonOperations<T>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<T> _logger;
    private readonly IMapper _mapper;

    public CommonOperations(IApplicationDbContext dbContext, ILogger<T> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public IApplicationDbContext DbContext => _dbContext;

    public ILogger<T> Logger => _logger;

    public IMapper Mapper => _mapper;
}