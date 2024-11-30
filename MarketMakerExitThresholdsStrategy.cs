// C# code for Python implementation of the following paper's methodology:
// "Market-Maker Hard Exit Thresholds Strategy" by David Shelton, Carlos Veiga

using System;
using System.Math;

private chi_minus_1(int h, float nu, int t)
{
    return (Math.Exp(h * nu) / Math.Abs(h)) * (
            Math.Exp(-Math.Abs(h) * nu) * (1 - norm(nu * t, sqrt(t)).cdf(Math.Abs(h))) +
            Math.Exp(Math.Abs(h) * nu) * norm(nu * t, Math.Sqrt(t)).cdf(-Math.Abs(h))
            )
}

private chi_0(int h, float nu, int t)
{
    raise System.NotImplementedException;
}

private chi_1(int h, float nu, int t)
{
    raise System.NotImplementedException;
}

private chi_2(int h, float nu, int t)
{
    raise System.NotImplementedException;
}

private chi(int k, int h, float nu, int t)
{
    raise System.NotImplementedException;
}

private psi(int k, int h, float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private varphi(int k, int h, float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private theta(int x, int y, float mu, int t)
{
    raise System.NotImplementedException;
}

private varrho(int x, int y, float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private sigma(int x, int y, float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private xi(int x, int y, float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private pi(var f, int a, int b, float mu, int n_start, int n_end)
{
    raise System.NotImplementedException;
}

private prob_a_or_b(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time_a_or_b(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time2_a_or_b(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private prob_c(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time_c(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time2_c(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private value_c(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private value2_c(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private prob_d(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time_d(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time2_d(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private value_d(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private value2_d(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

private time(float mu, float lambda_, int t)
{
    raise System.NotImplementedException;
}

public prob_b_given_tau(int a, int b, float lambda_, int t, float mu, int n_start, int n_end)
{
    raise System.NotImplementedException;
}

class Program
{
    double a = -0.33;
    double b = 0.33;
    double mu = -0.2;
    double lambda_ = 4.5;
    double t = 0.1;
    double sig = 0.2;
    // float p = prob_b_given_tau(a, b, mu, lambda_, t, -1, 1)
    Console.WriteLine($"Prob(A U B): {pi(prob_a_or_b(mu,lambda_,t), a, b, mu , -1, 1)}");
    Console.WriteLine($"E[1_(A U B)tau]: {pi(time_a_or_b(mu,lambda_,t), a, b, mu , -1, 1) / sig.pow(2)}");
    Console.WriteLine($"E[1_(A U B)tau**2]: {pi(time2_a_or_b(mu,lambda_,t), a, b, mu , -1, 1) / sig.pow(4)}");
    Console.WriteLine($"E[1_(A U B)W(tau)]: {pi(prob_a_or_b(mu,lambda_,t), a, b, mu , -1, 1) * (p * b + (1 - p) * a)}");
    Console.WriteLine($"E[1_(A U B)W(tau)**2]: {pi(prob_a_or_b(mu,lambda_,t), a, b, mu , -1, 1) * (p * b**2 + (1 - p) * a**2)}");
    Console.WriteLine($"Prob(C): {pi(prob_c(mu,lambda_,t), a, b, mu , -1, 1)}");
    Console.WriteLine($"E[1_(C) upsilon]: {pi(time_c(mu,lambda_,t), a, b, mu , -1, 1) / sig**2}");
    Console.WriteLine($"E[1_(C) upsilon**2]: {pi(time2_c(mu,lambda_,t), a, b, mu , -1, 1) / sig**4}");
    Console.WriteLine($"E[1_(C) W(upsilon)]: {pi(value_c(mu,lambda_,t), a, b, mu , -1, 1)}");
    Console.WriteLine($"E[1_(C) W(upsilon)**2]: {pi(value2_c(mu,lambda_,t), a, b, mu , -1, 1)}");
    Console.WriteLine($"Prob(D): {pi(prob_d(mu,lambda_,t), a, b, mu , -1, 1)}");
    Console.WriteLine($"E[1_(D) T]: {pi(time_d(mu,lambda_,t), a, b, mu , -1, 1) / sig**2}");
    Console.WriteLine($"E[1_(D) T**2]: {pi(time2_d(mu,lambda_,t), a, b, mu , -1, 1) / sig**4}");
    Console.WriteLine($"E[1_(D) W(T)]: {pi(value_d(mu,lambda_,t), a, b, mu , -1, 1) / sig**2}");
    Console.WriteLine($"E[1_(D) W(T)**2]: {pi(value2_d(mu,lambda_,t), a, b, mu , -1, 1) / sig**4}");
}