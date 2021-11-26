using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Application.Entities
{
    public enum ApiStatusCode
    {
        OtherError = 9000,
        SystemError = 8000,
        ExternalApiError = 5000,
        BusinessValidation = 1000
    }
}
