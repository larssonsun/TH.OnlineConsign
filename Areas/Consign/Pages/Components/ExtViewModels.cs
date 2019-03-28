using System.Collections.Generic;
using th.onlineconsign.Model;

namespace th.onlineconsign.ExtViewModels
{
    public class SampleStorageMainExt : SampleStorageMain
    {
        public List<ItemParameterExt> Parameters { get; set; }
        public string SampleUcViewComponentViewName { get; set; }
    }

    public class ItemParameterExt : ItemParameter
    {
        public bool Checked { get; set; }
    }

    public class SampleStorageAddonGangJinExt : SampleStorageAddonGangJin
    {
        public string GjBianMiaoBiaoShiImageBase64 { get; set; }
    }
}
