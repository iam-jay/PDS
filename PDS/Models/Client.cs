using Lombok.NET;
using PDS.Data;

namespace PDS.Models
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class Client : BaseData
    {
        public string ClientName { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public int Zipcode { get; set; }
        public String ContactNumber { get; set; } = String.Empty;
    }
}
