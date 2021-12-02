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

namespace lab_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Height = 500;
            Width = 1200;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphDrawer graphDrawer = new GraphDrawer();
            Smoother smoother = new Smoother();
            Signal signal = new Signal();
            Specter specter = new Specter();
            PointPairList signalPoints = signal.generateSignal(100, 10, 512);
            graphDrawer.drawSignal(signalPoints, zedGraphControl1, "original signal", Color.Black);
            specter.dpf(signalPoints);
            graphDrawer.drawSpecter(specter.specterA, "original signal", zedGraphControl2, Color.Black);
            graphDrawer.drawSpecter(specter.specterF, "original signal", zedGraphControl3, Color.Black);

            specter.dpf(smoother.averageSmoothing(signalPoints, 7));
            graphDrawer.drawSignal(smoother.averageSmoothing(signalPoints, 7), zedGraphControl1, "average smooth signal", Color.Red);
            graphDrawer.drawSpecter(specter.specterA, "average smooth signal", zedGraphControl4, Color.Red);
            graphDrawer.drawSpecter(specter.specterF, "average smooth signal", zedGraphControl5, Color.Red);

            specter.dpf(smoother.porabolSmoothing(signalPoints));
            graphDrawer.drawSignal(smoother.porabolSmoothing(signalPoints), zedGraphControl1, "porabola smooth signal", Color.Blue);
            graphDrawer.drawSpecter(specter.specterA, "porabola smooth signal", zedGraphControl8, Color.Blue);
            graphDrawer.drawSpecter(specter.specterF, "porabola smooth signal", zedGraphControl9, Color.Blue);

            specter.dpf(smoother.medianFilterSmoothing(signalPoints, 9));
            graphDrawer.drawSignal(smoother.medianFilterSmoothing(signalPoints, 9), zedGraphControl1, "median smooth signal", Color.Green);
            graphDrawer.drawSpecter(specter.specterA, "medeian smooth signal", zedGraphControl10, Color.Green);
            graphDrawer.drawSpecter(specter.specterF, "medeian smooth signal", zedGraphControl11, Color.Green);
        }

        
    }
    public class GraphDrawer 
    {
        bool cleanPaneFlag = false;
        public GraphDrawer(bool clean) 
        {
            this.cleanPaneFlag = clean;
        }
        public GraphDrawer()
        {
        }
        public void drawSignal(PointPairList points, ZedGraphControl zedGraphControl, string label, Color color) 
        {
            GraphPane pane = zedGraphControl.GraphPane;
            if (cleanPaneFlag)
            {
                pane.CurveList.Clear();
            }
            LineItem curve = pane.AddCurve(label, points, color, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        public void drawSpecter(List<double> values, string label, ZedGraphControl zedGraphControl, Color color)
        {
            double[] valuseForDiagram = values.ToArray();
            GraphPane pane = zedGraphControl.GraphPane;
            if (cleanPaneFlag)
            {
                pane.CurveList.Clear();
            }
            BarItem bar = pane.AddBar("specter", null, valuseForDiagram, color);
            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
    }

}
