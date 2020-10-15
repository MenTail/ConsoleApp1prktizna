using System;

namespace StructConlsole
{
    class Entrant
    {
        protected string Name; // – прiзвище та iнiцiали абiтурiєнта
        protected double IdNum; // – iдентифiкацiйний код абiтурiєнта
        protected float CoursePoints; // – бали за пiдготовчi курси
        protected float AvgPoints; // – бал атестату
        protected ZNO[] ZNOResults; // – масив об’єктiв типу ZNO;

        public Entrant(string Name = "", double IdNum = 0, float CoursePoints = 0, float AvgPoints = 0)
        {
            this.Name = Name;
            this.IdNum = IdNum;
            this.CoursePoints = CoursePoints;
            this.AvgPoints = AvgPoints;
            this.ZNOResults = new ZNO[3];
        }

        public Entrant(Entrant Copy)
        {
            this.Name = Copy.Name;
            this.IdNum = Copy.IdNum;
            this.CoursePoints = Copy.CoursePoints;
            this.AvgPoints = Copy.AvgPoints;
            this.ZNOResults = Copy.ZNOResults;
        }

        public void SetName(string Name) { this.Name = Name; }
        public string GetName() { return Name; }
        public void SetIdNum(double IdNum) { this.IdNum = IdNum; }
        public double GetIdNum() { return IdNum; }
        public void SetCoursePoints(float CoursePoints) { this.CoursePoints = CoursePoints; }
        public float GetCoursePoints() { return CoursePoints; }
        public void SetAvgPoints(float AvgPoints) { this.AvgPoints = AvgPoints; }
        public float GetAvgPoints() { return AvgPoints; }
        public void SetZNOResults(ZNO[] ZNOResults) { this.ZNOResults = ZNOResults; }
        public ZNO[] GetZNOResults() { return ZNOResults; }

        public string GetBestSubject() // повертає назву предмета, за яким абiтурiєнт має найкращий бал
        {
            float[] Temp = new float[3];
            for (int i = 0; i < 3; i++) { Temp[i] = ZNOResults[i].GetPoints(); }
            if ((Temp[0] > Temp[1] & Temp[0] > Temp[2])) { return ZNOResults[0].GetSubject(); }
            if ((Temp[1] > Temp[0] & Temp[1] > Temp[2])) { return ZNOResults[1].GetSubject(); }
            if ((Temp[2] > Temp[1] & Temp[2] > Temp[0])) { return ZNOResults[2].GetSubject(); }
            return "nope";
        }
        public float GetCompMark() // обраховує конкурсний бал абiтурiєнта
        {
            float[] Temp = new float[3];
            for (int i = 0; i < 3; i++) { Temp[i] = ZNOResults[i].GetPoints(); }
            if ((Temp[0] == 0) | (Temp[1] == 0) | (Temp[2] == 0)) { return 0; }
            else { return Convert.ToSingle((CoursePoints * 0.05 + AvgPoints * 0.1 + Temp[0] * 0.25 + Temp[1] * 0.40 + Temp[2] * 0.25)); }
        }

        public string GetWorstSubject() // повертає назву предмета, за яким абiтурiєнт має найгiрший бал
        {
            float[] Temp = new float[3];
            for (int i = 0; i < 3; i++) { Temp[i] = ZNOResults[i].GetPoints(); }
            if ((Temp[0] < Temp[1] & Temp[0] < Temp[2])) { return ZNOResults[0].GetSubject(); }
            if ((Temp[1] < Temp[0] & Temp[1] < Temp[2])) { return ZNOResults[1].GetSubject(); }
            if ((Temp[2] < Temp[1] & Temp[2] < Temp[0])) { return ZNOResults[2].GetSubject(); }
            return "nope";
        }
    }
    class ZNO
    {
        protected string Subject; // – назва предмета
        protected float Points; // – результат

        public ZNO(string Subject = "", float Points = 0)
        {
            this.Subject = Subject;
            this.Points = Points;
        }
        public ZNO(ZNO Copy)
        {
            this.Subject = Copy.Subject;
            this.Points = Copy.Points;
        }
        public void SetSubject(string Subject) { this.Subject = Subject; }
        public string GetSubject() { return Subject; }
        public void SetPoints(float Points) { this.Points = Points; }
        public float GetPoints() { return Points; }
    }
    class Program
    {
        static void Main()
        {
            Entrant[] info = ReadEntrantsArray();
            PrintEntrants(info);
        }

