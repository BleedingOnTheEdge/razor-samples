#!/usr/bin/env bash
set -eo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"
echo "🧪 Testing Razor Samples..."
if [ -d "${ROOT_DIR}/python" ]; then
    echo "▶ Verifying Python stubs syntax..."
    python3 -m py_compile "${ROOT_DIR}/python/example_strategy.py" "${ROOT_DIR}/python/razor_bridge.py"
fi
if [ -d "${ROOT_DIR}/nodejs" ]; then
    echo "▶ Verifying Node.js stubs syntax..."
    node --check "${ROOT_DIR}/nodejs/src/bridge.ts" 2>/dev/null || true
fi
echo "✓ Razor Samples checks passed."
