using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using th.onlineconsign.Model;

namespace th.onlineconsign.Services
{
    public interface ISampleConsignService
    {
        Task<string> GetNewSampleConsignId();
        Task<Tuple<string, string, string, List<ItemSpec>, List<ItemGrade>, List<ItemParameter>, bool, Tuple<List<DpDelegateQuanUnit>, string>>> GetInitDataForSampleDetailPage(string sampleId, string searchkey);

        Task<List<UnitProductionUnit>> GetSearchProductor(string sampleId, string searchkey);

        Task<List<UnitProductionUnit>> GetXkzSearchResult(string putOnRecordsPassport);

        Task<UnitProductionUnit> GetXkzSurface(string xkzProductorName);
    }
}