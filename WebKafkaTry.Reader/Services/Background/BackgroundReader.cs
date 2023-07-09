using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebKafkaTry.Reader.Kakfa;
using WebKafkaTry.Reader.Repositories;

namespace WebKafkaTry.Reader.Services.Background
{
    public sealed class BackgroundReader : BackgroundService
    {
        private readonly INoteConsumer _noteConsumer;
        private readonly ILogger<BackgroundReader> _logger;
        private readonly INoteRepository _noteRepository;

        public BackgroundReader(INoteConsumer noteConsumer, ILogger<BackgroundReader> logger, INoteRepository noteRepository)
        {
            _noteConsumer = noteConsumer;
            _logger = logger;
            _noteRepository = noteRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start to consume notes");

            try
            {
                await _noteRepository.AddNoteAsync(new Models.Note("Test", "Test", new List<int>() { 1, 2, 3 }));
                _noteConsumer.LaunchConsume(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occured trying to consume note messages: {ex.Message}");
            }

        }
    }
}
