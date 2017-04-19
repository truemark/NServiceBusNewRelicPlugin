using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System;
using System.Security;

namespace NServiceBusNewRelicPlugin
{
    class ConnectionUtil
    {
        public static ConnectSession connect(String host, String domain, String username, String password) {
            SecureString securepassword = new SecureString();
            foreach (Char p in password.ToCharArray())
            {
                securepassword.AppendChar(p);
            }


            CimCredential Credentials = new CimCredential(PasswordAuthenticationMechanism.Kerberos
                 + "", domain, username, securepassword);

            WSManSessionOptions SessionOptions = new WSManSessionOptions();
            SessionOptions.AddDestinationCredentials(Credentials);

            return new ConnectSession(CimSession.Create(host, SessionOptions));
        }
    }
}
