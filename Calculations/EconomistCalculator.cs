using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EconomistCalculator
{
    private const double A = 0.5;

    public EconomistCalculator()
    {
    }

    ///<summary>Розмір шкоди від щабруднення земель</summary>
    public double Рш(double Гоз, double Пд, double Кз, double Кег)
    {
        return A * Гоз * Пд * Кз * Кег;
    }

    ///<summary>Екстернальні (зовнішні) витрати можуть бути оцінені у вартісній формі як сума пофакторних (Ciф) або пореципієнтних (Ciп) збитків</summary>
    public double Сіф(double Аф, double Вф, double Зф)
    {
        return Аф + Вф + Зф;
    }

    public double Сіп(double Нр, double Мр, double Рсг, double Ррг,
                      double Рлг, double Ррек, double Рпзф)
    {
        return Нр + Мр + Рсг + Ррг + Ррек + Рпзф;
    }

    ///<summary>Нормативно-правова оцінка збитку від втрати життя та здоров'я населення</summary>
    public double Нр(double Втрр, double Вдп, double Ввтг)
    {
        return Втрр + Вдп + Ввтг;
    }

    ///<summary>Втрати від вибуття трудових ресурсів з виробництва</summary>
    public double Втрр(double N, double Мл, double Мт, double Мі, double Мз)
    {
        return (Мл * N) + (Мт * N) + (Мі * N) + (Мз * N);
    }

    ///<summary>Витрати на виплату допомоги на поховання</summary>
    public double Вдп(double Мдп, double Nз)
    {
        return Мдп * Nз;
    }

    ///<summary>Витрати на виплату пенсій у разі втрати годувальника</summary>
    public double Втг(double Мвтг, double Вд)
    {
        return 12 * Мвтг * (18 - Вд);
    }

    ///<summary>Розрахунок збитків від руйнування та пошкодження основинх фондів, знищення майна та продукції</summary>
    public double Мр(double Фв, double Фг, double Пр,
        double Пр_с, double Сн, double Мдг)
    {
        return Фв + Фг + Пр + Пр_с + Сн + Мдг;
    }

    ///<summary>Збитки від руйнування та пошкодження основних фондів невиробничого призначення</summary>
    public double Фг(double n, double[] p, double[] k, double Лв)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < n; i++)
        {
            result.Add(((p[i] * k[i]) - Лв));
        }
        return result.Sum();
    }

    ///<summary>Розрахунок збитків від втрат готової промислової та сільськогосподарсьгої продукції</summary>
    public double Пр(double Прп, double Прс)
    {
        return Прп + Прс;
    }

    ///<summary>Збитки від втрат готової промислової продукції</summary>
    public double Прп(double n, double[] C, double[] q)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < n; i++)
        {
            result.Add((C[i] * q[i]));
        }
        return result.Sum();
    }

    ///<summary>Збитки від втрат готової сільськогосподарської продукції</summary>
    public double Прс(double n, double[] Ц, double[] q)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < n; i++)
        {
            result.Add((Ц[i] * q[i]));
        }
        return result.Sum();
    }

    ///<summary>Збитки від втрат незібраної сільськогосподарської продукції</summary>
    public double Пр_с(double m, double[] S, double[] k, double[] У, double[] Ц, double[] Здод)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < m; i++)
        {
            result.Add( (S[i]*k[i]*У[i]*Ц[i]*Здод[i]) );
        }
        return result.Sum();
    }

    ///<summary>Збитки від втрат сировини, матеріалів та напівфабрикатів проміжної продукції</summary>
    public double Сн(double m, double[] Ц, double[] q)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < m; i++)
        {
            result.Add( (Ц[i]*q[i]) );
        }
        return result.Sum();
    }


    public double Мдг(double m, double[] Р, double[] Ка, double[] к,
        double[] qорг, double[] Цср, double[] qгр)
    {
        List<double> left = new List<double>();
        List<double> right = new List<double>();
        for (int i = 0; i < m; i++)
        {
            left.Add((Р[i] * Ка[i] * к[i] * qорг[i]));
            right.Add((Цср[i] * qгр[i]));
        }
        return left.Sum() + right.Sum();
    }

    ///<summary>Розрахунок збитків від вилучення або порушення сільськогосподарських угідь та втрат твариництва</summary>
    public double Рсг(double Рсг1, double Рсг2)
    {
        return Рсг1 + Рсг2;
    }


    public double Рсг1(double Н,  double П)
    {
        return Н * П;
    }
    public double Рсг2(double k, double Н, double П)
    {
        return (1 - k) * Н * П;
    }

    ///<summary>Розмір збитків тис. грн.</summary>
    public double Мтв(double B, double N)
    {
        return B * N;
    }

    ///<summary>Розрахунок збитків від втрати деревини та інших лісових ресурсів</summary>
    public double Рлг(double Рлг1, double Рлг2, double Рлг3)
    {
        return Рлг1 + Рлг2 + Рлг3;
    }

    ///<summary>Розмір збитків тис. грн.</summary>
    public double Рлг1(double H, double K, double П)
    {
        return H * K * П;
    }

    ///<summary>Розмір збитків тис. грн.</summary>
    public double Рлг2(double k, double H, double П)
    {
        return (1 - k) * H * П;
    }

    ///<summary>Розмір збитків тис. грн.</summary>
    public double Рлг3(double Н1, double Н2, double К, double П)
    {
        return (Н2 - Н1) * К * П;
    }

    ///<summary>Розрахунок збитків рибного господарства</summary>
    public double Ррг(double n, double N,double[] Ni)
    {
        return N + Ni.Sum();
    }

    ///<Summary>Величина збитків у натуральному виразі кг.</Summary>
    public double N(double П, double S, double M, double П1,
        double К1, double П2, double К2)
    {
        return (П * S * M) + (П1 * M * К1 / 100) + (M * S * К2 / 100);
    }

    ///<summary>обсяг збитків кг.</summary>
    public double N1(double П, double Z, double Q, double C,
        double K, double M)
    {
        return П * Z / 100 * Q * C * K / 100 * M;
    }

    ///<summary>обсяг збитків для планктону</summary>
    public double N2(double S, double H, double П, double P, double K1, double K2)
    {
        return (S * H * П * P * K1 * Math.Pow(10, -6) ) / (100 * K2);
    }

    ///<summary>обсяг збитків для бентосу</summary>
    public double N3(double S, double П, double P, double K1, double K2)
    {
        return (S * П * P * K1 * Math.Pow(10, -6)) / (100 * K2);
    }

    ///<summary>обсяг збитків кг.</summary>
    public double N4(double S, double P)
    {
        return S * P;
    }

    ///<summary>збитки від втрат потомства</summary>
    public double N5(double S, double П, double Z, 
        double Q, double C, double K, double M)
    {
        return S * П * Z / 100 * Q * C * K / 100 * M;
    }

    ///<summary>розрахунок збитків від знищення або погіршення якості рекреаційних зон</summary>
    public double Ррек(double n, double[] Зр, double[] Рп, double[] Рс)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < n; i++)
        {
            result.Add( (Зр[i] + (Рп[i] + Рс[i])) );
        }
        return result.Sum();
    }
    public double Ррек_2(double Т, double П)
    {
        return П * Т;
    }

    ///<summary>розрахунок збитків від втрат природно-заповідного фонду</summary>
    public double Рпзф(double Пз, double Рз)
    {
        return Пз * Рз;
    }

    ///<summary>витрати на відновлення природного стану об'єкта природно-заповідного фонду</summary>
    public double Пз(double Ап, double Анс, double[] Iі)
    {
        return Ап * Анс * Iі.Sum();
    }

    ///
    public double Рз(double m, double[] Qдо, double[] Qпісля)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < m; i++)
        {
            result.Add( (Qдо[i] - Qпісля[i]) );
        }
        return result.Sum();
    }

    ///____Економічна ефективність_____
    ///<summary>розрахунок національного доходу</summary>
    public double Qt(double Ut, double Ct, double Zt)
    {
        return Ut + Ct + Zt;
    }

    ///<summary>баланс підприємства(грн.)</summary>
    public double Кп(double ЗВ, double ГКР, double ВМП, double РКП)
    {
        return (ЗВ + ГКР - ВМП) / РКП;
    }

    ///<summary>читсий грошовий потік</summary>
    public double ЧГПп(double ЧГПо, double ЧГПі, double ЧГПф)
    {
        return ЧГПо + ЧГПі + ЧГПф;
    }

    public double Mr(double[] Mr, int N)
    {
        double sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += Mr[i];
        }
        return sum;
    }

    public double Kp(double Zv, double GKR, double VMP, double RKP)
    {
        return System.Math.Round(((Zv + GKR - VMP) / RKP), 2);
    }

    public double Pvc(int it, double[] Mi, double[] Npi)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Mi[i] * Npi[i];
        }
        return sum;
    }

    public double Vtg(int it, double[] Mdp, double[] Nz)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += 12 * Mdp[i] * (18 - Nz[i]);
        }
        return sum;
    }

    public double Pc(int it, double[] Mi, double[] Npi)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Mi[i] * Npi[i] * 1.5;
        }
        return sum;
    }

    public double Prv(int it, double[] Mi, double[] Npi)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Mi[i] * Npi[i] * 3 * 3;
        }
        return sum;
    }

    public double Fg(int it, double[] Pi, double[] Ki, double Lv)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Pi[i] * Ki[i];
        }
        return sum - Lv;
    }

    public double Vtrr(int it, double[] Ml, double[] Nl, double[] Mt, double[] Nt, double[] Mi, double[] Ni, double[] Mz, double[] Nz)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Ml[i] * Nl[i] + Mt[i] * Nt[i] + Mi[i] * Ni[i] + Mz[i] * Nz[i];
        }
        return sum;
    }

    public double Prc(int it, double[] Si, double[] Ki, double[] Ui, double[] Tci, double[] Zi_dod)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Si[i] * Ki[i] * Ui[i] * Tci[i] - Zi_dod[i];
        }
        return sum;
    }

    public double E(double vvp, double B0, int Tt, int Tr, double Ef)
    {
        return System.Math.Round(((vvp / Ef) * (System.Math.Exp(-1 * Ef * Tt) - System.Math.Exp(-1 * Ef * Tr)) + ((B0 * vvp) / (Ef * Ef)) * ((1 + Ef * Tt) * System.Math.Exp(-1 * Ef * Tt) - (1 + Ef * Tr) * System.Math.Exp(-1 * Ef * Tr))), 2);
    }

    public double Pz(int it, double[] Qi, double[] Qi_p)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Qi_p[i] - Qi[i];
        }
        return sum;
    }

    public double N(double[] Mr)
    {
        return Mr[0] * Mr[1] * Mr[2] + Mr[3] * Mr[2] * Mr[1] * Mr[4] / 100 + Mr[5] * Mr[2] * Mr[1] * Mr[6] / 100;
    }

    public double N1(double[] Mr)
    {
        return Mr[0] * Mr[1] / 100 * Mr[2] * Mr[3] * Mr[4] / 100 * Mr[5];
    }

    public double N2(double[] Mr)
    {
        return Mr[0] * Mr[1] * Mr[2] * Mr[3] * Mr[4] * 0.000001 / (100 * Mr[5]);
    }

    public double N3(double[] Mr)
    {
        return Mr[0] * Mr[1] * Mr[2] * Mr[3] * 0.000001 / (100 * Mr[4]);
    }

    public double N5(double[] Mr)
    {
        return Mr[0] * Mr[1] * Mr[2] / 100 * Mr[3] * Mr[4] * Mr[5] / 100 * Mr[6];
    }

    public double Mdg_o(int it, double[] Pi, double[] Ki, double[] ki, double[] qi)
    {
        double sum = 0;
        for (int i = 0; i < it; i++)
        {
            sum += Pi[i] * Ki[i] * ki[i] * qi[i];
        }
        return sum;
    }

    public double Под(double Taxation, double Pollution)
    {
        return Taxation * Pollution;
    }
}