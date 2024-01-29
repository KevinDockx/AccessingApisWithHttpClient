using Microsoft.AspNetCore.Mvc;

namespace Movies.Client.Models;

public class ExtendedProblemDetailsWithErrors : ProblemDetails
{
    public required Dictionary<string, string[]> Errors { get; set; }     
}
