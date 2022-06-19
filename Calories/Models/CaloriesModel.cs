using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calories.Models
{
    public class CaloriesModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Wybierz płeć")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Podaj swój wzrost")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Podaj swoją wagę")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Podaj swój wiek")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Wybierz swój poziom aktywności")]
        public string Activity { get; set; }

        [Required(ErrorMessage = "Określ swój cel")]
        public string Goal { get; set; }

        [Required(ErrorMessage = "Wybierz swój somatotyp")]
        public string Somatotype { get; set; }

        public string Date { get; set; }

        public int CalorieRequirement { get; set; }

        public float calculate(string gender, float weight, int height, int age, string activity, string goal, string somatotype)
        {
            float a = genderAlg(gender, weight, height, age);
            float b = activityAlg(activity);
            float c = a * b;
            float goalValue = 0;

            if (goal == "loss")
            {
                switch (somatotype)
                {
                    case "ectomorph":
                        goalValue = (float)0.1;
                        break;
                    case "mesomorph":
                        goalValue = (float)0.15;
                        break;
                    case "endomorph":
                        goalValue = (float)0.2;
                        break;
                }
                double d = c - (goalValue * c);
                return (float)d;
            } else if (goal == "build")
            {
                switch (somatotype)
                {
                    case "ectomorph":
                        goalValue = (float)0.2;
                        break;
                    case "mesomorph":
                        goalValue = (float)0.15;
                        break;
                    case "endomorph":
                        goalValue = (float)0.1;
                        break;
                }
                double d = c + (goalValue * c);
                return (float)d;
            } else
            {
                return c;
            }
        }

        public float genderAlg(string gender, float weight, int height, int age)
        {
            if (gender == "man")
            {
                double result = 66.5 + (13.7 * weight) + (5 * height) - (6.8 * age);
                return (float)result;
            }
            else
            {
                double result = 655 + (9.6 * weight) + (1.85 * height) - (4.7 * age);
                return (float)result;
            }
        }

        public float activityAlg(string activity)
        {
            switch (activity)
            {
                case "bMala":
                    return (float)1.2;
                    break;
                case "Mala":
                    return (float)1.4;
                    break;
                case "Srednia":
                    return (float)1.6;
                    break;
                case "Wyskoa":
                    return (float)1.8;
                    break;
                case "bWyskoa":
                    return (float)2.0;
                    break;
                default:
                    return (float)1.0;
                    break;
            }
        }
    }
}
