using System.Threading.Tasks;
using WebKafkaTry.Models;

namespace WebKafkaTry.Reader.Repositories
{
    public interface INoteRepository
    {
        Task AddNoteAsync(Note note);
    }
}
