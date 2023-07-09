using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKafkaTry.Reader.Services
{
    public class AddNoteRequest : IRequest
    {
        public AddNoteRequest(AddNoteRequest note)
        {
            Note = note;
        }

        public AddNoteRequest Note { get; }
    }
}
