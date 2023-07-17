using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POC_Shared.Certificates
{
    public class SigningAudienceCertificate : IDisposable
    {
        private readonly RSA _rsa;

        public SigningAudienceCertificate()
        {
            _rsa = RSA.Create();
        }

        public SigningCredentials GetAudienceSigningKey()
        {
            string privateXmlKey = File.ReadAllText(@"./private_key.xml");

            _rsa.FromXmlString(privateXmlKey);

            return new SigningCredentials(
                key: new RsaSecurityKey(_rsa),
                algorithm: SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            _rsa?.Dispose();
        }
    }
}
