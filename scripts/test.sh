#!/usr/bin/env bash
set -eo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

echo "🧪 Testing Razor Samples..."

# 1. Test .NET Samples
echo "▶ Running .NET sample unit tests..."
dotnet test "${ROOT_DIR}/dotnet/Samples.slnx" -c "${1:-Release}" --verbosity minimal

# 2. Test Python Samples
if [ -d "${ROOT_DIR}/python/tests" ]; then
    echo "▶ Running Python sample tests..."
    python3 -m unittest discover -s "${ROOT_DIR}/python/tests" -v
fi

# 3. Test Node.js / TypeScript Samples
if [ -d "${ROOT_DIR}/nodejs" ]; then
    echo "▶ Verifying Node.js / TypeScript samples..."
    (cd "${ROOT_DIR}/nodejs" && pnpm run build)
fi

echo "✓ All Razor Samples tests and verifications passed."
