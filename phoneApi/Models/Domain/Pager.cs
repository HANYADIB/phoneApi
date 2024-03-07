namespace phoneApi.Models.Domain
{
    public class Pager
    {
        public int Totalitem { get; set; }
        public int Crrentpage { get; set; }
        public int PageSize { get; set; }
        public int Totalpage { get; set; }
        public int Startpage { get; set; }
        public int Endpage { get; set; }
        public Pager()
        {

        }
        public Pager(int totalitem, int page, int pagesize = 8)
        {
            int totalpage = (int)Math.Ceiling((decimal)totalitem / (decimal)pagesize);
            int currentpage = page;
            int startpage = currentpage - 4;
            int endpage = currentpage + 3;
            if (startpage <= 0)
            {
                endpage = endpage - (startpage - 1);
                startpage = 1;
            }
            if (endpage > totalpage)
            {
                endpage = totalpage;
                if (endpage > 8)
                {
                    startpage = endpage - 7;
                }
            }
            Totalpage = totalpage;
            Crrentpage = currentpage;
            PageSize = pagesize;
            Totalitem = totalitem;
            Startpage = startpage;
            Endpage = endpage;
        }
    }
}
