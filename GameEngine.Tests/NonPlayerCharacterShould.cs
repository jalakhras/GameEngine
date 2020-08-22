using Xunit;

namespace GameEngine.Tests
{
    public class NonPlayerCharacterShould
    {
        [Theory]
        [MemberData(nameof(InternalHealthDamageTestData.TestData), MemberType = typeof(InternalHealthDamageTestData))]
        public void InternalTakeDamage(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }

        [Theory]
        [MemberData(nameof(ExternalHealthDamageTestData.TestData), MemberType = typeof(ExternalHealthDamageTestData))]
        public void ExtenalTakeDamage(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
        [Theory]
        [HealthDamageData]
        public void AttributeTakeDamage(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
    }
}
