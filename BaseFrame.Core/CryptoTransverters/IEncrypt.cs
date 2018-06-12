using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.CryptoTransverters
{
    /// <summary>
    /// 加密接口
    /// </summary>
    public interface IEncrypt
    {
        byte[] Encrypt(byte[] inputBuffer);
        string Encrypt(string inString);
    }
}
