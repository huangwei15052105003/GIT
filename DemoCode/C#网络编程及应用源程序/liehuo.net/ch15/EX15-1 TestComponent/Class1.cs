using System;
using System.Data;
using System.Data.SqlClient;
namespace TestComponent
{
	public class MyDataBase
	{
		//返回表中记录数
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
		//根据Select语句自动生成其他SQL语句
		public void BuildAdapter(ref SqlDataAdapter adapter)
		{
			SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
			adapter.DeleteCommand=builder.GetDeleteCommand();
			adapter.InsertCommand=builder.GetInsertCommand();
			adapter.UpdateCommand=builder.GetUpdateCommand();
		}
	}
}
