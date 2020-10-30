using System;
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
        public AlarmEventArgs(UInt16 alarmHour = 0, UInt16 alarmMinute = 0,string message="") :base()
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
                if (tmp!=null)
                {
                    tmp.minute = tmp.minute + 1 == 60 ? (UInt16)0 : (UInt16)(tmp.minute + 1);
                    tmp.hour += tmp.minute == 0 ? (tmp.hour+1==24?(UInt16)0 : (UInt16)1): (UInt16)0;
                Console.SetCursorPosition(0, 0);
                Console.Write($"{tmp.hour:00}:{tmp.minute:00}");

                }
                if (alarm?.alarmHour== tmp.hour &&alarm?.alarmMinute== tmp.minute)
                {
                    tmp.alarmStart(alarm);
                }
            }

        }
public static void main()
        {
            Watch w = new Watch(13,49);
            AlarmEventArgs alarm = new AlarmEventArgs(14, 1, "Good morning");
            for (int i = 0; i < 60; i++)
            {
                Thread.Sleep(1000);
                Watch.Tick(w, alarm);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Task_1.main();
        }
    }
}
