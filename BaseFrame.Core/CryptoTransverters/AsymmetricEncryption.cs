using BaseFrame.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.CryptoTransverters
{
    /// <summary>
    /// 不对称加密
    /// </summary>
    public class AsymmetricEncryption : IEncrypt, IDecrypt
    {
        public static AsymmetricEncryption Default { get; set; }
        static AsymmetricEncryption()
        {
            Default = new AsymmetricEncryption();
            //Default.FromXmlString("<RSAKeyValue><Modulus>1HNWtnaBRKmypOBwjNw+kjJVziajQRZsb72fu6TacLGXK9Aa2lluz0E24tXTtQ5+b8OSUkjpqFpDC/sKH93ir65GsgEX26wrVNWzMw57fXcHDZaH7a6L55QTknCfr3re4xDCIDLHHMFw6lksWkkTj2d/BhiOsG0o28VLTEAZo8U=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
            Default.FromXmlString("<RSAKeyValue><Modulus>1HNWtnaBRKmypOBwjNw+kjJVziajQRZsb72fu6TacLGXK9Aa2lluz0E24tXTtQ5+b8OSUkjpqFpDC/sKH93ir65GsgEX26wrVNWzMw57fXcHDZaH7a6L55QTknCfr3re4xDCIDLHHMFw6lksWkkTj2d/BhiOsG0o28VLTEAZo8U=</Modulus><Exponent>AQAB</Exponent><P>7ozP5ZPigrDRJvPlRd3M59lBA2hGF5+4ZbDdXJ38IyukrwYx4iNQEIeSkIaATz8uhOGsnTUd5Q7rJeQJ4ExEJw==</P><Q>4/3GwKd/nrMLGVpZP83OH8nhEE8jE/XKmI0sbQGdpbTtTHMP+ohnNj5egd9/UT8WFvNi77/Q0g4w4JjNP5FwMw==</Q><DP>zanNG0sygCZdS83+nwuou0LNEFj4BSoI2nNvhYgXd2MLKnKW0zZKstWPgNbVDH2WZ64BGdBPo8oG5bYC5cQbBQ==</DP><DQ>yAkYgl40Bez3lwYDeoy7Bn8dPi8BCvlECXcL/JRHWmWVMtddtKZLUHeGehK7ZXChk/911f8JW9PhpJ3Mr7KOqw==</DQ><InverseQ>jY3GfT3gW8aCZNce1GTRnUU08PrizsPmmacppsf43nfmlGGUxe7+oHDJrq1DS+SVsugWk80zhGzBRxUdgNRKiw==</InverseQ><D>H4+i6ihgu9qZ93SAQ+zUPtPLe3yx9BFoupDpEe9DpEo9svyPgLCYjaGajchGUzd8AQ6ExkSesav3GeiJcIJ+EmX6Y/XzmvrK91b6UHmiqHjz1yDv6T+ravX3av2kPZTbux/2W1FfcIvwqmehQAabZykqbr3+cZCh3jJyaWQIg2E=</D></RSAKeyValue>");
        }

        RSACryptoServiceProvider _rsa;

        public AsymmetricEncryption()
        {
            _rsa = new RSACryptoServiceProvider();
        }

        #region 导入密钥
        public void FromXmlString(string xmlString)
        {
            _rsa.FromXmlString(xmlString);
        }

        public void ImportParameters(RSAParameters parameters)
        {
            _rsa.ImportParameters(parameters);
        }

        public void ImportCspBlob(byte[] keyBlob)
        {
            _rsa.ImportCspBlob(keyBlob);
        }
        #endregion

        #region 导出密钥
        public string ToXmlString(bool includePrivateParameters)
        {
            return _rsa.ToXmlString(includePrivateParameters);
        }

        public RSAParameters ExportParameters(bool includePrivateParameters)
        {
            return _rsa.ExportParameters(includePrivateParameters);
        }

        public byte[] ExportCspBlob(bool includePrivateParameters)
        {
            return _rsa.ExportCspBlob(includePrivateParameters);
        }
        #endregion

        #region 加密
        public string Encrypt(string inString)
        {
            return Encrypt(inString.FromUTF8String()).ToBase64String();
        }

        public byte[] Encrypt(byte[] inputBuffer)
        {
            return _rsa.Encrypt(inputBuffer, false);
        }

        public byte[] Encrypt(byte[] inputBuffer, bool fOAEP)
        {
            return _rsa.Encrypt(inputBuffer, fOAEP);
        }
        #endregion

        #region 解密
        public string Decrypt(string inString)
        {
            return Decrypt(inString.FromBase64String()).ToUTF8String();
        }

        public byte[] Decrypt(byte[] inputBuffer)
        {
            return _rsa.Decrypt(inputBuffer, false);
        }

        public byte[] Decrypt(byte[] inputBuffer, bool fOAEP)
        {
            return _rsa.Decrypt(inputBuffer, fOAEP);
        }
        #endregion
    }
}
