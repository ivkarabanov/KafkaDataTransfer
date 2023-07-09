using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebKafkaTry.Common.Models;
using WebKafkaTry.Models;
using WebKafkaTry.Reader.Repositories;

namespace WebKafkaTry.Common.Repositories
{
    public class NoteRepository: INoteRepository
    {
        private readonly string _connectionString;

        public NoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddNoteAsync(Note note)
        {
			using (var connection = new SqlConnection(_connectionString))
			{
				var sql = "INSERT INTO Notes (Theme, Text, CreatedOn) VALUES (@Theme, @Text, @CreatedOn);" +
                    "SELECT SCOPE_IDENTITY();";
				var noteId = await connection.QueryAsync<int>(sql, note);

                var sqlCategories = "INSERT INTO NoteCategories (NoteId, CategoryId) VALUES (@NoteId, @CategoryId);";
                var categories = new List<NoteCategory>();
                foreach (var caregoryId in note.CategoryIds)
                {
                    await connection.ExecuteAsync(sqlCategories, new { NoteId  = noteId, CategoryId = caregoryId});
                }

                await connection.ExecuteAsync(sqlCategories, categories);
            }
		}
    }
}
