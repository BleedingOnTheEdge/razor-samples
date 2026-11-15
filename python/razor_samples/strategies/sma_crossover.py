"""Sample Simple Moving Average (SMA) crossover strategy in Python for Razor."""

from typing import List

class SmaCrossoverStrategy:
    """Sample strategy demonstrating how Python strategies will interact with Razor SDK."""

    def __init__(self, fast_period: int = 10, slow_period: int = 30):
        if fast_period <= 0 or slow_period <= 0:
            raise ValueError("SMA periods must be positive integers.")
        if fast_period >= slow_period:
            raise ValueError(f"fast_period ({fast_period}) must be strictly less than slow_period ({slow_period}).")
        self.fast_period = fast_period
        self.slow_period = slow_period
        self.prices: List[float] = []

    def on_price_update(self, price: float) -> str:
        """Evaluates price and returns trading signal: BUY, SELL, or HOLD."""
        self.prices.append(price)
        if len(self.prices) > self.slow_period * 4:
            del self.prices[:-self.slow_period]

        if len(self.prices) < self.slow_period:
            return "HOLD"

        fast_sma = sum(self.prices[-self.fast_period:]) / self.fast_period
        slow_sma = sum(self.prices[-self.slow_period:]) / self.slow_period

        if fast_sma > slow_sma:
            return "BUY"
        elif fast_sma < slow_sma:
            return "SELL"
        return "HOLD"
