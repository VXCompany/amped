using System.Threading.Tasks;

namespace Amped.Core;

public interface ICommandQueue
{
    Task Send<T>(T command) where T : class;
}