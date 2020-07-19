using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
namespace CryptoTest
{
	class TextFileCrypt
	{
		public static void Main(string[] args)
		{
			//将test.txt文件中的内容加密后放入temp.txt文件
			FileStream fsIn = File.Open(@"D:\test\test.txt",FileMode.Open,
				FileAccess.Read);
			FileStream fsOut = File.Open(@"D:\test\temp.txt", FileMode.Create,
				FileAccess.Write);
			//定义对称算法对象实例和接口
			SymmetricAlgorithm symm = new RijndaelManaged();
			ICryptoTransform transform = symm.CreateEncryptor();
			CryptoStream cstream = new CryptoStream(fsOut,transform,
				CryptoStreamMode.Write);
			BinaryReader br = new BinaryReader(fsIn);
			// 读取源文件到cryptostream 
			cstream.Write(br.ReadBytes((int)fsIn.Length),0,(int)fsIn.Length);
			cstream.FlushFinalBlock();
			cstream.Close();
			fsIn.Close();
			fsOut.Close();
			Console.WriteLine("created encrypted file {0}", @"D:\test\temp.txt");
			Console.WriteLine("will now decrypt and show contents");
			//将temp.txt文件中的内容解密后，显示输出
			fsIn = File.Open(@"D:\test\temp.txt",FileMode.Open,FileAccess.Read);
			transform = symm.CreateDecryptor();
			cstream = new CryptoStream(fsIn,transform,
				CryptoStreamMode.Read);
			br = new BinaryReader(cstream);
			Console.WriteLine("decrypted file text: " 
				+ Encoding.Default.GetString(br.ReadBytes((int)fsIn.Length)));
			fsIn.Close();
			Console.Read();
		}
	}
}
