using System;

namespace HomeWork27_06_19
{
    public class News
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public override string ToString()
        {
            return $"{Title}({PublishedDate.ToString("dd.MM.yy")})";
        }
    }
}
