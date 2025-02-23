using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTO.Response
{
    public record ServiceResponse(bool Succcess = false, string Message = null!);
}
