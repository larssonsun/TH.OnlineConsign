namespace th.onlineconsign.Services
{
    public interface ITools
    {
        TChild EntityCopy<TParent, TChild>(TParent parent) where TChild : TParent, new();

        TParent EntityCopyForParent<TChild, TParent>(TChild child) where TParent : new() where TChild : TParent;
    }
}