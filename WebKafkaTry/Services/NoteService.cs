using System.Threading;
using System.Threading.Tasks;
using WebKafkaTry.Kafka;
using WebKafkaTry.Models;

namespace WebKafkaTry.Services
{
    public class NoteService:INoteService
    {
        private readonly INoteProducer _noteProducer;

        public NoteService(INoteProducer noteProducer)
        {
            _noteProducer = noteProducer;
        }

        public async Task SendAsync(Note note, CancellationToken canellationToken)
        {
            await _noteProducer.ProduceAsync(note, canellationToken);
        }
    }
}
