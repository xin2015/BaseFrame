using BaseFrame.Core.Extensions;
using BaseFrame.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.CryptoTransverters
{
    /// <summary>
    /// 对称加密
    /// </summary>
    public class SymmetricalEncryption : IEncrypt, IDecrypt
    {
        public static SymmetricalEncryption Default { get; set; }
        static SymmetricalEncryption()
        {
            Default = new SymmetricalEncryption();
            Default.FromXmlString("<AesParameters><Key>Ej5CCFLLf3kOjc1t0yS6a7PXQQ5jxbHbLiDtdfeRlhQ=</Key><IV>qacMLnFkd6qOFZYzOF2H5w==</IV></AesParameters>");
        }

        AesCryptoServiceProvider _aes;

        public SymmetricalEncryption()
        {
            _aes = new AesCryptoServiceProvider();
        }

        #region 导入密钥
        public void FromXmlString(string xmlString)
        {
            ImportParameters(XmlSerializerHelper.DeserializeXML<AesParameters>(xmlString));
        }

        public void ImportParameters(AesParameters parameters)
        {
            _aes.Key = parameters.Key;
            _aes.IV = parameters.IV;
        }
        #endregion

        #region 导出密钥
        public string ToXmlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<AesParameters>");
            sb.Append("<Key>");
            sb.Append(_aes.Key.ToBase64String());
            sb.Append("</Key>");
            sb.Append("<IV>");
            sb.Append(_aes.IV.ToBase64String());
            sb.Append("</IV>");
            sb.Append("</AesParameters>");
            return sb.ToString();
        }

        public AesParameters ExportParameters()
        {
            return new AesParameters() { Key = _aes.Key, IV = _aes.IV };
        }
        #endregion

        #region 加密
        public string Encrypt(string inString)
        {
            return Encrypt(inString.FromUTF8String()).ToBase64String();
        }

        public byte[] Encrypt(byte[] inputBuffer)
        {
            return _aes.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        }
        #endregion

        #region 解密
        public string Decrypt(string inString)
        {
            return Decrypt(inString.FromBase64String()).ToUTF8String();
        }

        public byte[] Decrypt(byte[] inputBuffer)
        {
            return _aes.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        }
        #endregion
    }

    public struct AesParameters
    {
        public byte[] Key;
        public byte[] IV;
    }
}
