using Movies.API.InternalModels;

namespace Movies.API.Services;

public interface ITrailersRepository
{
    Task<Trailer?> GetTrailerAsync(Guid movieId, Guid trailerId);

    Task<Trailer> AddTrailer(Guid movieId, Trailer trailerToAdd);
}
