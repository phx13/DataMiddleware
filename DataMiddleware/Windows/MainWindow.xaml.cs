using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;
using DataMiddleware.Utils;
using DataMiddleware.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DataMiddleware.Windows
{
    /// <summary>
    /// 窗口
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        /// <summary>
        /// 动态数据显示控件
        /// </summary>
        private static System.Windows.Controls.ListBox m_DynamicDataLog;

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            InitSystemIcon(); //初始化系统托盘

            this.Loaded += MainWindow_Loaded;
        }
        private Random m_Random = new Random();
        /// <summary>
        /// 窗口加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            m_DynamicDataLog = ListBox1;
            WriteLog("准备就绪，开始写入数据 ......");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            loginInfo.Add("Username", "bingbao");
            loginInfo.Add("Password", "dc483e80a7a0bd9ef71d8cf973673924");

            DataHelper.ClearAllData("视综_静态库");
            DataHelper.ClearAllData("视综_人脸数据");
            DataHelper.ClearAllData("视综_车辆数据");
            DataHelper.ClearAllData("视综_设备统计");
            DataHelper.ClearAllData("视综_布控库");
            DataHelper.ClearAllData("视综_专网分局");
            DataHelper.ClearAllData("视综_公安网分局");
            DataHelper.ClearAllData("视综_人脸服务接口调用量");
            DataHelper.ClearAllData("视综_车辆服务接口调用量");

            DataHelper.ClearAllData("视综_设备监控实时数据统计");
            DataHelper.ClearAllData("视综_视频路数");
            DataHelper.ClearAllData("视综_视频综合概览");
            DataHelper.ClearAllData("视综_警车poi");
            DataHelper.ClearAllData("视综_警员poi");
            DataHelper.ClearAllData("视综_摄像头poi");
            DataHelper.ClearAllData("视综_人员布控信息");
            DataHelper.ClearAllData("视综_车辆布控信息");



            StaticBase();//静态库
            FaceNumData();//人脸数据
            VehicleNumData();//车辆数据
            DeviceFlowData();//设备统计
            DistributionLibData();//布控库
            VideoNetSubPoliceData();//专网分局
            PoliceNetSubPoliceData();//公安网分局
            InvokeStatisticsData();//人脸车辆服务接口调用量

            EquipmentCountData();//视频路数
            AlarmTypeCountData();//视频综合概览
            MultialgorithmData();//华为设备
            PointData();//点位信息
            ProtectionData();//布控信息

            sw.Stop();
            long tick = sw.Elapsed.Seconds;
            WriteLog(tick.ToString());
        }

        /// <summary>
        /// 静态库
        /// </summary>
        private void StaticBase()
        {
            ArchivesNumModel archivesNum = HttpHelper.HttpYiSuoGet<ArchivesNumModel>("http://14.29.73.91:8080/statisticsServer/archivesNum");

            DataTable table = new DataTable();
            table.TableName = "视综_静态库";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("People", typeof(Int32));
            table.Columns.Add("Car", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            DataRow row = table.NewRow();
            row["Id"] = 10;
            row["People"] = archivesNum.data.personArchivesNum;
            row["Car"] = archivesNum.data.vehicleArchivesNum;
            row["Time"] = DateTime.Now;
            table.Rows.Add(row);

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 人脸数据
        /// </summary>
        private void FaceNumData()
        {
            double count = DateTime.Now.Day;
            string startDate = DateTime.Now.AddDays(1 - count).ToString("yyyyMMdd");
            string endDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");

            FaceNumTrendModel faceNumModel = HttpHelper.HttpYiSuoGet<FaceNumTrendModel>("http://14.29.73.91:8080/statisticsServer/faceNumTrend?startDate=" + startDate + "&endDate=" + endDate);

            DataTable table = new DataTable();
            table.TableName = "视综_人脸数据";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Day", typeof(string));
            table.Columns.Add("Count", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            for (int index = 0; index < faceNumModel.data.Count; index++)
            {
                FaceNumTrendData data = faceNumModel.data[index];
                DataRow row = table.NewRow();
                row["Id"] = index;
                row["Day"] = data.date.Substring(data.date.Length - 2, 2);
                row["Count"] = data.num;
                row["Time"] = DateTime.Now;
                table.Rows.Add(row);
            }
            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 车辆数据
        /// </summary>
        private void VehicleNumData()
        {
            double count = DateTime.Now.Day;

            string startDate = DateTime.Now.AddDays(1 - count).ToString("yyyyMMdd");
            string endDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");

            VehicleNumTrendModel faceNumModel = HttpHelper.HttpYiSuoGet<VehicleNumTrendModel>("http://14.29.73.91:8080/statisticsServer/vehicleNumTrend?startDate=" + startDate + "&endDate=" + endDate);

            DataTable table = new DataTable();
            table.TableName = "视综_车辆数据";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Day", typeof(string));
            table.Columns.Add("Count", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            for (int index = 0; index < faceNumModel.data.Count; index++)
            {
                VehicleNumTrendData data = faceNumModel.data[index];
                DataRow row = table.NewRow();
                row["Id"] = index;
                row["Day"] = data.date.Substring(data.date.Length - 2, 2);
                row["Count"] = data.num;
                row["Time"] = DateTime.Now;
                table.Rows.Add(row);
            }
            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 设备统计 - 自定义区域接口
        /// </summary>
        private void CustomAreaData()
        {
            OtherLabelModel otherLabelModel = HttpHelper.HttpYiSuoGet<OtherLabelModel>("http://14.29.73.91:8080/statisticsServer/getOtherLabel");

            DataTable table = new DataTable();
            table.TableName = "视综_自定义区域";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Time", typeof(DateTime));

            for (int index = 0; index < otherLabelModel.data.Count; index++)
            {
                OtherLabelData data = otherLabelModel.data[index];
                DataRow row = table.NewRow();
                row["Id"] = index;
                row["Code"] = data.code;
                row["Name"] = data.name;
                row["Time"] = DateTime.Now;
                table.Rows.Add(row);
            }
            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 设备统计
        /// </summary>
        private void DeviceFlowData()
        {
            DataTable table = new DataTable();
            table.TableName = "视综_设备统计";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Active", typeof(Int32));
            table.Columns.Add("FaceActive", typeof(Int32));
            table.Columns.Add("CarActive", typeof(Int32));
            table.Columns.Add("Regist", typeof(Int32));
            table.Columns.Add("FaceRegist", typeof(Int32));
            table.Columns.Add("CarRegist", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            OtherLabelModel otherLabelModel = HttpHelper.HttpYiSuoGet<OtherLabelModel>("http://14.29.73.91:8080/statisticsServer/getOtherLabel");

            int faceRegist = 0;
            int carRegist = 0;
            int faceActive = 0;
            int carActive = 0;

            foreach (OtherLabelData item in otherLabelModel.data)
            {
                //人
                string urlPath = "http://14.29.73.91:8080/statisticsServer/getDeviceFlowInfo?TYPE=2&FCTYPE=1&CODE=" + item.code;
                DeviceFlowModel persionModel = HttpHelper.HttpYiSuoGet<DeviceFlowModel>(urlPath);
                foreach (DeviceFlowData person in persionModel.data)
                {
                    faceRegist += Convert.ToInt32(person.libNum);
                    faceActive += Convert.ToInt32(person.activeNum);
                }

                //车
                urlPath = "http://14.29.73.91:8080/statisticsServer/getDeviceFlowInfo?TYPE=2&FCTYPE=2&CODE=" + item.code;
                DeviceFlowModel carModel = HttpHelper.HttpYiSuoGet<DeviceFlowModel>(urlPath);
                foreach (DeviceFlowData car in carModel.data)
                {
                    carRegist += Convert.ToInt32(car.libNum);
                    carActive += Convert.ToInt32(car.activeNum);
                }
            }

            DataRow row = table.NewRow();
            row["Id"] = 1;
            row["FaceActive"] = faceActive;
            row["CarActive"] = carActive;
            row["FaceRegist"] = faceRegist;
            row["CarRegist"] = carRegist;
            row["Active"] = faceActive + carActive;
            row["Regist"] = faceRegist + carRegist;
            row["Time"] = DateTime.Now;
            table.Rows.Add(row);

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 布控库
        /// </summary>
        private void DistributionLibData()
        {
            //人脸布控库
            FaceDistributionLibNumModel faceDistributionLibNumModel = HttpHelper.HttpYiSuoGet<FaceDistributionLibNumModel>("http://14.29.73.91:8080/statisticsServer/faceDistributionLibNum");
            //车辆布控库
            VehicleDistributionLibNumModel vehicleDistributionLibNumModel = HttpHelper.HttpYiSuoGet<VehicleDistributionLibNumModel>("http://14.29.73.91:8080/statisticsServer/vehicleDistributionLibNum");

            DataTable table = new DataTable();
            table.TableName = "视综_布控库";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Face", typeof(Int32));
            table.Columns.Add("FourPeople", typeof(Int32));
            table.Columns.Add("Petition", typeof(Int32));
            table.Columns.Add("Car", typeof(Int32));
            table.Columns.Add("Abnormal", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            int face = 0;
            int fourPeople = 0;
            int petition = 0;
            int car = 0;
            int abnormal = 0;

            for (int index = 0; index < faceDistributionLibNumModel.data.Count; index++)
            {
                FaceDistributionLibNumData data = faceDistributionLibNumModel.data[index];
                if (data.libName == "四类人库")
                {
                    fourPeople = data.num;
                }
                else if (data.libName == "在逃人员库")
                {
                    face = data.num;
                }
                else if (data.libName == "重点上访人员库（京）")
                {
                    petition = data.num;
                }
                else if (data.libName == "精神病库")
                {
                    abnormal = data.num;
                }
            }
            for (int index = 0; index < vehicleDistributionLibNumModel.data.Count; index++)
            {
                VehicleDistributionLibNumData data = vehicleDistributionLibNumModel.data[index];
                if (data.libName == "车辆盗抢库")
                {
                    car = data.num;
                }
            }
            DataRow row = table.NewRow();
            row["Id"] = 1;
            row["Face"] = face;
            row["FourPeople"] = fourPeople;
            row["Petition"] = petition;
            row["Car"] = car;
            row["Abnormal"] = abnormal;
            row["Time"] = DateTime.Now;
            table.Rows.Add(row);
            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 专网分局
        /// </summary>
        private void VideoNetSubPoliceData()
        {
            DataTable table = new DataTable();
            table.TableName = "视综_专网分局";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("People", typeof(Int32));
            table.Columns.Add("Car", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            OtherLabelModel otherLabelModel = HttpHelper.HttpYiSuoGet<OtherLabelModel>("http://14.29.73.91:8080/statisticsServer/getOtherLabel");
            Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
            int carRegist = 0;
            foreach (OtherLabelData item in otherLabelModel.data)
            {
                //人
                DeviceFlowModel personModel = HttpHelper.HttpYiSuoGet<DeviceFlowModel>("http://14.29.73.91:8080/statisticsServer/getDeviceFlowInfo?TYPE=2&FCTYPE=1&CODE=" + item.code);
                //车
                DeviceFlowModel carModel = HttpHelper.HttpYiSuoGet<DeviceFlowModel>("http://14.29.73.91:8080/statisticsServer/getDeviceFlowInfo?TYPE=2&FCTYPE=2&CODE=" + item.code);

                //如果是北京市
                if (item.code == "110100")
                {
                    for (int i = 0; i < personModel.data.Count; i++)
                    {
                        if (personModel.data[i].name == "西站")
                        {
                            continue;
                        }
                        string[] temp = { personModel.data[i].libNum, "" };
                        dic.Add(personModel.data[i].name, temp);
                    }
                    foreach (DeviceFlowData car in carModel.data)
                    {
                        if (dic.ContainsKey(car.name))
                        {
                            dic[car.name][1] = car.libNum;
                        }
                    }
                }
                //如果是内保局
                else if (item.code == "111000")
                {
                    foreach (DeviceFlowData car in carModel.data)
                    {
                        carRegist += Convert.ToInt32(car.libNum);
                    }
                    string[] temp = { null, carRegist.ToString() };
                    dic.Add("内保局", temp);
                }
            }
            int index = 1;
            foreach (var key in dic.Keys)
            {
                DataRow row = table.NewRow();
                row["Id"] = index;
                row["Name"] = key;
                row["People"] = dic[key][0] != null ? Convert.ToInt32(dic[key][0]) : 0;
                row["Car"] = dic[key][1] != null ? Convert.ToInt32(dic[key][1]) : 0;
                row["Time"] = DateTime.Now;
                table.Rows.Add(row);
                index++;
            }

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 公安网分局
        /// </summary>
        private void PoliceNetSubPoliceData()
        {
            DataTable table = new DataTable();
            table.TableName = "视综_公安网分局";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("People", typeof(Int32));
            table.Columns.Add("Car", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            OtherLabelModel otherLabelModel = HttpHelper.HttpYiSuoGet<OtherLabelModel>("http://14.29.73.91:8080/statisticsServer/getOtherLabel");
            Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
            int carRegist = 0;
            foreach (OtherLabelData item in otherLabelModel.data)
            {
                //人
                DeviceFlowModel personModel = HttpHelper.HttpYiSuoGet<DeviceFlowModel>("http://14.29.73.91:8080/statisticsServer/getDeviceFlowInfo?TYPE=2&FCTYPE=1&CODE=" + item.code);
                //车
                DeviceFlowModel carModel = HttpHelper.HttpYiSuoGet<DeviceFlowModel>("http://14.29.73.91:8080/statisticsServer/getDeviceFlowInfo?TYPE=2&FCTYPE=2&CODE=" + item.code);

                //如果是北京市
                if (item.code == "110100")
                {
                    for (int i = 0; i < personModel.data.Count; i++)
                    {
                        if (personModel.data[i].name == "房山区" || personModel.data[i].name == "密云区" || personModel.data[i].name == "西站")
                        {
                            string[] temp = { personModel.data[i].libNum, "" };
                            dic.Add(personModel.data[i].name, temp);
                        }
                    }
                    foreach (DeviceFlowData car in carModel.data)
                    {
                        if (dic.ContainsKey(car.name))
                        {
                            dic[car.name][1] = car.libNum;
                        }
                    }
                }
                //如果是交管局
                else if (item.code == "110200")
                {
                    foreach (DeviceFlowData car in carModel.data)
                    {
                        carRegist += Convert.ToInt32(car.libNum);
                    }
                    string[] temp = { null, carRegist.ToString() };
                    dic.Add("交管局", temp);
                }
                //如果是检查站
                else if (item.code == "110300")
                {
                    foreach (DeviceFlowData car in carModel.data)
                    {
                        carRegist += Convert.ToInt32(car.libNum);
                    }
                    string[] temp = { null, carRegist.ToString() };
                    dic.Add("检查站", temp);
                }
            }
            int index = 1;
            foreach (var key in dic.Keys)
            {
                DataRow row = table.NewRow();
                row["Id"] = index;
                row["Name"] = key;
                row["People"] = dic[key][0] != null ? Convert.ToInt32(dic[key][0]) : 0;
                row["Car"] = dic[key][1] != null ? Convert.ToInt32(dic[key][1]) : 0;
                row["Time"] = DateTime.Now;
                table.Rows.Add(row);
                index++;
            }

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 服务接口调用
        /// </summary>
        private void InvokeStatisticsData()
        {
            DataTable table1 = new DataTable();
            table1.TableName = "视综_人脸服务接口调用量";
            table1.Columns.Add("Id", typeof(Int32));
            table1.Columns.Add("Type", typeof(string));
            table1.Columns.Add("Count", typeof(Int32));
            table1.Columns.Add("Time", typeof(DateTime));

            DataTable table2 = new DataTable();
            table2.TableName = "视综_车辆服务接口调用量";
            table2.Columns.Add("Id", typeof(Int32));
            table2.Columns.Add("Type", typeof(string));
            table2.Columns.Add("Count", typeof(Int32));
            table2.Columns.Add("Time", typeof(DateTime));

            InvokeStatisticsModel invokeStatisticsModel = HttpHelper.HttpYiSuoGet2<InvokeStatisticsModel>("http://14.29.73.107:8089/VIID/InvokeStatistics");

            for (int i = 0; i < invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject.Count; i++)
            {
                if (invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject[i].ServiceName.Contains("人脸"))
                {
                    DataRow row = table1.NewRow();
                    row["Id"] = i + 1;
                    row["Type"] = invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject[i].ServiceName;
                    row["Count"] = invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject[i].InvokeNum;
                    row["Time"] = DateTime.Now;
                    table1.Rows.Add(row);
                }
                else if (invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject[i].ServiceName.Contains("车辆"))
                {
                    DataRow row = table2.NewRow();
                    row["Id"] = i + 1;
                    row["Type"] = invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject[i].ServiceName;
                    row["Count"] = invokeStatisticsModel.ApplicationServiceStatisticsObject.ApplicationServiceStatisticsObject[i].InvokeNum;
                    row["Time"] = DateTime.Now;
                    table2.Rows.Add(row);
                }
            }

            DataHelper.SaveCsv(table1);
            DataHelper.SaveCsv(table2);
        }

        /// <summary>
        /// 视频路数
        /// </summary>
        private void EquipmentCountData()
        {
            DataTable table = new DataTable();
            table.TableName = "视综_视频路数";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("VideoNet", typeof(Int32));
            table.Columns.Add("PoliceNet", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            string result = HttpHelper.HttpWangLiGet("http://14.29.73.5/gateway/statistics_service/equipment_statistics/regionEquipmentCount", LoginWangLi());
            JObject json = JObject.Parse(result);
            JToken listToken = json["data"];
            int video = 0;
            int police = 0;
            foreach (JToken item in listToken)
            {
                int camera = Convert.ToInt32(item.First.AsJEnumerable()["1"].ToString());
                int person = Convert.ToInt32(item.First.AsJEnumerable()["11"].ToString());
                int vehicle = Convert.ToInt32(item.First.AsJEnumerable()["2"].ToString());

                video += camera;
                police += person + vehicle;
            }

            DataRow row = table.NewRow();
            row["Id"] = 1;
            row["VideoNet"] = video;
            row["PoliceNet"] = police;
            row["Time"] = DateTime.Now;
            table.Rows.Add(row);

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 视频综合概览
        /// </summary>
        private void AlarmTypeCountData()
        {
            DataTable table = new DataTable();
            table.TableName = "视综_视频综合概览";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("Face", typeof(Int32));
            table.Columns.Add("Car", typeof(Int32));
            table.Columns.Add("Strategy", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //DateTime startTime = new DateTime(2019, 9, 25);
            //DateTime endTime = new DateTime(2019, 9, 26);

            DateTime startTime = DateTime.Now.AddDays(-1);
            DateTime endTime = DateTime.Now;

            TimeSpan toStart = startTime.Subtract(dtStart);
            TimeSpan toEnd = endTime.Subtract(dtStart);

            string time = "{\"startTime\":" + toStart.Ticks.ToString().Substring(0, toStart.Ticks.ToString().Length - 4) + ",\"endTime\":" + toEnd.Ticks.ToString().Substring(0, toEnd.Ticks.ToString().Length - 4) + "}\"";
            
            AlarmTypeCountModel alarmTypeCountModel = HttpHelper.HttpWangLiPost<AlarmTypeCountModel>("http://14.29.73.5/gateway/statistics_service/alarm/statistics/alarmTypeCount", LoginWangLi(), time);

            int face = 0;
            int car = 0;
            int strategy = 0;
            for (int i = 0; i < alarmTypeCountModel.data.Count; i++)
            {
                if (alarmTypeCountModel.data[i].name == "人脸告警")
                {
                    face = alarmTypeCountModel.data[i].value;
                }
                else if (alarmTypeCountModel.data[i].name == "车辆告警")
                {
                    car = alarmTypeCountModel.data[i].value;
                }
            }

            StrategyModel strategyModel = HttpHelper.HttpWangLiGet<StrategyModel>("http://14.29.73.5/gateway/logservice/nplog/logs/projects/logs/logstores/appLog/querySettingList?currentPage=1&pageSize=10&beginTime=" + toStart.Ticks.ToString().Substring(0, toStart.Ticks.ToString().Length - 4) + "&endTime=" + toEnd.Ticks.ToString().Substring(0, toEnd.Ticks.ToString().Length - 4) + "", LoginWangLi());
            strategy = strategyModel.data.count;

            DataRow row = table.NewRow();
            row["Id"] = 1;
            row["Face"] = face;
            row["Car"] = car;
            row["Strategy"] = strategy;
            row["Time"] = DateTime.Now;
            table.Rows.Add(row);

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// 设备监控实时数据统计
        /// </summary>
        private void MultialgorithmData()
        {
            DataTable table = new DataTable();
            table.TableName = "视综_设备监控实时数据统计";
            table.Columns.Add("Id", typeof(Int32));
            table.Columns.Add("CPU", typeof(Int32));
            table.Columns.Add("Memory", typeof(Int32));
            table.Columns.Add("Disk", typeof(Int32));
            table.Columns.Add("Time", typeof(DateTime));

            MultialgorithmModel multialgorithmModel = HttpHelper.HttpWangLiPost<MultialgorithmModel>("http://14.29.73.5/gateway/multialgorithm_service/api/huawei/resourceAll");

            DataRow row = table.NewRow();
            row["Id"] = 1;
            row["CPU"] = (multialgorithmModel.data.gpu.available / multialgorithmModel.data.gpu.all) * 100;
            row["Memory"] = (multialgorithmModel.data.mem.available / multialgorithmModel.data.mem.all) * 100;
            row["Disk"] = (multialgorithmModel.data.storagePool.available / multialgorithmModel.data.storagePool.all) * 100;
            row["Time"] = DateTime.Now;
            table.Rows.Add(row);

            DataHelper.SaveCsv(table);
        }

        /// <summary>
        /// poi点位数据
        /// </summary>
        private void PointData()
        {
            DataTable tableMan = new DataTable();
            tableMan.TableName = "视综_警员poi";
            tableMan.Columns.Add("Id", typeof(Int32));
            tableMan.Columns.Add("Name", typeof(string));
            tableMan.Columns.Add("Lon", typeof(double));
            tableMan.Columns.Add("Lat", typeof(double));
            tableMan.Columns.Add("Time", typeof(DateTime));

            DataTable tableCar = new DataTable();
            tableCar.TableName = "视综_警车poi";
            tableCar.Columns.Add("Id", typeof(Int32));
            tableCar.Columns.Add("Name", typeof(string));
            tableCar.Columns.Add("Lon", typeof(double));
            tableCar.Columns.Add("Lat", typeof(double));
            tableCar.Columns.Add("Time", typeof(DateTime));

            DataTable tableCamera = new DataTable();
            tableCamera.TableName = "视综_摄像头poi";
            tableCamera.Columns.Add("Id", typeof(Int32));
            tableCamera.Columns.Add("Name", typeof(string));
            tableCamera.Columns.Add("Lon", typeof(double));
            tableCamera.Columns.Add("Lat", typeof(double));
            tableCamera.Columns.Add("Time", typeof(DateTime));

            string shiJi = "{\"eqPermissions\": null,\"excludeOrgPermissions\": null,\"orgPermissions\": [\"11\"],\"excludeEqPermissions\": null,\"isAdmin\": false,\"orgType\": 0,\"treeLevel\": 0,\"nodeType\": \"1,2,3\",\"keyword\": \"\",\"ids\": [\"11\"],\"businessOrgType\": null,\"workFunction\": [],\"dimensioned\": null,\"isOnline\": null,\"type\": [],\"cameraDefinitionType\": [],\"sortField\": \"acronym\",\"sortType\": \"asc\"}";
            ShiJiModel shiJiModel = HttpHelper.HttpWangLiPost<ShiJiModel>("http://14.29.73.5/urmService/organization/tree_posterity", LoginWangLi(), shiJi);

            for (int i = 0; i < shiJiModel.data.Count; i++)
            {
                string shiToQu = "{\"eqPermissions\": null,\"excludeOrgPermissions\": null,\"orgPermissions\": [\"11\"],\"excludeEqPermissions\": null,\"isAdmin\": false,\"orgType\": 0,\"treeLevel\": 0,\"nodeType\": \"1,2,3\",\"keyword\": \"\",\"ids\": [\"" + shiJiModel.data[i].id + "\"],\"businessOrgType\": null,\"workFunction\": [],\"dimensioned\": null,\"isOnline\": null,\"type\": [],\"cameraDefinitionType\": [],\"sortField\": \"acronym\",\"sortType\": \"asc\"}";
                QuJiModel quJiModel = HttpHelper.HttpWangLiPost<QuJiModel>("http://14.29.73.5/urmService/organization/tree_posterity", LoginWangLi(), shiToQu);
                for (int j = 0; j < quJiModel.data.Count; j++)
                {
                    string quToPaiChuSuo = "{\"eqPermissions\": null,\"excludeOrgPermissions\": null,\"orgPermissions\": [\"11\"],\"excludeEqPermissions\": null,\"isAdmin\": false,\"orgType\": 0,\"treeLevel\": 0,\"nodeType\": \"1,2,3\",\"keyword\": \"\",\"ids\": [\"" + quJiModel.data[j].id + "\"],\"businessOrgType\": null,\"workFunction\": [],\"dimensioned\": null,\"isOnline\": null,\"type\": [],\"cameraDefinitionType\": [],\"sortField\": \"acronym\",\"sortType\": \"asc\"}";
                    PaiChuSuoModel paiChuSuoModel = HttpHelper.HttpWangLiPost<PaiChuSuoModel>("http://14.29.73.5/urmService/organization/tree_posterity", LoginWangLi(), quToPaiChuSuo);
                    for (int t = 0; t < paiChuSuoModel.data.Count; t++)
                    {
                        string paiChuSuoToPoint = "{\"eqPermissions\": null,\"excludeOrgPermissions\": null,\"orgPermissions\": [\"11\"],\"excludeEqPermissions\": null,\"isAdmin\": false,\"orgType\": 0,\"treeLevel\": 0,\"nodeType\": \"1,2,3\",\"keyword\": \"\",\"ids\": [\"" + paiChuSuoModel.data[t].id + "\"],\"businessOrgType\": null,\"workFunction\": [],\"dimensioned\": null,\"isOnline\": null,\"type\": [],\"cameraDefinitionType\": [],\"sortField\": \"acronym\",\"sortType\": \"asc\"}";
                        DanDianModel danDianModel = HttpHelper.HttpWangLiPost<DanDianModel>("http://14.29.73.5/urmService/organization/tree_posterity", LoginWangLi(), paiChuSuoToPoint);
                        for (int h = 0; h < danDianModel.data.Count; h++)
                        {
                            //如果是人
                            if (danDianModel.data[h].type == "11")
                            {
                                DataRow row = tableMan.NewRow();
                                row["Id"] = h + 1;
                                row["Name"] = danDianModel.data[h].id;
                                row["Lon"] = danDianModel.data[h].longitude;
                                row["Lat"] = danDianModel.data[h].latitude;
                                row["Time"] = DateTime.Now;
                                tableMan.Rows.Add(row);
                            }
                            //如果是摄像机
                            else if (danDianModel.data[h].type == "1")
                            {
                                DataRow row = tableCamera.NewRow();
                                row["Id"] = h + 1;
                                row["Name"] = danDianModel.data[h].id;
                                row["Lon"] = danDianModel.data[h].longitude;
                                row["Lat"] = danDianModel.data[h].latitude;
                                row["Time"] = DateTime.Now;
                                tableCamera.Rows.Add(row);
                            }
                            //如果是车
                            else if (danDianModel.data[h].type == "2")
                            {
                                DataRow row = tableCar.NewRow();
                                row["Id"] = h + 1;
                                row["Name"] = danDianModel.data[h].id;
                                row["Lon"] = danDianModel.data[h].longitude;
                                row["Lat"] = danDianModel.data[h].latitude;
                                row["Time"] = DateTime.Now;
                                tableCar.Rows.Add(row);
                            }
                        }
                    }
                }
            }

            DataHelper.SaveCsv(tableMan);
            DataHelper.SaveCsv(tableCamera);
            DataHelper.SaveCsv(tableCar);
        }

        /// <summary>
        /// 布控信息
        /// </summary>
        private void ProtectionData()
        {
            DataTable tableMan = new DataTable();
            tableMan.TableName = "视综_人员布控信息";
            tableMan.Columns.Add("Id", typeof(Int32));
            tableMan.Columns.Add("Photo", typeof(string));
            tableMan.Columns.Add("Man", typeof(string));
            tableMan.Columns.Add("Mission", typeof(string));
            tableMan.Columns.Add("ControlTime", typeof(string));
            tableMan.Columns.Add("Area", typeof(string));
            tableMan.Columns.Add("Time", typeof(DateTime));

            DataTable tableCar = new DataTable();
            tableCar.TableName = "视综_车辆布控信息";
            tableCar.Columns.Add("Id", typeof(Int32));
            tableCar.Columns.Add("Car", typeof(string));
            tableCar.Columns.Add("Mission", typeof(string));
            tableCar.Columns.Add("ControlTime", typeof(string));
            tableCar.Columns.Add("Area", typeof(string));
            tableCar.Columns.Add("Time", typeof(DateTime));

            string man = "{\"targetType\":[\"face\"],\"currentPage\":1,\"pageSize\":8,\"systemType\":\"viua\"}";
            string car = "{\"targetType\":[\"vehicle\"],\"currentPage\":1,\"pageSize\":8,\"systemType\":\"viua\"}";
            int index = 1;
            ProtectionFaceModel protectionFaceModel = HttpHelper.HttpWangLiPost<ProtectionFaceModel>("http://14.29.73.5/gateway/ips_protection_fusion/api/task/list", LoginWangLi(), man);

            for (int j = 0; j < protectionFaceModel.data.list.Count; j++)
            {
                DataRow row = tableMan.NewRow();
                row["Id"] = index;
                row["Photo"] = "";
                row["Man"] = protectionFaceModel.data.list[j].face.libNames;
                row["Mission"] = protectionFaceModel.data.list[j].name;
                row["ControlTime"] = new DateTime(protectionFaceModel.data.list[j].taskStartTime).ToString("G") + "--" + new DateTime(protectionFaceModel.data.list[j].taskEndTime).ToString("G");
                row["Area"] = "北京市";
                row["Time"] = DateTime.Now;
                tableMan.Rows.Add(row);
                index++;
            }

            index = 1;
            ProtectionCarModel protectionCarModel = HttpHelper.HttpWangLiPost<ProtectionCarModel>("http://14.29.73.5/gateway/ips_protection_fusion/api/task/list", LoginWangLi(), car);

            for (int j = 0; j < protectionCarModel.data.list.Count; j++)
            {
                DataRow row = tableCar.NewRow();
                row["Id"] = index;
                row["Car"] = protectionCarModel.data.list[j].vehicle.libNames;
                row["Mission"] = protectionCarModel.data.list[j].name;
                row["ControlTime"] = new DateTime(protectionCarModel.data.list[j].taskStartTime).ToString("G") + "--" + new DateTime(protectionCarModel.data.list[j].taskEndTime).ToString("G");
                row["Area"] = "北京市";
                row["Time"] = DateTime.Now;
                tableCar.Rows.Add(row);
                index++;
            }

            DataHelper.SaveCsv(tableMan);
            DataHelper.SaveCsv(tableCar);
        }

        Dictionary<string, string> loginInfo = new Dictionary<string, string>();
        /// <summary>
        /// 获取网力token
        /// </summary>
        private string LoginWangLi()
        {
            LoginModel loginModel = HttpHelper.HttpWangLiToken<LoginModel>("http://14.29.73.5/gateway/login", loginInfo);
            return loginModel.data.tokenid;
        }

        #endregion

        #region 系统托盘

        /// <summary>
        /// 系统托盘对象
        /// </summary>
        private NotifyIcon m_SystemIcon;

        /// <summary>
        /// 初始化系统托盘对象
        /// </summary>
        private void InitSystemIcon()
        {
            m_SystemIcon = new NotifyIcon();
            m_SystemIcon.BalloonTipText = "正在运行...";
            m_SystemIcon.ShowBalloonTip(2000);
            m_SystemIcon.Text = "数字冰雹 - 数据中间件（正在运行）";
            m_SystemIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            m_SystemIcon.Visible = true;

            m_SystemIcon.MouseDoubleClick += M_SystemIcon_MouseDoubleClick;
        }

        /// <summary>
        /// 系统托盘双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_SystemIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
        }

        #endregion

        #region UI事件

        /// <summary>
        /// 窗口拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmWindow confirm = new ConfirmWindow();
            confirm.Hide();
            bool? result = confirm.ShowDialog();
            if (result == true)
            {
                m_SystemIcon.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 保持最后一行数据显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)e.OriginalSource;
            sv.ScrollToEnd();
        }

        #endregion

        #region 日志显示数据处理

        /// <summary>
        /// 添加日志
        /// </summary>
        public static void WriteLog(string log)
        {
            m_DynamicDataLog.Dispatcher.Invoke(() =>
            {
                try
                {
                    m_DynamicDataLog.Items.Add(log);
                }
                catch (Exception)
                {
                }
            });
        }

        #endregion
    }
}
