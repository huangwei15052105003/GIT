using System;
public class Child 
{
	private int age;
	private string name;
	// 不带参数的构造函数
	public Child() 
	{
		name = "none";
	}
	// 带参数的构造函数
	public Child(string name, int age) 
	{
		this.name = name;
		this.age = age;
	}
	// 输出方法
	public void PrintChild() 
	{
		Console.WriteLine("{0}, {1} years old.", name, age);
	}
}
public class MainClass 
{
	public static void Main() 
	{
		//使用new关键字创建对象，new后是调用的构造函数
		Child child1 = new  Child("Zhang San", 11);
		Child child2 = new Child("Li Si", 10);
		Child child3 = new Child(); 
		// 显示结果
		Console.Write("Child #1: ");
		child1.PrintChild();
		Console.Write("Child #2: ");
		child2.PrintChild();
		Console.Write("Child #3: ");
		child3.PrintChild();
		Console.ReadLine();
	}
}
