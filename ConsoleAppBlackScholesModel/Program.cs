// C# code for Python implementation of the following paper's methodology:
// "Market-Maker Hard Exit Thresholds Strategy" by David Shelton, Carlos Veiga

using System;
static double CDF(double x)
{

    //ref https://www.johndcook.com/blog/csharp_phi/

    // constants
    double a1 = 0.254829592;
    double a2 = -0.284496736;
    double a3 = 1.421413741;
    double a4 = -1.453152027;
    double a5 = 1.061405429;
    double p = 0.3275911;

    // Save the sign of x
    int sign = 1;
    if (x < 0)
        sign = -1;
    x = Math.Abs(x) / Math.Sqrt(2.0);

    // A&S formula 7.1.26
    double t = 1.0 / (1.0 + p * x);
    double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

    return 0.5 * (1.0 + sign * y);
}

static double Call_bsm(double StockPrice, double StrikePrice, double TimePeriod, double RateofIntrest, double roh_Volatility)
{
    //ref https://www.codearmo.com/python-tutorial/options-trading-black-scholes-model
    double model = -1;
    try
    {
        //d1 denote Get 
        double d1 = (Math.Log(StockPrice / StrikePrice) + (RateofIntrest + Math.Pow(roh_Volatility, 2) / 2) * TimePeriod) / (roh_Volatility * Math.Sqrt(TimePeriod));

        //d2 denote Pay 
        double d2 = d1 - roh_Volatility * Math.Sqrt(TimePeriod);

        model = StockPrice * CDF(d1) - StrikePrice * Math.Exp(-RateofIntrest * TimePeriod) * CDF(d2);
        return model;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error Ocurred(s) {ex.Message}");
    }
    return model;
}

static double put_bsm(double StockPrice, double StrikePrice, double TimePeriod, double RateofIntrest, double roh_Volatility)
{
    //ref https://www.codearmo.com/python-tutorial/options-trading-black-scholes-model
    double model = -1;
    try
    {
        //d1 denote Get 
        double d1 = (Math.Log(StockPrice / StrikePrice) + (RateofIntrest + Math.Pow(roh_Volatility, 2) / 2) * TimePeriod) / (roh_Volatility * Math.Sqrt(TimePeriod));

        //d2 denote Pay 
        double d2 = d1 - roh_Volatility * Math.Sqrt(TimePeriod);

        model = StrikePrice * Math.Exp(-RateofIntrest * TimePeriod) * CDF(-d2) - (StockPrice * CDF(-d1));
        return model;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error Ocurred(s) {ex.Message}");
    }
    return model;
}

//Ref https://www.codearmo.com/python-tutorial/options-trading-black-scholes-model
//Ref https://github.com/Coderixc/BlackScholesModel

// input the current price, strike price, interest rate, time of expiry, implied volatility here
double S_CurrentPrice = 10;
double K_StrikePrice = 20;
double R_Rate_of_intrest = 1;
int T_TimeofExpiry = 5;
double IV_ImpliedVolatiity = 0.5;
Console.WriteLine("Call option value using BS model is: " + Call_bsm(S_CurrentPrice, K_StrikePrice, T_TimeofExpiry, R_Rate_of_intrest, IV_ImpliedVolatiity));
Console.WriteLine("Put option value using BS model is: " + put_bsm(S_CurrentPrice, K_StrikePrice, T_TimeofExpiry, R_Rate_of_intrest, IV_ImpliedVolatiity));