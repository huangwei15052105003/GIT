using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//www.srcfans.com
namespace MyForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//获取LINQ返回序列的第一个元素
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers", MyConnection);
            DataTable MyTable=new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = (from MyCustomer in MyTable.AsEnumerable()
                           select new{CompanyName = 
                     MyCustomer.Field<string>("CompanyName")}).First();
                     //MyCustomer.Field<string>("CompanyName")}).FirstOrDefault();
            MessageBox.Show("第一家客户的公司名称是：" + 
                MyQuery.CompanyName, "信息提示", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {//获取LINQ返回序列的最后一个元素
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = (from MyCustomer in MyTable.AsEnumerable()
                           select new{CompanyName =
                  MyCustomer.Field<string>("CompanyName")}).Last();
                  //MyCustomer.Field<string>("CompanyName")}).LastOrDefault();
            MessageBox.Show("最后一家客户的公司名称是：" +
                MyQuery.CompanyName, "信息提示", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {//获取LINQ返回序列指定位置的元素
            int MyPos = 3;
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = (from MyCustomer in MyTable.AsEnumerable()
                           select new{CompanyName =
                     MyCustomer.Field<string>("CompanyName")}).ElementAt(MyPos);
                     //MyCustomer.Field<string>("CompanyName")}).ElementAtOrDefault(MyPos);
            MessageBox.Show("第"+MyPos.ToString()+"家客户的公司名称是：" +
                MyQuery.CompanyName, "信息提示", MessageBoxButtons.OK);
        }

        private void button4_Click(object sender, EventArgs e)
        {//获取LINQ返回序列的单个特定元素
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers Where CustomerID='AROUT'", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = (from MyCustomer in MyTable.AsEnumerable()
                           select new{CompanyName =
                       MyCustomer.Field<string>("CompanyName")}).Single();
                       //MyCustomer.Field<string>("CompanyName")}).SingleOrDefault();
            MessageBox.Show("客户ID为AROUT的公司名称是：" +
                MyQuery.CompanyName, "信息提示", MessageBoxButtons.OK);
        }

        private void button5_Click(object sender, EventArgs e)
        {//获取LINQ返回序列的非重复元素
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers ", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = (from MyCustomer in MyTable.AsEnumerable()
               select new{City =MyCustomer.Field<string>("City")}).Distinct();
            string MyInfo = "客户所在的城市名称包括：";
            foreach (var MyCity in MyQuery)
            {
                MyInfo += MyCity.City + "、";
            }
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button6_Click(object sender, EventArgs e)
        {//确定序列的所有元素是否满足条件
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            //string MySQL = "Select  * From Customers Where ContactTitle='Owner'";
            string MySQL = "Select  * From Customers Where ContactTitle='Owner' and City='Mexico'";
            SqlCommand MyCommand = new SqlCommand(MySQL, MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = from MyCustomer in MyTable.AsEnumerable()
                          select new { City = MyCustomer.Field<string>("City") };
            bool   bCity= MyQuery.All(MyCustomer =>MyCustomer.City.Equals("Mexico"));            
            if(bCity)
                MessageBox.Show("客户所在的城市名称全部为Mexico!", "信息提示", MessageBoxButtons.OK);
            else
                MessageBox.Show("客户所在的城市名称有部分不是Mexico!", "信息提示", MessageBoxButtons.OK);                                    
        }

        private void button7_Click(object sender, EventArgs e)
        {//确定序列是否存在符合条件的元素
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            string MySQL = "Select  * From Orders ";            
            SqlCommand MyCommand = new SqlCommand(MySQL, MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = from MyOrder in MyTable.AsEnumerable()
                          select new { Freight = MyOrder.Field<decimal>("Freight"),ShipCountry = MyOrder.Field<string>("ShipCountry") };
            bool bOrder = MyQuery.Any(MyOrder => MyOrder.Freight>300 && MyOrder.ShipCountry=="France");
            //bool bOrder = MyQuery.Any(MyOrder => MyOrder.Freight >30000 && MyOrder.ShipCountry == "France");
            if (bOrder)
                MessageBox.Show("订单表(返回序列)中存在符合条件的订单!", "信息提示", MessageBoxButtons.OK);            
            else
                MessageBox.Show("订单表(返回序列)中不存在符合条件的订单!", "信息提示", MessageBoxButtons.OK);            
        }

        private void button8_Click(object sender, EventArgs e)
        {//将两个序列的元素合并为一个序列
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            string MySQL1 = "Select  * From Orders  Where ShipCountry='France'";
            SqlCommand MyCommand = new SqlCommand(MySQL1, MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery1 = from MyOrder in MyTable.AsEnumerable()
                           select MyOrder;
            string MySQL2 = "Select  * From Orders Where ShipCountry='Germany'";
            MyCommand = new SqlCommand(MySQL2, MyConnection);
            MyTable = new DataTable();
            MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery2 = from MyOrder in MyTable.AsEnumerable()
                           select MyOrder ;
            var MyQuery = MyQuery1.Concat(MyQuery2);
            this.dataGridView1.DataSource = MyQuery.CopyToDataTable();
        }

        private void button9_Click(object sender, EventArgs e)
        {//获取序列中符合条件的元素个数
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = from MyCustomer in MyTable.AsEnumerable()
                          select new{City=MyCustomer.Field<string>("City")};
            int MyCount = MyQuery.Count(MyCustomer => MyCustomer.City.Equals("London"));
            //Int64 MyCount = MyQuery.LongCount(MyCustomer => MyCustomer.City.Equals("London"));                   
            MessageBox.Show("客户表中在London的客户数量是：" +MyCount, "信息提示", MessageBoxButtons.OK);
        }

        private void button10_Click(object sender, EventArgs e)
        {//合并元素索引将元素投影到新表
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyQuery = from MyCustomer in MyTable.AsEnumerable()
                          select new
                          {
                              CustomerID =MyCustomer.Field<string>("CustomerID"),
                              ContactName = MyCustomer.Field<string>("ContactName"),
                              City = MyCustomer.Field<string>("City"),
                              Phone = MyCustomer.Field<string>("Phone"),
                              CompanyName =MyCustomer.Field<string>("CompanyName")
                          };
            var MySelect = MyQuery.Select((MyCustomer,index) => new {MyID=index ,CompanyName = MyCustomer.CompanyName,City=MyCustomer.City});
            this.dataGridView1.DataSource = MySelect.ToList();
        }
        class MyReader
        {
            public string Name { get; set; }
            public List<string> Books { get; set; }
        }
        private void button11_Click(object sender, EventArgs e)
        {//将元素子级过滤结果投影到新表
            MyReader[] MyReaders ={new MyReader{ Name="汤柱兰",
                Books=new List<string>{"Visual C++ 编程技巧大全",
                "Visual Basic 编程技巧大全"," Visual C# 编程技巧大全"}},
                new MyReader{ Name="黄丽",Books=
                    new List<string>{"Visual C++ 精彩编程实例集锦",
                    "Visual Basic 编程技巧大全"," Visual C# 编程实例精粹"}},
                new MyReader{ Name="汤国跃",Books=
                    new List<string>{"Visual C# 编程技巧大全",
                    "Visual C# 编程实例集粹"," Visual C# 编程技巧精选集"}},
                new MyReader{ Name="蒋兰坤",Books=
                    new List<string>{"Visual C++ 编程技巧大全",
                     "Visual Basic 编程实例集粹"," Visual C# 精彩编程实例集锦"}},
                new MyReader{ Name="王彬",Books=
                    new List<string>{"Visual C++ 编程技巧精选集",
                        "Visual Basic 编程技巧大全"}}};
            var MyQuery = MyReaders.SelectMany(MyReader => MyReader.Books,
                (MyReader, BookName) => new { MyReader, BookName })
                .Where(MyFilter => MyFilter.BookName.Contains("Visual C#"))
                .Select(MyFilter =>
                new
                {
                    Name = MyFilter.MyReader.Name,
                    Book = MyFilter.BookName
                });
            //显示所有Visual C#图书的读者名字和对应的书名
            this.dataGridView1.DataSource = MyQuery.ToList();
        }
        
      
        private void button12_Click(object sender, EventArgs e)
        {//获取序列中符合指定条件的元素
            string MyDir = @"C:\Windows";
            string MyStartsWith = "se";
            string[] MyFiles = System.IO.Directory.GetFiles(MyDir);
            IEnumerable<string> MyQuery = MyFiles.Where((MyFile, index) => System.IO.Path.GetFileName(MyFile).StartsWith(MyStartsWith));
            string MyInfo = MyDir + "文件夹中文件名称首部包含"+MyStartsWith+"的文件包括：\n";
            foreach (var MyFile in MyQuery)
            {
                MyInfo += MyFile + System.Environment.NewLine;
            }
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);            
        }

        private void button13_Click(object sender, EventArgs e)
        {//生成包含指定重复次数的序列
            string MyDir = @"C:\Windows";
            string MyStartsWith = "se";
            string[] MyFiles = System.IO.Directory.GetFiles(MyDir);
            IEnumerable<string> MyQuery = MyFiles.Where((MyFile, index) => System.IO.Path.GetFileName(MyFile).StartsWith(MyStartsWith));
            string MyInfo = "将查询的元素进行2次重复的结果是：\n";            
            foreach (var MyFile in MyQuery)
            {
                IEnumerable<string> MyRepeat =Enumerable.Repeat(MyFile, 2);
                foreach (var MyName in MyRepeat)
                {
                    MyInfo += MyName + System.Environment.NewLine;
                }                
            }
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);            
        }

        private void button14_Click(object sender, EventArgs e)
        {//生成与原序列元素顺序相反的序列
            string MyData = "Visual C++";
            char[] MyArray = MyData.ToCharArray();
            char[] MyReversed = MyArray.Reverse().ToArray();
            string MyInfo = MyData + "反序后的结果是：";
            foreach (char MyChar in MyReversed)
                MyInfo += MyChar;
            //显示：Visual C++反序后的结果是：++C lausiV
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button15_Click(object sender, EventArgs e)
        {//根据类型筛选复杂序列中的元素
            System.Collections.ArrayList MyList = new System.Collections.ArrayList(100);
            MyList.Add("罗斌");
            MyList.Add(new DateTime(1972,8,5));
            MyList.Add(36);
            MyList.Add(new DateTime(1987,9,1));
            IEnumerable<DateTime> MyQuery = MyList.OfType<DateTime>();
            string MyInfo="筛选的日期元素分别是：";
            foreach (DateTime MyDate in MyQuery)
                MyInfo += MyDate.ToString("D") + "、";
            //显示：筛选的日期元素分别是：1972年8月5日、1987年9月1日、
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }
        private void button16_Click(object sender, EventArgs e)
        {//不重复两个序列的元素合并序列
            string[] MyCity1 = { "广州市", "深圳市", "中山市" };
            string[] MyCity2 = { "成都市", "广州市", "南京市" };            
            IEnumerable<string> MyCity = MyCity1.Union(MyCity2);
            string MyInfo = "合并后的结果是：";
            foreach (string MyName in MyCity)
                MyInfo += MyName + "、";
            //显示：合并后的结果是：广州市、深圳市、中山市、成都市、南京市、
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);          
        }
        private void button17_Click(object sender, EventArgs e)
        {//对两个序列中的元素进行交集运算
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = 
            @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            //string MySQL = "Select  * From Customers WHERE ";
            //MySQL += "Country='USA' AND ContactTitle='Owner'";
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers ", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyUSAQuery = from MyCustomer in MyTable.AsEnumerable() 
                             where MyCustomer.Field<string>("Country") == "USA"
                             select MyCustomer;
            var MyTitleQuery = from MyCustomer in MyTable.AsEnumerable()
                             where MyCustomer.Field<string>("ContactTitle") == "Owner"
                             select MyCustomer;
            var MyCustomers = MyUSAQuery.Intersect(MyTitleQuery, DataRowComparer.Default);
            this.dataGridView1.DataSource = MyCustomers.CopyToDataTable();           
        }

        private void button18_Click(object sender, EventArgs e)
        {//对两个序列中的元素进行差集运算
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            //Select  * From Customers WHERE (Country='USA' and CustomerID not in (Select  CustomerID From Customers WHERE ContactTitle='Owner' ))
            SqlCommand MyCommand = new SqlCommand("Select  * From Customers ", MyConnection);
            DataTable MyTable = new DataTable();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MyTable);
            var MyUSAQuery = from Customers in MyTable.AsEnumerable() where Customers.Field<string>("Country") == "USA" select Customers;
            var MyTitleQuery = from Customers in MyTable.AsEnumerable() where Customers.Field<string>("ContactTitle") == "Owner" select Customers;
            var MyCustomers = MyUSAQuery.Except(MyTitleQuery, DataRowComparer.Default);
            this.dataGridView1.DataSource = MyCustomers.CopyToDataTable();
        }

        private void button19_Click(object sender, EventArgs e)
        {//使用联接关键字查询序列中的元素
            SqlConnection MyConnection = new SqlConnection();
            MyConnection.ConnectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            MyConnection.Open();
            SqlCommand MyCommand = new SqlCommand("Select * From Customers;Select * From Orders; ", MyConnection);
            DataSet MySet = new DataSet();
            SqlDataAdapter MyAdapter = new SqlDataAdapter(MyCommand);
            MyAdapter.Fill(MySet);            
            DataTable MyCustomersTable = MySet.Tables[0];
            DataTable MyOrdersTable = MySet.Tables[1];
            var MyQuery =
                        from MyCustomers in MyCustomersTable.AsEnumerable()
                        join MyOrders in MyOrdersTable.AsEnumerable()
                        on MyCustomers.Field<string>("CustomerID") equals
                                    MyOrders.Field<string>("CustomerID")
                        where MyCustomers.Field<string>("CustomerID") == "ALFKI"
                        select new
                        {
                            OrderID = MyOrders.Field<int>("OrderID"),
                            OrderDate = MyOrders.Field<DateTime>("OrderDate"),
                            RequiredDate = MyOrders.Field<DateTime>("RequiredDate")
                        };          
            this.dataGridView1.DataSource = MyQuery.ToList();
        }      
    }
}
