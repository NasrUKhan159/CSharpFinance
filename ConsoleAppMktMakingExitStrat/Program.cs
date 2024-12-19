// C# code for Python implementation of the following paper's methodology:
// "Market-Maker Hard Exit Thresholds Strategy" by David Shelton, Carlos Veiga

using System;
using System.Math;
using StatisticFormula;
using System.Object.CenterSpace.NMath.Core.NormalDistribution;

// var dist = new NormalDistribution(a,b);
// double pdf = dist.PDF(c);
// double cdf = dist.CDF(c);

var dist = new NormalDistribution(nu * t, Math.Sqrt(t));
var posCdf = dist.CDF(Math.Abs(h));
var negCdf = dist.CDF(-Math.Abs(h));

private chi_minus_1(int h, float nu, int t)
{
    return (Math.Exp(h * nu) / Math.Abs(h)) * (
            Math.Exp(-Math.Abs(h) * nu) * (1 - posCdf) +
            Math.Exp(Math.Abs(h) * nu) * negCdf);
}

private chi_0(int h, float nu, int t)
{
    return (Math.Exp(h * nu) / nu) * (
    Math.Exp(-Math.Abs(h) * nu) * (1 - posCdf) -
    Math.Exp(Math.Abs(h) * nu) * negCdf);

}

private chi_1(int h, float nu, int t)
{
    return (Math.Exp(h * nu) / nu * *3) * (
    (1 + Math.Abs(h) * nu) * Math.Exp(-Math.Abs(h) * nu) *
    (1 - posCdf) +
    (Math.Abs(h) * nu - 1) * Math.Exp(Math.Abs(h) * nu) *
    negCdf) - (2 * t / nu * *2) * dist.PDF(h);
}

private chi_2(int h, float nu, int t)
{
    return (exp(h * nu) / nu * *5) * (
    (3 * (1 + abs(h) * nu) + (abs(h) * nu) * *2) *
    exp(-abs(h) * nu) *
    (1 - posCdf) +
    (3 * (abs(h) * nu - 1) - (abs(h) * nu) * *2) *
    exp(abs(h) * nu) * negCdf) - (2 * t / nu * *4) * (3 + t * nu * *2) * dist.PDF(h);
}

private chi(int k, int h, float nu, int t)
{
    switch (k):
        case -1:
            return chi_minus_1(h, nu, t);
        case 0:
            return chi_0(h, nu, t);
        case 1:
            return chi_1(h, nu, t);
        case 2:
            return chi_2(h, nu, t);
        default:
            throw new ArgumentException(String.Format("Chi not defined for k = {0}", k), "k");

}

private psi(int k, int h, float mu, float lambda_, int t)
{
    nu = Math.Sqrt(2 * lambda_ + mu * *2)
    return Math.Exp(h * (mu - nu)) * (
    (h / 2) * chi(k - 1, h, nu, t) +
    (mu / 2) * chi(k, h, nu, t)
    );
}

private varphi(int k, int h, float mu, float lambda_, int t)
{
    if k == -1:
        return max(0, sign(h));
    return (
    max(1, k) * varphi(k - 1, h, mu, lambda_, t) -
    exp(-lambda_ * t) * dist.CDF(h) * t * *k - psi(k, h, mu, lambda_, t)) / lambda_;
}

private theta(int x, int y, float mu, int t)
{
    return (
    (y + mu * t) * dist.CDF(x - y) -
    t * dist.PDF(x - y)
    );
}

private varrho(int x, int y, float mu, float lambda_, int t)
{
    return (
    (y + mu * t) * theta(x, y, mu, lambda_, t) +
    t * (dist.CDF(x - y) - x * dist.PDF(x - y)));
}

private sigma(int x, int y, float mu, float lambda_, int t)
{
    nu = Math.Sqrt(2 * lambda_ + mu * *2);
    return (
        y * varphi(0, x - y, mu, lambda_, t) +
        mu * varphi(1, x - y, mu, lambda_, t) -
        Math.Exp((x - y) * (mu - nu)) * chi(1, x - y, nu, t)
        )
}

