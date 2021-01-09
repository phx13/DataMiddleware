using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataMiddleware.Models
{
    /// <summary>
    /// 静态库模型
    /// </summary>
    public class ArchivesNumModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public ArchivesNumData data { get; set; }
    }

    /// <summary>
    /// 静态库数据
    /// </summary>
    public class ArchivesNumData
    {
        public int personArchivesNum { get; set; }

        public int vehicleArchivesNum { get; set; }
    }

    /// <summary>
    /// 人脸数据模型
    /// </summary>
    public class FaceNumTrendModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public List<FaceNumTrendData> data { get; set; }
    }

    /// <summary>
    /// 人脸数据
    /// </summary>
    public class FaceNumTrendData
    {
        public string date { get; set; }

        public int num { get; set; }
    }

    /// <summary>
    /// 车辆数据模型
    /// </summary>
    public class VehicleNumTrendModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public List<VehicleNumTrendData> data { get; set; }
    }

    /// <summary>
    /// 车辆数据
    /// </summary>
    public class VehicleNumTrendData
    {
        public string date { get; set; }

        public int num { get; set; }
    }

    /// <summary>
    /// 设备统计-自定义区域模型
    /// </summary>
    public class OtherLabelModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public List<OtherLabelData> data { get; set; }
    }

    /// <summary>
    /// 设备统计-自定义区域数据
    /// </summary>
    public class OtherLabelData
    {
        public string code { get; set; }

        public string name { get; set; }
    }

    /// <summary>
    /// 设备统计模型
    /// </summary>
    public class DeviceFlowModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public List<DeviceFlowData> data { get; set; }
    }

    /// <summary>
    /// 设备统计数据
    /// </summary>
    public class DeviceFlowData
    {
        public string code { get; set; }

        public string name { get; set; }
        
        public string libNum { get; set; }
        
        public string activeNum { get; set; }
    }

    /// <summary>
    /// 人脸布控库模型
    /// </summary>
    public class FaceDistributionLibNumModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public List<FaceDistributionLibNumData> data { get; set; }
    }

    /// <summary>
    /// 人脸布控库数据
    /// </summary>
    public class FaceDistributionLibNumData
    {
        public string libId { get; set; }

        public string libName { get; set; }

        public int num { get; set; }
    }

    /// <summary>
    /// 车辆布控库模型
    /// </summary>
    public class VehicleDistributionLibNumModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public string error { get; set; }

        public List<VehicleDistributionLibNumData> data { get; set; }
    }

    /// <summary>
    /// 车辆布控库数据
    /// </summary>
    public class VehicleDistributionLibNumData
    {
        public string libId { get; set; }

        public string libName { get; set; }

        public int num { get; set; }
    }

    /// <summary>
    /// 服务调用接口模型
    /// </summary>
    public class InvokeStatisticsModel
    {
        public InvokeStatisticsData ApplicationServiceStatisticsObject { get; set; }
    }

    /// <summary>
    /// 服务调用接口数据
    /// </summary>
    public class InvokeStatisticsData
    {
        public List<InvokeStatisticsDataInfo> ApplicationServiceStatisticsObject { get; set; }
    }

    public class InvokeStatisticsDataInfo
    {
        public string ServiceName { get; set; }

        public int InvokeNum { get; set; }
    }

    /// <summary>
    /// 视频路数模型
    /// </summary>
    public class EquipmentCountModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public List<EquipmentCountData> data { get; set; }
    }

    /// <summary>
    /// 视频路数数据
    /// </summary>
    public class EquipmentItemData
    {
        public string libId { get; set; }

        public string libName { get; set; }

        public int num { get; set; }
    }

    /// <summary>
    /// 视频路数数据
    /// </summary>
    public class EquipmentCountData
    {
        [XmlAttribute(AttributeName="11")]
        public int Camera { get; set; }

        [XmlAttribute(AttributeName = "1")]
        public int Person { get; set; }

        [XmlAttribute(AttributeName = "2")]
        public int Vehicle { get; set; }
    }


    /// <summary>
    /// 视频综合概览模型
    /// </summary>
    public class AlarmTypeCountModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public List<AlarmTypeCountData> data { get; set; }
    }

    /// <summary>
    /// 视频综合概览数据
    /// </summary>
    public class AlarmTypeCountData
    {
        public string name { get; set; }

        public string type { get; set; }

        public int value { get; set; }
    }

    /// <summary>
    /// 设备监控实时数据统计模型
    /// </summary>
    public class MultialgorithmModel
    {
        public string code { get; set; }

        public string message { get; set; }

        public MultialgorithmItemModel data { get; set; }
    }

    /// <summary>
    /// 设备监控实时数据统计数据
    /// </summary>
    public class MultialgorithmItemModel
    {
        public MultialgorithmData mem { get; set; }

        public MultialgorithmData gpu { get; set; }

        public MultialgorithmData storagePool { get; set; }

    }
    
    /// <summary>
    /// 设备监控实时数据统计数据
    /// </summary>
    public class MultialgorithmData
    {
        public double all { get; set; }

        public double available { get; set; }

        public double disabled { get; set; }

        public double unused { get; set; }
    }

    /// <summary>
    /// 登陆模型
    /// </summary>
    public class LoginModel
    {
        public string code { get; set; }

        public LoginData data { get; set; }
    }

    /// <summary>
    /// 登陆数据
    /// </summary>
    public class LoginData
    {
        public string tokenid { get; set; }
    }

    /// <summary>
    /// 市级模型
    /// </summary>
    public class ShiJiModel
    {
        public string code { get; set; }

        public List<ShiJiData> data { get; set; }
    }

    /// <summary>
    /// 市级数据
    /// </summary>
    public class ShiJiData
    {
        public string id { get; set; }

        public string name { get; set; }
    }

    /// <summary>
    /// 区级模型
    /// </summary>
    public class QuJiModel
    {
        public string code { get; set; }

        public List<QuJiData> data { get; set; }
    }

    /// <summary>
    /// 区级数据
    /// </summary>
    public class QuJiData
    {
        public string id { get; set; }

        public string name { get; set; }
    }

    /// <summary>
    /// 派出所模型
    /// </summary>
    public class PaiChuSuoModel
    {
        public string code { get; set; }

        public List<PaiChuSuoData> data { get; set; }
    }

    /// <summary>
    /// 派出所数据
    /// </summary>
    public class PaiChuSuoData
    {
        public string id { get; set; }

        public string name { get; set; }
    }

    /// <summary>
    /// 单点模型
    /// </summary>
    public class DanDianModel
    {
        public string code { get; set; }

        public List<DanDianData> data { get; set; }
    }

    /// <summary>
    /// 单点数据
    /// </summary>
    public class DanDianData
    {
        public string id { get; set; }

        public double latitude { get; set; }

        public string type { get; set; }

        public double longitude { get; set; }
    }

    /// <summary>
    /// 布控人脸模型
    /// </summary>
    public class ProtectionFaceModel
    {
        public string code { get; set; }

        public ProtectionFaceBase data { get; set; }
    }

    /// <summary>
    /// 布控人脸基础
    /// </summary>
    public class ProtectionFaceBase
    {
        public int pageSize { get; set; }

        public List<ProtectionFaceData> list { get; set; }
    }

    /// <summary>
    /// 布控人脸数据
    /// </summary>
    public class ProtectionFaceData
    {
        public string name { get; set; }

        public long taskStartTime { get; set; }

        public long taskEndTime { get; set; }

        public ProtectionFaceInfoData face { get; set; }
    }

    /// <summary>
    /// 布控人脸详细数据
    /// </summary>
    public class ProtectionFaceInfoData
    {
        public string libNames { get; set; }
    }

    /// <summary>
    /// 布控车辆模型
    /// </summary>
    public class ProtectionCarModel
    {
        public string code { get; set; }

        public ProtectionCarBase data { get; set; }
    }

    /// <summary>
    /// 布控人脸基础
    /// </summary>
    public class ProtectionCarBase
    {
        public int pageSize { get; set; }

        public List<ProtectionCarData> list { get; set; }
    }

    /// <summary>
    /// 布控车辆数据
    /// </summary>
    public class ProtectionCarData
    {
        public string name { get; set; }

        public long taskStartTime { get; set; }

        public long taskEndTime { get; set; }

        public ProtectionCarInfoData vehicle { get; set; }
    }

    /// <summary>
    /// 布控车辆详细数据
    /// </summary>
    public class ProtectionCarInfoData
    {
        public string libNames { get; set; }
    }

    /// <summary>
    /// 技战法模型
    /// </summary>
    public class StrategyModel
    {
        public string code { get; set; }

        public StrategyData data { get; set; }
    }

    /// <summary>
    /// 技战法数据
    /// </summary>
    public class StrategyData
    {
        public int count { get; set; }
    }
}
