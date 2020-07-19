using System;
class Method
{
	public int MyMethod()
	{
		Console.WriteLine("this is MyMethod.");
		int i=10;
		return i;
	}
	static void Main()
	{
		Method method=new Method();
		int j=5;
		j=method.MyMethod();
		Console.WriteLine("the value is {0}.",j);
		Console.Read();
	}
}
