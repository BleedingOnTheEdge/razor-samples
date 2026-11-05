import unittest
import sys
import os

# Add python directory to path so razor_samples package is importable
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), "..")))

from razor_samples.adapters.sample_broker import SampleBrokerAdapter, MarketTick
from razor_samples.strategies.sma_crossover import SmaCrossoverStrategy

class TestPythonSamples(unittest.TestCase):
    def test_sample_broker_adapter_connect_and_tick(self):
        adapter = SampleBrokerAdapter("TestBroker")
        self.assertEqual(adapter.broker_name, "TestBroker")
        self.assertFalse(adapter.is_connected)

        connected = adapter.connect({"api_key": "test_key"})
        self.assertTrue(connected)
        self.assertTrue(adapter.is_connected)

        tick = MarketTick(symbol="EURUSD", bid=1.1000, ask=1.1002, timestamp="2026-11-05T00:00:00Z")
        adapter.on_tick(tick)

    def test_sma_crossover_strategy_signals(self):
        strategy = SmaCrossoverStrategy(fast_period=3, slow_period=5)

        # First 4 prices should hold
        for p in [10.0, 10.0, 10.0, 10.0]:
            self.assertEqual(strategy.on_price_update(p), "HOLD")

        # 5th price: stable
        self.assertEqual(strategy.on_price_update(10.0), "HOLD")

        # Bullish spike: fast SMA > slow SMA -> BUY
        signal = strategy.on_price_update(15.0)
        self.assertEqual(signal, "BUY")

if __name__ == "__main__":
    unittest.main()
