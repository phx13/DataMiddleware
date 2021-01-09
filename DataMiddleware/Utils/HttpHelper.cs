using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using DataMiddleware.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataMiddleware.Utils
{
    public class HttpHelper
    {
        public static WebClient Client = new WebClient();

        /// <summary>
        /// 调用一所接口
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        public static T HttpYiSuoGet<T>(string url)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;chartset=UTF-8";
                request.UserAgent = null;
                request.Headers.Add("fri-sysid", "ShuZiBingBao");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();

                T model = JsonConvert.DeserializeObject<T>(result);
                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }

        /// <summary>
        /// 调用一所接口2
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        public static T HttpYiSuoGet2<T>(string url)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/viid+json;charset=utf-8";
                request.UserAgent = null;
                //request.Headers.Add("User-Identify", "11000000001205020098");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                //StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                //string result = reader.ReadToEnd();

                MemoryStream memoryStream = new MemoryStream();
                const int bufferLength = 1024;
                int actual;
                byte[] buffer = new byte[bufferLength];
                while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                {
                    memoryStream.Write(buffer, 0, actual);
                }
                byte[] arrayByte = memoryStream.ToArray();
                memoryStream.Close();
                BinaryReader binaryReader = new BinaryReader(responseStream);
                binaryReader.Read(arrayByte, 0, arrayByte.Length);
                string result = Encoding.GetEncoding("utf-8").GetString(arrayByte);

                T model = JsonConvert.DeserializeObject<T>(result);
                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }

        /// <summary>
        /// 调用网力接口Get
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        public static string HttpWangLiGet(string url, string token)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.UserAgent = null;
                request.Headers.Add("tokenId", token);
                request.Headers.Add("Cookie", "access_token=" + token + ";tokenId=" + token);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();

                return result;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + " 时出错！URL连接为 " + url);
                return null;
            }
        }

        /// <summary>
        /// 调用网力传参接口Get
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        public static T HttpWangLiGet<T>(string url, string token)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.UserAgent = null;
                request.Headers.Add("tokenId", token);
                request.Headers.Add("Cookie", "access_token=" + token + ";tokenId=" + token);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();
                T model = model = JsonConvert.DeserializeObject<T>(result);

                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }

        /// <summary>
        /// 调用网力接口Post
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        public static T HttpWangLiPost<T>(string url, string token = "")
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.UserAgent = null;
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Add("tokenId", token);
                    request.Headers.Add("Cookie", "access_token=" + token + ";tokenId=" + token);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();

                T model = JsonConvert.DeserializeObject<T>(result);
                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }

        /// <summary>
        /// 调用网力Post传参接口
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        //public static T HttpWangLiPost<T>(string url, string token, Dictionary<string, string> dic)
        public static T HttpWangLiPost<T>(string url, string token, string json)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.UserAgent = null;
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Add("tokenId", token);
                    request.Headers.Add("Cookie", "access_token=" + token + ";tokenId=" + token);
                }
                
                StringBuilder sb = new StringBuilder();
                //int i = 0;
                //foreach (var item in dic)
                //{
                //    if (i > 0)
                //    {
                //        sb.Append("&");
                //    }
                //    sb.AppendFormat("{0}={1}", item.Key, item.Value);
                //    i++;
                //}
                
                request.ServicePoint.Expect100Continue = false;
                request.KeepAlive = false;

                //byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
                byte[] data = Encoding.UTF8.GetBytes(json);
                request.ContentLength = data.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();

                T model = JsonConvert.DeserializeObject<T>(result);
                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }

        /// <summary>
        /// 调用网力token接口
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <param name="dic">参数字典</param>
        /// <returns>对象类型</returns>
        public static T HttpWangLiToken<T>(string url, Dictionary<string, string> dic)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                MemoryStream memStream = new MemoryStream();
                var boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                request.Method = "POST";
                request.ContentType = "multipart/form-data;boundary=" + boundary;

                //memStream.Write(beginBoundary, 0, beginBoundary.Length);
                var stringKeyHeader = "\r\n--" + boundary + "\r\nContent-Disposition:form-data;name=\"{0}\"" + "\r\n\r\n{1}\r\n";
                foreach (byte[] formitembytes in from string key in dic.Keys select string.Format(stringKeyHeader, key, dic[key]) into formitem select Encoding.UTF8.GetBytes(formitem))
                {
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
                memStream.Write(endBoundary, 0, endBoundary.Length);
                request.ContentLength = memStream.Length;

                Stream reqStream = request.GetRequestStream();
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                reqStream.Write(tempBuffer, 0, tempBuffer.Length);
                reqStream.Close();

                //StringBuilder sb = new StringBuilder();
                //int i = 0;
                //foreach (var item in dic)
                //{
                //    if (i > 0)
                //    {
                //        sb.Append("&");
                //    }
                //    sb.AppendFormat("{0}={1}", item.Key, item.Value);
                //    i++;
                //}
                //byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
                //request.ContentLength = data.Length;
                //using (Stream reqStream = request.GetRequestStream())
                //{
                //    reqStream.Write(data, 0, data.Length);
                //    reqStream.Close();
                //}

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();

                T model = JsonConvert.DeserializeObject<T>(result);
                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }

        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="url">接口请求的值</param>
        /// <returns>对象类型</returns>
        public static T HttpPostOnlyData<T>(string url)
        {
            try
            {
                //Client.Encoding = Encoding.UTF8;
                //Task<string> taskString = Client.DownloadStringTaskAsync(url);
                //string result = taskString.Result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;chartset=UTF-8";
                request.UserAgent = "fri-sysid=ShuZiBingBao";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string result = reader.ReadToEnd();
                JObject json = JObject.Parse(result);

                JToken listToken = json["data"];

                T model = JsonConvert.DeserializeObject<T>(result);
                return model;
            }
            catch (Exception)
            {
                MainWindow.WriteLog("处理数据类型：" + typeof(T) + " 时出错！URL连接为 " + url);
                return default(T);
            }
        }
    }
}
