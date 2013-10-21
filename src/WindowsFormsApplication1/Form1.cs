using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        LinguisticVariable L = new LinguisticVariable("Size of book", 40, 400);
        int CurveCount;

        public Form1()
        {
            InitializeComponent();

            string[] terms = { "little", "normal", "big" };
            L.InitTerms(terms);
            CurveCount = L.Term.Count;

            InitGraph();
        }

        private void InitGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = L.Name;
            myPane.XAxis.Title.Text = "Количество страниц";
            myPane.YAxis.Title.Text = "Значение функции принадлежности";

            //---------------------------------------------------------------------------
            PointPairList list0 = new PointPairList();
            for (int i = 0; i < L.Term[0].Set.Count; i++)
            {
                list0.Add(L.Term[0].Set[i].Value, L.Term[0].Set[i].MembershipFunction);

            }
            myPane.AddCurve(L.Term[0].Name, list0, Color.Yellow, SymbolType.None);

            PointPairList list1 = new PointPairList();
            for (int i = 0; i < L.Term[1].Set.Count; i++)
            {
                list1.Add(L.Term[1].Set[i].Value, L.Term[1].Set[i].MembershipFunction);

            }
            myPane.AddCurve(L.Term[1].Name, list1, Color.Black, SymbolType.None);

            PointPairList list2 = new PointPairList();
            for (int i = 0; i < L.Term[2].Set.Count; i++)
            {
                list2.Add(L.Term[2].Set[i].Value, L.Term[2].Set[i].MembershipFunction);

            }
            myPane.AddCurve(L.Term[2].Name, list2, Color.Blue, SymbolType.None);
            //-------------------------------------------------------------------------------

            myPane.XAxis.Scale.Min = L.Min;
            myPane.XAxis.Scale.Max = L.Max;
            myPane.YAxis.Scale.Min = 0.0;
            myPane.YAxis.Scale.Max = 1.0;
            zedGraphControl1.AxisChange();
        }

        private void DrawGraph(FuzzyVariable V)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList list1 = new PointPairList();

            for (int i = 0; i < V.Set.Count; i++)
            {
                list1.Add(V.Set[i].Value, V.Set[i].MembershipFunction);
            }

            if (pane.CurveList.Count == CurveCount + 1)
                pane.CurveList.RemoveAt(CurveCount);
            pane.AddCurve(V.Name + " " + L.Name, list1, Color.Red, SymbolType.Star);


            zedGraphControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Update();
            L.Generate(textBox1.Text);

            DrawGraph(L.Value);
        }

    }
}
