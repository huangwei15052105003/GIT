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
			//��test.txt�ļ��е����ݼ��ܺ����temp.txt�ļ�
			FileStream fsIn = File.Open(@"D:\test\test.txt",FileMode.Open,
				FileAccess.Read);
			FileStream fsOut = File.Open(@"D:\test\temp.txt", FileMode.Create,
				FileAccess.Write);
			//����Գ��㷨����ʵ���ͽӿ�
			SymmetricAlgorithm symm = new RijndaelManaged();
			ICryptoTransform transform = symm.CreateEncryptor();
			CryptoStream cstream = new CryptoStream(fsOut,transform,
				CryptoStreamMode.Write);
			BinaryReader br = new BinaryReader(fsIn);
			// ��ȡԴ�ļ���cryptostream 
			cstream.Write(br.ReadBytes((int)fsIn.Length),0,(int)fsIn.Length);
			cstream.FlushFinalBlock();
			cstream.Close();
			fsIn.Close();
			fsOut.Close();
			Console.WriteLine("created encrypted file {0}", @"D:\test\temp.txt");
			Console.WriteLine("will now decrypt and show contents");
			//��temp.txt�ļ��е����ݽ��ܺ���ʾ���
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
