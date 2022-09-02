
using Services.Models;

namespace Services.Interfaces
{
    public interface ILakeFinderClient
    {
        Task<AllLakesPerCountyModel> GetLakesAsync(string countyId);
        Task<LakesSurveyModel> GetLakeSurveyAsync(string lakeId);
    }
}
