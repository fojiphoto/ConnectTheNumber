using System.Collections;

namespace Ilumisoft.Hex.Operations
{
    public interface IOperation
    {
        IEnumerator Execute();
    }
}