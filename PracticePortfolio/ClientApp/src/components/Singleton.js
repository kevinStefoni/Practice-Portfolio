import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class Singleton extends Component {
    render() {
        const codeSnippet = `using System;

class Stock
{
    public string Symbol { get; }
    public double Price { get; set; }

    public Stock(string symbol, double price)
    {
        Symbol = symbol;
        Price = price;
    }
}

class StockAnalyzer
{
    public void AnalyzeStock(Stock stock)
    {
        if (stock.Price > 100)
        {
            Console.WriteLine($"Buy {stock.Symbol} stock!");
        }
        else if (stock.Price < 50)
        {
            Console.WriteLine($"Sell {stock.Symbol} stock!");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stock stock = new Stock("AAPL", 120);
        StockAnalyzer analyzer = new StockAnalyzer();
        analyzer.AnalyzeStock(stock);
    }
}`;

        return <CodeSnippet codeSnippet={codeSnippet} />;
    }
}
