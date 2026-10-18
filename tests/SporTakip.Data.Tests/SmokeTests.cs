using FluentAssertions;
using SporTakip.Data;
using Xunit;

namespace SporTakip.Data.Tests;

public class SmokeTests
{
    [Fact]
    public void Data_derlemesi_yuklenir()
    {
        typeof(SqlConnectionFactory).Assembly.Should().NotBeNull();
    }
}
