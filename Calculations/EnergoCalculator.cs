using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///namespace Calculations

public class EnergoCalculator
{
    //calculation of ОСЕрік код 215
    //monthscosts масив куда помесячно вводятся суммы употребленной среднесуточной электроэнергии по месяцам 
    //sum это цифра за год среднемесячная
    public float f(float[] monthscosts)//вводяться помісячно витрати за кожен місяць і рахується сумма
    {
        float sum = 0;
        foreach (float m in monthscosts)
        {
            sum += m;
        }
        return sum;
    }
}

