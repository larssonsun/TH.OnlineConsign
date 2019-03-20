namespace th.onlineconsign.Services
{
    public interface ITools
    {
        TChild EntityCopy<TParent, TChild>(TParent parent) where TChild : TParent, new();
    }
}