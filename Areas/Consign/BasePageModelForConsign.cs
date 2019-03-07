using Microsoft.AspNetCore.Mvc.RazorPages;

public class BasePageModelForConsign : PageModel
{
    public enum ReadonlyCssClass { ReadOnly, na };

    public enum ShowHideCssClass { show, hide };

    public enum SampleUcViewComponentType
    {
        None, Default, GangJin, GangJinZl, GJGYCL, GJGYCL2, HanJie, HNT, HNTKS, HNTKZ, JNPHBDefault, JXLianJie, QLMJJ, ShuiNi, SJJC,
        SZDL, MC, Zhuan, JiShi, TuGong, GuanSha, JNTongTiaoJian, GuanSha3, JNQK, GJL, GANGBANG, YHWZ, FMH, HNTQJL, WCXKY, JNJNWQNBWB,
        JNZhiZuoRiQi, ShenShui, JxljTt, GangJinZlXkz, GGKJ, GJL355, GJL355_Ttj
    };

    public string GetSampleUcViewComponentName(string sampleUcName)
    {


        return 
            // ShuiNi
            sampleUcName == "d_sampleuc_shuini" ? SampleUcViewComponentType.ShuiNi.ToString() :
            // GangJin
            sampleUcName == "d_sampleuc_gangjin" ? SampleUcViewComponentType.GangJin.ToString() :
            sampleUcName == "d_sampleuc_gangjin_zl" ? SampleUcViewComponentType.GangJinZl.ToString() :
            sampleUcName == "d_sampleuc_gjgycl2" ? SampleUcViewComponentType.GJGYCL2.ToString() :
            sampleUcName == "d_sampleuc_gangbang" ? SampleUcViewComponentType.GANGBANG.ToString() :
            sampleUcName == "d_sampleuc_gangjin_zl_xkz" ? SampleUcViewComponentType.GangJinZlXkz.ToString() :
            // Default
            sampleUcName == "d_sampleuc_default" ? SampleUcViewComponentType.Default.ToString() :

            SampleUcViewComponentType.Default.ToString();
    }
}