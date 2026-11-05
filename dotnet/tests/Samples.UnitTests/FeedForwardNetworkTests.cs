using Samples.Plugins.Strategies.CfdSmtDivergence;
using Xunit;

namespace Samples.UnitTests;

public class FeedForwardNetworkTests
{
    [Fact]
    public void FeedForwardNetwork_InitializesWithSpecifiedLayers()
    {
        var layerSizes = new[] { 4, 8, 1 };
        var network = new FeedForwardNetwork(layerSizes, FeedForwardNetwork.ActivationFunction.Tanh);

        Assert.Equal("FeedForward", network.ModelType);
        Assert.Equal(4, network.InputSize);
        Assert.Equal(1, network.OutputSize);
        // Layer 0: 4 in, 8 out -> 32 weights + 8 biases = 40 params
        // Layer 1: 8 in, 1 out -> 8 weights + 1 bias = 9 params
        // Total = 49 params
        Assert.Equal(49, network.ParameterCount);
    }

    [Fact]
    public void FeedForwardNetwork_Predict_ProducesExpectedOutput()
    {
        var layerSizes = new[] { 4, 8, 1 };
        var network = new FeedForwardNetwork(layerSizes, FeedForwardNetwork.ActivationFunction.Tanh);
        var inputs = new double[] { 1.0, 0.5, -0.2, 0.8 };

        double[] output = network.Predict(inputs);

        Assert.NotNull(output);
        Assert.Single(output);
        // Tanh activation should produce output bounded in [-1.0, 1.0]
        Assert.InRange(output[0], -1.0, 1.0);
    }

    [Fact]
    public void FeedForwardNetwork_ThrowsOnMismatchedInputSize()
    {
        var layerSizes = new[] { 4, 8, 1 };
        var network = new FeedForwardNetwork(layerSizes);
        var wrongInputs = new double[] { 1.0, 2.0 };

        Assert.Throws<ArgumentException>(() => network.Predict(wrongInputs));
    }
}
