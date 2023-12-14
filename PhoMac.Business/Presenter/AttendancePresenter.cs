using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter
{
    public class AttendancePresenter
    {
        PhoHa7_Attendance dic;
        public List<AttendancePresenter> ListAttendance;
        public PhoHa7_Attendance Attendances
        {
            //entity to database
            get
            {
                copyInstance();
                return dic;
            }
            //database to entity
            set
            {
                dic = value;
                this.Att_AttendanceID = dic.Att_AttendanceID;
                this.Att_EmployeeID = Convert.ToInt32(dic.Att_EmployeeID);
                this.Att_EmployeeName = dic.Att_EmployeeName;
                this.Day1 = convertToDouble(dic.Day1);
                this.Day2 = convertToDouble(dic.Day2);
                this.Day3 = convertToDouble(dic.Day3);
                this.Day4 = convertToDouble(dic.Day4);
                this.Day5 = convertToDouble(dic.Day5);
                this.Day6 = convertToDouble(dic.Day6);
                this.Day7 = convertToDouble(dic.Day7);
                this.Day8 = convertToDouble(dic.Day8);
                this.Day9 = convertToDouble(dic.Day9);
                this.Day10 = convertToDouble(dic.Day10);
                this.Day11 = convertToDouble(dic.Day11);
                this.Day12 = convertToDouble(dic.Day12);
                this.Day13 = convertToDouble(dic.Day13);
                this.Day14 = convertToDouble(dic.Day14);
                this.Day15 = convertToDouble(dic.Day15);
                this.Day16 = convertToDouble(dic.Day16);
                this.Day17 = convertToDouble(dic.Day17);
                this.Day18 = convertToDouble(dic.Day18);
                this.Day19 = convertToDouble(dic.Day19);
                this.Day20 = convertToDouble(dic.Day20);
                this.Day21 = convertToDouble(dic.Day21);
                this.Day22 = convertToDouble(dic.Day22);
                this.Day23 = convertToDouble(dic.Day23);
                this.Day24 = convertToDouble(dic.Day24);
                this.Day25 = convertToDouble(dic.Day25);
                this.Day26 = convertToDouble(dic.Day26);
                this.Day27 = convertToDouble(dic.Day27);
                this.Day28 = convertToDouble(dic.Day28);
                this.Day29 = convertToDouble(dic.Day29);
                this.Day30 = convertToDouble(dic.Day30);
                this.Day31 = convertToDouble(dic.Day31);
                this.Att_TotalDays = convertToDouble(dic.Att_TotalDays);
                this.Att_Month = Convert.ToInt32(dic.Att_Month);
                this.Att_Year = Convert.ToInt32(dic.Att_Year);
                this.Att_AmountCheck = Convert.ToDecimal(dic.Att_AmountCheck);
                this.Att_AmountCash = Convert.ToDecimal(dic.Att_AmountCash);
                this.Att_AmountExtra = Convert.ToDecimal(dic.Att_AmountExtra);
                this.Att_AmountSubtract = Convert.ToDecimal(dic.Att_AmountSubtract);
                this.Att_AmountTotal = Convert.ToDecimal(dic.Att_AmountTotal);
                this.Att_Description = dic.Att_Description;
                this.Att_Rate = Convert.ToDecimal(dic.Att_Rate);
            }
        }

        public AttendancePresenter()
        {
            dic = new PhoHa7_Attendance();
            ListAttendance = new List<AttendancePresenter>();
        }

        public void CopyToList(List<PhoHa7_Attendance> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                AttendancePresenter obj = new AttendancePresenter();
                obj.Attendances = pListDic[i];
                ListAttendance.Add(obj);
            }
        }

        void copyInstance()
        {
            dic.Att_AttendanceID = Att_AttendanceID;
            dic.Att_EmployeeID = Att_EmployeeID;
            dic.Att_EmployeeName = Att_EmployeeName;
            dic.Day1 = Day1;
            dic.Day2 = Day2;
            dic.Day3 = Day3;
            dic.Day4 = Day4;
            dic.Day5 = Day5;
            dic.Day6 = Day6;
            dic.Day7 = Day7;
            dic.Day8 = Day8;
            dic.Day9 = Day9;
            dic.Day10 = Day10;
            dic.Day11 = Day11;
            dic.Day12 = Day12;
            dic.Day13 = Day13;
            dic.Day14 = Day14;
            dic.Day15 = Day15;
            dic.Day16 = Day16;
            dic.Day17 = Day17;
            dic.Day18 = Day18;
            dic.Day19 = Day19;
            dic.Day20 = Day20;
            dic.Day21 = Day21;
            dic.Day22 = Day22;
            dic.Day23 = Day23;
            dic.Day24 = Day24;
            dic.Day25 = Day25;
            dic.Day26 = Day26;
            dic.Day27 = Day27;
            dic.Day28 = Day28;
            dic.Day29 = Day29;
            dic.Day30 = Day30;
            dic.Day31 = Day31;
            dic.Att_TotalDays = Att_TotalDays;
            dic.Att_Month = Att_Month;
            dic.Att_Year = Att_Year;
            dic.Att_AmountCheck = Att_AmountCheck;
            dic.Att_AmountCash = Att_AmountCash;
            dic.Att_AmountExtra = Att_AmountExtra;
            dic.Att_AmountSubtract = Att_AmountSubtract;
            dic.Att_AmountTotal = Att_AmountTotal;
            dic.Att_Description = Att_Description;
            dic.Att_Rate = Att_Rate;
        }



        #region Property

        public int Att_AttendanceID { get; set; }
        public int Att_EmployeeID { get; set; }
        public string Att_EmployeeName { get; set; }
        public double Day1 { get; set; }
        public double Day2 { get; set; }
        public double Day3 { get; set; }
        public double Day4 { get; set; }
        public double Day5 { get; set; }
        public double Day6 { get; set; }
        public double Day7 { get; set; }
        public double Day8 { get; set; }
        public double Day9 { get; set; }
        public double Day10 { get; set; }
        public double Day11 { get; set; }
        public double Day12 { get; set; }
        public double Day13 { get; set; }
        public double Day14 { get; set; }
        public double Day15 { get; set; }
        public double Day16 { get; set; }
        public double Day17 { get; set; }
        public double Day18 { get; set; }
        public double Day19 { get; set; }
        public double Day20 { get; set; }
        public double Day21 { get; set; }
        public double Day22 { get; set; }
        public double Day23 { get; set; }
        public double Day24 { get; set; }
        public double Day25 { get; set; }
        public double Day26 { get; set; }
        public double Day27 { get; set; }
        public double Day28 { get; set; }
        public double Day29 { get; set; }
        public double Day30 { get; set; }
        public double Day31 { get; set; }
        public double Att_TotalDays { get; set; }
        public int Att_Month { get; set; }
        public int Att_Year { get; set; }
        public decimal Att_AmountCheck { get; set; }
        public decimal Att_AmountCash { get; set; }
        public decimal Att_AmountExtra { get; set; }
        public decimal Att_AmountSubtract { get; set; }
        public decimal Att_AmountTotal { get; set; }
        public string Att_Description { get; set; }
        public decimal Att_Rate { get; set; }

        public Employee Employee;
        #endregion

        #region method

        double attendanceValue;
        int day;
        public double AttendanceValue
        {
            get
            {
                attendanceValue = getAttendanceDateValue(day);
                return attendanceValue;
            }
            set { attendanceValue = value; }
        }
        public int Day { get { return day; } set { day = value; } }

        public double totalAmount()
        {
            return convertToDouble(Att_Rate) * convertToDouble(Att_TotalDays) - convertToDouble(Att_AmountSubtract) + convertToDouble(Att_AmountExtra);
        }

        public double totalCash()
        {
            return convertToDouble(Att_AmountTotal) - convertToDouble(Att_AmountCheck);
        }

        public double totalDays()
        {
            return convertToDouble(Day1) + convertToDouble(Day2) + convertToDouble(Day3) + convertToDouble(Day4) + convertToDouble(Day5) + convertToDouble(Day6)
                + convertToDouble(Day7) + convertToDouble(Day8) + convertToDouble(Day9) + convertToDouble(Day10) + convertToDouble(Day11) + convertToDouble(Day12)
                + convertToDouble(Day13) + convertToDouble(Day14) + convertToDouble(Day15) + convertToDouble(Day16) + convertToDouble(Day17) + convertToDouble(Day18)
                + convertToDouble(Day19) + convertToDouble(Day20) + convertToDouble(Day21) + convertToDouble(Day22) + convertToDouble(Day23) + convertToDouble(Day24)
                + convertToDouble(Day25) + convertToDouble(Day26) + convertToDouble(Day27) + convertToDouble(Day28) + convertToDouble(Day29) + convertToDouble(Day30)
                + convertToDouble(Day31);
        }

        public double getAttendanceDateValue(int day)
        {
            switch (day)
            {
                case 1:
                    return convertToDouble(Day1);
                case 2:
                    return convertToDouble(Day2);
                case 3:
                    return convertToDouble(Day3);
                case 4:
                    return convertToDouble(Day4);
                case 5:
                    return convertToDouble(Day5);
                case 6:
                    return convertToDouble(Day6);
                case 7:
                    return convertToDouble(Day7);
                case 8:
                    return convertToDouble(Day8);
                case 9:
                    return convertToDouble(Day9);
                case 10:
                    return convertToDouble(Day10);
                case 11:
                    return convertToDouble(Day11);
                case 12:
                    return convertToDouble(Day12);
                case 13:
                    return convertToDouble(Day13);
                case 14:
                    return convertToDouble(Day14);
                case 15:
                    return convertToDouble(Day15);
                case 16:
                    return convertToDouble(Day16);
                case 17:
                    return convertToDouble(Day17);
                case 18:
                    return convertToDouble(Day18);
                case 19:
                    return convertToDouble(Day19);
                case 20:
                    return convertToDouble(Day20);
                case 21:
                    return convertToDouble(Day21);
                case 22:
                    return convertToDouble(Day22);
                case 23:
                    return convertToDouble(Day23);
                case 24:
                    return convertToDouble(Day24);
                case 25:
                    return convertToDouble(Day25);
                case 26:
                    return convertToDouble(Day26);
                case 27:
                    return convertToDouble(Day27);
                case 28:
                    return convertToDouble(Day28);
                case 29:
                    return convertToDouble(Day29);
                case 30:
                    return convertToDouble(Day30);
                case 31:
                    return convertToDouble(Day31);
            }
            return 0;
        }

        public void setAttendanceDateValue(int day, double value)
        {
            switch (day)
            {
                case 1:
                    Day1 = value;
                    break;
                case 2:
                    Day2 = value;
                    break;
                case 3:
                    Day3 = value;
                    break;
                case 4:
                    Day4 = value;
                    break;
                case 5:
                    Day5 = value;
                    break;
                case 6:
                    Day6 = value;
                    break;
                case 7:
                    Day7 = value;
                    break;
                case 8:
                    Day8 = value;
                    break;
                case 9:
                    Day9 = value;
                    break;
                case 10:
                    Day10 = value;
                    break;
                case 11:
                    Day11 = value;
                    break;
                case 12:
                    Day12 = value;
                    break;
                case 13:
                    Day13 = value;
                    break;
                case 14:
                    Day14 = value;
                    break;
                case 15:
                    Day15 = value;
                    break;
                case 16:
                    Day16 = value;
                    break;
                case 17:
                    Day17 = value;
                    break;
                case 18:
                    Day18 = value;
                    break;
                case 19:
                    Day19 = value;
                    break;
                case 20:
                    Day20 = value;
                    break;
                case 21:
                    Day21 = value;
                    break;
                case 22:
                    Day22 = value;
                    break;
                case 23:
                    Day23 = value;
                    break;
                case 24:
                    Day24 = value;
                    break;
                case 25:
                    Day25 = value;
                    break;
                case 26:
                    Day26 = value;
                    break;
                case 27:
                    Day27 = value;
                    break;
                case 28:
                    Day28 = value;
                    break;
                case 29:
                    Day29 = value;
                    break;
                case 30:
                    Day30 = value;
                    break;
                case 31:
                    Day31 = value;
                    break;
            }
            Att_TotalDays = totalDays();
        }

        double convertToDouble(Nullable<double> value)
        {
            return Convert.ToDouble(value == null ? 0 : value);
        }

        double convertToDouble(Nullable<decimal> value)
        {
            return Convert.ToDouble(value == null ? 0 : value);
        }

        #endregion


    }
}
