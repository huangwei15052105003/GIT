using System;
public class Test
{
	public static void Main()
	{
		int i=10,j=-5;
		double x=1.3,y=2.7;
		double a=2.0,b=5.0;
		Console.WriteLine(string.Format("-5的绝对值为{0}",Math.Abs(j)));
		Console.WriteLine(
			string.Format("大于等于1.3的最小整数为{0}",Math.Ceiling(x)));
		Console.WriteLine(
			string.Format("小于等于2.7的最大整数为{0}",Math.Floor(y)));
		Console.WriteLine(
			string.Format("10和-5的较大者为{0}",Math.Max(i,j)));
		Console.WriteLine(
			string.Format("1.3和2.7的较小者为{0}",Math.Min(x,y)));
		Console.WriteLine(string.Format("2的5次方为{0}",Math.Pow(a,b)));
		Console.WriteLine(string.Format("1.3的四舍五入值为{0}",Math.Round(x)));
		Console.WriteLine(string.Format("5的平方根为{0}",Math.Sqrt(b)));
		Console.ReadLine();
	}
}
