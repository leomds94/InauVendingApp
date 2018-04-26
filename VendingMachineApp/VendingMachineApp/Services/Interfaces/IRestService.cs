using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachineApp.Models;

namespace VendingMachineApp.Services.Interfaces
{
    public interface IRestService
    {
        Task<List<ProductMachine>> RefreshDataAsync { get; set; }
    }
}
