using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class ItemSample
    {
        public Guid Id { get; set; }
        public int SampleId { get; set; }
        public int ItemId { get; set; }
        public string SampleName { get; set; }
        public string SampleSymbol { get; set; }
        public string SampleJudge { get; set; }
        public int? SpecSn { get; set; }
        public int? GradeSn { get; set; }
        public int? ParameterSn { get; set; }
        public string SampleDescription { get; set; }
        public string RecodeDot { get; set; }
        public string ReportDot { get; set; }
        public int? CanConsign { get; set; }
        public string SampleUc { get; set; }
        public string SampleUnit { get; set; }
        public string DownloadTarget { get; set; }
        public int? KeyBuildingMaterials { get; set; }
    }
}
