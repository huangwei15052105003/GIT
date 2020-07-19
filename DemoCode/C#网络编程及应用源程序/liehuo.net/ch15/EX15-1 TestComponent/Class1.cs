using System;
using System.Data;
using System.Data.SqlClient;
namespace TestComponent
{
	public class MyDataBase
	{
		//���ر��м�¼��
		public int GetRecordCount(string connString,string tableName)
		{
			int number=-1;
			SqlConnection conn=new SqlConnection(connString);
			SqlCommand command=new SqlCommand("select count(*) from "+tableName,conn);
			try
			{
				conn.Open();
				number=(int)command.ExecuteScalar();
				conn.Close();
			}
			catch(Exception err)
			{
				throw new Exception(err.Message);
			}
			return number;
		}
		//����Select����Զ���������SQL���
		public void BuildAdapter(ref SqlDataAdapter adapter)
		{
			SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
			adapter.DeleteCommand=builder.GetDeleteCommand();
			adapter.InsertCommand=builder.GetInsertCommand();
			adapter.UpdateCommand=builder.GetUpdateCommand();
		}
	}
}
