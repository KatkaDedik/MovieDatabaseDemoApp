using MovieApp.Domain.Extensions;
using Xunit;

public class AgeTest
{
    [Fact]
    public async Task GetAge_ReturnCorrectAge()
    {
        //I am not sure how to test this automaticaly. So until my birthday this test should pass
        var birthDate = new DateOnly(1998, 2, 20);
        var today = DateOnly.FromDateTime(DateTime.Today);

        var expectedAge = 27;

        var result = birthDate.GetAge();
        Assert.Equal(expectedAge, result);
    }
}
