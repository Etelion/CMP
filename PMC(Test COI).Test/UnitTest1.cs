using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PMC_Test_COI_.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPointCountInConteinersIs100()
        {
            //точок 100
            Random R = new Random();
            //3 контейнери по 2 матриці по 5 позицій
            int countContainers = 3;
            int countMatrix = 2;
            int countPosition = 5;
            int count = 100; //заповнити 100 точок
            int tmp = 0;

            int[][][] countPoints = new int[3][][];
            for (int i = 0; i < countContainers; i++) //контейнери
            {
                countPoints[i] = new int[countMatrix][];
                for (int j = 0; j < countMatrix; j++) //матриці
                {
                    countPoints[i][j] = new int[countPosition]; //позиції в матриці
                }
            }
            for (int i = 0; i < countContainers; i++)
                for (int j = 0; j < countMatrix; j++)
                    for (int k = 0; k < countPosition; k++)
                    {
                        tmp = R.Next(4, 10); //3*2*5*3=90<100, 3*2*5*4=120>100 
                        tmp = tmp > count ? count : tmp;
                        countPoints[i][j][k] = tmp;
                        count -= tmp;
                    }
            PointDimentional[] PD = new PointDimentional[2];
            PD[0] = PointDimentional.XY;
            PD[1] = PointDimentional.X;


            Containers data = new Containers(countPoints, PointType.Int, PD);
            string rez = data.ToStr();
            string substring = "Point";
            int countPointRez = (rez.Length - rez.Replace(substring, "").Length) / substring.Length;


            Assert.AreEqual(100, countPointRez);
            Console.WriteLine(rez);
        }

        [TestMethod]
        public void TestXYZPointCountAlwaysEqual()
        {
            ///XYZ матриці мають однакову кількість точок у відповідних позиціях відповідних матриць
            Random R = new Random();
            //3 контейнери по 2 матриці по 2 позиції
            int countContainers = 3;
            int countMatrix = 2;
            int countPosition = 2;

            int[][][] countPoints = new int[3][][];
            for (int i = 0; i < countContainers; i++)
            {
                countPoints[i] = new int[countMatrix][];
                for (int j = 0; j < countMatrix; j++)
                {
                    countPoints[i][j] = new int[countPosition];
                }
            }
            for (int i = 0; i < countContainers; i++)
                for (int j = 0; j < countMatrix; j++)
                    for (int k = 0; k < countPosition; k++)
                    {
                        countPoints[i][j][k] = (i + 1) * (j + 1) * (k + 1);
                    }
            PointDimentional[] PD = new PointDimentional[2];
            PD[0] = PointDimentional.XY;
            PD[1] = PointDimentional.XYZ;


            Containers data = new Containers(countPoints, PointType.Double, PD);
            List<string> rez = new List<string>();
            foreach (Container c in data)
            {
                rez.Add(c.LMatrix[1].ToStr());
            }
            int[] countPointRez = new int[rez.Count]; //кількість входжень
            string substring = "Point";
            bool isEqual = true;
            for (int i = 0; i < rez.Count; i++)
            {
                countPointRez[i] = (rez[i].Length - rez[i].Replace(substring, "").Length) / substring.Length;
            }
            for (int i = 1; i < countPointRez.Length; i++) //якщо кількість точок у відповідній матриці кожного контейнера однакова, то все ок
            {
                if (countPointRez[i - 1] != countPointRez[i])
                    isEqual = false;
            }

            for (int i = 0; i < rez.Count; i++)
            {
                Console.WriteLine(rez[i]);
            }


            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void TestPlaceXYZHaveDifferentPossCount()
        {
            //відповідні матриці XYZ(і не лише) мають різну кількість позицій в різних контейнерах
            Random R = new Random();
            //3 контейнери по 2 матриці з різною кількістю позицій
            int countContainers = 3;
            int countMatrix = 2;
            int[] countPosition = new int[countContainers];

            for (int i = 0; i < countContainers; i++)
            {
                countPosition[i] = (i + 1);
            }            
            int[][][] countPoints = new int[3][][];
            for (int i = 0; i < countContainers; i++)
            {
                countPoints[i] = new int[countMatrix][];
                for (int j = 0; j < countMatrix; j++)
                {
                    countPoints[i][j] = new int[countPosition[i]];
                    // в принципі для другої матриці можна поставити статично по 5 позицій, але навіщо? :)
                }
            }
            for (int i = 0; i < countContainers; i++)
                for (int j = 0; j < countMatrix; j++)
                    for (int k = 0; k < countPosition[i]; k++)
                    {
                        countPoints[i][j][k] = R.Next(3,10);
                    }
            PointDimentional[] PD = new PointDimentional[2];
            PD[0] = PointDimentional.XYZ;
            PD[1] = PointDimentional.XY;

            Containers data = new Containers(countPoints, PointType.Decimal, PD);
            Console.WriteLine(data.ToStr());
            bool isEqual = true;
            string[] rez = new string[countContainers];
            int[] countPointRez = new int[countContainers];
            for (int i = 0; i < countContainers; i++)
            {
                rez[i] = data.container[i].ToStr();
            }
            string substring = "Position";
            for (int i = 0; i < rez.Length; i++)
            {
                countPointRez[i] = (rez[i].Length - rez[i].Replace(substring, "").Length) / substring.Length;
            }
            for (int i = 1; i < countPointRez.Length; i++) //якщо кількість точок у відповідній матриці кожного контейнера однакова, то все ок
            {
                if (countPointRez[i - 1] != countPointRez[i])
                    isEqual = false;
            }
            Assert.IsFalse(isEqual);
        }
    }
}
