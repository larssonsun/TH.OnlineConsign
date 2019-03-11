using System;

namespace th.onlineconsign.Services
{
    public interface ISampleUcControler
    {
        Tuple<string, string, bool> GetSampleUcViewComponentInfo(string sampleUcName, string sampleId);
        bool GetIfShouldAddScript(string sampleUcName);
    }
}