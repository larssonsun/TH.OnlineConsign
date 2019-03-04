using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class ItemParameter
    {
        public Guid Id { get; set; }
        public int ParameterId { get; set; }
        public int? SampleId { get; set; }
        public string ParameterName { get; set; }
        public bool? IsDefault { get; set; }
        public int? TestTimes { get; set; }
        public int? ReTestTimes { get; set; }
        public string TestStandard { get; set; }
        public string SetValue { get; set; }
        public string PassResult { get; set; }
        public string UnPassResult { get; set; }
        public string SetValueDescription { get; set; }
        public double? MengStep { get; set; }
        public double? TestFee { get; set; }
        public string ParameterDescription { get; set; }
        public string ParameterUc { get; set; }
        public string CriterionDescription { get; set; }
        public int? ChargeMode { get; set; }
        public int? SpecimenNumber { get; set; }
        public int? CanConsign { get; set; }
        public string AssessParmId { get; set; }
        public int? MustBeDetectFlag { get; set; }
        public int? GroupDetectFlag { get; set; }
        public int? RejectDetectFlag { get; set; }
        public int? DetectionPeriodFlag { get; set; }
        public double? Rebate { get; set; }
    }
}
