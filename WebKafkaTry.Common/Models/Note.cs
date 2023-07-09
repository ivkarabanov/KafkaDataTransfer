using System;
using System.Collections.Generic;

namespace WebKafkaTry.Models
{
    public class Note
    {
        public Note(string theme, string text, List<int> categoryIds)
        {
            Theme = theme;
            Text = text;
            CategoryIds = categoryIds;
            CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; }
        public string Theme { get; }
        public List<int> CategoryIds { get; }
        public string Text { get; }
    }
}
