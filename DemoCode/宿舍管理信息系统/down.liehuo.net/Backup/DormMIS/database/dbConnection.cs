using System;

namespace DormMIS.database
{
	/// <summary>
	/// dbConnection ��ժҪ˵����
	/// </summary>
	public class dbConnection
	{
		public dbConnection()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public static string connection
		{
			get
			{return"Data Source=dormMIS.mdb;Jet OLEDB:Engine Type=5;Provider=Microsoft.Jet.OLEDB.4.0;";}
		}
	}
}
