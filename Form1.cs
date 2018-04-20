using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _3DViewer_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const double EARTH_RADIUS = 6378.137; //地球半径（公里/千米）

        private void button1_Click(object sender, EventArgs e)
        {
            //测试
            //第1个点 X：120.28804508125516 Y：30.174517143189568 Z：5.4183104438707232
            //第2个点 X：120.28804882063631 Y：30.174492293330314 Z：0.33167422935366631
            double[,] dPoint3Ds = { { 120.28804508125516, 30.174517143189568, 5.4183104438707232 }, 
            { 120.28804882063631, 30.174492293330314, 0.33167422935366631 } };

            double dHeight = Math.Round(Math.Abs(dPoint3Ds[0, 2] - dPoint3Ds[1, 2]), 2);
            double dHorizontalDistance = Math.Round(GetDistance(dPoint3Ds[0, 0], dPoint3Ds[0, 1], dPoint3Ds[1, 0], dPoint3Ds[1, 1]) * 1000, 2);
            if (dHeight != 0)
            {
                double dSpaceDistance = Math.Round(Math.Sqrt(Math.Pow(dHorizontalDistance, 2) + Math.Pow(dHeight, 2)), 2);
                label1.Text = "高度距离： " + dHeight + "米" + "\r\n水平距离： " + dHorizontalDistance + "米" + "\r\n空间距离： " + dSpaceDistance + "米";
            }
            else
            {
                label1.Text = "水平距离： " + dHorizontalDistance + "米";
            }
            //测试
        }

        /// <summary>
        /// 给定的经度1,纬度1;经度2.纬度2.计算2个经纬度之间的距离
        /// </summary>
        /// <param name="lng1">经度1</param>
        /// <param name="lat1">纬度1</param>
        /// <param name="lng2">经度2</param>
        /// <param name="lat2">纬度2</param>
        /// <returns>距离（公里/千米）</returns>
        public static double GetDistance(double lng1, double lat1, double lng2, double lat2)
        {
            //经纬度转换成弧度
            lng1 = ConvertDegreesToRadians(lng1);
            lat1 = ConvertDegreesToRadians(lat1);
            lng2 = ConvertDegreesToRadians(lng2);
            lat2 = ConvertDegreesToRadians(lat2);

            //差值
            var vLon = Math.Abs(lng1 - lng2);
            var vLat = Math.Abs(lat1 - lat2);

            //用HaverSin公式计算球面两点间的距离
            double dh = HaverSin(vLat) + Math.Cos(lat1) * Math.Cos(lat2) * HaverSin(vLon);
            double dDistance = 2 * EARTH_RADIUS * Math.Asin(Math.Sqrt(dh));

            return dDistance;
        }

        /// <summary>
        /// 将角度换算为弧度
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns>弧度</returns>
        public static double ConvertDegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        /// <summary>
        /// 使用Sin函数公式
        /// </summary>
        /// <param name="theta">两点的经度/纬度差值</param>
        /// <returns></returns>
        public static double HaverSin(double theta)
        {
            double d = Math.Sin(theta / 2);
            return d * d;
        }
    }
}
