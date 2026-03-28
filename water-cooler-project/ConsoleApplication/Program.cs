using System;
using System.Reflection.Emit;

public class SettingValues
{
    public decimal setMoneyStart = 100M;
    public double setWaterGallonLiters = 18.9;
    public double setWaterGallonMiliLiters;
    public decimal setMoneyUse;
    public double setWaterLeft;
    public SettingValues()
    {
       setWaterGallonMiliLiters = setWaterGallonLiters * 1000;
       
    }
}
public class WaterCuler : SettingValues
{
    static void Main()
    {
        CoolWater();
    }
    public static void CoolWater()
    {
        try
        {
            SettingValues settingValues = new SettingValues();
            TypeOfWater typeOfWater = new TypeOfWater();

            decimal getMoneyStart = settingValues.setMoneyStart;
            double getWaterGallonMiliLiters = settingValues.setWaterGallonMiliLiters;
            double waterLeft = getWaterGallonMiliLiters;

            string? userInput, continueBuy;
            bool continueShopping = true;

            typeOfWater.SetInitialValues(getMoneyStart, waterLeft);
            
            Console.WriteLine("Welcome to the Water Cooler!");
            Console.WriteLine($"You have {getMoneyStart} dollars to spend on cooling water.");
            Console.WriteLine($"Gallon is full ({getWaterGallonMiliLiters} milliliters) of water.");

            Dictionary<string, Action> waterOptions = new Dictionary<string, Action>()
            {
              { "soda water", () => typeOfWater.SodaWater()},
              { "apple flavor water", () => typeOfWater.AppleFlavor()},
              { "tonic water", () => typeOfWater.TonicWater()},
            };

            do
            {
                Console.WriteLine($"The 0.3 soda water will cost 0.99c\nThe 0.5 apple flavor water will cost 1.99c\nThe 1 tonic water will cost 4.99c");
                Console.WriteLine($"What you will choose?");
                Console.WriteLine();
                userInput = Console.ReadLine() ?? string.Empty;


                if (getMoneyStart <= 0)
                {
                    Console.WriteLine("You have no more money left to spend. Thank you for using the Water Cooler!");
                    break;
                }
                else
                {
                    waterOptions.TryGetValue(userInput, out var water);
                    water = waterOptions[userInput];
                    water.Invoke();

                }


                Console.WriteLine("Do you want to continue shopping? (yes/no)\n");

                continueBuy = Console.ReadLine() ?? string.Empty;

                continueBuy = continueBuy.ToLower();

                Console.WriteLine();

                if (continueBuy == "yes" || continueBuy == "y") continueShopping = true;
                else if (continueBuy == "no" || continueBuy == "n") continueShopping = false;
                else
                {
                    Console.Error.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                    break;
                }


            } while (continueShopping);

        }
        catch (Exception e)
        {
            Console.WriteLine("Error:" + e.Message);

        }
        

    }



}

public class TypeOfWater : SettingValues
{
    private double _waterLeft;
    private decimal _moneyStart;

    public void SetInitialValues(decimal money, double water)
    {
        _moneyStart = money;
        _waterLeft = water;
    }

    public void SodaWater()
    {
        if (_waterLeft < 300) Console.WriteLine("Not enough water left!");
        else
        {
            _waterLeft -= 300;
            _moneyStart -= 0.99M;
            Console.WriteLine($"You have {_moneyStart} dollars and {_waterLeft} mL left.");
        }
    }
    public void AppleFlavor()
    {
        if (_waterLeft < 500) Console.WriteLine("Not enough water left!");
        else
        {
            _waterLeft -= 500;
            _moneyStart -= 1.99M;
            Console.WriteLine($"You have {_moneyStart} dollars and {_waterLeft} mL left.");
        }
    }
    public void TonicWater()
    {
        if (_waterLeft < 1000) Console.WriteLine("Not enough water left!");
        else
        {
            _waterLeft -= 1000;
            _moneyStart -= 4.99M;
            Console.WriteLine($"You have {_moneyStart} dollars and {_waterLeft} mL left.");
        }
    }

    public double GetWaterLeft() => _waterLeft;
    public decimal GetMoneyStart() => _moneyStart;

}

