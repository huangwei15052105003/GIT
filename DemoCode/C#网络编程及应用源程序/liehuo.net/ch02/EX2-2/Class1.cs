using System;
class Test
{
	enum days {Sun,Mon,Tue,Wed,Thu,Fri,Sat};
	static void Main()
	{
		Console.WriteLine(days.Wed);
		days myday=days.Fri;
		Console.WriteLine(myday);
		Console.Read();
	}
}
