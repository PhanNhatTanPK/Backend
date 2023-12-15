namespace TRANGTRAICHANNUOI.DTO
{
    public class PagingModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}