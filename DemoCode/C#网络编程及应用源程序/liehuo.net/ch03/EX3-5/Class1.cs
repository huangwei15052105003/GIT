using System;
class Test35
{
	static void F()
	{
		try
		{
			G();
		}
		catch (Exception err)
		{
			Console.WriteLine("����F�в���: " + err.Message);
			// �����׳���ǰ������ catch �鴦����쳣err
			throw;
		}
	}
	static void G()
	{
		throw new Exception("����G���׳����쳣��");
	}
	static void Main()
	{
		try
		{
			F();
		}
		catch (Exception err) 
		{
			Console.WriteLine("����Main�в���:" + err.Message);
		}
		Console.ReadLine();
	}
}
