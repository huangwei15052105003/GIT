using System;
public class Test31
{
	public static void Main()
	{
		Console.Write("请输入成绩：");
		//从键盘接收一行字符
		string str=Console.ReadLine();
		//转换为整型
		int i=Int32.Parse(str.Trim());
		if(i>100 || i<0)
		{
			Console.WriteLine("成绩不在0-100范围内");
		}
		else
		{
			switch(i/10)
			{
				case 10:
					Console.Write("满分，");
					goto case 9;
				case 9:
					Console.WriteLine("优秀");
					break;
				case 8:
				case 7:
					Console.WriteLine("良好");
					break;
				case 6:
					Console.WriteLine("及格");
					break;
				default:
					Console.WriteLine("不及格");
					break;
			}
		}
		Console.Read();
	}
}
