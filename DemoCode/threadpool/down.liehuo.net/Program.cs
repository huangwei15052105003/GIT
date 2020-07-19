using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//download by http://down.liehuo.net
namespace threadpooldemo
{
    
   class Program
      {
        static void Main(string[] args)
           {
   	//在循环中创建线程池中的线程
              for (int i = 0; i < 10; i++)
               {
         //在线程池中创建一个线程池线程来执行指定的方法（用委托WaitCallback来表示），并将该线程排入线程池的队列等待执行
                   ThreadPool.QueueUserWorkItem( new WaitCallback(MethodA), i);
                   }
   	//等待输入，主要是为了延时
                   Console.ReadLine();
               }
         static void MethodA(object Num)
            {
   	//转换接受到的线程号编码
             int QueueNum = (int)Num;
   	//显示线程号
             Console.WriteLine("线程号： {0} .", Num);
   	//输出空行，为了美观
            Console.WriteLine();
               }
           }
      
}
