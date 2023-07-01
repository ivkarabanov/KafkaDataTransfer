using System.Threading;
using System.Threading.Tasks;
using WebKafkaTry.Models;

namespace WebKafkaTry.Kafka
{
    public interface INoteProducer
    {
        Task ProduceAsync(Note note, CancellationToken cancellationToken);
    }
}
