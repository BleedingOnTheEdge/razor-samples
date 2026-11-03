export interface MarketTick {
  symbol: string;
  bid: number;
  ask: number;
  timestamp: string;
}

export class SampleBrokerAdapter {
  readonly brokerName: string;
  private isConnected = false;

  constructor(brokerName = "MockNodeBroker") {
    this.brokerName = brokerName;
  }

  public async connect(): Promise<boolean> {
    this.isConnected = true;
    return true;
  }

  public onTick(tick: MarketTick): void {
    // Process incoming tick
  }
}
