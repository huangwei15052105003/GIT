using System;
namespace ConsoleTest
{
	class TestArray
	{
		public static void Main()
		{
			//一维数组
			string[ ]  strArray1 = { "C", "C++", "C#" };
			string[ ]  strArray2 = new  string[3];
			int[ ] intArr1=new  int[4]{0,1,2,3};
			int[ ] intArr2 ={5,6,7};
			int  nVar = 5;
			int[ ] arrToo = new int[nVar]; 
			//二维数组
			int[,] myArr1 = {{0,1}, {2,3}, {4,5}};
			int[,] myArr2 = new int[2,3]{{11,12,13},{21,22,23}};
			//锯齿型数组
			string[ ][ ] stra=new string[3][ ];
			stra[0]=new string[2]{"a11","a12"};
			stra[1]=new string[3]{"b11","b12","b13"};
			stra[2]=new string[5]{"a","e","i","o","u"};
			//输出一维数组的元素
			for(int i=0;i<strArray1.Length;i++)
				Console.Write("  strArray1[{0}]={1}",i,strArray1[i]);
			Console.WriteLine();
			//输出二维数组的元素
			for(int i=0;i<3;i++)
			{
				for(int j=0;j<2;j++)
					Console.Write("  myArr1[{0},{1}]={2}",i,j,myArr1[i,j]);
				Console.WriteLine();
			}
			//输出锯齿型数组的元素
			for(int i=0;i<stra.Length;i++)
			{
				for(int j=0;j<stra[i].Length;j++)
					Console.Write("  stra[{0}][{1}]={2}",i,j,stra[i][j]);
				Console.WriteLine();
			}
			Console.Read();
		}
	}
}
