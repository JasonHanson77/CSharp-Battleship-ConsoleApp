using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
   public class CoordConverter
    {
        public int coord { get; set; }
        
        public CoordConverter (string coordinate)
        {
            if (String.IsNullOrWhiteSpace(coordinate))
            {
                this.coord = 0;
            }
            else
            {
                switch (coordinate[0].ToString().ToUpper())
                {
                    case "A":
                        coord = 1;
                        break;
                    case "B":
                        coord = 2;
                        break;
                    case "C":
                        coord = 3;
                        break;
                    case "D":
                        coord = 4;
                        break;
                    case "E":
                        coord = 5;
                        break;
                    case "F":
                        coord = 6;
                        break;
                    case "G":
                        coord = 7;
                        break;
                    case "H":
                        coord = 8;
                        break;
                    case "I":
                        coord = 9;
                        break;
                    case "J":
                        coord = 10;
                        break;
                    default:
                        coord = 0;
                        break;

                }
            }
            
        }
    }
}
