#!/usr/bin/env bash
set -eo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

echo "🔨 Building Razor Samples (.NET)..."
dotnet build "${ROOT_DIR}/dotnet/Samples.slnx" -c "${1:-Release}"

if [ -d "${ROOT_DIR}/nodejs" ]; then
    echo "🔨 Typechecking Razor Samples (Node.js/TypeScript)..."
    (cd "${ROOT_DIR}/nodejs" && pnpm run build)
fi

echo "✓ Razor Samples build succeeded."
