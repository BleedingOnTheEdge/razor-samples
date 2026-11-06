# Razor Samples & Community Solutions (`BleedingOnTheEdge/razor-samples`)

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Python](https://img.shields.io/badge/Python-3.11+-yellow.svg)](https://www.python.org/)
[![Node.js](https://img.shields.io/badge/Node.js-20+-green.svg)](https://nodejs.org/)
[![GitHub Org](https://img.shields.io/badge/Org-BleedingOnTheEdge-blue)](https://github.com/BleedingOnTheEdge)

Official reference solutions, broker adapters, trading strategies, and polyglot bridge templates demonstrating how to build extensions for the **Razor** algorithmic trading platform.

---

## 🏛 Organization Context & Cross-Repo Topology

This repository is part of the **BleedingOnTheEdge** ecosystem:

- **Orchestrator**: [`BleedingOnTheEdge/razor`](https://github.com/BleedingOnTheEdge/razor) (Central scripts, organization bootstrap, and cross-repo coordination)
- **Extension SDK**: [`BleedingOnTheEdge/razor-sdk`](https://github.com/BleedingOnTheEdge/razor-sdk) (Public contracts and interfaces implemented by these samples)
- **Engine Runtime**: [`BleedingOnTheEdge/razor-engine`](https://github.com/BleedingOnTheEdge/razor-engine) (Execution nodes that load and run compiled extension assemblies)
- **Control Panel**: [`BleedingOnTheEdge/razor-panel`](https://github.com/BleedingOnTheEdge/razor-panel) (SaaS control plane & marketplace where extensions are listed)
- **Architecture Blueprints**: [`BleedingOnTheEdge/razor-docs`](https://github.com/BleedingOnTheEdge/razor-docs) (Technical architecture documents, contract specs, and developer guides)

---

## 📁 Directory Structure & Polyglot Support

This repository is organized by language ecosystem to support native .NET extensions today and upcoming polyglot bridges:

### 1. `.NET 10` Solutions (`dotnet/`)
Production-ready extensions implementing Razor SDK interfaces (`BleedingOnTheEdge.Razor.Sdk`):
- **`Plugins/Adapters/MT5`**: MetaTrader 5 broker integration adapter supporting market data feeds and order placement.
- **`Plugins/Adapters/Nobitex`**: Nobitex cryptocurrency exchange adapter supporting real-time crypto execution.
- **`Plugins/Strategies/CfdSmtDivergence`**: SMT (Smart Money Technique) divergence multi-symbol strategy with integrated neural network signal filtering.
- **`tests/Samples.UnitTests`**: Comprehensive unit tests covering market calculators, order margin math, and neural network predictions.

### 2. `Python` Stubs & Templates (`python/`)
Scaffolded templates and IPC bridges for authoring hooks, indicators, and strategies in Python (3.11+):
- `razor_samples/adapters/sample_broker.py`: Sample broker adapter bridge for market ticks.
- `razor_samples/strategies/sma_crossover.py`: Sample SMA crossover strategy implementation.
- `tests/test_samples.py`: Python unit tests executing via standard `unittest`.

### 3. `Node.js / TypeScript` Stubs & Templates (`nodejs/`)
Scaffolded templates and IPC bridges for authoring hooks and strategies in TypeScript/JavaScript (Node 20+):
- `src/adapters/sampleBroker.ts`: TypeScript broker adapter interface and sample implementation.
- `src/strategies/rsiStrategy.ts`: TypeScript RSI trading strategy stub.

---

## 💻 Getting Started

### Building and Testing .NET Samples
Prerequisites: [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

```bash
cd dotnet
dotnet build Samples.slnx
dotnet test Samples.slnx
```

### Running Python Stubs & Tests
Prerequisites: [Python 3.11+](https://www.python.org/)

```bash
python3 -m unittest discover -s python/tests
```

### Building Node.js Stubs
Prerequisites: [Node.js 20+](https://nodejs.org/)

```bash
cd nodejs
pnpm install
pnpm run build
```

### Running All Autonomous Sample Tests
```bash
bash scripts/test.sh
```
pnpm run build
```

---

## 🤝 Contributing & License

Community contributions of new broker adapters, trading strategies, and language bridges are warmly welcomed!
Distributed under the **MIT License**. See `LICENSE` for details.
