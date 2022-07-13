

using Services.Models;

namespace Services.Interfaces
{
    public interface ICGIBinClient
    {      
        Task<MNCountiesModel> GetCountyListAsync();
    }
}
