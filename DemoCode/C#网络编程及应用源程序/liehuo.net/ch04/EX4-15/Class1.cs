using System;
public class Test
{
	public static void Main()
	{
		string[ ] weekDays={ "������","����һ","���ڶ�",
							   "������","������","������","������"};
		DateTime now=DateTime.Now;
		string str=string.Format("{0:������yyyy��M��d�գ�H��m��},{1}",
			now,weekDays[(int)now.DayOfWeek]);
		Console.WriteLine(str);
		DateTime start=new DateTime(2004,1,1);
		TimeSpan times=now-start;
		Console.WriteLine("��2004��1��1���������Ѿ�����{0}�죡",times.Days);
		Console.Read();
	}
}
