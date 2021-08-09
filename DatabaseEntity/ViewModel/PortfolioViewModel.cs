

namespace DatabaseEntity
{
    /// <summary>
    /// Represents the Portfolio Model List to be returned to the client. <see cref="GetPortfolios"/>
    /// </summary>
    public class PortfolioViewModel
    {
        public int PortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public bool PrefixWithMinisterFor { get; set; }
    }

    public class PortfolioInfoViewModel
    {
        public int PortfolioId { get; set; }
        public bool PrefixWithMinisterFor { get; set; }
        public int DepartmentPortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public string Department { get; set; }
        public int DepartmentId { get; set; }
        public string DeptCode { get; set; }
    }
}
