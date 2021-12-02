using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace lab_2
{
    class Smoother
    {
        public PointPairList averageSmoothing(PointPairList originalSignal, int K)
        {
            PointPairList smoothSignal = new PointPairList();
            int m = (K - 1) / 2;
            for (int i = 0; i < originalSignal.Count; i++)
            {
                double sum = 0;
                for (int j = i - m; j < i + m; j++)
                {
                    if (j < 0)
                    {
                        sum += originalSignal[0].Y;
                    }
                    else if (j >= i)
                    {
                        sum += originalSignal[i].Y;
                    }
                    else
                    {
                        sum += originalSignal[j].Y;
                    }
                }
                smoothSignal.Add(i, 1 / (double)K * sum);
            }
            return smoothSignal;
        }

        public PointPairList porabolSmoothing(PointPairList originalSignal)
        {
            double halfSum(double a, double b, double c) {
                return 5 * a - 30 * b + 75 * c;
            }
            PointPairList smoothSignal = new PointPairList();
            int K = 7, m = (K - 1) / 2;
            for (int i = 0; i < originalSignal.Count; i++)
            {
                smoothSignal.Add(i, (halfSum(i - 3 < 0 ? originalSignal[i].Y : originalSignal[i - 3].Y, i - 2 < 0 ? originalSignal[i].Y : originalSignal[i - 2].Y, i - 1 < 0 ? originalSignal[i].Y : originalSignal[i - 1].Y) + originalSignal[i].Y +
                   halfSum(i + 3 > i ? originalSignal[i].Y : originalSignal[i + 3].Y, i + 2 > i ? originalSignal[i].Y : originalSignal[i + 2].Y, i + 1 > i ? originalSignal[i].Y : originalSignal[i + 1].Y)) / 231);
            }
            return smoothSignal;
        }
        public PointPairList medianFilterSmoothing(PointPairList originalSignal, int K)
        {
            PointPairList smoothSignal = new PointPairList();
            List<double> window = new List<double>();
            int m = (K - 1) / 2;
            for (int i = 0; i < originalSignal.Count; i++)
            {
                window.Clear();
                for (int j = i - m; j <= i + m; j++)
                {
                    if (j < 0)
                    {
                        window.Add(originalSignal[0].Y);
                    }
                    else if (j >= originalSignal.Count)
                    {
                        window.Add(originalSignal[i].Y);
                    }
                    else
                    {
                        window.Add(originalSignal[j].Y);
                    }
                }
                window.Sort();
                smoothSignal.Add(i, window[m + 1]);
            }

            return smoothSignal;
        }
    }
}
