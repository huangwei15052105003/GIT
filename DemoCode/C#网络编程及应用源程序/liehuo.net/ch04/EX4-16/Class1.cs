using System;
public class Test
{
	public static void Main()
	{
		int i=10,j=-5;
		double x=1.3,y=2.7;
		double a=2.0,b=5.0;
		Console.WriteLine(string.Format("-5�ľ���ֵΪ{0}",Math.Abs(j)));
		Console.WriteLine(
			string.Format("���ڵ���1.3����С����Ϊ{0}",Math.Ceiling(x)));
		Console.WriteLine(
			string.Format("С�ڵ���2.7���������Ϊ{0}",Math.Floor(y)));
		Console.WriteLine(
			string.Format("10��-5�Ľϴ���Ϊ{0}",Math.Max(i,j)));
		Console.WriteLine(
			string.Format("1.3��2.7�Ľ�С��Ϊ{0}",Math.Min(x,y)));
		Console.WriteLine(string.Format("2��5�η�Ϊ{0}",Math.Pow(a,b)));
		Console.WriteLine(string.Format("1.3����������ֵΪ{0}",Math.Round(x)));
		Console.WriteLine(string.Format("5��ƽ����Ϊ{0}",Math.Sqrt(b)));
		Console.ReadLine();
	}
}
