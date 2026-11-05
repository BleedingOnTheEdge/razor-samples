using Samples.Plugins.Adapters.MT5;
using Sdk.Shared;
using Xunit;

namespace Samples.UnitTests;

public class Mt5MarketCalculatorTests
{
    private readonly Mt5MarketCalculator _calculator = new();

    private static SymbolProperties CreateProps(
        double minVol = 0.01,
        double tickSize = 0.0001,
        double contractSize = 100000.0,
        double takerFee = 0.0002)
    {
        return new SymbolProperties
        {
            AssetClass = AssetClass.Forex,
            MarginMode = MarginMode.Cross,
            PendingTrigger = PendingOrderTriggerMode.UseAskForBuy,
            MarginCurrency = "USD",
            ContractSize = contractSize,
            TickSize = tickSize,
            TickValue = 1.0,
            MinVolume = minVol,
            MaxLeverage = 100.0,
            SwapLong = -1.5,
            SwapShort = 0.5,
            SwapRolloverHourUtc = 0,
            TripleSwapDayMultiplier = 3,
            FundingRate = 0.0,
            HoldingCostIntervalTicks = TimeSpan.TicksPerDay,
            InitialMarginRate = 1.0,
            MaintenanceMarginRate = 0.5,
            MakerFeeRate = 0.0001,
            TakerFeeRate = takerFee
        };
    }

    [Fact]
    public void NormalizeVolume_RoundsToNearestLotStep()
    {
        var props = CreateProps(minVol: 0.01);
        double normalized = _calculator.NormalizeVolume(props, 0.014);
        Assert.Equal(0.01, normalized);

        double normalizedUp = _calculator.NormalizeVolume(props, 0.016);
        Assert.Equal(0.02, normalizedUp);
    }

    [Fact]
    public void NormalizePrice_RoundsToTickSize()
    {
        var props = CreateProps(tickSize: 0.0001);
        double normalized = _calculator.NormalizePrice(props, 1.08543);
        Assert.Equal(1.0854, normalized, precision: 4);
    }

    [Fact]
    public void CalculateRequiredMargin_CalculatesExpectedValue()
    {
        var props = CreateProps(contractSize: 100000.0);
        // volume = 1.0 lot, price = 1.1000, leverage = 100
        double margin = _calculator.CalculateRequiredMargin(props, 1.1000, 1.0, 100.0);
        // 1.0 * 100000 * 1.10 / 100 = 1100.0
        Assert.Equal(1100.0, margin, precision: 2);
    }

    [Fact]
    public void CalculatePnL_BuyAndSellCalculations_AreAccurate()
    {
        var props = CreateProps(contractSize: 100000.0);

        // Buy: Entry 1.1000, Current 1.1050 (+50 pips), volume 1.0
        double buyPnl = _calculator.CalculatePnL(props, 1.1000, 1.1050, 1.0, OrderType.Buy);
        Assert.Equal(500.0, buyPnl, precision: 2);

        // Sell: Entry 1.1000, Current 1.1050 (-50 pips), volume 1.0
        double sellPnl = _calculator.CalculatePnL(props, 1.1000, 1.1050, 1.0, OrderType.Sell);
        Assert.Equal(-500.0, sellPnl, precision: 2);
    }
}
