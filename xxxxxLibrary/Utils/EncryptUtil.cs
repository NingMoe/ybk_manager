using System;
using System.Security.Cryptography;
using System.Text;

namespace xxxxxLibrary.Utils
{
    public class EncryptUtil
    {
		// <summary>
		/// MD5加密函数
		/// </summary>
		/// <param name="str">原始字符串</param>
		/// <returns>MD5结果</returns>
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

		static AES Aes = new AES();
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Encrypt(string input)
		{
			return Aes.Encrypt(input);
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Decrypt(string input)
		{
			return Aes.Decrypt(input);
		}


		#region AES
		/// <summary> 
		/// 加密
		/// </summary> 
		public class AES
		{
			RijndaelManaged rijndaelProvider;
			ICryptoTransform rijndaelEncrypt;
			ICryptoTransform rijndaelDecrypt;
			/// <summary>
			/// 默认构造函数
			/// </summary>
			public AES()
				: this("DEFAULT CRYPTKEY")
			{

			}
			/// <summary>
			/// 
			/// </summary>
			/// <param name="_encryptKey"></param>
			public AES(string _encryptKey)
			{
				_encryptKey = _encryptKey.CutString(32);
				_encryptKey = _encryptKey.PadRight(32, ' ');
				rijndaelProvider = new RijndaelManaged();
				rijndaelProvider.Key = Encoding.UTF8.GetBytes(_encryptKey);
				rijndaelProvider.IV = Keys;
				rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
				rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
			}

			//默认密钥向量
			private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
			/// <summary>
			/// 加密
			/// </summary>
			/// <param name="encryptString"></param>
			/// <returns></returns>
			public string Encrypt(string encryptString)
			{
				byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
				byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
				return Convert.ToBase64String(encryptedData);
			}

			/// <summary>
			/// 解密
			/// </summary>
			/// <param name="decryptString"></param>
			/// <returns></returns>
			public string Decrypt(string decryptString)
			{
				try
				{
					byte[] inputData = Convert.FromBase64String(decryptString);
					byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
					return Encoding.UTF8.GetString(decryptedData);
				}
				catch (Exception ex)
				{
                    var msg = ex.Message.ToString();
					//LogHelper.Fatal("解密错误", ex);
					return string.Empty;
				}
			}

		}
		#endregion
	}
}
