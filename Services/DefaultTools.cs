namespace th.onlineconsign.Services
{
    public class DefaultTools : ITools
    {
        public TChild EntityCopy<TParent, TChild>(TParent parent) where TChild : TParent, new()
        {
            // The following 1st line is not possible without new() constraint:
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }
    }
}