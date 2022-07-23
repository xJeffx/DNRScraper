
using Services.Models;

namespace Services.Interfaces
{
    public interface ILakeFinderClient
    {
        Task<AllCountyLakesModel> GetLakesAsync(string countyId);
        Task<LakesSurveyModel> GetLakeSurveyAsync(string lakeId);
    }
}
