using System;
public class Test34
{
	public static void Main()
	{
		while(true)
		{
			Console.Write("������һ���ַ���(q����):");
			//�Ӽ��̽���һ����Ϣ
			string s=Console.ReadLine();
			if(s.Length==0) continue;
			//������յ����ַ�����Q����q,���˳�ѭ��
			if(s.Substring(0,1).ToUpper()=="Q")	break;
			int letterIndex=-1,digitIndex=-1;
			bool checkLetter,checkDigit;
			checkLetter=checkDigit=true;
			// s.Length���ַ������ȣ�ע���ַ���ΪUnicode�ַ���ɣ���
			//���ֺ���ĸ�������ֽڣ����磺��ab����c���ĳ���Ϊ5
			for(int i=0;i<s.Length;i++)
			{
				//����Ѿ��ҵ��״γ��ֵ���ĸλ�ú��״γ��ֵ�����λ�ú���ǿ���˳�//forѭ��
				if(!checkLetter && !checkDigit) break;
				if(checkLetter)
					//�����i���ַ����״γ��ֵ���ĸ�����ס��ĸλ��
					if(Char.IsLetter(s[i]))
					{ 
						letterIndex=i;
						checkLetter=false;
					} 
				if(checkDigit==true)
				{
					//�����i���ַ����״γ��ֵ����֣����ס����λ��
					if(Char.IsDigit(s[i]))
					{
						digitIndex=i;
						checkDigit=false;
					}
				}
			}
			if(letterIndex>-1)
			{
				Console.WriteLine("�����ĵ�һ����ĸ��'{0}'��",s[letterIndex]);
			}
			else
			{
				Console.WriteLine("�ַ����в�������ĸ��");
			}
			if(digitIndex>-1)
			{
				Console.WriteLine("�����ĵ�һ��������'{0}'��",s[digitIndex]);
			}
			else
			{
				Console.WriteLine("�ַ����в��������֡�");
			}
		}
	}
}
