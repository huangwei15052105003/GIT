using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace sgFrmCheckWork
{
    public partial class sgFrmSeo : Office2007Form
    {
        protected static string cookieHeader;
        public static CookieCollection myCookies;
        public static CookieContainer myCookieContainer;
        public string sss;

        public sgFrmSeo()
        {
            InitializeComponent();
        }

        private void sgFrmSeo_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string strReContent = string.Empty;
            //登录
            strReContent = PostLogin("http://my.chinaz.com/login.html", "__PROCESSINPUTVIEWSTATE=YXV0b2xvZ2luI2ludmlzaWJsZQ%3D%3D&logintype=email&username=smlcw%40163.com&password=sanmao&returnurl=%2Fdefault.html&login=%E7%99%BB%E5%BD%95", "http://my.chinaz.com/login.html");
            //asp.net登录传递的参数需注意 
            //strReContent = PostLogin("http://www.mystand.com.cn/login.aspx","__VIEWSTATE=dDwtNjkzMjUyNDczO3Q8O2w8aTwzPjs+O2w8dDxwPHA8bDxUZXh0Oz47bDxcZTs+Pjs+Ozs+Oz4+Oz6aX2dtqkJTK+KbNPsjd7Op/l26Iw==&txtUserName=hxf&txtPassword=hxf0000&btnEnter=登录","http://www.mystand.com.cn/login.aspx");
            //获取页面
            strReContent = GetPage("http://bbs.chinaz.com/Club/list-1.html", "http://my.chinaz.com/login.html");
            //strReContent = GetPage("http://www.mystand.com.cn/Modules/index.aspx","http://www.mystand.com.cn/login.aspx");
            //可以对获得的内容进行处理：strReContent
            //textBox1.Text = strReContent;

            string sFirstUrl = string.Empty;
            string sbegin = strReContent.Substring(0, strReContent.IndexOf("<tbody class=\"forumtopics-list\">"));
            sFirstUrl = strReContent.Substring(strReContent.IndexOf("<tbody class=\"forumtopics-list\">"), strReContent.Length-sbegin.Length);
            sFirstUrl = sFirstUrl.Substring(0,sFirstUrl.IndexOf("</table>"));


            //正则获取当前html代码段中所有的a链接
            //正则表达式
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""\s]?)(?<href>[^'""\s]*)\1[^>]*?>");
            //获取第一条符合正则的链接
            Match match = reg.Match(sFirstUrl);

            strReContent = GetPage("http://bbs.chinaz.com/" + match.Groups["href"].Value, "http://my.chinaz.com/login.html");


            sbegin = strReContent.Substring(0, strReContent.IndexOf("<form id=\"quicklypost\" method=\"post\" enctype=\"multipart/form-data\" action=\""+match.Groups["href"].Value+"\">"));
            sFirstUrl = strReContent.Substring(strReContent.IndexOf("<form id=\"quicklypost\" method=\"post\" enctype=\"multipart/form-data\" action=\"" + match.Groups["href"].Value + "\">"), strReContent.Length - sbegin.Length);
            sFirstUrl = sFirstUrl.Substring(0,sFirstUrl.IndexOf("</form>")+7);

            reg = new Regex(@"<input type=""(?<value>.*?)"" [\s\S]*?name=""(?<name>.*?)"" [\s\S]*?value=""(?<value>.*?)"" [\s\S]*?>");
            //@"(?is)<input[^>]*?(name=(['""]*)(?<name>[^>]*)\2)*[^>](value=(['""]*)(?<value>[^>]*)\4)*[^>]+/>"
            Match match1 = reg.Match(sFirstUrl);

            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://bbs.chinaz.com" + match.Groups["href"].Value);

            StringBuilder sb = new StringBuilder();
            byte[] btest = Encoding.UTF8.GetBytes("谢谢楼主分享,支持！");
            for (int i = 0; i < btest.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(btest[i], 16));
            }

            string pd = match1.Groups["name"].Value + "=" + "dG9sYXN0cGFnZQ%3D%3D&editor="+sb.ToString()+"&postPageNumber=1&postButton=";
            byte[] message = Encoding.UTF8.GetBytes(pd);

            myWebRequest.AllowAutoRedirect = true;
            myWebRequest.KeepAlive = true;
            myWebRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/msword, application/x-shockwave-flash, */*";

            myWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 2.0.50727)";
            myWebRequest.ContentType = "text/html";
            myWebRequest.Method = "POST";
            myWebRequest.ContentLength = message.Length;
            myWebRequest.CookieContainer = myCookieContainer;
            Stream mystream;
            mystream = myWebRequest.GetRequestStream();
            mystream.Write(message, 0, message.Length);
            mystream.Close();
            HttpWebResponse response = (HttpWebResponse)myWebRequest.GetResponse();
            System.IO.StreamReader sReader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            //MessageBox.Show("恭喜您，您的日志已经发表成功，请查看！~");
            //__PROCESSINPUTVIEWSTATE=dG9sYXN0cGFnZQ%3D%3D&editor=%E6%94%AF%E6%8C%81%E4%B8%80%E4%B8%8B%E6%A5%BC%E4%B8%BB%EF%BC%81&postPageNumber=1&postButton=

            textBox2.Text = pd;
            //BitConverter.ToString(btest);
            textBox1.Text = sReader.ReadToEnd();
        }

        /**/
        /// <summary>
        /// 功能描述：模拟登录页面，提交登录数据进行登录，并记录Header中的cookie
        /// </summary>
        /// <param name="strURL">登录数据提交的页面地址</param>
        /// <param name="strArgs">用户登录数据</param>
        /// <param name="strReferer">引用地址</param>
        /// <returns>可以返回页面内容或不返回</returns>
        public static string PostLogin(string strURL, string strArgs, string strReferer)
        {
            string strResult = "";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
            myHttpWebRequest.AllowAutoRedirect = true;
            myHttpWebRequest.KeepAlive = true;
            myHttpWebRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/msword, application/x-shockwave-flash, */*";
            myHttpWebRequest.Referer = strReferer;

            myHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 2.0.50727)";
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.Method = "POST";

            myCookies = null;
            myCookieContainer = new CookieContainer();
            myHttpWebRequest.CookieContainer = myCookieContainer;

            Stream MyRequestStrearm = myHttpWebRequest.GetRequestStream();
            StreamWriter MyStreamWriter = new StreamWriter(MyRequestStrearm, Encoding.ASCII);
            //把数据写入HttpWebRequest的Request流
            MyStreamWriter.Write(strArgs);
            //关闭打开对象 
            MyStreamWriter.Close();
            MyRequestStrearm.Close();

            HttpWebResponse response = null;
            System.IO.StreamReader sr = null;
            response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            
            cookieHeader = myHttpWebRequest.CookieContainer.GetCookieHeader(new Uri(strURL));

            myCookies = myHttpWebRequest.CookieContainer.GetCookies(new Uri(strURL)); //response.Cookies;

            sr = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")); // //utf-8
            strResult = sr.ReadToEnd();
            return strResult;
        }
                 /**//// <summary>
         /// 功能描述：在PostLogin成功登录后记录下Headers中的cookie，然后获取此网站上其他页面的内容
         /// </summary>
         /// <param name="strURL">获取网站的某页面的地址</param>
         /// <param name="strReferer">引用的地址</param>
         /// <returns>返回页面内容</returns>
        public static string GetPage(string strURL, string strReferer)
        {
            string strResult = "";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
            myHttpWebRequest.ContentType = "text/html";
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.Referer = strReferer;
            myHttpWebRequest.Headers.Add("cookie:" + cookieHeader);

            HttpWebResponse response = null;
            System.IO.StreamReader sr = null;
            response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            sr = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")); // //utf-8
            strResult = sr.ReadToEnd();
            return strResult;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }
        private void webBrowser1_DocumentCompleted(object sender, EventArgs e)
        {
            //if (webBrowser1.DocumentText.Contains("</html>"))
            //{
            //MessageBox.Show("成功加载");

            HtmlElement hUsername = webBrowser1.Document.GetElementById("username");
            HtmlElement hPassword = webBrowser1.Document.GetElementById("password");
            HtmlElement hBtn = webBrowser1.Document.GetElementById("max_unnamedsubmit_6");

            hUsername.SetAttribute("value","我找老李");
            hPassword.SetAttribute("value","sanmao");
            hBtn.InvokeMember("click");

            //webBrowser1.Navigate(sss);
            MessageBox.Show(webBrowser1.Url.ToString());
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted1);

            //}
            //else
            //{
 
            //}
        }

        private void webBrowser1_DocumentCompleted1(object sender, EventArgs e)
        {
            if (webBrowser1.DocumentText.Contains("</html>"))
            {
                MessageBox.Show(webBrowser1.Url.ToString());
                HtmlElement htest = webBrowser1.Document.GetElementById("editor_area");
                htest.SetAttribute("value", "谢谢楼主分享，支持一下！");
                //HtmlDocument hb = webBrowser1.Document.GetElementById("");
            }
            else
            {

            }
        }
    }
}
