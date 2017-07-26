using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace xxxxxLibrary.Utils
{
    public class EncryptUtil
    {
		/// <summary>
		/// MD5加密函数
		/// </summary>
		/// <returns>MD5结果.</returns>
		/// <param name="str">原始字符串.</param>
		public static string MD5(string str)
		{
			byte[] b = Encoding.UTF8.GetBytes(str);
			b = new MD5CryptoServiceProvider().ComputeHash(b);
			string ret = "";
			for (int i = 0; i < b.Length; i++)
				ret += b[i].ToString("x").PadLeft(2, '0');

			return ret;
		}

		/// <summary>
		/// SHA256函数
		/// </summary>
		/// /// <param name="str">原始字符串</param>
		/// <returns>SHA256结果</returns>
		public static string SHA256(string str)
		{
			byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
			SHA256Managed Sha256 = new SHA256Managed();
			byte[] Result = Sha256.ComputeHash(SHA256Data);
			return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
		}

		#region AES 加密解密
		/// <summary>
		/// AES 加密 C#与PHP兼容
		/// xqy 20150908
		/// </summary>        
		public static string Encode(string message, string encryptKey)
		{
			MemoryStream mStream = new MemoryStream();
			RijndaelManaged aes = new RijndaelManaged();

			byte[] plainBytes = Encoding.UTF8.GetBytes(message);
			Byte[] bKey = new Byte[32];
			Array.Copy(Encoding.UTF8.GetBytes(encryptKey.PadRight(bKey.Length)), bKey, bKey.Length);
			aes.Mode = CipherMode.ECB;
			aes.Padding = PaddingMode.Zeros;
			aes.KeySize = 128;
			aes.Key = bKey;
			CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
			try
			{
				cryptoStream.Write(plainBytes, 0, plainBytes.Length);
				cryptoStream.FlushFinalBlock();
				return Convert.ToBase64String(mStream.ToArray());
			}
			finally
			{
				cryptoStream.Close();
				mStream.Close();
				aes.Clear();
			}

		}
		/// <summary>  
		/// AES解密
		/// xqy 20150908
		/// </summary>  
		/// <param name="encryptedBytes">被加密的明文</param>  
		/// <param name="key">密钥</param>  
		/// <returns>明文</returns>  
		public static string Decode(string Data, string Key)
		{
			Byte[] encryptedBytes = Convert.FromBase64String(Data);
			Byte[] bKey = new Byte[32];
			Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

			MemoryStream mStream = new MemoryStream(encryptedBytes);
			//mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
			//mStream.Seek( 0, SeekOrigin.Begin );  
			RijndaelManaged aes = new RijndaelManaged();
			aes.Mode = CipherMode.ECB;
			aes.Padding = PaddingMode.Zeros;
			aes.KeySize = 128;
			aes.Key = bKey;
			//aes.IV = _iV;  
			CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
			try
			{
				byte[] tmp = new byte[encryptedBytes.Length + 32];
				int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
				byte[] ret = new byte[len];
				Array.Copy(tmp, 0, ret, 0, len);
				return Encoding.UTF8.GetString(ret).Trim('\0');
			}
			finally
			{
				cryptoStream.Close();
				mStream.Close();
				aes.Clear();
			}
		}
		#endregion
	}
}
