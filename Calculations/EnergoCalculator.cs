using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

///namespace Calculations

public class EnergoCalculator
{
    int[,] year_month_daysoff = new int[7, 13] { {2001, 10, 8,  9,  9,  8,  9,  9,  8, 10,  8,  8, 10 },
                                                 { 2002,  8, 8, 10,  8,  8, 10,  8,  9,  9,  8,  9,  9},
                                                 { 2003,  8, 8, 10,  8,  9,  9,  8, 10,  8,  8, 10,  8 },
                                                 { 2004,  9, 9,  8,  8, 10,  8,  9,  9,  8, 10,  8,  8 },
                                                 { 2005, 10, 8,  8,  9,  9,  8, 10,  8,  8, 10,  8,  9},
                                                 { 2006,  9, 8,  8, 10,  8,  8, 10,  8,  9,  9,  8, 10},
                                                 { 2007,  8, 8,  9,  9,  8,  9,  9,  8, 10,  8,  8, 10} };
    int count_years = 7;
    //calculation of ОСЕрік код 215
    //monthscosts масив куда помесячно вводятся суммы употребленной среднесуточной электроэнергии по месяцам 
    //sum это цифра за год среднемесячная
    public float sum_months(float[] monthscosts)//вводяться помісячно витрати за кожен місяць і рахується сумма
    {
        float sum = 0;
        foreach (float m in monthscosts)
        {
            sum += m;
        }
        return sum;
    }

    public float costs_mid_year(float[] monthscosts, int year, int work_type)//вводяться помісячно витрати за кожен місяць і рахується сумма
    {//work_type 0 - 7/7 all days in year, 1 - 5/7 work with daysoff

        float count_day = 0;
        for (int i = 0; i < count_years; i++)
        {
            if (year_month_daysoff[i, 0] == year)
            {
                for (int j = 1; j <= 12; j++)
                    if (monthscosts[j - 1] > 0)
                        count_day += DateTime.DaysInMonth(year, j) - year_month_daysoff[i, j] * work_type;
                float sum = sum_months(monthscosts);
                return sum / count_day;
            }
        }
        return -1;
    }

    public float[] mid_months(float[] monthscosts, int year, int work_type)//вводяться помісячно витрати за кожен місяць і повертає масив середньомісячних значень
    {//work_type 0 - 7/7 all days in year, 1 - 5/7 work with daysoff

        float[] res = new float[12];
        float count_day = 0;
        for (int i = 0; i < count_years; i++)
        {
            if (year_month_daysoff[i, 0] == year)
            {
                for (int j = 1; j <= 12; j++)
                {
                    if (monthscosts[j - 1] > 0)
                        count_day = DateTime.DaysInMonth(year, j) - year_month_daysoff[i, j] * work_type;
                    res[j - 1] = monthscosts[j - 1] / count_day;
                }
            }            
        }
        return res;
    }

}

