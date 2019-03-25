using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class SampleStorageMain
    {
        public Guid Id { get; set; }
        public string SampleNo { get; set; }
        public string ContractSignNumber { get; set; }
        public string JzcertificateNo { get; set; }
        public string QycertificateNo { get; set; }
        public DateTime DetectonDate { get; set; }
        public string ProJectPart { get; set; }
        public string ProduceFactory { get; set; }
        public string RecordCertificate { get; set; }
        public double DelegateQuan { get; set; }
        public string DelegateQuanUnit { get; set; }
        public DateTime MoldingDate { get; set; }
        public int AgeTime { get; set; }
        public int KindId { get; set; }
        public int ItemId { get; set; }
        public int SampleId { get; set; }
        public int SpecId { get; set; }
        public int GradeId { get; set; }
        public string ExamParameter { get; set; }
        public Guid OperatorUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LastEditDateTime { get; set; }
        public string KindName { get; set; }
        public string ItemName { get; set; }
        public string SampleName { get; set; }
        public string SpecName { get; set; }
        public string GradeName { get; set; }
        public string ExamParameterCn { get; set; }
        public string SampleUcDbTableName { get; set; }
    }
}
