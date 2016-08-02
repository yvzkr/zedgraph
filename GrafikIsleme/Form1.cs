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
        int TickStart,sayac=0;
        double ScaleMinimumX = 0.0, ScaleMaxsimumX = 180.0;
        double[] x = new double[180];
        double[] y = new double[400];
        string ScaleMod;
        Random rastgele = new Random();

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Draw canlı olarak çizilen grafik 
            int sayi = rastgele.Next(50, 100);
            Draw(sayi);           
            
            
        }

        private void Different(double drawinggraph,double selectedgraph)
        {
            if (drawinggraph == null || selectedgraph == null)
            {
                return;
            }
            var different = drawinggraph - selectedgraph;
            if (different>100 )
            {
                trackBar1.Value = 100;
                
            }
            else if (different<-100 )
            {
                trackBar1.Value = -100;

            }
            else
            {
               trackBar1.Value=Convert.ToInt32(different);
            }

            if (different < 0)
            {
                trackBar1.BackColor = Color.Orange;
                textBoxDerece.Text = (different * -1).ToString() + " Derece Az";
            }
            else if (different > 0)
            {
                trackBar1.BackColor = Color.Red;
                textBoxDerece.Text = different.ToString() + " Derece Fazla";
            }
            else
            {
                trackBar1.BackColor = Color.Red;
            }
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
           //RollingPointPairList list2 = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve("List", list, Color.Blue, SymbolType.None);
            //LineItem curve2 = myPane.AddCurve("list2",list2,Color.Red,SymbolType.Star);

            //bunlar grafiğin bayutlarını ayarlar
            myPane.XAxis.Scale.Min = ScaleMinimumX;
            myPane.XAxis.Scale.Max = ScaleMaxsimumX;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 250;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 5;
            zedGraphControl1.AxisChange();
            TickStart = Environment.TickCount;
      
        }

        private void SecilenGrafik()
        {
            
            int i;      
            for (i = 0; i <x.Length; i++)
            {                
                x[i] = i;
                y[i] = i;
            }
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
            Random rast = new Random();
            for ( i = 180; i < y.Length; i++)
            {
                y[i] = rast.Next(1,250);
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
           //takipetme

            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (ScaleMod=="takipet")
            {
                if (time>90)
                {
                    xScale.Max = time + 90;
                    xScale.Min = time - 90;
                    if (zedGraphControl1.GraphPane.XAxis.Scale.Min < 0)
                    {
                        zedGraphControl1.GraphPane.XAxis.Scale.Min = ScaleMinimumX;
                        zedGraphControl1.GraphPane.XAxis.Scale.Max = ScaleMaxsimumX;
                    }

                }
                
            }
            //takipetme
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            
        }

        private void ScaleAyari() {

            zedGraphControl1.GraphPane.XAxis.Scale.Min = ScaleMinimumX;
            zedGraphControl1.GraphPane.XAxis.Scale.Max = ScaleMaxsimumX;
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
                
                ScaleMod = "serbest";
                button1.Text = "TAKİP ET";
            }

        }

        private void zedGraphControl1_ScrollEvent(object sender, ScrollEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScaleAyari();
        }

        private void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
           if (zedGraphControl1.GraphPane.XAxis.Scale.Min<0)
           {
               zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
           }
            //ScaleMaxsimumX=zedGraphControl1.GraphPane.XAxis.Scale.Max;
            //ScaleMinimumX = zedGraphControl1.GraphPane.XAxis.Scale.Min;
                      
        }
    }
}
