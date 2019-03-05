using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class UnitProductionUnit
    {
        public Guid ProductionUnitId { get; set; }
        public string Name { get; set; }
        public string LegalPerson { get; set; }
        public string LinkMan { get; set; }
        public string LinkManPhone { get; set; }
        public string LinkPhone { get; set; }
        public string Fax { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string ProductionUnitType { get; set; }
        public string PutOnRecordsPassport { get; set; }
        public short SurfaceFlagType { get; set; }
        public string SurfaceFlagText { get; set; }
        public byte[] SurfaceFlagPicture { get; set; }
        public decimal? Orders { get; set; }
        public string RecordsPassportOrdersPart1 { get; set; }
        public string RecordsPassportOrdersPart2 { get; set; }
        public string RecordsPassportOrdersPart3 { get; set; }
        public Guid UnitId { get; set; }
        public int PutOnRecordsPassportType { get; set; }
        public int Enable { get; set; }
        public string Bindlicences { get; set; }
    }
}
