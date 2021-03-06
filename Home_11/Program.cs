﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Home_11
{
    static class Task_1
    {
        class AlarmEventArgs
        {
            public UInt16 alarmHour { get; }
            public UInt16 alarmMinute { get; }
            public string message { get; }
            public AlarmEventArgs(UInt16 alarmHour = 0, UInt16 alarmMinute = 0, string message = "") : base()
            {
                this.alarmHour = alarmHour;
                this.alarmMinute = alarmMinute;
                this.message = message;
            }
        }
        class Watch
        {
            UInt16 hour;
            UInt16 minute;
            public Watch(UInt16 hour = 0, UInt16 minute = 0)
            {
                this.hour = hour;
                this.minute = minute;
            }
            void alarmStart(AlarmEventArgs alarm)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write($"{alarm.alarmHour:00}:{alarm.alarmMinute:00}  Message : {alarm.message}");
            }
            static public void Tick(object obj, AlarmEventArgs alarm)
            {
                Watch tmp = obj as Watch;
                if (tmp != null)
                {
                    tmp.minute = tmp.minute + 1 == 60 ? (UInt16)0 : (UInt16)(tmp.minute + 1);
                    tmp.hour += tmp.minute == 0 ? (tmp.hour + 1 == 24 ? (UInt16)0 : (UInt16)1) : (UInt16)0;
                    Console.SetCursorPosition(0, 0);
                    Console.Write($"{tmp.hour:00}:{tmp.minute:00}");

                }
                if (alarm?.alarmHour == tmp.hour && alarm?.alarmMinute == tmp.minute)
                {
                    tmp.alarmStart(alarm);
                }
            }

        }
        public static void main()
        {
            Watch w = new Watch(13, 49);
            AlarmEventArgs alarm = new AlarmEventArgs(14, 1, "Good morning");
            for (int i = 0; i < 60; i++)
            {
                Thread.Sleep(1000);
                Watch.Tick(w, alarm);
            }
        }
    }
    class Task_2
    {
        class Car
        {
            static public UInt16 counterID = 0;
            static Random rnd = new Random();
            static public UInt16 MaxId => counterID;

            public delegate void FinishCar(Car car);
            const UInt16 MinSpeed = 1;
            const UInt16 MaxSpeed = 5;
            UInt16 speed = (UInt16)rnd.Next(MinSpeed, MaxSpeed);
            UInt16 id = counterID++;
            UInt16 position = 0;
            event FinishCar finish;
            public FinishCar Finish
            {
                set => finish = value;
            }
            void newSpeed()
            {
                if (rnd.Next(0, 1) == 0)
                {
                    speed = (UInt16)rnd.Next(MinSpeed, MaxSpeed);
                }
            }
            public void Go()
            {
                newSpeed();
                position = (position + speed > 100) ? (UInt16)100 : (UInt16)(position + (UInt16)speed);
                Console.SetCursorPosition(position, id);
                Console.WriteLine(this.GetType().Name);
                if (position == 100)
                {
                    finish(this);
                }
            }
            public void ToStart()
            {
                position = 0;
                Console.SetCursorPosition(position, id);
                Console.WriteLine(this.GetType().Name);
            }
        }
        class SportsCar : Car
        {

        }
        class TrucksCar : Car
        {

        }
        class BusesCar : Car
        {

        }
        class Game
        {

            List<Car> cars = new List<Car>();
            Action toStart;
            Action carsGo;
            bool finish = false;
            public Game(List<Car> cars)
            {
                this.cars = cars;
                for (int i = 0; i < cars.Count; i++)
                {
                    toStart += cars[i].ToStart;
                    cars[i].Finish = Finish;
                    carsGo += cars[i].Go;

                }
            }
            void Finish(Car car)
            {
                finish = true;
                Console.SetCursorPosition(0, Car.MaxId);
                Console.WriteLine("Won " + car.GetType().Name);
                Thread.Sleep(2000);
            }
            public void ToStart()
            {
                Console.Clear();
                finish = false;
                toStart();
            }
            public void Go()
            {

                while (!finish)
                {
                    Console.Clear();
                    carsGo();
                    Thread.Sleep(111);
                }
            }
        }

        static public void main()
        {
            Game a = new Game(new List<Car>() { new Car(), new SportsCar(), new TrucksCar(), new BusesCar() });
            a.Go();
            a.ToStart();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
             //Task_1.main();
             Task_2.main();
        }
    }
}
