using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo
{
    interface IAuthenticate
    {
        Task Authenticate(MobileServiceAuthenticationProvider provider);
    }
}
