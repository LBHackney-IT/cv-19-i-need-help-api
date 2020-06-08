using System.Linq;
using AutoFixture;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelpTest.TestHelpers
{
    public static class CustomizeFixture
    {
        public static void V2ResidentResponseParsable(Fixture fixture)
        {
            var randomNumbers = fixture.CreateMany<int>(4).ToList();
            fixture.Customize<ResidentSupportAnnex>(
                ob => ob
                    .With(x => x.DobDay, "13")
                    .With(x => x.DobMonth, "11")
                    .With(x => x.DobYear, "1960")
                    .With(x => x.FormId, randomNumbers.ElementAt(0).ToString)
                    .With(x => x.NumberOfPeopleInHouse, randomNumbers.ElementAt(0).ToString)
                    .With(x => x.DaysWorthOfFood, randomNumbers.ElementAt(0).ToString)
                    .With(x => x.DaysWorthOfMedicine, randomNumbers.ElementAt(0).ToString)
            );
        }
    }
}