        static Entrant[] ReadEntrantsArray() // читає з клавiатури данi i повертає масив об’єктiв типу Entrant(n штук)
        {
            Console.Write("|+|Введiть кiлькiсть абiтурiєнтiв -> ");
            int size = Convert.ToInt32(Console.ReadLine());
            Entrant[] temp_arr = new Entrant[size];

            for (int i = 0; i < size; i++)
            {
                temp_arr[i] = new Entrant();
                ZNO[] temp = new ZNO[3];
                Console.Write("|+|Абiтурiєнт #" + (i + 1) + "\n|+|Ввести П.I.Б -> "); temp_arr[i].SetName(Console.ReadLine());
                Console.Write("|+|Ввести iдентифiкацiйний код абiтурiєнта -> "); temp_arr[i].SetIdNum(Convert.ToDouble(Console.ReadLine()));
                Console.Write("|+|Ввести бал за пiдготовчi курси -> "); temp_arr[i].SetIdNum(Convert.ToSingle(Console.ReadLine()));
                Console.Write("|+|Ввести бал aтестату - > "); temp_arr[i].SetIdNum(Convert.ToSingle(Console.ReadLine()));
                for (i = 0; i < 3; i++)
                {
                    string on;
                    switch (i)
                    {
                        case 0: on = "1-ого"; break;
                        case 1: on = "2-ого"; break;
                        case 2: on = "3-ого"; break;
                        default: on = "|+|Невiрно набраний символ!|+|"; break;
                    }
                    Console.Write("|+|Назва " + on + " предмету -> "); temp[i].SetSubject(Console.ReadLine());
                    Console.Write("|+|Ввести результат ЗНО по предмету -> "); temp[i].SetPoints(Convert.ToSingle(Console.ReadLine()));
                }
                temp_arr[i].SetZNOResults(temp); Console.WriteLine();
            }
            return temp_arr;
        }

        static void PrintEntrant(Entrant obj) // – приймає об’єкт типу Entrant i виводить його на екран
        {
            Console.Write("|+|Iмя абiтурiєнта -> " + obj.GetName());
            Console.Write("\n|+|Iндетифiкацiйний код -> " + obj.GetIdNum());
            Console.Write("\n|+|Балiв за пiдготовчi курси -> " + obj.GetCoursePoints());
            Console.Write("\n|+|Бал атестата -> " + obj.GetAvgPoints());
            for (int i = 0; i < 3; i++)
            {
                ZNO[] temp = obj.GetZNOResults();
                Console.Write("\n|+|Предмет ЗНО -> " + temp[i].GetSubject() + " бал -> " + temp[i].GetPoints() + "\n");
            }
        }
        static Entrant[] SortEntrantsByName(Entrant[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - i; j++)
                {
                    if (Convert.ToBoolean(String.Compare(arr[j].GetName(), arr[j + 1].GetName())))
                    {
                        Entrant temp_arr = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp_arr;
                    }
                }
            }
            return arr;
        }
        static Entrant[] SortEntrantsByPoints(Entrant[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - i; j++)
                {
                    if (arr[j].GetCompMark() < arr[j + 1].GetCompMark())
                    {
                        Entrant temp_arr = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp_arr;
                    }
                }
            }

            return arr;
        }
        static void PrintEntrants(Entrant[] obj) // приймає масив об’єктiв типу Entrant i виводить його на екран
        {
            for (int i = 0; i < obj.Length; i++)
            {
                Console.Write("|+|Абiтурiєнт номер -> " + (i + 1) + "\n");
                PrintEntrant(obj[i]);
            }
        }

        static void GetEntrantsInfo(Entrant[] obj, out double max, out double min) // приймає масив об’єктiв типу Entrant i повертає через out- параметри
        {
            max = obj[0].GetCompMark();
            min = max;

            for (int i = 1; i < obj.Length; i++)
            {
                if (max < obj[i].GetCompMark()) { max = obj[i].GetCompMark(); }
                else if (min > obj[i].GetCompMark()) { min = obj[i].GetCompMark(); }
            }
        }
    }
}