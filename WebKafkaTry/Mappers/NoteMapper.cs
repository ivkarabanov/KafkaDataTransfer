using Google.Protobuf.WellKnownTypes;
using System.Linq;
using WebKafkaTry.Models;
using WebKafkaTry.Proto;

namespace WebKafkaTry.Mappers
{
    public static class NoteMapper
    {
        public static KafkaNote ToKafkaNote(this Note note)
        {
            return new KafkaNote()
            {
                CategoryIds = { note.CategoryIds.Select(x=> (long)x) },
                CreatedOn = Timestamp.FromDateTimeOffset(note.CreatedOn),
                Text = note.Text,
                Theme = note.Theme
            };
        }
    }
}
