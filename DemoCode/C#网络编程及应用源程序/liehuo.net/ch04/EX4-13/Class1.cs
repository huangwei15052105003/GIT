using System;
class Test
{
	public static void Main()
	{
		double d1=23.5D,d2=23.4D;
		int i1=Convert.ToInt32(d1);
		int i2=Convert.ToInt32(d2);
		Console.WriteLine("{0},{1}",i1,i2);
		int i=0;
		bool b1=Convert.ToBoolean(d1);
		bool b2=Convert.ToBoolean(i);
		Console.WriteLine("{0},{1}",b1,b2);
		string s="123";
		i=Convert.ToInt32(s);
		Console.WriteLine("{0},{1}",s,i);
		Console.Read();
	}
}
