using System;
class Test
{
	public static void Main()
	{
		int i=10;
		long j=20;
		double k=30D;
		//��ʽת��
		j=i;
		k=j;
		Console.WriteLine("{0},{1},{2}",i,j,k);
		k=30.6D;
		//��ʽת��
		j=(long)k;
		i=(int)j;
		Console.WriteLine("{0},{1},{2}",i,j,k);
		Console.Read();
	}
}
