using BaseFrame.Core.Extensions;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.Helpers
{
    public class RSACSharpJavaConvertHelper
    {
        static string RSAPublicKeyCSharpToJava(RsaKeyParameters rkp)
        {
            SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(rkp);
            return spki.ToAsn1Object().GetDerEncoded().ToBase64String();
        }

        static string RSAPrivateKeyCSharpToJava(RsaKeyParameters rkp)
        {
            PrivateKeyInfo pki = PrivateKeyInfoFactory.CreatePrivateKeyInfo(rkp);
            return pki.ToAsn1Object().GetEncoded().ToBase64String();
        }

        public static string RSAPublicKeyCSharpToJava(RSAParameters parameters)
        {
            BigInteger modulus = new BigInteger(1, parameters.Modulus);
            BigInteger publicExponent = new BigInteger(1, parameters.Exponent);
            RsaKeyParameters publicKey = new RsaKeyParameters(false, modulus, publicExponent);
            return RSAPublicKeyCSharpToJava(publicKey);
        }

        public static string RSAPrivateKeyCSharpToJava(RSAParameters parameters)
        {
            BigInteger modulus = new BigInteger(1, parameters.Modulus);
            BigInteger publicExponent = new BigInteger(1, parameters.Exponent);
            BigInteger d = new BigInteger(1, parameters.D);
            BigInteger p = new BigInteger(1, parameters.P);
            BigInteger q = new BigInteger(1, parameters.Q);
            BigInteger dP = new BigInteger(1, parameters.DP);
            BigInteger dQ = new BigInteger(1, parameters.DQ);
            BigInteger qInv = new BigInteger(1, parameters.InverseQ);
            RsaPrivateCrtKeyParameters privateCrtKey = new RsaPrivateCrtKeyParameters(modulus, publicExponent, d, p, q, dP, dQ, qInv);
            return RSAPrivateKeyCSharpToJava(privateCrtKey);
        }

        public static RSAParameters RSAPublicKeyJavaToCSharp(string javaParameters)
        {
            RsaKeyParameters publicKey = (RsaKeyParameters)PublicKeyFactory.CreateKey(javaParameters.FromBase64String());
            return new RSAParameters()
            {
                Modulus = publicKey.Modulus.ToByteArrayUnsigned(),
                Exponent = publicKey.Exponent.ToByteArrayUnsigned()
            };
        }

        public static RSAParameters RSAPrivateKeyJavaToCSharp(string javaParameters)
        {
            RsaPrivateCrtKeyParameters privateKey = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(javaParameters.FromBase64String());
            return new RSAParameters()
            {
                Modulus = privateKey.Modulus.ToByteArrayUnsigned(),
                Exponent = privateKey.PublicExponent.ToByteArrayUnsigned(),
                D = privateKey.Exponent.ToByteArrayUnsigned(),
                P = privateKey.P.ToByteArrayUnsigned(),
                Q = privateKey.Q.ToByteArrayUnsigned(),
                DP = privateKey.DP.ToByteArrayUnsigned(),
                DQ = privateKey.DQ.ToByteArrayUnsigned(),
                InverseQ = privateKey.QInv.ToByteArrayUnsigned()
            };
        }
    }
}
