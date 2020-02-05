using System.Collections.Generic;
using System.Threading.Tasks;

namespace bushour.api
{
    public interface IBusHourService
    {
        Task<IEnumerable<LinhaVM>> GetBusId(string id);

        Task<IEnumerable<LinhaVM>> GetBusName(string name);

        Task<IEnumerable<HorarioVM>> GetHours(string id);

    }
}
