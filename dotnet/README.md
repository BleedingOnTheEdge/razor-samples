# Razor .NET Sample Plugins (`dotnet`)

This directory contains official reference plugins, custom broker adapters, and algorithmic trading strategies implemented in C# (.NET 10) targeting the [`BleedingOnTheEdge.Sdk`](https://github.com/BleedingOnTheEdge/razor-sdk).

---

## 📦 Projects Overview

### 1. Broker Adapters (`Plugins/Adapters/`)
- **[`MT5`](Plugins/Adapters/MT5/README.md)**: MetaTrader 5 broker adapter communicating via TCP socket to an MQL5 Expert Advisor (`Mql5BridgeEA.mq5`).
- **`Nobitex`**: Cryptocurrency exchange adapter for Nobitex spot market with custom market calculator and rate limiting.

### 2. Trading Strategies (`Plugins/Strategies/`)
- **`CfdSmtDivergence`**: Multi-asset Smart Money Technique (SMT) divergence strategy comparing correlated CFD instruments with session filters, dynamic volume sizing, and an optional feed-forward neural network filter.

### 3. Automated Unit Tests (`tests/Samples.UnitTests/`)
- Comprehensive test coverage verifying market calculators, tick value calculations, leverage modes, and neural network inference across sample adapters and strategies.

---

## 🛠 Building and Testing

```bash
# Build the .NET samples solution
dotnet build Samples.slnx -c Release

# Run automated unit tests
dotnet test Samples.slnx -c Release
```
