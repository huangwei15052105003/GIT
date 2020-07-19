using System;
class MyIndexer
{
	private string[ ] myArray=new string[4];
	public string this[int index]
	{
		get
		{
			if(index<0||index>=4)
				return null;
			else
				return myArray[index];
		}
		set
		{
			if(!(index<0||index>=4))
				myArray[index]=value;
		}
	}
	static void Main()
	{
		MyIndexer idx=new MyIndexer();
		idx[0]="abc";
		idx[1]="def";
		for(int i=0;i<=3;i++)
		{
			Console.WriteLine(idx[i]);
		}
		Console.Read();
	}
}
