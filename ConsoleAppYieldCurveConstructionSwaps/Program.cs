// Yield curve construction using swaps in C#

using System;
using System.Collections;
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

static List<DateTime> _DatesForTenors_(List<string> tenors, string busdayconv, string calendar, DateTime curvedate, int settledays)
{
    List<DateTime> date_list = new List<DateTime>{ };
    foreach(string tenor in tenors)
    {
        int num = int.Parse(tenor.Substring(0, 1));
        DateTime settledate = __GetSwapCurveData__(busdayconv, calendar, curvedate, settledays);
        DateTime date = _AddBusinessYears_(settledate, num, calendar);
        date_list.Add(date);
    }
    return date_list;
}

static (List<double>, List<double>) _YearFractionsForTenors_(List<string> tenors, string busdayconv, string calendar, DateTime curvedate, int settledays, List<string> daycount)
{
    List<DateTime> date_list = _DatesForTenors_(tenors, busdayconv, calendar, curvedate, settledays);
    // continue from here
    DateTime settledate = __GetSwapCurveData__(busdayconv, calendar, curvedate, settledays);
    List<double> yearfractions = new List<double> { };
    List<double> cumyearfractions = new List<double> { };
    for (int i = 0; i < daycount.Count; i++)
    {
        // access the i-th element of daycount convention 
        string daycntconv = daycount[i];
        if (i == 0)
        {
            double yearfraction = _YFrac_(settledate, date_list[i], daycntconv);
            yearfractions.Add(yearfraction);
        }
        else
        {
            double yearfraction = _YFrac_(date_list[i-1], date_list[i], daycntconv);
            yearfractions.Add(yearfraction);
        }
        double cumyearfraction = _YFrac_(settledate, date_list[i], daycntconv);
        cumyearfractions.Add(cumyearfraction);
    }
    return (yearfractions, cumyearfractions);
}

static List<double> _SwapYearFractions_(string frequency, int term, string busdayconv, string calendar, DateTime curvedate, int settledays)
{
    DateTime settledate = __GetSwapCurveData__(busdayconv, calendar, curvedate, settledays);
    double period = 0.5;
    if (frequency == "S")
    {
        period = 0.5;
    }
    else
    {
        // extension: add more logic for frequencies
        period = 0.5;
    }
    List<int> swap_months = new List<int> { };
    List<DateTime> swap_dates = new List<DateTime> { };
    List<double> swap_year_fractions = new List<double> { };
    for (int i = 1; i < 2 * term; i++)
    {
        int swap_month = Convert.ToInt32(12 * period * i);
        swap_months.Add(swap_month);
    }
    foreach (int nummonths in swap_months)
    {
        swap_dates.Add(_AddBusinessMonths_(settledate, nummonths, calendar));
    }
    foreach (DateTime swap_date in swap_dates)
    {
        swap_year_fractions.Add(_YFrac_(settledate, swap_date, "ACT360"));
    }
    return swap_year_fractions;
}

static double[] convert_list_to_double(List<double> arbitrary_list)
{
    double[] arr = new double[arbitrary_list.Count];
    for (int i = 0; i < arbitrary_list.Count; i++)
    {
        arr[i] = arbitrary_list[i];
    }
    return arr;
}

static List<double> _ZeroRates(List<double> swaprate, List<string> frequency, List<string> tenors, string busdayconv, string calendar, DateTime curvedate, int settledays, List<string> daycount)
{
    var (yearfractions, cumyearfractions) = _YearFractionsForTenors_(tenors, busdayconv, calendar, curvedate, settledays, daycount);
    List<double> zero_rate_list = new List<double> { };
    for (int i = 0; i < swaprate.Count; i++)
    {
        double sumproduct = 0.0;
        string frequency_element = frequency[i];
        int term = int.Parse(tenors[i].Substring(0, 1));
        List<double> swap_year_fractions = _SwapYearFractions_(frequency_element, term, busdayconv, calendar, curvedate, settledays);
        // set up the interpolation object
        List<double> x = cumyearfractions.Take(i + 4).ToList();
        List<double> y = swaprate.Take(i + 4).ToList();
        // convert the variables to doubles
        double[] x_double = convert_list_to_double(x);
        double[] y_double = convert_list_to_double(y);
        // apply the interpolation
        (double[] xs2, double[] ys2) = Cubic.InterpolateXY(x_double, y_double, 50);
        foreach (double swap_yf in swap_year_fractions)
        {
            sumproduct += (swaprate[i] / 2) * Math.Exp(-ys2[i] * swap_yf);
        }
        double zero_rate = (-1 * Math.Log((1 - sumproduct) / (1 + swaprate[i] / 2))) / cumyearfractions[i];
        zero_rate_list.Add(zero_rate);
    }
    return zero_rate_list;
}

