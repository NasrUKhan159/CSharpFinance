// Yield curve construction using swaps in C#

using System;
using System.Collections.Generic;

// complete implementation

static bool _IsHoliday_(DateTime date, string calendar)
{
    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
    {
        return false;
    }
    String dy = date.Day.ToString();
    String mn = date.Month.ToString();
    String yy = date.Year.ToString();

    // only implemented NY calendar here - as extension, can implement other calendars
    if (calendar == "NY")
    {
        // Jan 1 bank holiday
        if ((mn == "1" && dy == "1") || (mn == "12" && dy == "31" && date.DayOfWeek == DayOfWeek.Friday) || (mn == "1" && dy == "2" && date.DayOfWeek == DayOfWeek.Monday))
        {
            return true;
        }
    }
    return false;
}

static bool _IsWeekend_(DateTime date)
{
    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Saturday)
    {
        return true;
    }
    else
    {
        return false;
    }
}

static DateTime _AddBusinessDays_(DateTime date, int numdays, string calendar)
{
    DateTime next_date = date;
    for (int i = 0; i < numdays; i++)
    {
        next_date.AddDays(1);
        while (_IsWeekend_(next_date) || _IsHoliday_(next_date, "NY"))
        {
            next_date.AddDays(1);
        }
    }
    return next_date;
}

static DateTime _AddBusinessMonths_(DateTime date, int nummonths, string calendar)
{
    DateTime next_date = date;
    for (int i = 0; i < nummonths; i++)
    {
        next_date.AddMonths(nummonths);
        while (_IsWeekend_(next_date) || _IsHoliday_(next_date, "NY"))
        {
            next_date.AddDays(1);
        }
    }
    return next_date;
}

static DateTime _AddBusinessYears_(DateTime date, int numyears, string calendar)
{
    DateTime next_date = date;
    for (int i = 0; i < numyears; i++)
    {
        next_date.AddMonths(numyears);
        while (_IsWeekend_(next_date) || _IsHoliday_(next_date, "NY"))
        {
            next_date.AddDays(1);
        }
    }
    return next_date;
}

static double _YFrac_(DateTime date1, DateTime date2, string daycountconvention)
{
    if (daycountconvention == "ACTACT")
    {
        throw new NotImplementedException();
    }
    else if (daycountconvention == "ACT365")
    {
        TimeSpan delta = date2.Subtract(date1);
        double delta_fraction = delta.Days / 365.0;
        return delta_fraction;
    }
    else if (daycountconvention == "ACT360")
    {
        TimeSpan delta = date2.Subtract(date1);
        double delta_fraction = delta.Days / 360.0;
        return delta_fraction;
    }
    else if (daycountconvention == "Thirty360")
    {
        throw new NotImplementedException();
    }
    else
    {
        // default to ACT360 methodology
        TimeSpan delta = date2.Subtract(date1);
        double delta_fraction = delta.Days / 360.0;
        return delta_fraction;
    }
}

static DateTime __GetSwapCurveData__(string busdayconv, string calendar, DateTime curvedate, int settledays)
{
    while (_IsWeekend_(curvedate) || _IsHoliday_(curvedate, "NY"))
    {
        curvedate.AddDays(1);
    }
    DateTime curvesettledate = _AddBusinessDays_(curvedate, settledays, calendar);
    return curvesettledate;
}

static double[,] _DatesForTenors_(List<string> tenors, string busdayconv, string calendar, DateTime curvedate, int settledays)
{
    List<DateTime> date_list = new List<DateTime>{ };
    foreach(string tenor in tenors)
    {
        int num = int.Parse(tenor.Substring(0, 1));
        DateTime settledate = __GetSwapCurveData__(busdayconv, calendar, curvedate, settledays);
        DateTime date = _AddBusinessYears_(settledate, num, calendar);
        date_list.Add(date);
    }
}

static double[,] _YearFractionsForTenors_()
{
    // continue from here
    throw new NotImplementedException();
}

static List<double> _SwapYearFractions_(string frequency, int term)
{
    throw new NotImplementedException();
}

static double[,] _ZeroRates()
{
    throw new NotImplementedException();
}

// define the necessary variables
DateTime curvedate = new DateTime(2024, 8, 26);
int settledays = 2;
string calendar = "London";
string busdayconv = "Following";
string daycount_conv = "ACT360";
List<string> tenors = new List<string> { "1Y", "2Y", "3Y", "5Y", "7Y", "10Y", "12Y", "15Y", "20Y", "25Y" };
List<double> swaprate = new List<double> { 0.042, 0.043, 0.047, 0.054, 0.057, 0.06, 0.061, 0.059, 0.056, 0.0555 };
List<string> type = Enumerable.Repeat("Swap", 10).ToList();
List<string> frequency = Enumerable.Repeat("S", 10).ToList();
List<string> daycount = Enumerable.Repeat("ACT360", 10).ToList();