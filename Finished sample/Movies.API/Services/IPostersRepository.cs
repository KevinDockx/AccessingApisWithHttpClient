using Movies.API.InternalModels;

namespace Movies.API.Services;

public interface IPostersRepository
{
    Task<Poster?> GetPosterAsync(Guid movieId, Guid posterId);          

    Task<Poster> AddPoster(Guid movieId, Poster posterToAdd); 
}
