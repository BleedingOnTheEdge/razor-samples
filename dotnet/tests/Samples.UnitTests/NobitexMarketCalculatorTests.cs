using Samples.Plugins.Adapters.Nobitex;
using Sdk.Shared;
using Xunit;

namespace Samples.UnitTests;

public class NobitexMarketCalculatorTests
{
    private readonly NobitexMarketCalculator _calculator = new();

    private static SymbolProperties CreateProps(
        double minVol = 0.001,
        double tickSize = 1000.0,
        double contractSize = 1.0,
        double takerFee = 0.002)
    {
        return new SymbolProperties
        {
            AssetClass = AssetClass.CryptoSpot,
            MarginMode = MarginMode.Isolated,
            PendingTrigger = PendingOrderTriggerMode.UseAskForBuy,
            MarginCurrency = "IRT",
            ContractSize = contractSize,
            TickSize = tickSize,
            TickValue = 1.0,
            MinVolume = minVol,
            MaxLeverage = 1.0,
            SwapLong = 0.0,
            SwapShort = 0.0,
            SwapRolloverHourUtc = 0,
            TripleSwapDayMultiplier = 1,
            FundingRate = 0.0,
            HoldingCostIntervalTicks = TimeSpan.TicksPerDay,
            InitialMarginRate = 1.0,
            MaintenanceMarginRate = 1.0,
            MakerFeeRate = 0.001,
            TakerFeeRate = takerFee
        };
    }

    [Fact]
    public void NormalizeVolume_ClampsToMinVolume()
    {
        var props = CreateProps(minVol: 0.001);
        double normalized = _calculator.NormalizeVolume(props, 0.0005);
        Assert.Equal(0.001, normalized);
    }

    [Fact]
    public void NormalizePrice_SnapsToTickSize()
    {
        var props = CreateProps(tickSize: 1000.0);
        double normalized = _calculator.NormalizePrice(props, 5500450.0);
        Assert.Equal(5500000.0, normalized);
    }

    [Fact]
    public void CalculatePnL_CryptoSpotPnL_IsAccurate()
    {
        var props = CreateProps(contractSize: 1.0);
        // Buy 0.5 BTC at 50,000, price rises to 60,000 -> PnL = (60000 - 50000) * 1 * 0.5 = 5000
        double pnl = _calculator.CalculatePnL(props, 50000.0, 60000.0, 0.5, OrderType.Buy);
        Assert.Equal(5000.0, pnl, precision: 2);
    }
}
