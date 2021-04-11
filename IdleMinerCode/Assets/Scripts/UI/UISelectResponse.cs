using Komastar.IdleMiner.Data;

namespace Komastar.IdleMiner.UI
{
    public class UISelectResponse<T> where T : IDataObject
    {
        public T Data;

        public UISelectResponse(T data)
        {
            Data = data;
        }
    }
}
