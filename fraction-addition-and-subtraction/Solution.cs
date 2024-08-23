using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fraction_addition_and_subtraction
{
    internal class Solution
    {
        public static string FractionAddition(string expression)
        {

            var z = expression.Split('+','-').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string[] signs = new string[z.Length];
            int count = 0;

            if (!expression[0].Equals('-'))
            {
                signs[0] = "+";
                count++;
            }

            for (int i = 0; i < expression.Length; i++)
            {
               
                if (expression[i].Equals('-')|| expression[i].Equals('+'))
                {
                    signs[count] = expression[i].ToString();
                    count++;
                }
            }

            int signCount = 0;
            for (int i = 0; i < z.Length; i++)
            {
                z[i] = signs[signCount] + z[i];
                signCount++;
            }

            double total = 0;
            double[] nominatores = new double[z.Length];
            double[] denominators = new double[z.Length];
            for (int i = 0; i < z.Length; i++)
            {
                var fin = z[i].Split('/');
                nominatores[i] = double.Parse(fin[0]);
                denominators[i] = double.Parse(fin[1]);
                
            }
            //check if all denominators are same
            //Find LCM
            double LCM = FindLCM(denominators);
            //increment up noms
            double final = 0;
            for (int i = 0; i < nominatores.Length; i++)
            {
                double muli =   LCM/ denominators[i];

                nominatores[i] = nominatores[i] * muli;
                final += nominatores[i];
            }

            if(final/LCM % 1 == 0)
            {
                return $"{final / LCM}/1";
            }

            int gdc = Simplify((double)final, (double)LCM);

            
            return $"{final / gdc}/{LCM / gdc}";
            
        }

        static int Simplify(double x, double y)
        {
            double gcd = GCD((double)x, (double)y); // Find the GCD of x and y

            // Divide both x and y by the GCD to simplify the fraction
            x /= gcd;
            y /= gcd;
            if(gcd>0)
            return (int)gcd;
            else return (int)Math.Sqrt(gcd * gcd);
        }

        static double GCD(double a, double b)
        {
            while (b != 0)
            {
                double temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Function to calculate LCM of two numbers
        static double LCM(double a, double b)
        {
            return (a / GCD(a, b)) * b;
        }

        // Function to calculate LCM of an array of numbers
        static double FindLCM(double[] denominators)
        {
            double lcm = denominators[0];
            for (int i = 1; i < denominators.Length; i++)
            {
                lcm = LCM(lcm, denominators[i]);
            }
            return lcm;
        }
    }
}
