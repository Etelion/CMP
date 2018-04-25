using System;
using System.Text;
using PMC_Test_COI_;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int countContainer;
            int countMatrix;
            int countPosition;
            PointType pointType;
            PointDimentional[] matrixDimentional;
            int[][][] pointCount;
            int tmp;
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine("Введіть кількість контейнерів");
                countContainer = Int32.Parse(Console.ReadLine());
                if (countContainer <= 0)
                    throw new Exception("кількість контейнерів повинна бути більшою за нуль");
                pointCount = new int[countContainer][][];

                Console.WriteLine("Введіть кількість матриць в контейнерах");
                countMatrix = Int32.Parse(Console.ReadLine());
                if (countMatrix <= 0)
                    throw new Exception("кількість матриць повинна бути більшою за нуль");
                for (int i = 0; i < countContainer; i++)
                {
                    pointCount[i] = new int[countMatrix][];
                }

                Console.WriteLine("Введіть тип даних: 1 int, 2 double, 3 decimal");
                switch (Int32.Parse(Console.ReadLine()))
                { 
                    case 1:
                        pointType = PointType.Int;
                        break;
                    case 2:
                        pointType = PointType.Double;
                        break;
                    case 3:
                        pointType = PointType.Decimal;
                        break;
                    default:
                        throw new Exception("невірний тип даних");
                        break;
                }
                matrixDimentional = new PointDimentional[countMatrix];
                for (int i = 0; i < countMatrix; i++)
                {
                    Console.WriteLine("Введіть розмірність точок для даних матриці "+(i+1)+": 1 1D, 2 2D, 3 3D");
                    switch (Int32.Parse(Console.ReadLine()))
                    {
                        case 1:
                            matrixDimentional[i] = PointDimentional.X;
                            break;
                        case 2:
                            matrixDimentional[i] = PointDimentional.XY;
                            break;
                        case 3:
                            matrixDimentional[i] = PointDimentional.XYZ;
                            break;
                        default:
                            throw new Exception("невірна розмірність даних");
                            break;
                    }
                }
                int[,] maxCountPos = new int[2,countMatrix]; //номер контейнера та значення де максимальна кількість позицій в матриці, масив розріджений, лише для типу XYZ
                Console.WriteLine("Введіть кількість позицій даних матрицях");
                countPosition = Int32.Parse(Console.ReadLine());
                if (countPosition <= 0)
                    throw new Exception("кількість позицій повинна бути більшою за нуль");
                
                for (int i = 0; i < countMatrix; i++) //ініціалізація позицій в матрицях 
                {
                    if (matrixDimentional[i] == PointDimentional.XYZ) //матрицях типу XYZ
                    {
                        for (int j = 0; j < countContainer; j++)
                        {
                            Console.WriteLine("Введіть кількість позицій даних матриці {0} в контейнері {1}", i+1, j+1);
                            tmp = Int32.Parse(Console.ReadLine());
                            if (tmp <= 0)
                                throw new Exception("кількість позицій повинна бути більшою за нуль");
                            pointCount[j][i] = new int[tmp];
                            if (tmp > maxCountPos[1,j])
                            {
                                maxCountPos[0, i] = j;
                                maxCountPos[1, i] = tmp;
                            }
                        }
                    }
                    else // у інших матрицях однакова кількість позицій у всіх контейнерах
                    {
                        for (int j = 0; j < countContainer; j++)
                        {
                            pointCount[j][i] = new int[countPosition];
                        }
                    }                    
                }

                for (int i = 0; i < countMatrix; i++)
                {
                    if (matrixDimentional[i] == PointDimentional.XYZ) //якщо матриця типу XYZ, то кількість точок в позиціях рівні через еквівалентні індекси матриць та контейнерів
                    //тому варто заповнити найбільші послідовності, а потім витягнути підпослідовності в інші контейнери де кількість позицій менша
                    {
                        for (int j = 0; j < maxCountPos[1, i]; j++) //другий стовпчик з кількостями позицій
                        {
                            Console.WriteLine("Введіть кількість точок даних матриці {0} в позиції {1}", i+1, j+1);
                            tmp = Int32.Parse(Console.ReadLine());
                            if (tmp <= 0)
                                throw new Exception("кількість точок повинна бути більше рівне нулю");
                            pointCount[maxCountPos[0, i]][i][j] = tmp;
                        }
                        for (int indexContainer = 0; indexContainer < countContainer; indexContainer++)//цикл по контейнерах
                        {
                            for (int indexPosition = 0; indexPosition < pointCount[indexContainer][i].Length; indexPosition++) //цикл по позиціях в даній матриці XYZ
                            {
                                pointCount[indexContainer][i][indexPosition] = pointCount[maxCountPos[0, i]][i][indexPosition]; //присвоюємо кількість точок таку ж, як і в ідентичній матриці контейнера, де кількість позицій максимальна
                            }
                        }
                    }
                    else //я так і не зрозумів чи лише для XY даних кількість точок різна, а для X всюди однакова, чи всюди різна тому тут можливість ввести всі дані 
                    {
                        for (int k = 0; k < countContainer; k++) //цикл по контейнерах
                        {
                            for (int j = 0; j < pointCount[k][i].Length; j++) //цикл по позиціях
                            {
                                Console.WriteLine("Введіть кількість точок даних матриці {0} в позиції {1} в контейнері {2}", i+1, j+1, k+1);
                                tmp = Int32.Parse(Console.ReadLine());
                                if (tmp < 0)
                                    throw new Exception("кількість точок повинна бути більше рівне нулю");
                                pointCount[k][i][j] = tmp;
                            }
                        }
                    }
                }

                //створення Containers з даними та виведення в консоль
                Containers data = new Containers(pointCount, pointType, matrixDimentional);

                Console.WriteLine(data.ToStr());

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка " + ex.Message);
                Console.ReadKey();
            }            
        }
    }
}
