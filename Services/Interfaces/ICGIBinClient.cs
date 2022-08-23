

using Services.Models;

namespace Services.Interfaces
{
    public interface ICgiBinClient
    {      
        Task<MNCountiesModel> GetCountyListAsync();
    }
}
