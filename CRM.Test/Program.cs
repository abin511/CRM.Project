using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main开始执行");
            var reslt = Asv2();
            Console.WriteLine("Function已执行");
            Console.WriteLine("Main开始完成，输出结果：" + reslt);
            Console.ReadLine();
        }

        public static string _container = "";
        public static string Asv2()
        {
            //dead lock
            var task = AssignValue2();
            task.Wait();
            return _container;
        }

        private static void Assign()
        {
            _container = "Hello World";
        }

        public static async Task AssignValue2()
        {
            await Task.Delay(500);
            await Task.Run(() => Assign());
        }

        public static async Task<int> IndexAsync()
        {
            var cancellationToken = new CancellationToken(false);
            var cnblogsTask = GetStringAsync1(8000, cancellationToken);
            var myblogTask = GetStringAsync2(3000, cancellationToken);
            // Task.WaitAll(cnblogsTask, myblogTask);
            var task = await Task.WhenAll(cnblogsTask, myblogTask);
            return cnblogsTask.Result + myblogTask.Result;
        }
        private static async Task<int> GetStringAsync1(int time, CancellationToken cancelToken = default(CancellationToken))
        {
           var task = await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(time);
                Console.WriteLine(time + "秒的任务完成,返回数字8");
                return 8;
            });
            return task;
        }
        private static async Task<int> GetStringAsync2(int time, CancellationToken cancelToken = default(CancellationToken))
        {
            return await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(time);
                Console.WriteLine(time + "秒的任务完成,返回数字5");
                return 5;
            });
        }
    }
}
