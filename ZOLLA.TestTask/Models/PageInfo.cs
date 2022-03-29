using System;

namespace ZOLLA.TestTask.Models
{
    public class PageInfo
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalSize / Size);
        public int TotalSize { get; set; }
    }
}