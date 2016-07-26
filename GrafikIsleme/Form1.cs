using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace GrafikIsleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SecilenGrafik();      
        }
        int TickStart;
        string ScaleMod;
        Random rastgele = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {

            int sayi = rastgele.Next(50, 150);
            
            Draw(sayi);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Bobin";
            myPane.XAxis.Title.Text = "Zaman";
            myPane.YAxis.Title.Text = "Derece";
            // buradaki 60000 i 5 yada başka bir şey yaparsak ekranda gözüken
            //çizginin boyu 5 birim oluyor.
            RollingPointPairList list = new RollingPointPairList(60000);
            RollingPointPairList list2 = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve("List", list, Color.Blue, SymbolType.None);

            //bunlar grafiğin bayutlarını ayarlar
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 180;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 250;
            

            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 5;
            zedGraphControl1.AxisChange();
            TickStart = Environment.TickCount;
            

        }
        private void SecilenGrafik()
        {
            double[] x = new double[180];
            double[] y = new double[400];
            int i;
           
            
            for (i = 0; i <x.Length; i++)
            {
                
                x[i] = i;
                y[i] = i;
            }
           
            //msj = sayi % 2 == 0 ? "çift sayi girdiniz" : "Tek sayi girdiniz";
            //koşul? doğru ifade: yanlış ifade
            for ( i = 10; i < 61; i++)
            {
                y[i] = 200;
            }
            for (i = 60; i < 121; i++)
            {
                y[i] = 150;
            }
            for (i = 120; i < 180; i++)
            {
                y[i] = 200;
            }

             
            zedGraphControl1.GraphPane.AddCurve("Sinus Dalgası", x, y, Color.Red, SymbolType.None);
            //myPane.AddCurve("Sinus Dalgası", x, y, Color.Red, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

        }
        private void Draw(double yekseni)
        {
            zedGraphControl1.AutoScroll = true;
            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
            {
                return;
            }
            LineItem curve1 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            if (curve1 == null)
            {
                return;
            }
            IPointListEdit list1 = curve1.Points as IPointListEdit;
            if (list1 == null)
            {
                return;
            }

            double time = (Environment.TickCount - TickStart) / 1000.0;
            list1.Add(time, yekseni);
           // Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            //if (time>xScale.Max-xScale.MajorStep)
            //{
            //    if (intMode==1)
            //    {
            //        xScale.Max = time + xScale.MajorStep;
            //        xScale.Min = xScale.Max - 30.0;

            //    }
            //    else
            //    {
            //        xScale.Max = time + xScale.MajorStep;
            //        xScale.Min = 0;
            //    }
            //}
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (ScaleMod=="takipet")
            {
                xScale.Max = 180;
                //xScale.Min = xScale.Max - 200;
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

        }
        private void ScaleAyari() {
            
            zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
            zedGraphControl1.GraphPane.XAxis.Scale.Max = 180;
            zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
            zedGraphControl1.GraphPane.YAxis.Scale.Max = 250;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "TAKİP ET")
            {
                ScaleAyari();
                ScaleMod = "takipet";
                button1.Text = "SERBEST MODE";
            }
            else if(button1.Text=="SERBEST MODE")
            {
                ScaleAyari();
                ScaleMod = "serbest";
                button1.Text = "TAKİP ET";
            }

        }

        private void zedGraphControl1_ScrollEvent(object sender, ScrollEventArgs e)
        {
            //zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
        }

        private void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            //if (zedGraphControl1.GraphPane.XAxis.Scale.Min<0)
            //{
            //    zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
            //}
            
        }
    }
}
