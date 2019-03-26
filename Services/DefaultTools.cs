using System.Linq;

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

        public TParent EntityCopyForParent<TChild, TParent>(TChild child) where TParent : new() where TChild : TParent
        {
            TParent parent = new TParent();
            var propChild = typeof(TChild).GetProperties();
            var propParent = typeof(TParent).GetProperties();
            foreach (var propertyChild in propChild)
            {
                if (propertyChild.CanRead && propertyChild.CanWrite)
                {
                    if(propParent.Any(x=>x.Name == propertyChild.Name))
                        propertyChild.SetValue(parent, propertyChild.GetValue(child, null), null);
                }
            }
            return parent;
        }
    }
}