// runner code - define the necessary variables
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

// call the method
List<double> zero_rate_list = _ZeroRates(swaprate, frequency, tenors, busdayconv, calendar, curvedate, settledays, daycount);
foreach (double zerorate in zero_rate_list)
{
    Console.WriteLine(zerorate);
}

// need to define spline class in C#
// ref: https://swharden.com/blog/2022-01-22-spline-interpolation/
public static class Cubic
{
    /// <summary>
    /// Generate a smooth (interpolated) curve that follows the path of the given X/Y points
    /// </summary>
    public static (double[] xs, double[] ys) InterpolateXY(double[] xs, double[] ys, int count)
    {
        if (xs is null || ys is null || xs.Length != ys.Length)
            throw new ArgumentException($"{nameof(xs)} and {nameof(ys)} must have same length");

        int inputPointCount = xs.Length;
        double[] inputDistances = new double[inputPointCount];
        for (int i = 1; i < inputPointCount; i++)
        {
            double dx = xs[i] - xs[i - 1];
            double dy = ys[i] - ys[i - 1];
            double distance = Math.Sqrt(dx * dx + dy * dy);
            inputDistances[i] = inputDistances[i - 1] + distance;
        }

        double meanDistance = inputDistances.Last() / (count - 1);
        double[] evenDistances = Enumerable.Range(0, count).Select(x => x * meanDistance).ToArray();
        double[] xsOut = Interpolate(inputDistances, xs, evenDistances);
        double[] ysOut = Interpolate(inputDistances, ys, evenDistances);
        return (xsOut, ysOut);
    }

    private static double[] Interpolate(double[] xOrig, double[] yOrig, double[] xInterp)
    {
        (double[] a, double[] b) = FitMatrix(xOrig, yOrig);

        double[] yInterp = new double[xInterp.Length];
        for (int i = 0; i < yInterp.Length; i++)
        {
            int j;
            for (j = 0; j < xOrig.Length - 2; j++)
                if (xInterp[i] <= xOrig[j + 1])
                    break;

            double dx = xOrig[j + 1] - xOrig[j];
            double t = (xInterp[i] - xOrig[j]) / dx;
            double y = (1 - t) * yOrig[j] + t * yOrig[j + 1] +
                t * (1 - t) * (a[j] * (1 - t) + b[j] * t);
            yInterp[i] = y;
        }

        return yInterp;
    }

    private static (double[] a, double[] b) FitMatrix(double[] x, double[] y)
    {
        int n = x.Length;
        double[] a = new double[n - 1];
        double[] b = new double[n - 1];
        double[] r = new double[n];
        double[] A = new double[n];
        double[] B = new double[n];
        double[] C = new double[n];

        double dx1, dx2, dy1, dy2;

        dx1 = x[1] - x[0];
        C[0] = 1.0f / dx1;
        B[0] = 2.0f * C[0];
        r[0] = 3 * (y[1] - y[0]) / (dx1 * dx1);

        for (int i = 1; i < n - 1; i++)
        {
            dx1 = x[i] - x[i - 1];
            dx2 = x[i + 1] - x[i];
            A[i] = 1.0f / dx1;
            C[i] = 1.0f / dx2;
            B[i] = 2.0f * (A[i] + C[i]);
            dy1 = y[i] - y[i - 1];
            dy2 = y[i + 1] - y[i];
            r[i] = 3 * (dy1 / (dx1 * dx1) + dy2 / (dx2 * dx2));
        }

        dx1 = x[n - 1] - x[n - 2];
        dy1 = y[n - 1] - y[n - 2];
        A[n - 1] = 1.0f / dx1;
        B[n - 1] = 2.0f * A[n - 1];
        r[n - 1] = 3 * (dy1 / (dx1 * dx1));

        double[] cPrime = new double[n];
        cPrime[0] = C[0] / B[0];
        for (int i = 1; i < n; i++)
            cPrime[i] = C[i] / (B[i] - cPrime[i - 1] * A[i]);

        double[] dPrime = new double[n];
        dPrime[0] = r[0] / B[0];
        for (int i = 1; i < n; i++)
            dPrime[i] = (r[i] - dPrime[i - 1] * A[i]) / (B[i] - cPrime[i - 1] * A[i]);

        double[] k = new double[n];
        k[n - 1] = dPrime[n - 1];
        for (int i = n - 2; i >= 0; i--)
            k[i] = dPrime[i] - cPrime[i] * k[i + 1];

        for (int i = 1; i < n; i++)
        {
            dx1 = x[i] - x[i - 1];
            dy1 = y[i] - y[i - 1];
            a[i - 1] = k[i - 1] * dx1 - dy1;
            b[i - 1] = -k[i] * dx1 + dy1;
        }

        return (a, b);
    }
}