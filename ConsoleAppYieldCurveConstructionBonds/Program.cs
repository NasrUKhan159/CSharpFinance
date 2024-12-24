// Yield curve construction using C#
// Reference for Python implementation: https://github.com/omarja12/Bootstrap-Yield-Curve/blob/main/Bootstrapping_Yield_Curve.ipynb

/*
 * In this script, we aim to do the following:
 * a. Yield curves framework that receives array with info on maturity, price and coupon for set of bonds
 * b. Add method to bootstrap discount factors using matrix operations
 * c. Determine spot rate (annual compounding) for each maturity from the calculated discount factors
 * d. Determine the YTM for each bond
 * e. Determine the 1Y forward rate starting in each of the years from 1 to 9
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

// maturities, coupon prices and dirty prices arrays, based on bond mkt info
List<int> maturities = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
// precision of floats is around 6-7 decimal places, precision of doubles is around 15 decimal places
List<float> prices = { 96.6, 93.71, 91.56, 90.24, 89.74, 90.04, 91.09, 92.82, 95.19, 98.14};
List<float> coupons = { 0.015, 0.0175, 0.02, 0.0225, 0.025, 0.0275, 0.03, 0.0325, 0.035, 0.0375};
double[,] matrixBonds = { maturities, prices, coupons};

/*
 * Expected value for cash_flows:
 * [[101.5, 0, 0, 0, 0, 0, 0, 0, 0, 0], 
 * [1.75, 101.75, 0, 0, 0, 0, 0, 0, 0, 0], 
 * [2.0, 2.0, 102.0, 0, 0, 0, 0, 0, 0, 0], 
 * [2.25, 2.25, 2.25, 102.25, 0, 0, 0, 0, 0, 0], 
 * [2.5, 2.5, 2.5, 2.5, 102.5, 0, 0, 0, 0, 0], 
 * [2.75, 2.75, 2.75, 2.75, 2.75, 102.75, 0, 0, 0, 0], 
 * [3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 103.0, 0, 0, 0], 
 * [3.25, 3.25, 3.25, 3.25, 3.25, 3.25, 3.25, 103.25, 0, 0], 
 * [3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 103.5, 0], 
 * [3.75, 3.75, 3.75, 3.75, 3.75, 3.75, 3.75, 3.75, 3.75, 103.75]]
 */

double[,] define_cash_flows(double[,] matrixBonds)
{
    foreach (List<float> row in matrixBonds)
    {
        for (int n = 1; n < row[0] + 1 ; n++)
        {
            raise System.NotImplementedError;
        }
    }
}

class Program
{
    static void Main(string[] args)
    { 
        // write runner code here
    }
}