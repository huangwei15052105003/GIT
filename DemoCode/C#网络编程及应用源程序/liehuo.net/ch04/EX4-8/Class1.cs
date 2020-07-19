using System;
class Method
{
	public static float VarLenParam(params long[ ] v)
	{
		long total,i;
		for(i=0,total=0;i<v.Length;++i)
			total+=v[i];
		return (float)total/v.Length;
	}
	static void Main()
	{
		float x=VarLenParam(1,2,3,5);
		Console.WriteLine("1+2+3+5的平均值为{0}",x);
		x=VarLenParam(4,5,6,7,8);
		Console.WriteLine("4+5+6+7+8的平均值为{0}",x);
		Console.Read();
	}
}
