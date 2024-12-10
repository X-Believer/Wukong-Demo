namespace WukongDemo.Util.Responses
{
    public class PaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public required IEnumerable<T> Data { get; set; }
    }
}
