using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace eegsdk_demo
{
    internal class ChartAdd
    {
        private Chart chart;
        private List<Queue<double>> data = new List<Queue<double>>();  // 数据源

        public ChartAdd(Chart cha, int size)
        {
            this.chart = cha;
            for (int i = 0; i < size; i++)
            {
                data.Add(new Queue<double>());
            }

        }

        public void addPoints(params JArray[] chs)
        {

            /*chart.BeginInvoke((MethodInvoker)delegate
            {
                for (int i = 0; i < chs.Length; i++)
                {
                    for (int j = 0; j < chs[0].Count; j++)
                    {
                        data[i].Enqueue((double)chs[i][j]);
                        if (data[i].Count > 3000)
                        {
                            data[i].Dequeue();
                        }
                    }

                }
                for (int i = 0; i < chs.Length; i++)
                {
                    chart.Series[i].Points.Clear();
                    chart.Series[i].Points.DataBindY(data[i]);
                }
            });*/

            chart.Invoke(new Action(() =>
            {
                for (int i = 0; i < chs.Length; i++)
                {
                    for (int j = 0; j < chs[0].Count; j++)
                    {
                        data[i].Enqueue((double)chs[i][j]);
                        if (data[i].Count > 3000)
                        {
                            data[i].Dequeue();
                        }
                    }

                }
                for (int i = 0; i < chs.Length; i++)
                {
                    chart.Series[i].Points.Clear();
                    chart.Series[i].Points.DataBindY(data[i]);
                }
            }));


        }
        public void addPoint(params double[] chs)
        {
            chart.Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < chs.Length; i++)
                {
                    data[i].Enqueue(chs[i]);
                    if (data[i].Count > 180)
                    {
                        data[i].Dequeue();
                    }

                }
                for (int i = 0; i < chs.Length; i++)
                {
                    chart.Series[i].Points.Clear();
                    chart.Series[i].Points.DataBindY(data[i]);
                }
            });
        }
    }
}
