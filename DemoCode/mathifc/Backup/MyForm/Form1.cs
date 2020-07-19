using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//使用对称算法加密解密数据
            var MyData = "中华人民共和国13036371686罗斌";
            var MyPassword = "40405690";
            var MyEnData = "";
            var MyDeData = "";
            //使用对称算法加密数据
            byte[] MyIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] MyKey = Encoding.UTF8.GetBytes(MyPassword);
            var MyProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] MyBytes = Encoding.UTF8.GetBytes(MyData);
            var MyMemory = new System.IO.MemoryStream();
            var MyCrypt = new System.Security.Cryptography.CryptoStream(MyMemory, MyProvider.CreateEncryptor(MyKey, MyIV), System.Security.Cryptography.CryptoStreamMode.Write);
            MyCrypt.Write(MyBytes, 0, MyBytes.Length);
            MyCrypt.FlushFinalBlock();
            MyEnData = Convert.ToBase64String(MyMemory.ToArray());
            //使用对称算法解密数据
            byte[] MyEnKey = System.Text.Encoding.Default.GetBytes(MyPassword);
            byte[] MyEnBytes = Convert.FromBase64String(MyEnData);
            var MyDeMemory = new System.IO.MemoryStream();
            var MyDeCrypt = new System.Security.Cryptography.CryptoStream(MyDeMemory, MyProvider.CreateDecryptor(MyEnKey, MyIV), System.Security.Cryptography.CryptoStreamMode.Write);
            MyDeCrypt.Write(MyEnBytes, 0, MyEnBytes.Length);
            MyDeCrypt.FlushFinalBlock();
            var MyEncoding = new System.Text.UTF8Encoding();
            MyDeData = MyEncoding.GetString(MyDeMemory.ToArray());
            var MyInfo = "\n加密前的数据：" + MyData;
            MyInfo += "\n加密密码：" + MyPassword;
            MyInfo += "\n加密后的数据：" + MyEnData;
            MyInfo += "\n解密后的数据：" + MyDeData;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {//以编程方式创建XML文档
            var MyXmlDoc = new System.Xml.XmlDocument();
            //创建根节点
            var MyCustomers = MyXmlDoc.CreateElement("客户表");
            MyXmlDoc.AppendChild(MyCustomers);
            //添加子节点
            var MyCustomer = MyXmlDoc.CreateElement("客户");
            MyCustomer.SetAttribute("公司名称", "无锡宝特软件有限公司");
            MyCustomer.SetAttribute("联系电话", "40251988");
            MyCustomers.AppendChild(MyCustomer);
            MyCustomer = MyXmlDoc.CreateElement("客户");
            MyCustomer.SetAttribute("公司名称", "深圳唯佳物流有限公司");
            MyCustomer.SetAttribute("联系电话", "68265419");
            MyCustomers.AppendChild(MyCustomer);
            MyCustomer = MyXmlDoc.CreateElement("客户");
            MyCustomer.SetAttribute("公司名称", "重庆深达科技有限公司");
            MyCustomer.SetAttribute("联系电话", "40405690");
            MyCustomers.AppendChild(MyCustomer);
            //保存XML文档
            MyXmlDoc.Save("MyXML.xml");
            //显示XML文档
            MessageBox.Show(MyXmlDoc.OuterXml, "信息提示", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {//从XML文档中读写类(型)数据
            //在XML文件“SerializedData.xml”中写入整数“123456”
            var MyWriter = new System.Xml.Serialization.XmlSerializer(typeof(Int32));
            var MyWriteFile = new System.IO.StreamWriter("SerializedData.xml");
            MyWriter.Serialize(MyWriteFile, 123456);
            MyWriteFile.Close();
            //从XML文件“SerializedData.xml”中读入整数“123456”
            var MyReader = new System.Xml.Serialization.XmlSerializer(typeof(Int32));
            var MyReadFile = new System.IO.StreamReader("SerializedData.xml");
            var MyData= (Int32)MyReader.Deserialize(MyReadFile);
            MessageBox.Show(MyData.ToString(), "信息提示", MessageBoxButtons.OK);     
        }

        private void button4_Click(object sender, EventArgs e)
        {//以缩进格式读取XML文档数据
            var MyXMLDoc = "<?xml version='1.0'?>" +
                      "<!-- 我的 XML例子 -->" +
                      "<?pi MyProcessingInstruction?>" +
                      "<Root>" +
                      "<Node1 nodeId='1'>第一个节点</Node1>" +
                      "<Node2 nodeId='2'>第二个节点</Node2>" +
                      "<Node3 nodeId='3'>第三个节点</Node3>" +
                      "</Root>";
            byte[] MyBytes = Encoding.UTF8.GetBytes(MyXMLDoc);
            MyXML = "";
            using(var MyStream = new System.IO.MemoryStream(MyBytes))
            {
                var MySettings = new System.Xml.XmlReaderSettings();
                MySettings.CheckCharacters = true;
                using(var MyReader = System.Xml.XmlReader.Create(MyStream, MySettings))
                {
                    int MyLevel = 0;
                    while (MyReader.Read())
                    {
                        switch (MyReader.NodeType)
                        {
                            case System.Xml.XmlNodeType.CDATA:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("CDATA: {0}", MyReader.Value) + "\n";
                                break;
                            case System.Xml.XmlNodeType.Comment:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("COMMENT: {0}", MyReader.Value) + "\n";
                                break;
                            case System.Xml.XmlNodeType.DocumentType:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("DOCTYPE: {0}={1}" + "\n",
                                     MyReader.Name, MyReader.Value);
                                break;
                            case System.Xml.XmlNodeType.Element:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("ELEMENT: {0}", MyReader.Name) + "\n";
                                MyLevel++;
                                while (MyReader.MoveToNextAttribute())
                                {
                                    IndentWhiteSpace(MyLevel);
                                    MyXML += String.Format("ATTRIBUTE: {0}='{1}'",
                                    MyReader.Name, MyReader.Value) + "\n";
                                }
                                break;
                            case System.Xml.XmlNodeType.EndElement:
                                MyLevel--;
                                break;
                            case System.Xml.XmlNodeType.EntityReference:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("ENTITY: {0}", MyReader.Name) + "\n";
                                break;
                            case System.Xml.XmlNodeType.ProcessingInstruction:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("INSTRUCTION: {0}={1}",
                                MyReader.Name, MyReader.Value) + "\n";
                                break;
                            case System.Xml.XmlNodeType.Text:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("TEXT: {0}", MyReader.Value) + "\n";
                                break;
                            case System.Xml.XmlNodeType.XmlDeclaration:
                                IndentWhiteSpace(MyLevel);
                                MyXML += String.Format("DECLARATION: {0}={1}",
                                 MyReader.Name, MyReader.Value) + "\n";
                                break;
                        }
                    }
                }
            }
            MessageBox.Show(MyXML, "信息提示", MessageBoxButtons.OK);
        }
        private string MyXML = "";
        private void IndentWhiteSpace(int MyLevel)
        {//添加空格
            for (int i = 0; i < MyLevel; i++)
                MyXML += " ";
        }

        private void button5_Click(object sender, EventArgs e)
        {//查询符合条件的XML文档数据
            var MyXMLDoc = new System.Xml.XmlDocument();
            MyXMLDoc.Load("MyXMLData.xml");
            string MyFind = "ANTON";
            string MyFindString = "XML客户/客户[客户ID=" + '"' + MyFind + '"' + "]";
            var MyNode = MyXMLDoc.SelectSingleNode(MyFindString);
            if (MyNode != null)
                MessageBox.Show(MyFind + "客户的公司名称为：" + MyNode.SelectSingleNode("公司名称").InnerText, "信息提示", MessageBoxButtons.OK);
        }

        private void button6_Click(object sender, EventArgs e)
        {//监视XML文档中的数据变化
            var MyXMLContent = "<?xml version='1.0'?>" +
                "<!-- XML示例 -->" +
                "<Root>" +
                "<节点1 节点Id='1'>第一个节点</节点1>" +
                "<节点2 节点Id='2'>第二个节点</节点2>" +
                "<节点3 节点Id='3'>第三个节点</节点3>" +
                "</Root>";
            var MyXMLDoc = new System.Xml.XmlDocument();
            MyXMLDoc.LoadXml(MyXMLContent);
            //添加事件处理程序
            MyXMLDoc.NodeChanging += new System.Xml.XmlNodeChangedEventHandler(NodeChangingEvent);
            MyXMLDoc.NodeChanged += new System.Xml.XmlNodeChangedEventHandler(NodeChangedEvent);
            MyXMLDoc.NodeInserting += new System.Xml.XmlNodeChangedEventHandler(NodeInsertingEvent);
            MyXMLDoc.NodeInserted += new System.Xml.XmlNodeChangedEventHandler(NodeInsertedEvent);
            MyXMLDoc.NodeRemoving += new System.Xml.XmlNodeChangedEventHandler(NodeRemovingEvent);
            MyXMLDoc.NodeRemoved += new System.Xml.XmlNodeChangedEventHandler(NodeRemovedEvent);
            //添加新节点
            var MyElement = MyXMLDoc.CreateElement("节点4");
            var MyText = MyXMLDoc.CreateTextNode("第四个节点");
            MyXMLDoc.DocumentElement.AppendChild(MyElement);
            MyXMLDoc.DocumentElement.LastChild.AppendChild(MyText);
            //修改第一个节点
            MyXMLDoc.DocumentElement.FirstChild.InnerText = "修改过的第一个节点";
            //移除第二个节点.
            var MyNodes = MyXMLDoc.DocumentElement.ChildNodes;
            foreach (System.Xml.XmlNode MyNode in MyNodes)
                if (MyNode.Name == "节点2")
                {
                    MyXMLDoc.DocumentElement.RemoveChild(MyNode);
                    break;
                }
            MessageBox.Show(MyXMLDoc.OuterXml, "信息提示", MessageBoxButtons.OK);
        }
        private static void WriteNodeInfo(string MyAction, System.Xml.XmlNode MyNode)
        {
            if (MyNode.Value != null)
                MessageBox.Show(String.Format("元素: <{0}> {1} ", MyNode.Name, MyAction), "信息提示", MessageBoxButtons.OK);
            else
                MessageBox.Show(String.Format("元素: <{0}> {1} ", MyNode.Name, MyAction), "信息提示", MessageBoxButtons.OK);
        }
        public static void NodeChangingEvent(object source, System.Xml.XmlNodeChangedEventArgs e)
        {
            WriteNodeInfo("正在修改...", e.Node);
        }
        public static void NodeChangedEvent(object source, System.Xml.XmlNodeChangedEventArgs e)
        {
            WriteNodeInfo("已经修改", e.Node);
        }
        public static void NodeInsertingEvent(object source, System.Xml.XmlNodeChangedEventArgs e)
        {
            WriteNodeInfo("正在插入...", e.Node);
        }
        public static void NodeInsertedEvent(object source, System.Xml.XmlNodeChangedEventArgs e)
        {
            WriteNodeInfo("已经插入", e.Node);
        }
        public static void NodeRemovingEvent(object source, System.Xml.XmlNodeChangedEventArgs e)
        {
            WriteNodeInfo("正在移除....", e.Node);
        }
        public static void NodeRemovedEvent(object source, System.Xml.XmlNodeChangedEventArgs e)
        {
            WriteNodeInfo("已经移除", e.Node);
        }

        private void button7_Click(object sender, EventArgs e)
        {//使用XPath查询XML节点内容
            var MyXmlFragment = "<?xml version='1.0'?>" +
                            "<省级行政区>" +
                            "<省级 type='直辖市'>上海市</省级>" +
                            "<省级 type='直辖市'>北京市</省级>" +
                            "<省级 type='自治区'>广西壮族自治区</省级>" +
                            "<省级 type='自治区'>西藏自治区</省级>" +
                            "<省级 type='自治区'>宁夏回族自治区</省级>" +
                            "</省级行政区>";
            using(var MyReader = new System.IO.StringReader(MyXmlFragment))
            {
                var MyXPathDoc = new System.Xml.XPath.XPathDocument(MyReader);
                var MyXPathNav = MyXPathDoc.CreateNavigator();
                //查询带“族”字的自治区
                var MyXPathQuery = "/省级行政区/省级[attribute::type='自治区'] [contains(text(),'族')]";
                var MyXPathExpr = MyXPathNav.Compile(MyXPathQuery);
                var MyXPathIter = MyXPathNav.Select(MyXPathExpr);
                var MyInfo = "查询结果：\n";
                while (MyXPathIter.MoveNext())
                    MyInfo += MyXPathIter.Current.Value + "\n";
                MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
            } 
        }

        private void button8_Click(object sender, EventArgs e)
        {//使用XPath读取XML节点内容
            var MyDoc =new System.Xml.XPath.XPathDocument("MyNewXML.xml");
            var MyNav = MyDoc.CreateNavigator();
            var MyExp = MyNav.Compile("descendant::Book/Name");
            var MyIter = MyNav.Select(MyExp);
            var MyInfo = "罗斌最近出版的新书目录：";
            while (MyIter.MoveNext())
                MyInfo += "\n" + MyIter.Current.ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button9_Click(object sender, EventArgs e)
        {//获取或设置货币显示符号
            var MyNumberFormatInfo =new System.Globalization.CultureInfo("zh-CN", false).NumberFormat;
            var MyNumber = 510214720805231;
            var MyInfo= "输出简体中文默认货币符号：" + String.Format(MyNumber.ToString("C", MyNumberFormatInfo));
            MyNumberFormatInfo.CurrencySymbol = "*";
            MyInfo += "\n输出自定义＊货币符号：" + String.Format(MyNumber.ToString("C", MyNumberFormatInfo));
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button10_Click(object sender, EventArgs e)
        {//使用堆栈实现数据后进先出
          var MyStack = new System.Collections.Stack();
          MyStack.Push("成都");
          MyStack.Push("昆明");
          MyStack.Push("贵阳");
          MyStack.Push("武汉");
          MyStack.Push("南昌");
          MyStack.Push("南宁");
          MyStack.Push("广州");
          MyStack.Push("南京");
          MyStack.Push("西安");
          MyStack.Push("太原");
          var MyInfo = "中国部分省会城市：";
          while (MyStack.Count > 0)
            MyInfo += "\n" + MyStack.Pop().ToString();
          //按入栈顺序的反序输出结果
          MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button11_Click(object sender, EventArgs e)
        {//使用队列实现数据先进先出
          var MyQueue = new System.Collections.Queue();
          MyQueue.Enqueue("成都");
          MyQueue.Enqueue("昆明");
          MyQueue.Enqueue("贵阳");
          MyQueue.Enqueue("武汉");
          MyQueue.Enqueue("南昌");
          MyQueue.Enqueue("南宁");
          MyQueue.Enqueue("广州");
          MyQueue.Enqueue("南京");
          MyQueue.Enqueue("西安");
          MyQueue.Enqueue("太原");
          var MyInfo = "中国部分省会城市：";
          while (MyQueue.Count > 0)
            MyInfo += "\n" + MyQueue.Dequeue().ToString();
          //按入队顺序输出结果
          MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button12_Click(object sender, EventArgs e)
        {//获取数据类型包含的成员字段
            var MyTypeName = "Microsoft.Win32.Registry";
            var MyInfo = MyTypeName + "的成员字段包括：";
            var MyType = Type.GetType(MyTypeName);
            System.Reflection.MemberInfo[] MyMemberInfos = 
                MyType.FindMembers(System.Reflection.MemberTypes.Field,
                 System.Reflection.BindingFlags.Public | 
                 System.Reflection.BindingFlags.Static | 
                 System.Reflection.BindingFlags.Instance, null, null);
            for(var i = 0; i < MyMemberInfos.Length; i++)
              MyInfo += "\n" + MyMemberInfos[i].ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);           
        }

        private void button13_Click(object sender, EventArgs e)
        {//获取数据类型包含的成员属性
            var MyTypeName = "System.Threading.Thread";
            var MyInfo = MyTypeName + "的成员属性包括：";
            var MyType = Type.GetType(MyTypeName);
            System.Reflection.MemberInfo[] MyMemberInfos = 
                MyType.FindMembers(System.Reflection.MemberTypes.Property,
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Static | 
                System.Reflection.BindingFlags.Instance, null, null);
            for (int i = 0; i < MyMemberInfos.Length; i++)
                MyInfo += "\n" + MyMemberInfos[i].ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button14_Click(object sender, EventArgs e)
        {//获取数据类型包含的成员方法
            var MyTypeName = "System.IO.File";
            var MyInfo = MyTypeName + "的成员方法包括：";
            var MyType = Type.GetType(MyTypeName);
            System.Reflection.MemberInfo[] MyMemberInfos = 
                MyType.FindMembers(System.Reflection.MemberTypes.Method,
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Static | 
                System.Reflection.BindingFlags.Instance, null, null);
            for (int i = 0; i < MyMemberInfos.Length; i++)
               MyInfo += "\n" + MyMemberInfos[i].ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button15_Click(object sender, EventArgs e)
        {//获取数据类型包含的成员事件
            var MyTypeName = "System.Windows.Forms.TreeView";
            var MyInfo = MyTypeName + "的成员事件包括：";
            var MyTreeView = new TreeView();
            Type MyType = MyTreeView.GetType();
            System.Reflection.MemberInfo[] MyMemberInfos = 
                MyType.FindMembers(System.Reflection.MemberTypes.Event,
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Static | 
                System.Reflection.BindingFlags.Instance, null, null);
            for (int i = 0; i < MyMemberInfos.Length; i++)
               MyInfo += "\n" + MyMemberInfos[i].ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);           
        }

        private void button16_Click(object sender, EventArgs e)
        {//获取数据类型包含的所有成员
            var MyTypeName = "Microsoft.Win32.Registry";
            var MyInfo = MyTypeName + "的所有成员包括：\n";
            Type MyType = Type.GetType(MyTypeName);
            System.Reflection.MemberInfo[] MyMemberInfos = MyType.GetMembers();
            foreach (System.Reflection.MemberInfo MyMemberInfo in MyMemberInfos)
            {
                if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Method)
                  MyInfo += "Method";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Constructor)
                  MyInfo += "Constructor";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Event)
                  MyInfo += "Event";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Field)
                  MyInfo += "Field";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Method)
                  MyInfo += "Method";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Property)
                  MyInfo += "Property";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.TypeInfo)
                  MyInfo += "TypeInfo";
                else if (MyMemberInfo.MemberType == System.Reflection.MemberTypes.Custom)
                  MyInfo += "Custom";
                MyInfo += "：" + MyMemberInfo.ToString() + "\n";
            }
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);           
        }

        private void button17_Click(object sender, EventArgs e)
        {//以键值对的形式组织管理数据
            var MyData = new System.Collections.Specialized.NameValueCollection();
            MyData.Add("湖北", "东风汽车");
            MyData.Add("重庆", "长安汽车");
            MyData.Add("吉林", "一汽轿车");
            MyData.Add("重庆", "中国嘉陵");
            MyData.Add("上海", "上海汽车");
            var MyInfo = "沪深5月份推荐汽车类股票：\n";
            for(var i = 0; i < MyData.Keys.Count; i++)
            {
                String MyKeyName = MyData.GetKey(i);
                MyInfo += String.Format("{0}地区：{1}\n", MyData.Keys[i], MyData.Get(MyKeyName));
            }
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);  
        }

        private void button18_Click(object sender, EventArgs e)
        {//使用SortedList泛型类排序数据
            var MyPath = @"C:\";
            var MyDir = new System.IO.DirectoryInfo(MyPath);
            System.IO.FileInfo[] MyFiles = MyDir.GetFiles();
            var MyList = new System.Collections.SortedList();
            var MyInfo = MyPath + "文件夹中的文件排序前如下：";
            foreach (var MyFile in MyFiles)
            {
                MyList.Add(MyFile.Name, MyFile);
                MyInfo += "\n" + MyFile.Name;
            }
            MyInfo += "\n" + MyPath + "文件夹中的文件排序后如下：";
            foreach (System.Collections.DictionaryEntry MyItem in MyList)
                MyInfo += "\n" + ((System.IO.FileInfo)MyItem.Value).Name;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button19_Click(object sender, EventArgs e)
        {//设置StringBuilder的最大容量
           var MyInfo = new System.Text.StringBuilder(10, 10);
           MyInfo.Append("123456789");
           // MyInfo.Append("0123");//超出容量将报错
           MessageBox.Show(MyInfo.ToString(), "信息提示", MessageBoxButtons.OK);
        }

        private void button20_Click(object sender, EventArgs e)
        {//使用结构获取未托管函数信息
            OSVersionInfo MyVersion = new OSVersionInfo();
            MyVersion.dwOSVersionInfoSize = System.Runtime.InteropServices.Marshal.SizeOf(MyVersion);
            GetVersionEx(MyVersion);
            var MyInfo ="主版本: " + MyVersion.dwMajorVersion;
            MyInfo += "\n次版本: " + MyVersion.dwMinorVersion;
            MyInfo += "\n内建号: " + MyVersion.dwBuildNumber;
            MyInfo += "\n平台ID: " + MyVersion.dwPlatformId;
            MyInfo += "\nCSD版本: " + MyVersion.szCSDVersion;
            MyInfo += "\n平台名称: " + Environment.OSVersion.Platform;
            MyInfo += "\n版本号: " + Environment.OSVersion.Version;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);              
        }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public class OSVersionInfo
        {
            [System.Runtime.InteropServices.FieldOffset(0)]
            public int dwOSVersionInfoSize;
            [System.Runtime.InteropServices.FieldOffset(4)]
            public int dwMajorVersion;
            [System.Runtime.InteropServices.FieldOffset(8)]
            public int dwMinorVersion;
            [System.Runtime.InteropServices.FieldOffset(12)]
            public int dwBuildNumber;
            [System.Runtime.InteropServices.FieldOffset(16)]
            public int dwPlatformId;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
            [System.Runtime.InteropServices.FieldOffset(20)]
            public String szCSDVersion;
        }
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool GetVersionEx([System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] OSVersionInfo MyVersion);

        private void button21_Click(object sender, EventArgs e)
        {//获取未托管的错误信息
            var MyBadWindowHandle = 453;
            MessageBoxA(MyBadWindowHandle, "信息", "标题", 0);
            var MyMyErrorCode = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
            var MyInfo =MyMyErrorCode.ToString() + "\n";
            MyInfo += GetErrorMessage(MyMyErrorCode);
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int FormatMessage(int MyFlags, int lpSource, int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, int Arguments);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern int MessageBoxA(int hWnd, string pText, string pCaption, int uType);
        public static string GetErrorMessage(int MyErrorCode)
        {//格式化错误信息        
            var FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
            var FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
            var FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
            var MySize = 255;
            var MyBuffer = "";
            var MyFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;
            var MyReturnValue = FormatMessage(MyFlags, 0, MyErrorCode, 0, ref MyBuffer, MySize, 0);
            if (0 == MyReturnValue)
                return null;
            else
                return MyBuffer;            
        }

        private void button22_Click(object sender, EventArgs e)
        {//判断一个字符串是否是数字
            //var MyNumber= "ASD";
            var MyNumber = "123465.789";
            double MyResult = 0;
            if (double.TryParse(MyNumber, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.CurrentInfo, out MyResult))
                MessageBox.Show(MyNumber + "是数字！", "信息提示", MessageBoxButtons.OK);
            else
                MessageBox.Show(MyNumber + "不是数字！", "信息提示", MessageBoxButtons.OK);         
        }

        private void button23_Click(object sender, EventArgs e)
        {//判断字母是否在指定范围内
            char MyChar = char.ToUpper('h');
            char MinChar = char.ToUpper('D');
            char MaxChar = char.ToUpper('u');
            var MyInfo = "";
            if (MyChar >= MinChar && MyChar <= MaxChar)
                MyInfo = "字母　" + MyChar.ToString() + "在" + MinChar.ToString() + "-" + MaxChar.ToString() + "之间！";
            else
                MyInfo = "字母　" + MyChar.ToString() + "不在" + MinChar.ToString() + "-" + MaxChar.ToString() + "之间！";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);                              
        }

        private void button24_Click(object sender, EventArgs e)
        {//判断字母数字标点符号字符
            var MyChars = "1AB+-a546,)*78/ 78ZYB";
            var MyInfo = MyChars + " 字符串中的字符判断如下：\n";
            for(var i = 0; i < MyChars.Length; i++)
            {
                char MyChar = MyChars[i];
                if (char.IsControl(MyChar))
                    MyInfo += MyChar.ToString() + " 是控制字符\n";
                if (char.IsLower(MyChar))
                    MyInfo += MyChar.ToString() + " 是小写字母\n";
                if (char.IsUpper(MyChar))
                    MyInfo += MyChar.ToString() + " 是大写字母\n";
                if (char.IsWhiteSpace(MyChar))
                    MyInfo += MyChar.ToString() + " 是空白字符\n";
                if (char.IsDigit(MyChar))
                    MyInfo += MyChar.ToString() + " 是数字\n";
                if (char.IsPunctuation(MyChar))
                    MyInfo += MyChar.ToString() + " 是标点符号\n";
                if (char.IsSymbol(MyChar))
                    MyInfo += MyChar.ToString() + " 是符号字符\n";
            }
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);    
        }

    }
}
