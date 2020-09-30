using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;


namespace PurpleMoon.Core
{
    public static class Clock
    {
        // time - numerals
        public static int GetHour() { return RTC.Hour; }
        public static int GetMinute() { return RTC.Minute; }
        public static int GetSecond() { return RTC.Second; }
        public static float GetMillisecond() { return (float)RTC.Second * (float)1000; }

        // time - strings
        public static string GetTime() { return RTC.Hour.ToString() + ":" + RTC.Minute.ToString() + ":" + RTC.Second.ToString(); }
        public static string FormattedTime { get; private set; }
        public static string HourString { get; private set; }
        public static string MinuteString { get; private set; }
        public static string SecondString { get; private set; }

        // update
        public static void Update()
        {
            // format hour
            int hr; bool morning = true;
            if (GetHour() <= 12) { hr = GetHour(); if (hr < 11) { morning = true; } }
            else { hr = GetHour() - 12; if (hr < 12) { morning = false; } }

            // update hour string
            if (hr < 10) { HourString = "0" + hr.ToString(); }
            if (hr == 0) { HourString = "12"; }
            else { HourString = hr.ToString(); }

            // update minute string
            if (GetMinute() < 10) { MinuteString = "0" + GetMinute().ToString(); }
            else { MinuteString = GetMinute().ToString(); }

            // update second string
            if (GetSecond() < 10) { SecondString = "0" + GetSecond().ToString(); }
            else { SecondString = GetSecond().ToString(); }

            // update time string
            if (morning) { FormattedTime = HourString + ":" + MinuteString + " AM"; }
            else { FormattedTime = HourString + ":" + MinuteString + " PM"; }
        }
    }
}
