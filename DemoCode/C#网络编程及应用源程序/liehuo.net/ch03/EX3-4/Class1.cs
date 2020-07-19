using System;
public class Test34
{
	public static void Main()
	{
		while(true)
		{
			Console.Write("请输入一个字符串(q结束):");
			//从键盘接收一行信息
			string s=Console.ReadLine();
			if(s.Length==0) continue;
			//如果接收的首字符等于Q或者q,则退出循环
			if(s.Substring(0,1).ToUpper()=="Q")	break;
			int letterIndex=-1,digitIndex=-1;
			bool checkLetter,checkDigit;
			checkLetter=checkDigit=true;
			// s.Length是字符串长度，注意字符串为Unicode字符组成，即
			//汉字和字母均是两字节，例如：“ab章三c”的长度为5
			for(int i=0;i<s.Length;i++)
			{
				//如果已经找到首次出现的字母位置和首次出现的数字位置后，则强制退出//for循环
				if(!checkLetter && !checkDigit) break;
				if(checkLetter)
					//如果第i个字符是首次出现的字母，则记住字母位置
					if(Char.IsLetter(s[i]))
					{ 
						letterIndex=i;
						checkLetter=false;
					} 
				if(checkDigit==true)
				{
					//如果第i个字符是首次出现的数字，则记住数字位置
					if(Char.IsDigit(s[i]))
					{
						digitIndex=i;
						checkDigit=false;
					}
				}
			}
			if(letterIndex>-1)
			{
				Console.WriteLine("包含的第一个字母是'{0}'。",s[letterIndex]);
			}
			else
			{
				Console.WriteLine("字符串中不包含字母。");
			}
			if(digitIndex>-1)
			{
				Console.WriteLine("包含的第一个数字是'{0}'。",s[digitIndex]);
			}
			else
			{
				Console.WriteLine("字符串中不包含数字。");
			}
		}
	}
}
