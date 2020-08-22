using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    [Trait("Category", "Fixture")]

    public class GameStateShould :IClassFixture<GameStateFixture>

    {
        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;

        public GameStateShould(GameStateFixture gameStateFixture,ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;
            _output = output;
        }
        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(player1);
            _gameStateFixture.State.Players.Add(player2);

            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

            _gameStateFixture.State.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact]
        public void Reset()
        {
            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(player1);
            _gameStateFixture.State.Players.Add(player2);

            _gameStateFixture.State.Reset();

            Assert.Empty(_gameStateFixture.State.Players);
        }
    }

    //[Trait("Category", "Fixture")]
    //public class GameStateShould1
    //{
    //    private readonly ITestOutputHelper _output;

    //    public GameStateShould1(ITestOutputHelper output)
    //    {
    //        _output = output;
    //    }
    //    [Fact]
    //    public void DamageAllPlayersWhenEarthquake()
    //    {
    //        var sut = new GameState();
    //        _output.WriteLine($"GameState ID={sut.Id}");

    //        var player1 = new PlayerCharacter();
    //        var player2 = new PlayerCharacter();

    //        sut.Players.Add(player1);
    //        sut.Players.Add(player2);

    //        var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

    //        sut.Earthquake();

    //        Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
    //        Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
    //    }

    //    [Fact]
    //    public void Reset()
    //    {
    //        var sut = new GameState();
    //        _output.WriteLine($"GameState ID={sut.Id}");

    //        var player1 = new PlayerCharacter();
    //        var player2 = new PlayerCharacter();

    //        sut.Players.Add(player1);
    //        sut.Players.Add(player2);

    //        sut.Reset();

    //        Assert.Empty(sut.Players);
    //    }
    //}

}
