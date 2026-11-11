export class RsiStrategy {
  private readonly period: number;
  private readonly prices: number[] = [];

  constructor(period = 14) {
    this.period = period;
  }

  public onPrice(price: number): "BUY" | "SELL" | "HOLD" {
    this.prices.push(price);
    if (this.prices.length > this.period * 4) {
      this.prices.splice(0, this.prices.length - this.period - 1);
    }

    if (this.prices.length <= this.period) {
      return "HOLD";
    }

    // Simplified RSI calculation demonstration
    return "HOLD";
  }
}