private xi(int x, int y, float mu, float lambda_, int t)
{
    nu = Math.Sqrt(2 * lambda_ + mu * *2);
    return (
    y * *2 * varphi(0, x - y, mu, lambda_, t) +
    (2 * y * mu + 1) * varphi(1, x - y, mu, lambda_, t) +
    mu * *2 * varphi(2, x - y, mu, lambda_, t) -
    Math.Exp((x - y) * (mu - nu)) * (
    mu * chi(2, x - y, nu, t) + (x + y) * chi(1, x - y, nu, t)));
}

private pi(var f, int a, int b, float mu, int n_start, int n_end)
{
    float result = 0;
    for (int n = n_start; n_end + 1; n++)
    {
        result += (
        Math.Exp(2 * n * (b - a) * mu) * (
        f(b, 2 * n * (b - a)) -
        f(a, 2 * n * (b - a))
        ) -
        Math.Exp((2 * a + 2 * n * (b - a)) * mu) * (
        f(b, 2 * a + 2 * n * (b - a)) -
        f(a, 2 * a + 2 * n * (b - a))
        ));
    }
    return result;
}

private prob_a_or_b(float mu, float lambda_, int t)
{
    return psi(0, x - y, mu, lambda_, t);
}

private time_a_or_b(float mu, float lambda_, int t)
{
    return psi(1, x - y, mu, lambda_, t);
}

private time2_a_or_b(float mu, float lambda_, int t)
{
    return psi(2, x - y, mu, lambda_, t);
}

private prob_c(float mu, float lambda_, int t)
{
    returnn lambda_ *varphi(0, x - y, mu, lambda_, t);
}

private time_c(float mu, float lambda_, int t)
{
    return lambda_ * varphi(1, x - y, mu, lambda_, t);
}

private time2_c(float mu, float lambda_, int t)
{
    return lambda_* varphi(2 , x - y , mu , lambda_ , t);
}

private value_c(float mu, float lambda_, int t)
{
    return lambda_ * sigma(x, y, mu, lambda_, t);
}

private value2_c(float mu, float lambda_, int t)
{
    return lambda_ * xi(x, y, mu, lambda_, t);
}

private prob_d(float mu, float lambda_, int t)
{
    return Math.Exp(-lambda_ * t) * dist.CDF(x - y);
}

private time_d(float mu, float lambda_, int t)
{
    return prob_d(mu, lambda_, t)(x, y) * t;
}

private time2_d(float mu, float lambda_, int t)
{
    return prob_d(mu, lambda_, t)(x, y) * t * *2;
}

private value_d(float mu, float lambda_, int t)
{
    return Math.Exp(-lambda_ * t) * theta(x, y, mu, lambda_, t);
}

private value2_d(float mu, float lambda_, int t)
{
    return Math.Exp(-lambda_ * t) * varrho(x, y, mu, lambda_, t);
}

private time(float mu, float lambda_, int t)
{
    return time_a_or_b(mu, lambda_, t)(x, y) +
    time_c(mu, lambda_, t)(x, y) +
    time_d(mu, lambda_, t)(x, y);
}

public prob_b_given_tau(int a, int b, float mu, float lambda_, int t, int n_start, int n_end)
{
    return (
    mu * pi(time(mu, lambda_, t), a, b, mu, n_start, n_end) -
    pi(value_d(mu, lambda_, t), a, b, mu, n_start, n_end) -
    pi(value_c(mu, lambda_, t), a, b, mu, n_start, n_end)
    ) / (
    pi(prob_a_or_b(mu, lambda_, t), a, b, mu, n_start, n_end) *
    (b - a)
    ) - a / (b - a);
}

class Program
{
    double a = -0.33;
    double b = 0.33;
    double mu = -0.2;
    double lambda_ = 4.5;
    double t = 0.1;
    double sig = 0.2;
    float p = prob_b_given_tau(a, b, mu, lambda_, t, -1, 1)
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