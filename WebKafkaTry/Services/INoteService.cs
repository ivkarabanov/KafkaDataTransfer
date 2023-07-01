using System.Threading;
using System.Threading.Tasks;
using WebKafkaTry.Models;

namespace WebKafkaTry.Services
{
    public interface INoteService
    {
        Task SendAsync(Note note, CancellationToken canellationToken);
    }
}
