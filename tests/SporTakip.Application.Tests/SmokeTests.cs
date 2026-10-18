using FluentAssertions;
using SporTakip.Application.Validation;
using Xunit;

namespace SporTakip.Application.Tests;

public class SmokeTests
{
    [Fact]
    public void Application_derlemesi_yuklenir()
    {
        typeof(ValidationResult).Assembly.Should().NotBeNull();
    }
}
