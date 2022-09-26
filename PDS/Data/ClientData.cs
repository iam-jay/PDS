using Lombok.NET;
using PDS.Models;
using System.ComponentModel.DataAnnotations;

namespace PDS.Data
{
    [AllArgsConstructor]
    [NoArgsConstructor]
    public partial class ClientData : Client
    {
        public ClientData addClientDetails(Client client)
        {
            this.ClientName = client.ClientName;
            this.City = client.City;
            this.ContactNumber = client.ContactNumber;
            this.Country = client.Country;
            this.Location = client.Location;
            this.Zipcode = client.Zipcode;
            this.Email = client.Email;
            this.Password = client.Password;
            return this;
        }
    }

}
