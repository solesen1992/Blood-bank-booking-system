using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ServiceLayer
{
    public interface IDbnServiceConnection
    {
        string? BaseUrl { get; }

        string? UseUrl { get; set; }

        Task<HttpResponseMessage?> CallServiceGet();

        Task<HttpResponseMessage?> CallServicePut(StringContent postJson);

        Task<HttpResponseMessage?> CallServiceDelete();
    }
}
