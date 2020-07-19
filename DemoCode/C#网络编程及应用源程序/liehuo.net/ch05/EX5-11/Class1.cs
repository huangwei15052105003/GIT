using System;
namespace test
{ 
	//��һ��������ί��
	public delegate string MyDelegate(string name);
	public class Test
	{
		//�ڶ��������屻���õķ���
		public static string FunctionA(string name) 
		{
			return "A say Hello to "+name; 
		}
		public static string FunctionB(string name) 
		{
			return "B say Hello to "+name; 
		}
		//������������delegate���͵Ĵ����������ڴ˺�����
		//         ͨ��delegate���͵��ò���2����ķ���
		public static void MethodA(MyDelegate Me)
		{
			Console.WriteLine(Me("����")); 
		}
		public static int Main( )
		{
			//���Ĳ�������ʵ��������׼�����õķ�����
			MyDelegate a=new MyDelegate(FunctionA); 
			MyDelegate b=new MyDelegate(FunctionB); 
			MethodA(a); 
			MethodA(b);
			Console.Read(); 
			return 0; 
		}
	}
}
