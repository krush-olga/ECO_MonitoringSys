using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Calculations{
public class EcologCalculator
{
    const double A = 0.5d;                            //витрати на ліквідацію забруднення земельної ділянки
    const double T = 0.2d;                           //товща земельного шару
    const double MILIGRAM_NA_KILOGRAM = 1_000_000;  //розрахунковий коефіцієнт, що дорівнює 1 000 000 мг/кг
    readonly double[] KOEF_NARODNOGOSPODARSKOGO_ZNACH = { 1.00d, 1.25d, 1.65d };
    readonly double[] KOEF_CHISELNOSTI_NASELENIYA = { 1.00d, 1.20d, 1.35d, 1.55d, 1.80d };
    readonly double[] KOEF_ECOLOG_GOSPODAR_ZNACH = { 5.5d, 5.0d, 4.5d, 4.0d, 4.0d, 4.0d, 3.5d, 1.0d, 1.0d, 1.0d, 1.0d };
    readonly double[] INDEX_POPRAVKI_NA_GLIBINY = { 0.100, 0.082, 0.070, 0.060, 0.054, 0.049, 0.044, 0.040, 0.037, 0.033 };
    readonly double[] PONYZYVALNYI_KOEF = { 0.95, 0.7, 0.9, 0.8, 0.2, 0.5, 0.5 };

    public enum PonuzuvalnuyKoef
    {
        PERELOGI,
        LISOVI_ZEMLI,
        LISOSMUGI_NASADJENYA,
        CHAGARNYKI,
        ZABUDOVANI_ZEMLI,
        ZABOLOXHENI_ZEMLI,
        VIDKRYTI_ZEMLI,
    }

    public enum RivenZasmichenia
    {
        VELUKYUI = 100,
        NIZKUYI = 10
    }

    public enum ChiselnistNaseleniya
    {
        LESS_THAN_100 = 0,
        FROM_100_TO_250,
        FROM_250_TO_500,
        FROM_500_TO_1000,
        MORE_THAN_1000
    }

    public enum TypeNaseleniiPynkt
    {
        AGRANI,      //Організаційно-господарські центри місцевого значення з перевагою аграрно-промислових функцій та села
        VELYKI,     //Багатофункціональні центри, центри з перевагою промислових і транспортних функцій
        KURORTNI   //Курортні міста згідно з 
    }

    public enum ShkalaEcologGospodarZnach
    {
        SANITARNI,
        OZDOROVCHI,
        PRIRODNO_ZOPOVIDNI,
        ZINNI_PRIRODNI_OBJECTY,
        RECREAZINI,
        ISTORIKO_KULTURNI,
        ZINNI_ZEMLI,
        ZHITLOVI,
        LISOVI,
        PROMISLOVI
    }

    //Вибрати понижувальний коефіцієнт
    /// <summary>
    /// Pозмір шкоди від забруднення земель
    /// </summary>
    /// <param name="groshovaOcinka"></param>
    /// <param name="ploshaZabrudZemelDil"></param>
    /// <param name="koefZabrudZemelDil"></param>
    /// <param name="koefNebespechZabrudRech"></param>
    /// <param name="koefEcologGospodarZnach"></param>
    /// <param name="ponuzuvalnuyKoef"></param>
    /// <returns>double</returns>
    public double Рш(double groshovaOcinka, double ploshaZabrudZemelDil,
                            double koefZabrudZemelDil, double koefNebespechZabrudRech,
                            double koefEcologGospodarZnach, PonuzuvalnuyKoef ponuzuvalnuyKoef)
    {
        double ponuzKoef = PONYZYVALNYI_KOEF[(int)ponuzuvalnuyKoef];

        return (A * groshovaOcinka * ploshaZabrudZemelDil * koefZabrudZemelDil * koefEcologGospodarZnach * koefNebespechZabrudRech) * ponuzKoef;
    }
    /// <summary>
    /// Коефіцієнт забруднення землі, при наявності інформації про об'єм забрунюючої речовини, що проникла у землю
    /// </summary>
    /// <param name="ploshaZabrudZemelDil"></param>
    /// <param name="obyemZabrudRech"></param>
    /// <param name="indexPopravkDoVutratNaLikvidaciu"></param>
    /// <returns>double</returns>
    public double Кз(double ploshaZabrudZemelDil, double obyemZabrudRech, double indexPopravkDoVutratNaLikvidaciu)
    {
        double buff = 0;
        buff = (T * ploshaZabrudZemelDil * indexPopravkDoVutratNaLikvidaciu);

        if (buff != 0)
            return obyemZabrudRech / buff;
        else
            throw new DivideByZeroException("Division by Zero");
    }
    /// <summary>
    /// При наявності інформації лише про масу речовини, що проникла у землю(Об'єм забруднюючої речовини)
    /// </summary>
    /// <param name="masaZabrudRech"></param>
    /// <param name="vidnosnaGustZabrudRech"></param>
    /// <returns>double</returns>
    public double Озр(double masaZabrudRech, double vidnosnaGustZabrudRech)
    {
        if (vidnosnaGustZabrudRech != 0)
            return masaZabrudRech / vidnosnaGustZabrudRech;
        else
            throw new DivideByZeroException("Division by Zero");
    }
    /// <summary>
    /// Якщо вміст забрунюючої речовини встановлювався за результатами інструментального лабораторного контролю
    /// </summary>
    /// <param name="consentraciaZabrudRech"></param>
    /// <param name="tovshaZemelSharu"></param>
    /// <param name="indexPopravkuDoVutratNaLikvidZabrud"></param>
    /// <returns>double</returns>
    public double Кз_2(double consentraciaZabrudRech, double tovshaZemelSharu, double indexPopravkuDoVutratNaLikvidZabrud)
    {

        if (indexPopravkuDoVutratNaLikvidZabrud != 0)
            return (consentraciaZabrudRech * tovshaZemelSharu) / (T * indexPopravkuDoVutratNaLikvidZabrud * MILIGRAM_NA_KILOGRAM);
        else
            throw new DivideByZeroException("Divided by Zero");

    }
    /// <summary>
    /// Загальний розмір відшкодування при одночасному забрудненні земельної ділянки декількома забруднюючими речовинами
    /// </summary>
    /// <param name="maxRozmirShkodi"></param>
    /// <param name="arr"></param>
    /// <returns>double</returns>
    //!Але одним суб'єктом господарювання чи фізичною особою
    //метод повертає загальний розмір шкоди від забруднення ділянки (грн.)
    public double Рш_заг(double maxRozmirShkodi, double[] arr) // var arg
    {
        double sum = arr.Sum();
        return maxRozmirShkodi * 0.5 * sum;
    }

    /// <summary>
    /// Розмір шкоди внаслідок засмічення земель
    /// </summary>
    /// <param name="rivenZasmichenia"></param>
    /// <param name="groshovaOcinkaZemelDil"></param>
    /// <param name="ploshaZasmichZemelDil"></param>
    /// <param name="koefZasmichZemelDil"></param>
    /// <param name="koefNebezpekuVidhodiv"></param>
    /// <param name="koefEcologGospodZnachZemel"></param>
    /// <returns>double</returns>
    public double Ршз(RivenZasmichenia rivenZasmichenia, double groshovaOcinkaZemelDil,
                            double ploshaZasmichZemelDil, double koefZasmichZemelDil,
                            double koefNebezpekuVidhodiv, double koefEcologGospodZnachZemel)
    {
        int valueRivenZasmich = Convert.ToInt16(rivenZasmichenia);
        return A * valueRivenZasmich * groshovaOcinkaZemelDil * ploshaZasmichZemelDil * koefZasmichZemelDil * koefNebezpekuVidhodiv * koefEcologGospodZnachZemel;
    }
    /// <summary>
    /// Розрахнок маси наднормативного викиду забруднюючої речовини в атмосферне повітря від джерела викиду 
    /// забруднюючих речовин відносного до ОСНОВНИХ джерел викидів
    /// </summary>
    /// <param name="seredneZnachMassovoiKoncentracii"></param>
    /// <param name="ZnachNormatuvnugoVukud"></param>
    /// <param name="objemniVutratuGazyPulyPotoky"></param>
    /// <returns>double</returns>
    public double Formula7(double seredneZnachMassovoiKoncentracii, double ZnachNormatuvnugoVukud, double objemniVutratuGazyPulyPotoky)
    {
        return 3.6 * Math.Pow(10, -6) * (seredneZnachMassovoiKoncentracii - ZnachNormatuvnugoVukud) * objemniVutratuGazyPulyPotoky * T;
    }
    /// <summary>
    /// Альтернативний 1 Розрахунок маси наднормативного викиду забруднюючої речовини в атмосферне повітря від джерела викиду забруднюючих речовин
    /// відносного до ОСНОВНИХ джерел викидів
    /// </summary>
    /// <param name="serednyeZnachMasovoiVytrati"></param>
    /// <param name="ZnachNormatuvnugoVukud"></param>
    /// <returns>double</returns>
    public double Formula7Alternative1(double serednyeZnachMasovoiVytrati, double ZnachNormatuvnugoVukud)
    {
        return 3.6 * Math.Pow(10, -3) * (serednyeZnachMasovoiVytrati - ZnachNormatuvnugoVukud) * T;
    }
    //Альтернативний 2 Розрахунок маси наднормативного викиду забруднюючої речовини в атмосферне повітря від джерела викиду забруднюючих речовин
    //відносного до ІНШИХ джерел викидів
    public double Formula7Alternative2(double serednyeZnachMasovoiVytrati, double ZnachNormatuvnugoVukud)
    {
        return 3.6 * Math.Pow(10, -3) * (serednyeZnachMasovoiVytrati - ZnachNormatuvnugoVukud) * T;
    }
    //Розрахунок маси наднормативного викиду газоподібної забрунючої речовини в атмосферне повітря від ПАЛОВИКОРИСТОВУВАЛЬНОГО обладнання
    public double Folmula8(double serednyeZnachMasovoiVytrati, double znachZatverdzNormVikidy, double znachObjemVutrat)
    {
        return 3.6 * Math.Pow(10, -6) * (serednyeZnachMasovoiVytrati - znachZatverdzNormVikidy) * znachObjemVutrat * T;
    }
    //АЛЬТЕРНАТИВНИЙ розрахунок маси наднормативного викиду газоподібної забрунючої речовини в атмосферне повітря
    //від ПАЛОВИКОРИСТОВУВАЛЬНОГО обладнання
    public double Folmula8Alternative(double serednyeZnachMasovoiVytrati, double znachZatverdzNormVikidy)
    {
        return 3.6 * Math.Pow(10, -3) * (serednyeZnachMasovoiVytrati - znachZatverdzNormVikidy) * T;
    }
    /// <summary>
    /// Значення масової концентрації і-тої забруднючої речовини приведене до регламентованого вмісту кисню
    /// </summary>
    /// <param name="znachMassovoiKoncentraciiZabrudRech"></param>
    /// <param name="reglementovanuyVmistKucny"></param>
    /// <param name="obyemnaChastkaKusnuZaResultVumur"></param>
    /// <returns></returns>
    public double ро_ві(double znachMassovoiKoncentraciiZabrudRech, double reglementovanuyVmistKucny, double obyemnaChastkaKusnuZaResultVumur)
    {
        return (znachMassovoiKoncentraciiZabrudRech * (21 - reglementovanuyVmistKucny)) / (21 - obyemnaChastkaKusnuZaResultVumur);
    }
    /// <summary>
    /// У разі неможливого інструментального вимірювання необхідних параметрів для визначення 
    /// об'ємної витрати газопилового потоку від паловикористувального обладнання
    /// </summary>
    /// <param name="vutratuPaluva"></param>
    /// <param name="teorObyemProductGorinnya"></param>
    /// <param name="teorObyemPovitrya"></param>
    /// <param name="koefNadlushkyPovitrya"></param>
    /// <returns></returns>
    public double Кзі(double vutratuPaluva, double teorObyemProductGorinnya, double teorObyemPovitrya, double koefNadlushkyPovitrya)
    {

        return (vutratuPaluva * (teorObyemProductGorinnya + teorObyemPovitrya * (koefNadlushkyPovitrya - 1))) / 3600;
    }
    /// <summary>
    /// Розмір відшкодування збитків за наднормативний викид однієї тонни забрунюючої речовини в атмосферне повітря 
    /// </summary>
    /// <param name="massaZabrudRech"></param>
    /// <param name="minimalZarobitPlata"></param>
    /// <param name="vidnosnaNebezpechZabrudRech"></param>
    /// <param name="koefSocEcelogOsobluvist"></param>
    /// <param name="koefRivnyaZabrudAtmosferPovitria"></param>
    /// <returns></returns>
    public double З(double massaZabrudRech, double minimalZarobitPlata, double vidnosnaNebezpechZabrudRech,
                            double koefSocEcelogOsobluvist, double koefRivnyaZabrudAtmosferPovitria)
    {
        return massaZabrudRech * (1.1 * minimalZarobitPlata) * vidnosnaNebezpechZabrudRech * koefSocEcelogOsobluvist * koefRivnyaZabrudAtmosferPovitria;
    }
    //Показник відносно небезпечність забруднюючої речовини 
    public double Formula12()
    {
        return 0;
    }
    /// <summary>
    /// Коефіцієнт що враховує територіальний соціально-екологічні особливості
    /// </summary>
    /// <param name="chiselnistNaseleniya"></param>
    /// <param name="typeNaseleniiPynkt"></param>
    /// <returns></returns>
    public double Кт(double chiselnistNaseleniya, double typeNaseleniiPynkt)
    {
        return chiselnistNaseleniya * typeNaseleniiPynkt;
    }
    //Коефіцієнт що залежить від рівня забруднення атмосферного повітря населеного пункту забруднюючою речовиною 
    ////serednyaDobovaGranchDopustumaConcentrciaZabrudRech = 1, якщо в данному населному пункті інструментальні вимірювання концентрації дано забруднюючої речовини
    //не виконуються а також якщо рівень забруднення атмоферного повітря населенго пункта забруднюючою речовиною не первищує ГДК(гранична допустима концентрація)
    public double Formula14(double serednyaRichnaKoncentraciaZabrudRech, double serednyaDobovaGranchDopustumaConcentrciaZabrudRech)
    {
        return serednyaRichnaKoncentraciaZabrudRech / serednyaDobovaGranchDopustumaConcentrciaZabrudRech;
    }
    //Кількість викидів забруднуючої речовини від автотранспорту
    public double Formula15(double fuelAmount, double amountOfFuelWaste)
    {
        return fuelAmount * amountOfFuelWaste;
    }
    /// <summary>
    /// ГДС(граничнодопустимий спад) для кожного показника якості води
    /// </summary>
    /// <param name="maxPermisibbleValue"></param>
    /// <param name="maxSewageWaste"></param>
    /// <returns></returns>
    public double ГДС(double maxPermisibbleValue, double maxSewageWaste)
    {
        return maxPermisibbleValue * maxSewageWaste;
    }
    /// <summary>
    /// Об'єм атмосферних опадів за рік
    /// </summary>
    /// <param name="flowCoefficient"></param>
    /// <param name="areaOfGround"></param>
    /// <param name="precipitations"></param>
    /// <returns></returns>
    public double Formula17(double flowCoefficient, double areaOfGround, double precipitations)
    {
        return 10 * flowCoefficient * areaOfGround * precipitations;
    }
    /// <summary>
    /// Об'єм води для поливання території
    /// </summary>
    /// <param name="waterConsumption"></param>
    /// <param name="wateringAmount"></param>
    /// <param name="areaOfGround"></param>
    /// <returns></returns>
    public double Formula18(double waterConsumption, double wateringAmount, double areaOfGround)
    {
        return 10 * waterConsumption * wateringAmount * areaOfGround;
    }
    /// <summary>
    /// Сумарна річна кількість виносу речовин стічними водами  
    /// </summary>
    /// <param name="waterConsumption"></param>
    /// <param name="wateringAmount"></param>
    /// <param name="areaOfGround"></param>
    /// <returns></returns>
    public double Formula19(double volumeOfRain, double concentrationOfPollutantsInRain, double volumeOfSnow,
                            double concentrationOfPollutantsInSnow, double volumeOfWatering, double concentrationOfPollutantsInWatering)
    {
        return volumeOfRain * concentrationOfPollutantsInRain * volumeOfSnow * concentrationOfPollutantsInSnow * volumeOfWatering * concentrationOfPollutantsInWatering;
    }
    //Максимальне значення приземної концентрації шкідливої речовини у разі викидів з одиночного точкового джерела  
    public double Formula20(double atmosphereTemperatureCoefficient, double massOfHarmfulSubstance, double speedOfSubstanceSedimentationCoefficient,
                            double firstGasPollutionCondition, double secondGasPollutionCondition, double terrainTopographyCoefficient, double sourceHeight,
                            double fixedCostsOfGasEmission, double temperatureDifference)
    {
        double top = (atmosphereTemperatureCoefficient * massOfHarmfulSubstance * speedOfSubstanceSedimentationCoefficient * firstGasPollutionCondition * secondGasPollutionCondition * terrainTopographyCoefficient);
        double bottom = Math.Pow(sourceHeight, 2) * Math.Pow((fixedCostsOfGasEmission * temperatureDifference), (1.0 / 3));

        return bottom == 0 ? 0 : top / bottom;
    }
    /// <summary>
    /// Кількість забруднюючої речовини
    /// </summary>
    /// <param name="burnedMassOfTPV"></param>
    /// <param name="specificEmission"></param>
    /// <returns></returns>
    public double Formula21(double burnedMassOfTPV, double specificEmission)
    {
        return burnedMassOfTPV * specificEmission;
    }
    /// <summary>
    /// ІЗВ(індекс забруднення води)
    /// </summary>
    /// <param name="concentrations"></param>
    /// <param name="maxPermisibbleValues"></param>
    /// <param name="amountOfComponents"></param>
    /// <param name="waterQuality"></param>
    /// <returns></returns>
    public double ІЗВ(double[] concentrations, double[] maxPermisibbleValues, int amountOfComponents, ref string waterQuality)
    {
        double result = -1.0;
        try
        {
            for (int i = 0; i < amountOfComponents; i++)
            {
                if (maxPermisibbleValues[i] == 0)
                {
                    throw new DivideByZeroException();
                }
                result += (concentrations[i] / maxPermisibbleValues[i]);
            }
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e.Message);
        }
        result /= amountOfComponents;

        switch (result)
        {
            case object res when (Convert.ToDouble(res) <= 0.2):
                {
                    waterQuality = "I";
                    break;
                }
            case object res when ((Convert.ToDouble(res) >= 0.2) && (Convert.ToDouble(res) <= 1.0)):
                {
                    waterQuality = "II";
                    break;
                }
            case object res when ((Convert.ToDouble(res) >= 1.0) && (Convert.ToDouble(res) <= 2.0)):
                {
                    waterQuality = "III";
                    break;
                }
            case object res when ((Convert.ToDouble(res) >= 2.0) && (Convert.ToDouble(res) <= 4.0)):
                {
                    waterQuality = "IV";
                    break;
                }
            case object res when ((Convert.ToDouble(res) >= 4.0) && (Convert.ToDouble(res) <= 6.0)):
                {
                    waterQuality = "V";
                    break;
                }
            case object res when ((Convert.ToDouble(res) >= 6.0) && (Convert.ToDouble(res) <= 10.0)):
                {
                    waterQuality = "VI";
                    break;
                }
            case object res when ((Convert.ToDouble(res) > 10.0)):
                {
                    waterQuality = "VII";
                    break;
                }
            default:
                break;
        }

        return result;
    }
    /// <summary>
    /// Масова частка компонента у відсотках
    /// </summary>
    /// <param name="massOfSubstance"></param>
    /// <param name="massOfLiquid"></param>
    /// <returns></returns>
    public double W(double massOfSubstance, double massOfLiquid)
    {
        return (massOfSubstance / massOfLiquid) * 100;
    }
    /// <summary>
    /// Масова частка компонента
    /// </summary>
    /// <param name="massOfSubstance"></param>
    /// <param name="massOfLiquid"></param>
    /// <returns></returns>
    public double Wв(double massOfSubstance, double massOfLiquid)
    {
        double result = 0.0;
        try
        {
            result = massOfSubstance / massOfLiquid;
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e.Message);
        }
        return result;
    }
    /// <summary>
    /// Молярна концентрація компонента
    /// </summary>
    /// <param name="componentMolarValue"></param>
    /// <param name="volumeOfLiquid"></param>
    /// <returns></returns>
    public double Formula25(double componentMolarValue, double volumeOfLiquid)
    {
        double result = 0.0;
        try
        {
            result = componentMolarValue / volumeOfLiquid;
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e.Message);
        }
        return result;
    }
    /// <summary>
    /// Середнє арифметичне значення концентрації домішки
    /// </summary>
    /// <param name="q"></param>
    /// <param name="amountOfConcentration"></param>
    /// <returns></returns>
    //(проверки на ошибки нет потому, что не может быть 0 концентраций(интерфейс програмы не позволит))
    public double q_i(double[] q, double amountOfConcentration)
    {
        double result = 0.0;
        result = q.Sum();
        result /= amountOfConcentration;
        return result;
    }
    /// <summary>
    /// Середнє квадратичне відхилення результатів вимірювань
    /// від середнього арифметочного
    /// </summary>
    /// <param name="q"></param>
    /// <param name="averageConcentration"></param>
    /// <param name="amountOfConcentration"></param>
    /// <returns></returns>
    public double S_i(double[] q, double averageConcentration, double amountOfConcentration)
    {
        double result = 0.0;
        try
        {
            foreach (double value in q)
            {
                result += Math.Sqrt(Math.Pow((value - averageConcentration), 2) / (amountOfConcentration - 1));
            }
        }
        catch
        {
            result = -1.0;
        }
        return result;
    }
    /// <summary>
    /// Коефіцієнт варіації вказує на ступінь змінності концентрації домішки
    /// </summary>
    /// <param name="averageQuadraticDeviationOfMeasurement"></param>
    /// <param name="averageConcentration"></param>
    /// <returns></returns>
    public double V_i(double averageQuadraticDeviationOfMeasurement, double averageConcentration)
    {
        return averageQuadraticDeviationOfMeasurement / averageConcentration;
    }
    //Максимальне значення концентрації домішки
    public double Function29(double numberOfCities, double[] substanceConcentration)
    {
        double result = 0.0;
        result = substanceConcentration.Sum();
        return result / numberOfCities;
    }
    /// <summary>
    /// Індекс забруднення атмосфери(ІЗА)
    /// </summary>
    /// <param name="concentration"></param>
    /// <param name="maxPermisibbleValue"></param>
    /// <param name="dangerLevel"></param>
    /// <returns></returns>
    public double ІЗА(double concentration, double maxPermisibbleValue, int dangerLevel)
    {
        double[] degreeOfHarm = { 1.7, 1.3, 1.0, 0.9 };
        return Math.Pow((concentration / maxPermisibbleValue), degreeOfHarm[dangerLevel - 1]); ;
    }
    /// <summary>
    /// Комплексний індекс забруднення атмосфери міста(КІЗА)
    /// </summary>
    /// <param name="indexOfSubstanceAirPollution"></param>
    /// <returns></returns>
    public double КІЗА(double[] indexOfSubstanceAirPollution)
    {
        return indexOfSubstanceAirPollution.Sum();
    }

    public double mi1(double cif, double cid, double qif, double t)//
    {
        double res;
        res = (cif - cid) * qif * t * 0.000001;
        return res;
    }

    public double mi2(double mif, double mil)//
    {
        double res;
        res = mif - mil;
        return res;
    }

    public double mi3(double cif, double cik, double qif, double t)//
    {
        double res;
        res = (cif - cik) * qif * t * 0.000001;
        return res;
    }

    public double mi4(double cif, double qif, double t)//
    {
        double res;
        res = cif * qif * t * 0.000001;
        return res;
    }

    public double cif(double []cin, int n)//
    {
        double res = 0;
        for(int i=0; i<n; i++)
        {
            res += cin[i];
        }
        res /= n;
        return res;
    }
    /*
    ///<summary>
    /// Формула розрахунку податку за класом небезпеки речовини
    /// </summary>
    /// <param name="Taxation"></param>
    /// <returns></returns>
    public double Под(double Taxation, double Pollution)
    {
        return Taxation * Pollution;
    }*/
}
//}
