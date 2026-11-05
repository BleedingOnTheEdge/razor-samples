---
id: repo:razor-samples/agent
scope: razor-samples
level: repo
kind: agent
requires_org_docs: true
org_docs_fallback: notify
skill: docs
excluded_paths: [.git/, node_modules/, dist/, bin/, obj/, TestResults/, __pycache__/]
included_paths: []
---

# Razor Samples — Repository Agent Manifest

This repository contains official community sample plugins, custom broker adapters, and trading strategies across multiple runtimes (.NET, Python, Node.js/TypeScript) for the **Razor** algorithmic trading platform.

---

## 1. Architectural Profiles
- **`dotnet`**: Profile D (Community Samples & Extension Plugins)
- **`python`**: Profile D (Python Adapter & Strategy Prototypes)
- **`nodejs`**: Profile D (TypeScript Adapter & Strategy Prototypes)

---

## 2. Local Execution Commands
- Build all samples: `bash scripts/build.sh`
- Test all samples: `bash scripts/test.sh`
- .NET Tests: `dotnet test dotnet/Samples.slnx`
- Python Tests: `python3 -m unittest discover -s python/tests`
- Node.js Typecheck: `cd nodejs && pnpm run build`

---

## 3. Tier 1 Governance Invariants
- All sample strategies and broker adapters must consume `@razor/sdk` or `BleedingOnTheEdge.Sdk`.
- Zero broken imports or unresolved dependencies.
- Multi-tier branch promotion (`main`, `test`, `stage`, `prod`). Human-only production merge.
