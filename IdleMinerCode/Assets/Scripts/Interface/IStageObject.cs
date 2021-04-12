using Komastar.IdleMiner.Data;

namespace Komastar.IdleMiner.Interface
{
    public interface IInteractable
    {
        IInteractResult Interact();
    }

    public interface IInteractResult : IDataObject
    {
        int Amount { get; set; }
    }
}
