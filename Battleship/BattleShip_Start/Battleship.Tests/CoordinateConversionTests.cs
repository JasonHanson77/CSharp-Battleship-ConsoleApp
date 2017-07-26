using BattleShip.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Tests
{       [TestFixture]
    class CoordinateConversionTests
    {
        [TestCase("B1", ExpectedResult = 2)]
        [TestCase("A10", ExpectedResult = 1)]
        [TestCase("f6", ExpectedResult = 6)]
        public int canConvertUpperCase(string coordinate)
        {
            CoordConverter coordConverter = new CoordConverter(coordinate);

            return coordConverter.coord;

        }

        [TestCase("f7", ExpectedResult = 6)]
        [TestCase("c10", ExpectedResult = 3)]
        [TestCase("j3", ExpectedResult = 10)]
        public int canConvertLowerCase(string coordinate)
        {
            CoordConverter coordConverter = new CoordConverter(coordinate);

            return coordConverter.coord;

        }

        [TestCase("310", ExpectedResult = 0)]
        [TestCase("09", ExpectedResult = 0)]
        [TestCase("&4", ExpectedResult = 0)]
        public int firstCharNotALetter(string coordinate)
        {
            CoordConverter coordConverter = new CoordConverter(coordinate);

            return coordConverter.coord;

        }

        [TestCase("", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        public int noEmptyString(string coordinate)
        {
            CoordConverter coordConverter = new CoordConverter(coordinate);

            return coordConverter.coord;

        }

    }
}
