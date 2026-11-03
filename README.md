# Razor Samples & Community Solutions

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Python](https://img.shields.io/badge/Python-3.11+-yellow.svg)](https://www.python.org/)
[![Node.js](https://img.shields.io/badge/Node.js-20+-green.svg)](https://nodejs.org/)

Reference solutions, adapters, and trading strategy implementations demonstrating how to build extensions for the **Razor** algorithmic trading platform.

---

## Directory Structure

This repository is organized by language ecosystem:

- **`dotnet/`**: Production-ready .NET 10 extensions implementing Razor SDK interfaces:
  - `Plugins/Adapters/MT5`: MetaTrader 5 broker adapter.
  - `Plugins/Adapters/Nobitex`: Nobitex cryptocurrency exchange adapter.
  - `Plugins/Strategies/CfdSmtDivergence`: SMT divergence multi-symbol trading strategy with neural network signal filtering.
- **`python/`**: Python stubs and templates for future polyglot hook/slot bridges.
- **`nodejs/`**: Node.js & TypeScript stubs and templates for future polyglot hook/slot bridges.

---

## Getting Started (.NET)

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Razor SDK (`BleedingOnTheEdge.Sdk`)

### Building the .NET Samples
```bash
cd dotnet
dotnet build Samples.slnx
```

---

## Contributing & License

Contributions of new adapters and strategies are welcome!
Distributed under the **MIT License**. See `LICENSE` for details.
