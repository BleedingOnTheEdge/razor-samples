# MetaTrader 5 (MT5) Broker Adapter Plugin

The **MetaTrader 5 (MT5) Broker Adapter** is a reference community plugin for the **Razor** algorithmic trading platform. It establishes a high-throughput, low-latency bridge between a running MetaTrader 5 terminal and the Razor execution engine.

---

## 🏛 Architecture

The adapter uses a decoupled TCP socket architecture to communicate with MT5 without requiring unmanaged DLL injection into the MetaTrader terminal process:

```
┌─────────────────────────────────┐           TCP Socket (Port 5555)           ┌─────────────────────────────┐
│       Razor Execution Engine    │ <────────────────────────────────────────> │      MetaTrader 5 Terminal  │
│  (Mt5Adapter / Mt5BridgeClient) │                                            │   (Mql5BridgeEA.mq5 Expert) │
└─────────────────────────────────┘                                            └─────────────────────────────┘
```

- **`Mql5BridgeEA.mq5`**: Lightweight Expert Advisor running on any chart in MT5. Uses WinAPI `winsock.mqh` to listen for incoming connections from Razor.
- **`Mt5Adapter.cs`**: Implements `IBrokerAdapter` from `BleedingOnTheEdge.Sdk`. Handles connection lifecycle, market data streaming, and order dispatch.
- **`Mt5BridgeClient.cs`**: High-performance socket client managing framed message serialization, command request/response, and heartbeat pings.
- **`Mt5MarketCalculator.cs`**: Implements margin calculations (Forex, CFD, Futures), pip values, and contract sizing.
- **`Mt5MessageProtocol.cs`**: Binary / framed text message protocol specification.

---

## 🚀 Setup & Installation

### Step 1: Install MQL5 Bridge in MetaTrader 5
1. Copy `winsock.mqh` into your MetaTrader 5 data directory:
   ```
   <MT5 Data Folder>\MQL5\Include\WinAPI\winsock.mqh
   ```
2. Copy `Mql5BridgeEA.mq5` into:
   ```
   <MT5 Data Folder>\MQL5\Experts\Mql5BridgeEA.mq5
   ```
3. Open MetaEditor in MT5 and compile `Mql5BridgeEA.mq5` to generate `Mql5BridgeEA.ex5`.
4. In MT5 Terminal settings:
   - Navigate to **Tools -> Options -> Expert Advisors**.
   - Check **Allow algorithmic trading**.
   - Check **Allow DLL imports** (required for `ws2_32.dll` socket calls).
5. Drag and drop `Mql5BridgeEA` onto any active chart. By default, it listens on port `5555`.

### Step 2: Configure Razor Engine Adapter
Reference the compiled `MT5.dll` extension in your Razor strategy or engine configuration:

```json
{
  "Adapter": {
    "TypeName": "Samples.Plugins.Adapters.MT5.Mt5Adapter",
    "Host": "127.0.0.1",
    "Port": 5555,
    "Symbols": ["EURUSD", "GBPUSD", "USDJPY"]
  }
}
```

---

## 🧪 Testing and Verification

Run the automated test suite in `dotnet/tests/Samples.UnitTests`:

```bash
dotnet test dotnet/tests/Samples.UnitTests/Samples.UnitTests.csproj --filter "FullyQualifiedName~Mt5"
```

---

## 📄 License
Licensed under the **MIT License**.
