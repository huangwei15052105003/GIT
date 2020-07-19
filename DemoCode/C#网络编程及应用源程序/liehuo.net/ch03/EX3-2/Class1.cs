using System; 
class Test32
{
	public static void Main()
	{
		for(int i=1,j=1;i<=6 && j>=-5;i++,j--)
		{
			string s=string.Format("i={0},j={1}",i,j);
			Console.WriteLine(s);
		}
		Console.Read();
	}
}
