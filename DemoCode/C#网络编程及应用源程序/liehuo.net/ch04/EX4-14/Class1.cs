using System;
class Test
{
	public static void Main()
	{
		DateTime dt1 = new System.DateTime
			(    2003,       // Year
			12,         // Month
			31,         // Day
			22,         // Hour
			35,         // Minute
			5,          // Second
			15         // Millisecond
			);
		DateTime dt2=new DateTime(2002,7,05);
		Console.WriteLine("{0:F}---{1}", dt1,dt2);
		dt1=DateTime.Now;
		int i=dt1.Day;   //���µڼ���
		int j=dt1.Month; //��
		int k=dt1.Year;  //��
		Console.WriteLine("{0}---{1}---{2}---{3}",dt1,i,j,k);
		DateTime t1=dt1.Date;       //���ڲ���
		k=dt1.Hour;                 //Сʱ
		Console.WriteLine("{0}---{1}",dt1,k);
		TimeSpan ts1=dt1.TimeOfDay;  //�����ʱ��
		TimeSpan ts2=dt1-dt2;
		i=ts2.Days;
		Console.WriteLine("{0}---{1}---{2}",ts1,ts2,i);
		string s1=dt1.ToLongDateString();
		string s2=dt1.ToShortDateString();
		string s3=string.Format("{0:yyyy.MM.dd}",dt1);  //ע�⣺MΪ�£�mΪ����
		Console.WriteLine("{0}---{1}---{2}---{3}",dt1,s1,s2,s3);
		Console.Read();
	}
}
