using System;
public class Child 
{
	private int age;
	private string name;
	// ���������Ĺ��캯��
	public Child() 
	{
		name = "none";
	}
	// �������Ĺ��캯��
	public Child(string name, int age) 
	{
		this.name = name;
		this.age = age;
	}
	// �������
	public void PrintChild() 
	{
		Console.WriteLine("{0}, {1} years old.", name, age);
	}
}
public class MainClass 
{
	public static void Main() 
	{
		//ʹ��new�ؼ��ִ�������new���ǵ��õĹ��캯��
		Child child1 = new  Child("Zhang San", 11);
		Child child2 = new Child("Li Si", 10);
		Child child3 = new Child(); 
		// ��ʾ���
		Console.Write("Child #1: ");
		child1.PrintChild();
		Console.Write("Child #2: ");
		child2.PrintChild();
		Console.Write("Child #3: ");
		child3.PrintChild();
		Console.ReadLine();
	}
}
