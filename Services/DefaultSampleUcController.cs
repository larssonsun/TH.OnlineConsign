using System;

namespace th.onlineconsign.Services
{
    public class DefaultSampleUcController : ISampleUcControler
    {
        public string UseXKZsampleIdStr = "|112201|112202|122701|122702|112225|112226|122725|122726|";//使用许可证的sample字符串

        private string XhzOperator(string sampleId, string orgSampleucName)
        {
            string sampleucName = orgSampleucName;

            //含许可证的uc
            if ("d_sampleuc_gangjin_zl".Equals(sampleucName.ToString()) && GetIfSampleUseXKZ(sampleId))
                sampleucName = "d_sampleuc_gangjin_zl_xkz";

            return sampleucName;
        }

        private bool GetIfSampleUseXKZ(string sampleId)
        {
            if (sampleId == null)
                return false;
            else
                return UseXKZsampleIdStr.IndexOf("|" + sampleId + "|") >= 0;
        }

        public enum SampleUcViewComponentViewType
        {
            None, Default, GangJin, GangJinZl, GJGYCL, GJGYCL2, HanJie, HNT, HNTKS, HNTKZ, JNPHBDefault, JXLianJie, QLMJJ, ShuiNi, SJJC,
            SZDL, MC, Zhuan, JiShi, TuGong, GuanSha, JNTongTiaoJian, GuanSha3, JNQK, GJL, GANGBANG, YHWZ, FMH, HNTQJL, WCXKY, JNJNWQNBWB,
            JNZhiZuoRiQi, ShenShui, JxljTt, GangJinZlXkz, GGKJ, GJL355, GJL355_Ttj
        };

        public Tuple<string, string> GetSampleUcViewComponentInfo(string sampleUcName, string sampleId)
        {
            sampleUcName = XhzOperator(sampleId, sampleUcName);

            SampleUcViewComponentViewType viewName;
            SampleUcViewComponentViewType componentName;
            
            // ShuiNi
            switch(sampleUcName)
            {
                case "d_sampleuc_shuini":
                    viewName = SampleUcViewComponentViewType.ShuiNi;
                    componentName = SampleUcViewComponentViewType.ShuiNi;
                    break;

                case "d_sampleuc_gangjin":
                    viewName = SampleUcViewComponentViewType.GangJin;
                    componentName = SampleUcViewComponentViewType.GangJin;
                    break;
                case "d_sampleuc_gangjin_zl_xkz":
                    viewName = SampleUcViewComponentViewType.GangJinZlXkz;
                    componentName = SampleUcViewComponentViewType.GangJin;
                    break;
                default:
                    viewName = SampleUcViewComponentViewType.Default;
                    componentName = SampleUcViewComponentViewType.Default;
                    break;
            }

            return new Tuple<string, string>(componentName.ToString(), viewName.ToString());



            // // GangJin
            // viewName == "d_sampleuc_gangjin_zl" ? SampleUcViewComponentType.GangJinZl.ToString() :
            // viewName == "d_sampleuc_gjgycl2" ? SampleUcViewComponentType.GJGYCL2.ToString() :
            // viewName == "d_sampleuc_gangbang" ? SampleUcViewComponentType.GANGBANG.ToString() :
        }

        public bool GetIfShouldAddScript(string sampleUcName)
        {
            //  TODO: c# / 2019-03-07 16_37 / here depends on the different sampleuc to decide whether to load the corresponding js file
            return false;
        }
    }
}