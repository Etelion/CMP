using System;
using System.Collections;
using System.Collections.Generic;

namespace PMC_Test_COI_
{    
    public enum PointType { Int = 1, Double = 2, Decimal = 3 };
    public enum PointDimentional { X = 1, XY = 2, XYZ = 3 };
    public abstract class Point
    {
        internal abstract string ToStr();        
    }

    public class IntPoint:Point
    {
        public int[] data
        {
            get { return _data; }
        }
        private readonly int[] _data;
        internal IntPoint(int[] paramData)
        {
            _data = new int[paramData.Length];
            for (int i = 0; i < paramData.Length; i++)
            {
                _data[i] = paramData[i];
            }
        }
        /// <summary>
        /// створення рядка з координатами
        /// </summary>
        /// <returns>результат</returns>
        internal override string ToStr()
        {
            string str = String.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                str += data[i].ToString() + " ";
            }
            return str;
        }
    }
    public class DoublePoint :Point
    {
        public double[] data
        {
            get { return _data; }
        }
        private readonly double[] _data;
        internal DoublePoint(double[] paramData)
        {
            _data = new double[paramData.Length];
            for (int i = 0; i < paramData.Length; i++)
            {
                _data[i] = paramData[i];
            }
        }
        /// <summary>
        /// створення рядка з координатами
        /// </summary>
        /// <returns>результат</returns>
        internal override string ToStr()
        {
            string str = String.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                str += data[i].ToString() + " ";
            }
            return str;
        }
    }
    public class DecimalPoint : Point
    {
        public decimal[] data
        {
            get { return _data; }
        }
        private readonly decimal[] _data;
        internal DecimalPoint(decimal[] paramData)
        {
            _data = new decimal[paramData.Length];
            for (int i = 0; i < paramData.Length; i++)
            {
                _data[i] = paramData[i];
            }
        }
        /// <summary>
        /// створення рядка з координатами
        /// </summary>
        /// <returns>результат</returns>
        internal override string ToStr()
        {
            string str = String.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                str += data[i].ToString() + " ";
            }
            return str;
        }
    }

    public class Positions
    {
        private static Random R = new Random();

        private List<Point> LPoint = new List<Point>();

        internal Positions(int countPoint, PointType pointType, PointDimentional pointDimentional )
        {
            LPoint.Clear();
            for (int i = 0; i < countPoint; i++)
            {
                if (pointType == PointType.Int)
                {
                    
                    int[] val;
                    if (pointDimentional == PointDimentional.X)
                    {
                        val = new int[1];
                        val[0] = R.Next(0, 100);
                        LPoint.Add(new IntPoint(val));
                    }
                    else if (pointDimentional == PointDimentional.XY)
                    {
                        val = new int[2];
                        val[0] = R.Next(0, 100);
                        val[1] = R.Next(0, 100);
                        LPoint.Add(new IntPoint(val));
                    }
                    else if (pointDimentional == PointDimentional.XYZ)
                    {
                        val = new int[3];
                        val[0] = R.Next(0, 100);
                        val[1] = R.Next(0, 100);
                        val[2] = R.Next(0, 100);
                        LPoint.Add(new IntPoint(val));
                    }
                }
                else if (pointType == PointType.Double)
                {
                    double[] val;
                    if (pointDimentional == PointDimentional.X)
                    {
                        val = new double[1];
                        val[0] = R.Next(0, 10000)/100.0;
                        LPoint.Add(new DoublePoint(val));
                    }
                    else if (pointDimentional == PointDimentional.XY)
                    {
                        val = new double[2];
                        val[0] = R.Next(0, 10000) / 100.0;
                        val[1] = R.Next(0, 10000) / 100.0;
                        LPoint.Add(new DoublePoint(val));
                    }
                    else if (pointDimentional == PointDimentional.XYZ)
                    {
                        val = new double[3];
                        val[0] = R.Next(0, 10000) / 100.0;
                        val[1] = R.Next(0, 10000) / 100.0;
                        val[2] = R.Next(0, 10000) / 100.0;
                        LPoint.Add(new DoublePoint(val));
                    }
                }
                else if (pointType == PointType.Decimal)
                {
                    decimal[] val;
                    if (pointDimentional == PointDimentional.X)
                    {
                        val = new decimal[1];
                        val[0] = R.Next(0, 100);
                        LPoint.Add(new DecimalPoint(val));
                    }
                    else if (pointDimentional == PointDimentional.XY)
                    {
                        val = new decimal[2];
                        val[0] = R.Next(0, 100);
                        val[1] = R.Next(0, 100);
                        LPoint.Add(new DecimalPoint(val));
                    }
                    else if (pointDimentional == PointDimentional.XYZ)
                    {
                        val = new decimal[3];
                        val[0] = R.Next(0, 100);
                        val[1] = R.Next(0, 100);
                        val[2] = R.Next(0, 100);
                        LPoint.Add(new DecimalPoint(val));
                    }
                }
            }
        }
        /// <summary>
        /// створення списку точок з координатами
        /// </summary>
        /// <returns>результат</returns>
        internal string ToStr()
        {
            string str = string.Empty;
            for (int i = 0; i < LPoint.Count; i++)
            {
                str += "Point №" + (i + 1) + " ";
                str += LPoint[i].ToStr() + Environment.NewLine;
            }
            return str;
        }
    }
    public class Matrix
    {
        private List<Positions> LPosition = new List<Positions>();
        internal Matrix(int[] countPoint, PointType pointType, PointDimentional pointDimentional)
        {
            LPosition.Clear();
            for (int i = 0; i < countPoint.Length; i++)
            {
                LPosition.Add(new Positions(countPoint[i], pointType, pointDimentional));
            }
        }
        /// <summary>
        /// створення списку позицій з списком точок з координатами
        /// </summary>
        /// <returns>результат</returns>
        public string ToStr()
        {
            string str = string.Empty;
            for (int i = 0; i < LPosition.Count; i++)
            {
                str += "Position №" + (i + 1) + Environment.NewLine;
                str += LPosition[i].ToStr() + Environment.NewLine;
            }
            return str;
        }
    }
    public class Container
    {
        public List<Matrix> LMatrix = new List<Matrix>();
        public Container(int[][] countPoint, PointType pointType, PointDimentional[] pointDimentional)
        {
            LMatrix.Clear();
            for (int i = 0; i < countPoint.Length; i++)
            {
                LMatrix.Add(new Matrix(countPoint[i], pointType, pointDimentional[i]));
            }
        }
        /// <summary>
        /// створення списку матриць з списком позицій з списком точок з координатами :)
        /// </summary>
        /// <returns>результат</returns>
        public string ToStr()
        {
            string str = string.Empty;
            for (int i = 0; i < LMatrix.Count; i++)
            {
                str += "Matrix №" + (i + 1) + Environment.NewLine;
                str += LMatrix[i].ToStr() + Environment.NewLine;
            }
            return str;
        }
    }

    public class Containers : IEnumerable
    {
        public Container[] container; //контейнери
        /// <summary>
        /// створення об'єкту типу Containers
        /// </summary>
        /// <param name="countPoint">кількість точок в кожному контейнері, матриці та позиції</param>
        /// <param name="pointType">тип точок(int, double, decimal)</param>
        /// <param name="pointDimentional">вимір точок(1D, 2D, 3D)</param>
        public Containers(int[][][] countPoint, PointType pointType, PointDimentional[] pointDimentional)
        {
            for (int j = 1; j < countPoint.Length; j++) //контейнери
            {
                for (int i = 0; i < countPoint[j].Length; i++) //матриці
                {
                    if (pointDimentional[i] == PointDimentional.XYZ) //якщо 3Д
                    {
                        int maxPosContainerNum = 0;
                        int max = 0;
                        
                        for (int l = 0; l < countPoint.Length; l++)//по контейнерах
                        {
                            if (countPoint[l][i].Length > max) //якщо кількість позицій більша за макс
                            {
                                max = countPoint[l][i].Length; //макс оновити
                                maxPosContainerNum = l; // контейнер де в цій матриці макс позицій - поточний
                            }
                        }
                        for (int k = 0; k < countPoint[j][i].Length; k++) //позиції
                        {                                
                            countPoint[j][i][k] = countPoint[maxPosContainerNum][i][k];//кількість точок така як в контейнері де кількість позицій максимальна                            
                        }
                    }
                }
            }

            container = new Container[countPoint.Length];
            for (int i = 0; i < countPoint.Length; i++)
            {
                container[i] = new Container(countPoint[i], pointType, pointDimentional);
            }
            
        }        
        /// <summary>
        /// відображення внутрішнього стану списку контейнерів в текстовому форматі
        /// </summary>
        /// <returns>результат</returns>
        public string ToStr()
        {
            string str = string.Empty;
            if (container != null && container.Length > 0) 
                for (int i = 0; i < container.Length; i++)
                {
                    str += "Container №" + (i + 1) + Environment.NewLine;
                    str += container[i].ToStr() + Environment.NewLine;
                }
            return str;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ContainerEnum GetEnumerator()
        {
            return new ContainerEnum(container);
        }

    }
    public class ContainerEnum : IEnumerator
    {
        private Container[] _container;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public ContainerEnum(Container[] list)
        {
            _container = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _container.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Container Current
        {
            get
            {
                try
                {
                    return _container[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

}
