using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFrame.Core.CryptoTransverters
{
    /// <summary>
    /// 解密接口
    /// </summary>
    public interface IDecrypt
    {
        byte[] Decrypt(byte[] inputBuffer);
        string Decrypt(string inString);
    }
}
