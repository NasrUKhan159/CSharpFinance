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

static double[,] __GetSwapCurveData__(Dictionary<string, List<object>> curveparams)
{
    throw new NotImplementedException();

}

static double[,] _DatesForTenors_()
{
    throw new NotImplementedException();
}

static double[,] _YearFractionsForTenors_()
{
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