using System;
using System.Collections;
class Test33
{
	public static void Main()
	{
		Hashtable hs = new Hashtable();
		hs.Add("001", "str1");
		hs.Add("002", "str2");
		hs.Add("003", "str3");
		Console.WriteLine("code       string");
		foreach (string s in hs.Keys) 
		{
			Console.WriteLine(s + "          " + hs[s]);
		}
		Console.Read();
	}
}
