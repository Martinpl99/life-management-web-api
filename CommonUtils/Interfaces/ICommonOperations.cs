
using AutoMapper;
using MyFirstWebAPIProject.Database.DatabaseContext;
using WebAPITrainingProject.Services.Implementation;

public interface ICommonOperations<T>
{
   IApplicationDbContext DbContext { get; }
   ILogger<T> Logger { get;  }
   IMapper Mapper { get; }
}