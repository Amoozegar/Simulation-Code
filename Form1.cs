using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int SimulationTime;
        double clock;

        //-------------------------events
        string CurrentAction;

        //the length of the line for station 1,2, 3
        int L1;
        int L2;
        int L3;

        //the status of employeess in station 1
        int status1;
        int status2;
        int status3;
        int status4;
        int status5;
        int status6;
      
        int BusyNum1;//the number of busy employees in station 1
     
        int RestNum1;//the number of employees who rest in station 1
              
        int Rest1op;//the next employee who should rest in station 1
      
        int Rest1t;//the next  rest time for the employees in station 1

        int Rest2t;//the next employee who should rest in station 2       
           
        int Rest2op;//the next employee who should rest in station 2
        
        //the status of employees in station 2
        int status7;
        int status8;

        int BusyNum2;//the number of busy employees in station 2
        
        int RestNum2;//the number of employees who rest in station 2
       
        int FreeChair;//the number of free chairs in the dining room
    
        int MaxBusyChair;//the maximum number of occupied chairs 
        
        //the number of buses/cars/passengers which arrive at the restaurant
        int BusCount;
        int CarCount;
        int WalkCount;


        double modatesarfT;//total time for eating the food

        double TRestSt1;//total rest time
        double TtimeBus;
        double TtimeWalk;
        double TtimeCar;

        int MaxL1;//maximum length of the line in station 1

        int QNumSt1;

        int TCountRest;
        int UnderCountRest;

        double TtimeQSt1;

        //these variables are used to calculate the number of people who entered the stations and left the stations
        int Counter1;
        int Counter2;
        int Counter3;
        int OrderCount;
        int PayCount;
        int GiveCount;
        int EatCount;
       int  CounterExit;
//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


        const int free = 0;
        const int busy = 1;
        const int rest = 2;
       
        //events definition////////////////////////////////////////
        const int BusEnter = 0;//Bus arrival event
        const int WalkEnter = 1;//passenger arrival event
        const int CarEnter = 2;//car arrival event

        const int ArriveStation1 = 3;//Arriving to station1
        const int ArriveStation2 = 4;//Arriving to station2
        const int ArriveStation3 = 5;//Arriving to station3

        //start of the Rest time for employees in station1
        const int RestTimeGetSt1_1 = 6;
        const int RestTimeGetSt1_2 = 7;
        const int RestTimeGetSt1_3 = 8;
        const int RestTimeGetSt1_4 = 9;
        const int RestTimeGetSt1_5 = 10;
        const int RestTimeGetSt1_6 = 11;

        //the start of the Rest time for employees in station2
        const int RestTimeGetSt2_7 = 12;
        const int RestTimeGetSt2_8 = 13;

        //the end of the rest time for employees in station 1
        const int RestTimeEndSt1_1 = 14;
        const int RestTimeEndSt1_2 = 15;
        const int RestTimeEndSt1_3 = 16;
        const int RestTimeEndSt1_4 = 17;
        const int RestTimeEndSt1_5 = 18;
        const int RestTimeEndSt1_6 = 19;
 
        //the end of the rest time for employees in station 2
        const int RestTimeEndSt2_7 = 20;
        const int RestTimeEndSt2_8 = 21;
   
        //the end of ordering
        const int OrderEnd1 = 22;
        const int OrderEnd2 = 23;
        const int OrderEnd3 = 24;
        const int OrderEnd4 = 25;
        const int OrderEnd5 = 26;
        const int OrderEnd6 = 27;

        //the end of the payment
        const int PayEnd1 = 28;
        const int PayEnd2 = 29;
        const int PayEnd3 = 30;
        const int PayEnd4 = 31;
        const int PayEnd5 = 32;
        const int PayEnd6 = 33;
 
        ////the end of receiving food event
        const int GiveEnd7 = 34;
        const int GiveEnd8 = 35;

        const int EatEnd = 36;//the end of ordering the food
        const int Exit = 37;//leaving the restuarant
        const int remove = 38;// The end of the day

        //---FEL definition-------------------------------------
        List<List<double>> FEL = new List<List<double>>();
        int action = 0;
        int time = 1;

//--------------this table shows the status and other information of simulation---------------------------
        Random r = new Random();
        DataTable TraceDT= new DataTable();
        DataTable FELDT = new DataTable();
//----------------------------

        List<List<double>> SaveChair = new List<List<double>>();
        List<List<double>> SaveRestSt1=new List<List<double>>();
        List<List<double>> SaveEntersToSt=new List<List<double>>();
        List<List<double>> SaveToQ = new List<List<double>>();//The average time that customers spend in line in station1

        void starter1()//Starter for the first day
        {
            clock = 0;
            SimulationTime = 240;
            MaxL1 = 0;
            L1 = 0;
            L2 = 0;
            L3 = 0;
            status1 = free;
            status2 = free;
            status3 = free;
            status4 = free;
            status5 = free;
            status6 = free;
            status7 = free;
            status8 = free;
            BusyNum1 = 0;
            BusyNum2 = 0;
            RestNum1 = 0;
            RestNum2 = 0;
            FreeChair = 30;
            MaxBusyChair = 0;
            modatesarfT=0 ;

            TRestSt1 = 0;

            TtimeBus=0;
            TtimeWalk=0;
            TtimeCar = 0;

            TCountRest=0;
            UnderCountRest = 0;

            WalkCount = 0;
            BusCount = 0;
            CarCount = 0;


            //.....these variables are used to calculate the number of people entered to the stations.....//
            Counter1 = 0;
            Counter2 = 0;
            Counter3 = 0;
            OrderCount = 0;
            PayCount = 0;
            GiveCount = 0;
            EatCount = 0;
            CounterExit = 0;


            TtimeQSt1 = 0;//The average time for waiting in line 1;
            QNumSt1 = 0;

            Rest1op = 1;
            Rest1t = 50;

            Rest2op = 7;
            Rest2t = 50;

            FELConstructor(remove);
            FELConstructor(WalkEnter);
            FELConstructor(CarEnter);
            FELConstructor(BusEnter);
            FELConstructor(RestTimeGetSt1_1);
            FELConstructor(RestTimeGetSt2_7);
            TraceMaker();
            
        }
        
        //-------------------------------------
        void 
            starter()//starter for the days other than the first day
        {
            //List<List<double>> FEL = new List<List<double>>();
            clock = 0;
            SimulationTime = 240;
            L1 = 0;
            L2 = 0;
            L3 = 0;//The length of line for station 1, 2,3 
            status1 = free;
            status2 = free;
            status3 = free;
            status4 = free;
            status5 = free;
            status6 = free;
            status7 = free;
            status8 = free;
            BusyNum1 = 0;
            BusyNum2 = 0;
            RestNum1 = 0;
            RestNum2 = 0;
            FreeChair = 30;
            FELConstructor(remove);
            FELConstructor(WalkEnter);
            FELConstructor(CarEnter);
            FELConstructor(BusEnter);
            MaxBusyChair = 0;
            MaxL1 = 0;
            modatesarfT = 0;

            TRestSt1 = 0;

            TtimeQSt1 = 0;
            QNumSt1 = 0;

            TtimeBus = 0;
            TtimeCar = 0;
            TtimeWalk = 0;

            UnderCountRest = 0;

            CarCount = 0;
            WalkCount = 0;
            BusCount = 0;


            Rest2op = 8;

            if (Rest1op == 1)
                FELConstructor(RestTimeGetSt1_1);
            else if (Rest1op == 3)
                   FELConstructor(RestTimeGetSt1_3);
               else if (Rest1op == 5)
                        FELConstructor(RestTimeGetSt1_5);
               
            
           
                FELConstructor(RestTimeGetSt2_8);

            TraceMaker();


        }

        void FELConstructor(int action)  // check the events 
        {
            List<double> felrow = new List<double>();
            felrow.Add(action);
            switch (action)
            {
                case remove:
                    felrow.Add(240.0001);
                    break;

                case BusEnter:   
                    felrow.Add(60 + (120 * r.NextDouble())); //uniform(11,13)-the bus arrival follows uniform distribution and random numbers are generated from this distribution
                    break;
                case WalkEnter:
                    if (clock < 195)
                        felrow.Add(clock + (-3* Math.Log(r.NextDouble())));  //the arrival of the passengers follows exponential distribution
                    else
                        felrow.Add(clock + (-10* Math.Log(r.NextDouble()))); //random numbers are generated from this distribution
                    break;
                case CarEnter:
                    if (clock < 195)
                        felrow.Add(clock + (-2* Math.Log(r.NextDouble()))); //the car arrival follows exponential distribution
                    else
                        felrow.Add(clock + (-8 * Math.Log(r.NextDouble())));//random numbers are generated from this distribution
                    break;

                case ArriveStation1:
                    felrow.Add(clock + (-0.5 * Math.Log(r.NextDouble()))); // the ariival to station 1 follows exponential distribution and random number are generated from this distribution
                    break;
                case ArriveStation2:
                    felrow.Add(clock + (-0.5 * Math.Log(r.NextDouble())));// the ariival to station 2 follows exponential distribution and random number are generated from this distribution
                    break;
                case ArriveStation3:
                    felrow.Add(clock + (-0.5 * Math.Log(r.NextDouble())));// the ariival to station 3 follows exponential distribution and random number are generated from this distribution
                    break;
                           
                case RestTimeGetSt1_1:
                    if (clock == 0)
                        felrow.Add(50); //filling out FEL table
                    else
                    {
                        felrow.Add(clock + 60);
                    }
                    break;
                //-----------------------------------------
                case RestTimeGetSt1_2:
                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                        //if (clock + 60 < 240)
                            felrow.Add(clock + 60);
                        //else
                        //    felrow.Add(50);
                    }
                    break;
                //------------------------
                case RestTimeGetSt1_3:

                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                       // if (clock + 60 < 240)
                            felrow.Add(clock + 60);
                        //else
                        //    felrow.Add(50);
                    }
                    break;
                //--------------------------------------------
                case RestTimeGetSt1_4:

                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                       //if (clock + 60 < 240)
                            felrow.Add(clock + 60);
                        //else
                        //    felrow.Add(50);
                    }
                    break;
                //------------------------------------------------
                case RestTimeGetSt1_5:
                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                       // if (clock + 60 < 240)
                            felrow.Add(clock + 60);
                        //else
                        //    felrow.Add(50);
                    }
                    break;
                //------------------------------------------------
                case RestTimeGetSt1_6:

                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                       // if (clock + 60 < 240)
                            felrow.Add(clock + 60);
                        //else
                        //    felrow.Add(50);
                    }
                    break;
                //-----------------------------------------------------------------
                case RestTimeGetSt2_7://the rest time for the employee in station 2
                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                       // if (clock + 180 < 240)
                            felrow.Add(clock + 180);
                        //else
                        //    felrow.Add(50);
                    }
                    break;
                //-------------------------------------
                case RestTimeGetSt2_8://the rest time for the employee in station 2
                    if (clock == 0)
                        felrow.Add(50);
                    else
                    {
                            felrow.Add(clock + 180);
                    }
                    break;
               
                case RestTimeEndSt1_1:
                    felrow.Add(Rest1t + 10);
                    break;
                case RestTimeEndSt1_2:
                    felrow.Add(Rest1t + 10);
                    break;
                case RestTimeEndSt1_3:
                    felrow.Add(Rest1t + 10);
                    break;
                case RestTimeEndSt1_4:
                    felrow.Add(Rest1t + 10);
                    break;
                case RestTimeEndSt1_5:
                    felrow.Add(Rest1t + 10);
                    break;
                case RestTimeEndSt1_6:
                    felrow.Add(Rest1t + 10);
                    break;
                //-------------------------------------------------------
                case RestTimeEndSt2_7:
                    felrow.Add(Rest2t + 10);
                    break;
                case RestTimeEndSt2_8:
                    felrow.Add(Rest2t + 10);
                    break;
                //----------------------------------------------------------

                case OrderEnd1:
                    felrow.Add(clock + (1 + r.NextDouble() * 3));//generating random numbers from uniform(1,4) distribution
                    break;
                case OrderEnd2:
                    felrow.Add(clock + (1 + r.NextDouble() * 3));
                    break;
                case OrderEnd3:
                    felrow.Add(clock + (1 + r.NextDouble() * 3));
                    break;
                case OrderEnd4:
                    felrow.Add(clock + (1 + r.NextDouble() * 3));
                    break;
                case OrderEnd5:
                    felrow.Add(clock + (1 + r.NextDouble() * 3));
                    break;
                case OrderEnd6:
                    felrow.Add(clock + (1 + r.NextDouble() * 3));
                    break;
                case PayEnd1:
                    felrow.Add(clock + (1 + r.NextDouble() * 2));//generating random numbers from uniform(1,3) distribution
                    break;
                case PayEnd2:
                    felrow.Add(clock + (1 + r.NextDouble() * 2));
                    break;
                case PayEnd3:
                    felrow.Add(clock + (1 + r.NextDouble() * 2));
                    break;
                case PayEnd4:
                    felrow.Add(clock + (1 + r.NextDouble() * 2));
                    break;
                case PayEnd5:
                    felrow.Add(clock + (1 + r.NextDouble() * 2));
                    break;
                case PayEnd6:
                    felrow.Add(clock + (1 + r.NextDouble() * 2));
                    break;
                case GiveEnd7:
                    felrow.Add(clock + (0.5 + r.NextDouble() * 1.5));//generating random numbers from uniform(0.5,2) distribution
                    break;
                case GiveEnd8:
                    felrow.Add(clock + (0.5 + r.NextDouble() * 1.5));
                    break;
                case EatEnd:
                    felrow.Add(clock + (Math.Pow((-2) * Math.Log(r.NextDouble()), 0.5) * Math.Cos(2 * Math.PI * r.NextDouble())) * 3 + 20);//normal(20,3)
                    break;
                case Exit:
                    felrow.Add(clock + (-1 * Math.Log(r.NextDouble())));
                    break;
            }
            FEL.Add(felrow);
        }
//-----------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            //adding headers to TraceDT table...................
            TraceDT.Columns.Add("clock");
            TraceDT.Columns.Add("Currentaction");
            TraceDT.Columns.Add("status1");
            TraceDT.Columns.Add("status2");
            TraceDT.Columns.Add("status3");
            TraceDT.Columns.Add("status4");
            TraceDT.Columns.Add("status5");
            TraceDT.Columns.Add("status6");
            TraceDT.Columns.Add("status7");
            TraceDT.Columns.Add("status8");
            TraceDT.Columns.Add("L1");
            TraceDT.Columns.Add("L2");
            TraceDT.Columns.Add("L3");
            TraceDT.Columns.Add("freechair");
            TraceDT.Columns.Add("rest1Op");
            TraceDT.Columns.Add("rest1t");
            TraceDT.Columns.Add("rest2Op");
            TraceDT.Columns.Add("rest2t");
            TraceDT.Columns.Add("BusyNum1");
            TraceDT.Columns.Add("RestNum1");
            TraceDT.Columns.Add("QNumSt1");
            TraceDT.Columns.Add("BusCount");
            TraceDT.Columns.Add("CarCount");
            TraceDT.Columns.Add("WalkCount");
            TraceDT.Columns.Add("TtimeBus");
            TraceDT.Columns.Add("TtimeCar");
            TraceDT.Columns.Add("TtimeWalk");
            TraceDT.Columns.Add("MaxL1");
            TraceDT.Columns.Add("MaxBusychair");
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            for (int i = 0; i <120; i++)
         FELDT.Columns.Add((i + 1).ToString());
       starter1();
       main();

       //providing the results of simulation in labels(the first day of simulation).......................
       label1.Text = MaxBusyChair.ToString();
       label24.Text = MaxL1.ToString();
       label35.Text = (modatesarfT/7200).ToString();
       label56.Text = (TtimeQSt1/QNumSt1).ToString();
       label50.Text = (TtimeBus/BusCount).ToString();
       label51.Text = (TtimeWalk/WalkCount ).ToString();
       label52.Text = (TtimeCar / CarCount).ToString();
       label99.Text = (TRestSt1 / 4).ToString();
       label109.Text = ((double)UnderCountRest / 4).ToString();
       label122.Text=Counter1.ToString();
       label123.Text=Counter2.ToString();
       label124.Text = Counter3.ToString();
       label129.Text=OrderCount.ToString();
       label130.Text=PayCount.ToString();
       label131.Text=GiveCount.ToString();
       label132.Text = EatCount.ToString();
       label134.Text = CounterExit.ToString();

          //  timer1.Enabled = true;
       for (int i = 2; i <= 10; i++)
       {
           starter();
           main();
//providing the results of simulation in labels(the first day of simulation).

           if (i == 2)
           {
               label2.Text = MaxBusyChair.ToString();
               label25.Text = MaxL1.ToString();
               label36.Text = (modatesarfT / 7200).ToString();
               label59.Text = (TtimeQSt1 / QNumSt1).ToString();
               label69.Text = (TtimeBus /BusCount).ToString();
               label78.Text = (TtimeWalk / WalkCount).ToString();
               label87.Text = (TtimeCar / CarCount).ToString();
               label100.Text = (TRestSt1 / 4).ToString();
               label110.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 3)
           {
               label3.Text = MaxBusyChair.ToString();
               label26.Text = MaxL1.ToString();
               label37.Text = (modatesarfT / 7200).ToString();
               label60.Text = (TtimeQSt1 / QNumSt1).ToString();
               label70.Text = (TtimeBus/BusCount).ToString();
               label79.Text = (TtimeWalk / WalkCount).ToString();
               label88.Text = (TtimeCar / CarCount).ToString();
               label101.Text = (TRestSt1 / 4).ToString();
               label111.Text = ((double)UnderCountRest / 4).ToString();

           }
           else if (i == 4)
           {
               label4.Text = MaxBusyChair.ToString();
               label27.Text = MaxL1.ToString();
               label38.Text = (modatesarfT / 7200).ToString();
               label61.Text = (TtimeQSt1 / QNumSt1).ToString();
               label71.Text = (TtimeBus/BusCount ).ToString();
               label80.Text = (TtimeWalk / WalkCount).ToString();
               label89.Text = (TtimeCar / CarCount).ToString();
               label102.Text = (TRestSt1 / 4).ToString();
               label112.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 5)
           {
               label5.Text = MaxBusyChair.ToString();
               label28.Text = MaxL1.ToString();
               label39.Text = (modatesarfT / 7200).ToString();
               label62.Text = (TtimeQSt1 / QNumSt1).ToString();
               label72.Text = (TtimeBus /BusCount).ToString();
               label81.Text = (TtimeWalk / WalkCount).ToString();
               label90.Text = (TtimeCar / CarCount).ToString();
               label103.Text = (TRestSt1 / 4).ToString();
               label113.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 6)
           {
               label6.Text = MaxBusyChair.ToString();
               label29.Text = MaxL1.ToString();
               label40.Text = (modatesarfT / 7200).ToString();
               label63.Text = (TtimeQSt1 / QNumSt1).ToString();
               label73.Text = (TtimeBus/BusCount).ToString();
               label82.Text = (TtimeWalk / WalkCount).ToString();
               label91.Text = (TtimeCar / CarCount).ToString();
               label104.Text = (TRestSt1 / 4).ToString();
               label114.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 7)
           {
               label7.Text = MaxBusyChair.ToString();
               label30.Text = MaxL1.ToString();
               label41.Text = (modatesarfT / 7200).ToString();
               label64.Text = (TtimeQSt1 / QNumSt1).ToString();
               label74.Text = (TtimeBus /BusCount).ToString();
               label83.Text = (TtimeWalk / WalkCount).ToString();
               label92.Text = (TtimeCar / CarCount).ToString();
               label105.Text = (TRestSt1 / 4).ToString();
               label115.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 8)
           {
               label8.Text = MaxBusyChair.ToString();
               label31.Text = MaxL1.ToString();
               label42.Text = (modatesarfT / 7200).ToString();
               label65.Text = (TtimeQSt1 / QNumSt1).ToString();
               label75.Text = (TtimeBus/BusCount ).ToString();
               label84.Text = (TtimeWalk / WalkCount).ToString();
               label93.Text = (TtimeCar / CarCount).ToString();
               label106.Text = (TRestSt1 / 4).ToString();
               label116.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 9)
           {
               label9.Text = MaxBusyChair.ToString();
               label32.Text = MaxL1.ToString();
               label43.Text = (modatesarfT / 7200).ToString();
               label66.Text = (TtimeQSt1 / QNumSt1).ToString();
               label76.Text = (TtimeBus/BusCount).ToString();
               label85.Text = (TtimeWalk / WalkCount).ToString();
               label94.Text = (TtimeCar / CarCount).ToString();
               label107.Text = (TRestSt1 / 4).ToString();
               label117.Text = ((double)UnderCountRest / 4).ToString();
           }
           else if (i == 10)
           {
               label10.Text = MaxBusyChair.ToString();
               label33.Text = MaxL1.ToString();
               label44.Text = (modatesarfT / 7200).ToString();
               label67.Text = (TtimeQSt1 / QNumSt1).ToString();
               label77.Text = (TtimeBus /BusCount).ToString();
               label86.Text = (TtimeWalk / WalkCount).ToString();
               label95.Text = (TtimeCar / CarCount).ToString();
               label108.Text = (TRestSt1 / 4).ToString();
               label118.Text = ((double)UnderCountRest / 4).ToString();
           }

       }
       label46.Text = ((double)(double.Parse(label99.Text) + double.Parse(label100.Text) + double.Parse(label101.Text) + double.Parse(label102.Text) + double.Parse(label103.Text) + double.Parse(label104.Text) + double.Parse(label105.Text) + double.Parse(label106.Text) + double.Parse(label107.Text) + double.Parse(label108.Text)) / 10).ToString();//TRestSt1
            //***********TrestSt1
       label54.Text = ((double)(double.Parse(label109.Text) + double.Parse(label110.Text) + double.Parse(label111.Text) + double.Parse(label112.Text) + double.Parse(label113.Text) + double.Parse(label114.Text) + double.Parse(label115.Text) + double.Parse(label116.Text) + double.Parse(label117.Text) + double.Parse(label118.Text)) / 10).ToString();
       label22.Text = ((double)(int.Parse(label1.Text) + int.Parse(label2.Text) + int.Parse(label3.Text) + int.Parse(label4.Text) + int.Parse(label5.Text) + int.Parse(label6.Text) + int.Parse(label7.Text) + int.Parse(label8.Text) + int.Parse(label9.Text) + int.Parse(label10.Text)) / 10).ToString();
       label57.Text = ((double)(int.Parse(label24.Text) + int.Parse(label25.Text) + int.Parse(label26.Text) + int.Parse(label27.Text) + int.Parse(label28.Text) + int.Parse(label29.Text) + int.Parse(label30.Text) + int.Parse(label31.Text) + int.Parse(label32.Text) + int.Parse(label33.Text)) / 10).ToString();
       label58.Text = ((double)(double.Parse(label35.Text) + double.Parse(label36.Text) + double.Parse(label37.Text) + double.Parse(label38.Text) + double.Parse(label39.Text) + double.Parse(label40.Text) + double.Parse(label41.Text) + double.Parse(label42.Text) + double.Parse(label43.Text) + double.Parse(label44.Text)) / 10).ToString();


       label96.Text = ((double)(double.Parse(label50.Text) + double.Parse(label69.Text) + double.Parse(label70.Text) + double.Parse(label71.Text) + double.Parse(label72.Text) + double.Parse(label73.Text) + double.Parse(label74.Text) + double.Parse(label75.Text) + double.Parse(label76.Text) + double.Parse(label77.Text)) / 10).ToString();

       label68.Text = ((double)(double.Parse(label56.Text) + double.Parse(label59.Text) + double.Parse(label60.Text) + double.Parse(label61.Text) + double.Parse(label62.Text) + double.Parse(label63.Text) + double.Parse(label64.Text) + double.Parse(label65.Text) + double.Parse(label66.Text) + double.Parse(label67.Text)) / 10).ToString();

       label97.Text = ((double)(double.Parse(label51.Text) + double.Parse(label78.Text) + double.Parse(label79.Text) + double.Parse(label80.Text) + double.Parse(label81.Text) + double.Parse(label82.Text) + double.Parse(label83.Text) + double.Parse(label84.Text) + double.Parse(label85.Text) + double.Parse(label86.Text)) / 10).ToString();
       label98.Text = ((double)(double.Parse(label52.Text) + double.Parse(label87.Text) + double.Parse(label88.Text) + double.Parse(label89.Text) + double.Parse(label90.Text) + double.Parse(label91.Text) + double.Parse(label92.Text) + double.Parse(label93.Text) + double.Parse(label94.Text) + double.Parse(label95.Text)) / 10).ToString();

        }
       //--------------this function finds the upcoming event and calls its function-------
        void main()
        {
            while (clock <= SimulationTime)
            {
                List<double> CurrentFEL = (from k in FEL orderby k[time] ascending select k).FirstOrDefault();//this part sorts FEL by the time and finds the upcoming event// 
                
                FELMaker();
              
                clock = CurrentFEL[time];

                switch (Convert.ToInt16(CurrentFEL[action]))
                {
                    case remove:
                        CurrentAction = "Remove";
                        Fremove();
                        break;

                    case BusEnter:   //bus arrival
                        CurrentAction = "BusEnter";
                        FBusEnter();
                        break;

                    case WalkEnter://passenger arrival
                        CurrentAction = "WalkEnter";
                        FWalkEnter();
                        break;

                    case CarEnter://car arrival
                        CurrentAction = "CarEnter";
                        FCarEnter();
                        break;

                    case ArriveStation1:
                        CurrentAction = "ArriveStation1";
                        FArriveStation1();
                        break;

                    case ArriveStation2:
                        CurrentAction = "ArriveStation2";
                        FArriveStation2();
                        break;

                    case ArriveStation3:
                        CurrentAction = "ArriveStation3";
                        FArriveStation3();
                        break;

                    case RestTimeGetSt1_1:
                        CurrentAction = "RestTimeGetSt1_1";
                        FRestTimeGetSt1_1();
                        break;

                    case RestTimeGetSt1_2:
                        CurrentAction = "RestTimeGetSt1_2";
                        FRestTimeGetSt1_2();
                        break;

                    case RestTimeGetSt1_3:
                        CurrentAction = "RestTimeGetSt1_3";
                        FRestTimeGetSt1_3();
                        break;


                    case RestTimeGetSt1_4:
                        CurrentAction = "RestTimeGetSt1_4";
                        FRestTimeGetSt1_4();
                        break;

                    case RestTimeGetSt1_5:
                        CurrentAction = "RestTimeGetSt1_5";
                        FRestTimeGetSt1_5();
                        break;
                    case RestTimeGetSt1_6:
                        CurrentAction = "RestTimeGetSt1_6";
                        FRestTimeGetSt1_6();
                        break;
                    case RestTimeEndSt1_1:
                        CurrentAction = "RestTimeEndSt1_1";
                        FRestTimeEndSt1_1();
                        break;
                    case RestTimeEndSt1_2:
                        CurrentAction = "RestTimeEndSt1_2";
                        FRestTimeEndSt1_2();
                        break;

                    case RestTimeEndSt1_3:
                        CurrentAction = "RestTimeEndSt1_3";
                        FRestTimeEndSt1_3();
                        break;


                    case RestTimeEndSt1_4:
                        CurrentAction = "RestTimeEndSt1_4";
                        FRestTimeEndSt1_4();
                        break;

                    case RestTimeEndSt1_5:
                        CurrentAction = "RestTimeEndSt1_5";
                        FRestTimeEndSt1_5();
                        break;
                    case RestTimeEndSt1_6:
                        CurrentAction = "RestTimeEndSt1_6";
                        FRestTimeEndSt1_6();
                        break;
                    case RestTimeGetSt2_7:
                        CurrentAction = "RestTimeGetSt2_7";
                        FRestTimeGetSt2_7();
                        break;

                    case RestTimeGetSt2_8:
                        CurrentAction = "RestTimeGetSt2_8";
                        FRestTimeGetSt2_8();
                        break;
                    case RestTimeEndSt2_7:
                        CurrentAction = "RestTimeEndSt2_7";
                        FRestTimeEndSt2_7();
                        break;

                    case RestTimeEndSt2_8:
                        CurrentAction = "RestTimeEndSt2_8";
                        FRestTimeEnd2_8();
                        break;

                    case OrderEnd1:
                        CurrentAction = "OrderEnd1";
                        FOrderEnd1();

                        break;
                    case OrderEnd2:
                        CurrentAction = "OrderEnd2";
                        FOrderEnd2();
                        break;
                    case OrderEnd3:
                        CurrentAction = "OrderEnd3";
                        FOrderEnd3();
                        break;
                    case OrderEnd4:
                        CurrentAction = "OrderEnd4";
                        FOrderEnd4();
                        break;
                    case OrderEnd5:
                        CurrentAction = "OrderEnd5";
                        FOrderEnd5();
                        break;
                    case OrderEnd6:
                        CurrentAction = "OrderEnd6";
                        FOrderEnd6();
                        break;

                    case PayEnd1:
                        CurrentAction = "PayEnd1";
                        FpayEnd1();
                        break;

                    case PayEnd2:
                        CurrentAction = "PayEnd2";
                        FpayEnd2();
                        break;

                    case PayEnd3:
                        CurrentAction = "PayEnd3";
                        FpayEnd3();
                        break;

                    case PayEnd4:
                        CurrentAction = "PayEnd4";
                        FpayEnd4();
                        break;

                    case PayEnd5:
                        CurrentAction = "PayEnd5";
                        FpayEnd5();
                        break;
                    case PayEnd6:
                        CurrentAction = "PayEnd6";
                        FPayEnd6();
                        break;

                    case GiveEnd7:
                        CurrentAction = " GiveEnd7";
                        FGiveEnd7();
                        break;

                    case GiveEnd8:
                        CurrentAction = " GiveEnd8";
                        FGiveEnd8();
                        break;

                    case EatEnd:
                        CurrentAction ="EatEnd";
                        FEatEnd();
                        break;

                    case Exit:
                        CurrentAction = "Exit";
                        FExit();
                        break;

                }
                TraceMaker();
                
                FEL.Remove(CurrentFEL);
            }
         
        }

        //-----------------------functions-----------------
        void FBusEnter()
        {
            Random r = new Random();
           double a;
           a = (Math.Pow((-2) * Math.Log(r.NextDouble()), 0.5) * Math.Cos(2 * Math.PI * r.NextDouble())); //generate a random number from standard normal equation//
           double x = a * (Math.Sqrt(30)) +30;
           double y = Math.Abs(x);
           int z = (int)Math.Round(y);
           BusCount = BusCount + z; //update number of poeple enter with bus

            for (int j = 1; j <= z; j++)
            {
                FELConstructor(ArriveStation1);//update the number of people who enter station 1 with z// 
                List<double> s=new List<double>();
                s.Add(1); //save the type of arrival to the restaurant would be updated- 1 for the bus
                s.Add(clock); //save the arrival time
                SaveEntersToSt.Add(s);

            }
        }
        //----------------------------------------
        void FWalkEnter()
        {
            WalkCount = WalkCount + 1;
            FELConstructor(ArriveStation1);
            FELConstructor(WalkEnter);
            List<double> s = new List<double>();
            s.Add(2);//save the type of arrival to the restaurant would be updated- 2 for passengers//
            s.Add(clock);//save the arrival time
            SaveEntersToSt.Add(s);
        }
        //------------------------------------
        void FCarEnter()
        {
          //calculating the number of people in the car, according to probability distribution of the number of people in the car
            Random r = new Random();
            double temp = r.NextDouble(); 

            if (temp <= 0.2)
                CarCount = CarCount + 1;
            else if (temp <= 0.5)
                CarCount = CarCount + 2;
            else if (temp <= 0.8)
                CarCount = CarCount + 3;
            else
                CarCount = CarCount + 4;


            if (temp <= 0.2)
            {
                FELConstructor(ArriveStation1); //build the number of arrival to station 1 according to the number of people in the car-1 in this case//
                List<double> s = new List<double>();
                s.Add(3);//save the type of arrival to the restaurant would be updated- 3 for the car//
                s.Add(clock);//save the arrival time
                SaveEntersToSt.Add(s);
            }
            else if (temp <= 0.5)
            {
                for (int i = 1; i <= 2; i++)//build the number of arrival to station 1 according to the number of people in the car-2 in this case//
                {
                    FELConstructor(ArriveStation1);
                    List<double> s = new List<double>();
                    s.Add(3);
                    s.Add(clock);
                    SaveEntersToSt.Add(s);
                }
            }
            else if (temp <= 0.8) 
            {
                for (int i = 1; i <= 3; i++)//build the number of arrival to station 1 according to the number of people in the car-3 in this case//
                {
                    FELConstructor(ArriveStation1);
                    List<double> s = new List<double>();
                    s.Add(3);
                    s.Add(clock);
                    SaveEntersToSt.Add(s);
                }
            }
            else
            {
                for (int i = 1; i <= 4; i++)//build the number of arrival to station 1 according to the number of people in the car-4 in this case//
                {
                    FELConstructor(ArriveStation1);
                    List<double> s = new List<double>();
                    s.Add(3);
                    s.Add(clock);
                    SaveEntersToSt.Add(s);
                }
            }
           
            FELConstructor(CarEnter);

        }

 //---------------------arrival to the 1st station------------------------------
        void FArriveStation1()
        {
            Counter1++;
            int temp;//the number of free employees
            temp = 6 - (BusyNum1 + RestNum1);
            if (temp == 0) //there would be a line for a station when there is no free employee//
            {
                List<double> s = new List<double>();
                s.Add(1);
                s.Add(clock);
                SaveToQ.Add(s);
               
                L1++;
                if (MaxL1 < L1)  //update maximum length of the line//
                    MaxL1 = L1;

            }
            else
            {

                if (status1 == free) //if the employee is free, he/she would become busy by the arrival of a new customer to station 1//
                {
                    status1 = busy;
                    BusyNum1++;
                    FELConstructor(OrderEnd1);

                }
                else if (status2 == free)
                {
                    status2 = busy;
                    BusyNum1++;
                    FELConstructor(OrderEnd2);
                }
                else if (status3 == free)
                {
                    status3 = busy;
                    BusyNum1++;
                    FELConstructor(OrderEnd3);
                }
                else if (status4 == free)
                {
                    status4 = busy;
                    BusyNum1++;
                    FELConstructor(OrderEnd4);
                }
                else if (status5 == free)
                {
                    status5 = busy;
                    BusyNum1++;
                    FELConstructor(OrderEnd5);
                }
                else if (status6 == free)
                {
                    status6 = busy;
                    BusyNum1++;
                    FELConstructor(OrderEnd6);//cunstruct the next event
                }
            }
        }


//-----------------------arrival to the 2nd station---------   
        void FArriveStation2()
        {
            Counter2++;
            int temp;//the number of free emplyees
            temp = 2 - (BusyNum2 + RestNum2);
            if (temp == 0)//if there is no free employees the customers would form a line
                L2++;
            else
            {

                if (status7 == free) //if the employee is free he/she would become busy//
                {
                    status7 = busy;
                    BusyNum2++;
                    FELConstructor(GiveEnd7);//cunstruct the next event
                }
                else
                {
                    if (status8 == free)
                    {
                        status8 = busy;
                        BusyNum2++;
                        FELConstructor(GiveEnd8);//cunstruct the next event
                    }

                }
            }
        }
        //-----------------------arrival to station 3(dining room)-----------

        void FArriveStation3()
        {
            Counter3++;
            if (FreeChair == 0) //when there is no free chairs, customers would form a line//
                L3++;
            else
            {           
               
                FreeChair--;//decreasing number of free chairs//
                if (30 - FreeChair > MaxBusyChair)//updating maximum number of busy chairs 
                {
                    MaxBusyChair = 30 - FreeChair;
                }
                FELConstructor(EatEnd);//cunstruct the next event
                List<double> save = new List<double>();
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveChair.Add(save);


            }
        }



//---------------------------------the start of rest time for employee1 in station1-------------------------------------    
        void FRestTimeGetSt1_1()//if the employee is free would go for rest otherwise he/she would start resting after finishing his/her duty//
        {
            if (status1 == free) 
            {
                List<double> save = new List<double>();
                status1 = rest;
                RestNum1++;
                FELConstructor(RestTimeEndSt1_1);
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveRestSt1.Add(save);
            }
            else
            {
                UnderCountRest++;
            }
            
            FELConstructor(RestTimeGetSt1_2);//cunstruct the next event
        }

 //----------------------------the start of rest time for employee2 in station1-------------//
        void FRestTimeGetSt1_2()//if the employee is free would go for rest otherwise he/she would start resting after finishing his/her duty//
        {
            if (status2 == free)
            {
                List<double> save = new List<double>();
                status2 = rest;
                RestNum1++;
                FELConstructor(RestTimeEndSt1_2);
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveRestSt1.Add(save);

            }
            else
            {
                UnderCountRest++;
            }
           
            FELConstructor(RestTimeGetSt1_3);
        }

        //------------------------the start of rest time for employee3 in station1-----------------//
        void FRestTimeGetSt1_3()//if the employee is free would go for rest otherwise he/she would start resting after finishing his/her duty//
        {
            if (status3 == free)
            {
                List<double> save = new List<double>();
                status3 = rest;
                RestNum1++;
                FELConstructor(RestTimeEndSt1_3);
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveRestSt1.Add(save);
            }
            else
            {
                UnderCountRest++;
            }

            FELConstructor(RestTimeGetSt1_4);
        }

        //----------------------------the start of rest time for employee4 in station1--------------//
        void FRestTimeGetSt1_4()//if the employee is free would go for rest otherwise he/she would start resting after finishing his/her duty//
        {
            if (status4 == free)
            {
                List<double> save = new List<double>();
                status4 = rest;
                RestNum1++;
                FELConstructor(RestTimeEndSt1_4);
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveRestSt1.Add(save);
            }
            else
            {
                UnderCountRest++;
            }

            FELConstructor(RestTimeGetSt1_5);
        }

        //--------------------------the start of rest time for employee5 in station1---------------//
        void FRestTimeGetSt1_5()//if the employee is free would go for rest otherwise he/she would start resting after finishing his/her duty//
        {
            if (status5 == free)
            {
                List<double> save = new List<double>();
                status5 = rest;
                RestNum1++;
                FELConstructor(RestTimeEndSt1_5);
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveRestSt1.Add(save);
            }
            else
            {
                UnderCountRest++;
            }

            FELConstructor(RestTimeGetSt1_6);
        }

        //----------------------------the start of rest time for employee6 in station1-------------//
        void FRestTimeGetSt1_6()//if the employee is free would go for rest otherwise he/she would start resting after finishing his/her duty//
        {
            if (status6 == free)
            {
                List<double> save = new List<double>();
                status6 = rest;
                RestNum1++;
                FELConstructor(RestTimeEndSt1_6);
                save.Add(clock);
                save.Add(FEL[FEL.Count() - 1][1]);
                SaveRestSt1.Add(save);
            }
            else
            {
                UnderCountRest++;
            }
  
            FELConstructor(RestTimeGetSt1_1);
        }

        //------------------------the end of the rest time for employee1 in station1-----------------//
        void FRestTimeEndSt1_1()
        {
            TCountRest++;

            for (int i = 0; i < SaveRestSt1.Count(); i++)
            {
                if (SaveRestSt1[i][1] == clock)
                {
                    TRestSt1 = TRestSt1 + (clock - SaveRestSt1[i][0]);
                    SaveRestSt1.Remove(SaveRestSt1[i]);
                   
                    break;
                }

                
            }
            
           //after getting rest we have to detrmine the status of employee, if the length of line is zero, the employee is going to be free otherwise he/she will be busy//
            if (L1 == 0)
            {
                status1 = free;
                RestNum1--;
            }
            else
            {
                L1--;
                if (clock != 240)
                {
                    status1 = busy;
                    BusyNum1++;
                    RestNum1--;
                    FELConstructor(OrderEnd1);
                }
                else
                {
                    status1 = free;
                    RestNum1--;
                }

            }
            Rest1op = 2;//update the next person who can rest the next time
            if (clock == 60)
                Rest1t = 110;

            else if (clock == 180)
                Rest1t = 230;


        }


        //------------------------------the end of the rest time for employee2 in station1-----------//
        void FRestTimeEndSt1_2()
        {
            TCountRest++;

            for (int i = 0; i < SaveRestSt1.Count(); i++)
            {
                if (SaveRestSt1[i][1] == clock)
                {
                    TRestSt1 = TRestSt1 + (clock - SaveRestSt1[i][0]);
                    //if (clock - SaveRestSt1[i][0] < 10)
                    //    UnderCountRest++;
                    
                    SaveRestSt1.Remove(SaveRestSt1[i]);
                 

            break;
                }
               
            }


            if (L1 == 0)
            {
                status2 = free;
                RestNum1--;
            }
            else
            {
                L1--;
                if (clock != 240)
                {
                    status2 = busy;
                    BusyNum1++;
                    RestNum1--;
                    FELConstructor(OrderEnd2);
                }
                else
                {
                    status2 = free;
                    RestNum1--;
                }

            }
            Rest1op = 3;
            if (clock == 120)
                Rest1t = 170;
            else if (clock == 240)
                Rest1t = 50;

        }


        //--------------------------the end of the rest time for employee3 in station1---------------//
        void FRestTimeEndSt1_3()
        {
            TCountRest++;

            for (int i = 0; i < SaveRestSt1.Count(); i++)
            {
                if (SaveRestSt1[i][1] == clock)
                {
                    TRestSt1 = TRestSt1 + (clock - SaveRestSt1[i][0]);
                    //if (clock - SaveRestSt1[i][0] < 10)
                    //    UnderCountRest++;
                    SaveRestSt1.Remove(SaveRestSt1[i]);
                    
            break;
                }

              
            }

            if (L1 == 0)
            {
                status3 = free;
                RestNum1--;
            }
            else
            {
                L1--;
                if (clock != 240)
                {
                    status3 = busy;
                    BusyNum1++;
                    RestNum1--;
                    FELConstructor(OrderEnd3);
                }
                else//clock==240
                {
                    status3 = free;
                    RestNum1--;
                }

            }
            Rest1op = 4;
            if (clock == 60)
                Rest1t = 110;

            else if (clock == 180)
                Rest1t = 230;


        }
        //-------------------------the end of the rest time for employee4 in station1----------//
        void FRestTimeEndSt1_4()
        {
            TCountRest++;
            for (int i = 0; i < SaveRestSt1.Count(); i++)
            {
                if (SaveRestSt1[i][1] == clock)
                {
                    TRestSt1 = TRestSt1 + (clock - SaveRestSt1[i][0]);
                    //if (clock - SaveRestSt1[i][0] < 10)
                    //    UnderCountRest++;
                    SaveRestSt1.Remove(SaveRestSt1[i]);
                    
            break;
                }

                
            }

            if (L1 == 0)
            {
                status4 = free;
                RestNum1--;
            }
            else
            {
                L1--;
                if (clock != 240)
                {
                    status4 = busy;
                    BusyNum1++;
                    RestNum1--;
                    FELConstructor(OrderEnd4);
                }
                else//clock==240
                {
                    status4 = free;
                    RestNum1--;
                }

            }
            Rest1op = 5;

            if (clock == 120)
                Rest1t = 170;

            else if (clock == 240)
                Rest1t = 50;

        }
        //----------------------the end of the rest time for employee5 in station1-------------//
        void FRestTimeEndSt1_5()
        {
            TCountRest++;

            for (int i = 0; i < SaveRestSt1.Count(); i++)
            {
                if (SaveRestSt1[i][1] == clock)
                {

                    TRestSt1 = TRestSt1 + (clock - SaveRestSt1[i][0]);

                    //if (clock - SaveRestSt1[i][0] < 10)
                    //    UnderCountRest++;
                    SaveRestSt1.Remove(SaveRestSt1[i]);

            break;
                }

            }

            if (L1 == 0)
            {
                status5 = free;
                RestNum1--;
            }
            else
            {
                L1--;
                if (clock != 240)
                {
                    status5 = busy;
                    BusyNum1++;
                    RestNum1--;
                    FELConstructor(OrderEnd5);
                }
                else//clock==240
                {
                    status5 = free;
                    RestNum1--;
                }

            }
            Rest1op = 6;
            if (clock == 60)
                Rest1t = 110;

            else if (clock == 180)
                Rest1t = 230;


        }
        //------------------------the end of the rest time for employee6 in station1-----------//
        void FRestTimeEndSt1_6()
        {
            TCountRest++;

            for (int i = 0; i < SaveRestSt1.Count(); i++)
            {
                if (SaveRestSt1[i][1] == clock)
                {
                    TRestSt1 = TRestSt1 + (clock - SaveRestSt1[i][0]);
                    //if (clock - SaveRestSt1[i][0] < 10)
                    //    UnderCountRest++;
                    SaveRestSt1.Remove(SaveRestSt1[i]);
                    

                    break;
                }
              
            }
            if (L1 == 0)
            {
                status6 = free;
                RestNum1--;
            }
            else
            {
                L1--;
                if (clock != 240)
                {
                    status6 = busy;
                    BusyNum1++;
                    RestNum1--;
                    FELConstructor(OrderEnd6);
                }
                else//clock==240
                {
                    status6 = free;
                    RestNum1--;
                }

            }

            Rest1op = 1;
            if (clock == 120)
                Rest1t = 170;

            else if (clock == 240)
                Rest1t = 50;

        }
        //-----------------------the start of the rest time for employee 7 in station 2------------//


        void FRestTimeGetSt2_7()
        {
            if (status7 == free)
            {
                status7 = rest;
                RestNum2++;
                FELConstructor(RestTimeEndSt2_7);
            }
            FELConstructor(RestTimeGetSt2_8);
        }
        //-----------------------the start of the rest time for employee 8 in station 2---------------//

        void FRestTimeGetSt2_8()
        {

            if (status8 == free)
            {
                status8 = rest;
                RestNum2++;
                FELConstructor(RestTimeEndSt2_8);//construct the next event
            }
 
            FELConstructor(RestTimeGetSt2_7);//construct the next event
        }

        //--------------------------the end of the rest time for employee7 in station2---------------------//
        void FRestTimeEndSt2_7()
        {
            if (L2 == 0)
            {
                status7 = free;
                RestNum2--;
            }
            else
            {
                L2--;
                status7 = busy;
                BusyNum2++;
                RestNum2--;
                FELConstructor(GiveEnd7);

            }
            Rest2op = 8;
            if (clock == 60)
                Rest2t = 230;
            else Rest2t = 50;
        }
        //---------------------------the end of the rest time for employee8 in station2------------------//

        void FRestTimeEnd2_8()
        {
            if (L2 == 0)
            {
                status8 = free;
                RestNum2--;
            }
            else
            {
                L2--;
 
                status7 = busy;
                BusyNum2++;
                RestNum2--;
                FELConstructor(GiveEnd8);

            }
            Rest2op = 7;
            if (clock == 60)
                Rest2t = 230;
            else Rest2t = 50;//clock==240
        }
        //--------------the end of ordering---------------//  

        void FOrderEnd1()
        {
            OrderCount++;//increase number of orders by one
            FELConstructor(PayEnd1);//costruct the next event
        }

        void FOrderEnd2()
        {
            OrderCount++;
            FELConstructor(PayEnd2);
        }
        void FOrderEnd3()
        {
            OrderCount++;
            FELConstructor(PayEnd3);
        }
        void FOrderEnd4()
        {
            OrderCount++;
            FELConstructor(PayEnd4);
        }
        void FOrderEnd5()
        {
            OrderCount++;
            FELConstructor(PayEnd5);
        }
        void FOrderEnd6()
        {
            OrderCount++;
            FELConstructor(PayEnd6);
        }
        //----------------------the end of payment(employee 1)----------------//
        void FpayEnd1()
        {
            PayCount++;
            FELConstructor(ArriveStation2);
            if (Rest1op == 1)  //after the end of payment if it is the rest time for the employee, he/she should rest//
            {
                if (clock < Rest1t + 10 & clock > Rest1t)//check if the employee still has tie to rest//
                {
                    List<double> save = new List<double>();
                    status1 = rest;
                    RestNum1++;
                    BusyNum1--;
                    FELConstructor(RestTimeEndSt1_1);
                    save.Add(clock);
                    save.Add(FEL[FEL.Count() - 1][1]);
                    SaveRestSt1.Add(save);
                }
                else
                {
                    if (L1 == 0) //if the length of the line is zero, the employee would become free//
                    {
                        BusyNum1--;
                        status1 = free;
                    }
                    else
                    {
                        if (clock > SaveToQ[0][1])//the first time of payment
                        {
                            TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                            SaveToQ.Remove(SaveToQ[0]);
                            QNumSt1++;
                        }

                        L1--;
                     
                        FELConstructor(OrderEnd1);
                    }
                }
            }
            else
            {
                if (L1 == 0)
                {
                    BusyNum1--;
                    status1 = free;
                }
                else
                {
                    if (clock > SaveToQ[0][1])
                    {
                    TtimeQSt1=TtimeQSt1+(clock-SaveToQ[0][1]) ;
                    SaveToQ.Remove(SaveToQ[0]);
                    QNumSt1++;
                    }



                    L1--;
                   
                    FELConstructor(OrderEnd1);
                }
            }
        }
        //------------------the end of payment(employee 2)---------------//

        void FpayEnd2()
        {
            PayCount++;
            FELConstructor(ArriveStation2);
            if (Rest1op == 2)
            {
                if (clock < Rest1t + 10 & clock > Rest1t)
                {
                    List<double> save = new List<double>();
                    status2 = rest;
                    RestNum1++;
                    BusyNum1--;
                    FELConstructor(RestTimeEndSt1_2);
                    save.Add(clock);
                    save.Add(FEL[FEL.Count() - 1][1]);
                    SaveRestSt1.Add(save);
                }
                else
                {
                    if (L1 == 0)
                    {
                        BusyNum1--;
                        status2 = free;
                    }
                    else
                    {
                        if (clock > SaveToQ[0][1])
                        {
                            TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                            SaveToQ.Remove(SaveToQ[0]);
                            QNumSt1++;
                        }
                        L1--;
                      
                        FELConstructor(OrderEnd2);
                    }
                }
            }
            else
            {
                if (L1 == 0)
                {
                    BusyNum1--;
                    status2 = free;
                }
                else
                {
                    if (clock > SaveToQ[0][1])
                    {
                        TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                        SaveToQ.Remove(SaveToQ[0]);
                        QNumSt1++;
                    }
                    L1--;
                   
                    FELConstructor(OrderEnd2);
                }
            }
        }
        //------------------the end of payment(employee 3)----------------//

        void FpayEnd3()
        {
            PayCount++;
            FELConstructor(ArriveStation2);
            if (Rest1op == 3)
            {
                if (clock < Rest1t + 10 & clock > Rest1t)
                {
                    List<double> save = new List<double>();
                    status3 = rest;
                    RestNum1++;
                    BusyNum1--;
                    FELConstructor(RestTimeEndSt1_3);
                    save.Add(clock);
                    save.Add(FEL[FEL.Count() - 1][1]);
                    SaveRestSt1.Add(save);
                }
                else
                {
                    if (L1 == 0)
                    {
                        BusyNum1--;
                        status3 = free;
                    }
                    else
                    {
                        if (clock > SaveToQ[0][1])
                        {
                            TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                            SaveToQ.Remove(SaveToQ[0]);
                            QNumSt1++;
                        }
                        L1--;
                        
                        FELConstructor(OrderEnd3);
                    }
                }
            }
            else
            {
                if (L1 == 0)
                {
                    BusyNum1--;
                    status3 = free;
                }
                else
                {
                    if (clock > SaveToQ[0][1])
                    {
                        TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                        SaveToQ.Remove(SaveToQ[0]);
                        QNumSt1++;
                    }
                    L1--;
                    
                    FELConstructor(OrderEnd3);
                }
            }
        }
        //------------------the end of payment(employee 4)----------------//

        void FpayEnd4()
        {
            PayCount++;
            FELConstructor(ArriveStation2);
            if (Rest1op == 4)
            {
                if (clock < Rest1t + 10 & clock > Rest1t)
                {
                    List<double> save = new List<double>();
                    status4 = rest;
                    RestNum1++;
                    BusyNum1--;
                    FELConstructor(RestTimeEndSt1_4);
                    save.Add(clock);
                    save.Add(FEL[FEL.Count() - 1][1]);
                    SaveRestSt1.Add(save);
                }
                else
                {
                    if (L1 == 0)
                    {
                        BusyNum1--;
                        status4 = free;
                    }
                    else
                    {
                        if (clock > SaveToQ[0][1])
                        {
                            TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                            SaveToQ.Remove(SaveToQ[0]);
                            QNumSt1++;
                        }
                        L1--;
                       
                        FELConstructor(OrderEnd4);
                    }
                }
            }
            else
            {
                if (L1 == 0)
                {
                    BusyNum1--;
                    status4 = free;
                }
                else
                {
                    if (clock > SaveToQ[0][1])
                    {
                        TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                        SaveToQ.Remove(SaveToQ[0]);
                        QNumSt1++;
                    }
                    L1--;
                   
                    FELConstructor(OrderEnd4);
                }
            }
        }
        //-------------------the end of payment(employee 5)---------------//

        void FpayEnd5()
        {
            PayCount++;
            FELConstructor(ArriveStation2);
            if (Rest1op == 5)
            {
                if (clock < Rest1t + 10 & clock > Rest1t)
                {
                    List<double> save = new List<double>();
                    status5 = rest;
                    RestNum1++;
                    BusyNum1--;
                    FELConstructor(RestTimeEndSt1_5);
                    save.Add(clock);
                    save.Add(FEL[FEL.Count() - 1][1]);
                    SaveRestSt1.Add(save);
                }
                else
                {
                    if (L1 == 0)
                    {
                        BusyNum1--;
                        status5 = free;
                    }
                    else
                    {
                        if (clock > SaveToQ[0][1])
                        {
                            TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                            SaveToQ.Remove(SaveToQ[0]);
                            QNumSt1++;
                        }
                        L1--;
                        
                        FELConstructor(OrderEnd5);
                    }
                }
            }
            else
            {
                if (L1 == 0)
                {
                    BusyNum1--;
                    status5 = free;
                }
                else
                {
                    if (clock > SaveToQ[0][1])
                    {
                        TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                        SaveToQ.Remove(SaveToQ[0]);
                        QNumSt1++;
                    }
                    L1--;
                    
                    FELConstructor(OrderEnd5);
                }
            }
        }
        //-------------------the end of payment(employee 6)---------------//

        void FPayEnd6()
        {
            PayCount++;
            FELConstructor(ArriveStation2);
            if (Rest1op == 6)
            {
                if (clock < Rest1t + 10 & clock > Rest1t)
                {
                    List<double> save = new List<double>();
                    status6 = rest;
                    RestNum1++;
                    BusyNum1--;
                    FELConstructor(RestTimeEndSt1_6);
                    save.Add(clock);
                    save.Add(FEL[FEL.Count() - 1][1]);
                    SaveRestSt1.Add(save);
                }
                else
                {
                    if (L1 == 0)
                    {
                        BusyNum1--;
                        status6 = free;
                    }
                    else
                    {
                        if (clock > SaveToQ[0][1])
                        {
                            TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                            SaveToQ.Remove(SaveToQ[0]);
                            QNumSt1++;
                        }
                        L1--;
                      
                        FELConstructor(OrderEnd6);
                    }
                }
            }
            else
            {
                if (L1 == 0)
                {
                    BusyNum1--;
                    status6 = free;
                }
                else
                {
                    if (clock > SaveToQ[0][1])
                    {
                        TtimeQSt1 = TtimeQSt1 + (clock - SaveToQ[0][1]);
                        SaveToQ.Remove(SaveToQ[0]);
                        QNumSt1++;
                    }
                    L1--;
               
                    FELConstructor(OrderEnd6);
                }
            }
        }
        //----------------giving the customer his/her order(employee 7)-----------------

        void FGiveEnd7() //station2
        {
            GiveCount++;
            FELConstructor(ArriveStation3);
            if (Rest2op == 7) //if it is the rest time for the employee, he/she would start resting//
            {
                if (clock < Rest2t + 10 & clock > Rest2t)
                {
                    status7 = rest;
                    RestNum2++;
                    BusyNum2--;
                    FELConstructor(RestTimeEndSt2_7);//construct the next event
                }
                else //if the the rest time has passed , the employee would become free or busy according to the length of the line//
                {
                    if (L2 == 0)
                    {
                        BusyNum2--;
                        status7 = free;
                    }
                    else
                    {
                        L2--;
                       
                        FELConstructor(GiveEnd7);
                    }
                }
            }
            else
            {
                if (L2 == 0)
                {
                    BusyNum2--;
                    status7 = free;
                }
                else
                {
                    L2--;
                 
                    FELConstructor(GiveEnd7);
                }
            }

        }
        //--------------------------giving the customer his/her order(employee 8)---------------//

        void FGiveEnd8() //  station 2
        {
            GiveCount++;
            FELConstructor(ArriveStation3);
            if (Rest2op == 8)
            {
                if (clock < Rest2t + 10 & clock > Rest2t)
                {
                    status8 = rest;
                    RestNum2++;
                    BusyNum2--;
                    FELConstructor(RestTimeEndSt2_8);
                }
                else
                {
                    if (L2 == 0)
                    {
                        BusyNum2--;
                        status8 = free;
                    }
                    else
                    {
                        L2--;
                     
                        FELConstructor(GiveEnd8);
                    }
                }
            }
            else
            {
                if (L2 == 0)
                {
                    BusyNum2--;
                    status8 = free;
                }
                else
                {
                    L2--;
                   
                    FELConstructor(GiveEnd8);
                }
            }

        }
        //--------------------------the end of serving the food-----------------------


        void FEatEnd()
        {
            EatCount++;
            double modatesarf = 0;
         
            int b = SaveChair.Count();

            for (int i = 0; i < b; i++)
            {
                if (SaveChair[i][1] == clock)//calculating the length of the time for serving the food//
                {
                    modatesarf = clock - SaveChair[i][0];
                    modatesarfT = modatesarfT+modatesarf;
                    SaveChair.Remove(SaveChair[i]);
                    break;
                }
                
            }


           FELConstructor(Exit);
            if (L3 == 0)
                FreeChair++;
            else
            {
                L3--;
                FELConstructor(EatEnd);
            }
            

        }

        //------------------------leaving the restaurant-------------------//
        void FExit()
        {
            CounterExit++;
                if (SaveEntersToSt[0][0] == 1)
                {
                   
                }
                else if (SaveEntersToSt[0][0] == 2) //calculate the length of the time that type 2 customer is in the restaurant
                {
                        TtimeWalk = TtimeWalk + (clock - SaveEntersToSt[0][1]);
                        SaveEntersToSt.Remove(SaveEntersToSt[0]);//removing the arrival time to station 1 for that customer from the list//
              
                }
                else if (SaveEntersToSt[0][0] == 3)//calculate the length of the time that type 3 customer is in the restaurant
                {
                        TtimeCar = TtimeCar + (clock - SaveEntersToSt[0][1]);
                        SaveEntersToSt.Remove(SaveEntersToSt[0]);

                }
        }
        //----------------------this function removes FEL-----------------------//
        void Fremove()//this function clears the lists and FEL table
        {
            int s = SaveChair.Count();
            for (int i = 0; i <s; i++)
            {
                modatesarfT = modatesarfT + (240 - SaveChair[0][0]);
                SaveChair.Remove(SaveChair[0]);
            }

            int u = SaveEntersToSt.Count();
            for (int i = 0; i < u; i++)//claculating the total length of the time that customers stay in the system after closing the entry door of the restaurant(after 240 min)//
            {
                if (SaveEntersToSt[0][0] == 1)
                {
                    TtimeBus = TtimeBus + (240 - SaveEntersToSt[0][0]);
                    SaveEntersToSt.Remove(SaveEntersToSt[0]);

                }
                else if (SaveEntersToSt[0][0] == 2)
                {
                    TtimeWalk = TtimeWalk + (240 - SaveEntersToSt[0][0]);
                    SaveEntersToSt.Remove(SaveEntersToSt[0]);

                }
                else if (SaveEntersToSt[0][0] == 3)
                {
                    TtimeCar = TtimeCar + (240 - SaveEntersToSt[0][0]);
                    SaveEntersToSt.Remove(SaveEntersToSt[0]);

                }


            }

            int a = FEL.Count();
            for (int j = 0; j < a; j++)
            {
                List<double> del = (from k in FEL select k).FirstOrDefault();
                FEL.Remove(del);
            }
            // empty the list of people in the line
            int b = SaveToQ.Count();
            for (int j = 0; j < b; j++)
            {
                List<double> del = (from k in SaveToQ select k).FirstOrDefault();
                SaveToQ.Remove(del);
            }
            //empty the list of people of different kind who just entered the system
            int c = SaveEntersToSt.Count();
            for (int j = 0; j < c; j++)
            {
                List<double> del = (from k in SaveEntersToSt select k).FirstOrDefault();
                SaveEntersToSt.Remove(del);
            }
            //

            int d = SaveChair.Count();
            for (int j = 0; j < d; j++)
            {
                List<double> del = (from k in SaveChair select k).FirstOrDefault();
                SaveChair.Remove(del);
            }


            int e = SaveRestSt1.Count();
            for (int j = 0; j < e; j++)
            {
                List<double> del = (from k in SaveRestSt1 select k).FirstOrDefault();
                SaveRestSt1.Remove(del);
            }





        }

        //---------------------------------
        void TraceMaker()// a new column of TraceDT table would be created, clock:current time and current action- the other columns would be filled with checking the status of system//
        {

            DataRow row = TraceDT.NewRow();
            row["clock"] = clock;
            row["Currentaction"] = CurrentAction;


            switch (status1)//fillinig the status of employee 1 in TracDT table//
            {
                case free:
                    row["status1"] = "free";
                    break;
                case busy:
                    row["status1"] = "busy";
                    break;
                case rest:
                    row["status1"] = "rest";
                    break;
            }
            //---------------
            switch (status2)
            {
                case free:
                    row["status2"] = "free";
                    break;
                case busy:
                    row["status2"] = "busy";
                    break;
                case rest:
                    row["status2"] = "rest";
                    break;
            }
            //--------------------
            switch (status3)
            {
                case free:
                    row["status3"] = "free";
                    break;
                case busy:
                    row["status3"] = "busy";
                    break;
                case rest:
                    row["status3"] = "rest";
                    break;
            }
            //---------------
            switch (status4)
            {
                case free:
                    row["status4"] = "free";
                    break;
                case busy:
                    row["status4"] = "busy";
                    break;
                case rest:
                    row["status4"] = "rest";
                    break;
            }
            //----------------------
            switch (status5)
            {
                case free:
                    row["status5"] = "free";
                    break;
                case busy:
                    row["status5"] = "busy";
                    break;
                case rest:
                    row["status5"] = "rest";
                    break;
            }
            //------------------------
            switch (status6)
            {
                case free:
                    row["status6"] = "free";
                    break;
                case busy:
                    row["status6"] = "busy";
                    break;
                case rest:
                    row["status6"] = "rest";
                    break;
            }
            //-----------------------------
            switch (status7)
            {
                case free:
                    row["status7"] = "free";
                    break;
                case busy:
                    row["status7"] = "busy";
                    break;
                case rest:
                    row["status7"] = "rest";
                    break;
            }
            //------------------
            switch (status8)
            {
                case free:
                    row["status8"] = "free";
                    break;
                case busy:
                    row["status8"] = "busy";
                    break;
                case rest:
                    row["status8"] = "rest";
                    break;
            }
            row["L1"] = L1;
            row["L2"] = L2;
            row["L3"] = L3;
            row["freechair"] = FreeChair;
            row["Rest1op"] = Rest1op;
            row["Rest1t"] = Rest1t;
            row["Rest2op"] = Rest2op;
            row["Rest2t"] = Rest2t;
            row["BusyNum1"] = BusyNum1;
            row["RestNum1"] = RestNum1;
            row["QNumSt1"] = QNumSt1;
            row["BusCount"] = BusCount;
            row["CarCount"] = CarCount;
            row["WalkCount"] = WalkCount;
            row["TtimeBus"] = TtimeBus;
            row["TtimeCar"] = TtimeCar;
            row["TtimeWalk"] = TtimeWalk;
            row["MaxL1"] = MaxL1;
            row["MaxBusychair"] = MaxBusyChair;
            TraceDT.Rows.Add(row);
            dataGridView1.DataSource = TraceDT;



        }



        void FELMaker()//
        {

            DataRow row1 = FELDT.NewRow();
            DataRow row2 = FELDT.NewRow();
            row1[0] = CurrentAction;
            row2[0] = "clock";
            int i = 1;
            List<List<double>> OrderFEL = (from k in FEL orderby k[time] ascending select k).ToList();//the events and the time of happening would be entered in FEL// 
            foreach (List<double> item in OrderFEL)
            {

                switch (Convert.ToInt16(item[action]))
                {

                    case BusEnter:
                        row1[i] = "BusEnter";
                        break;

                    case WalkEnter:
                        row1[i] = "WalkEnter";
                        break;

                    case CarEnter:
                        row1[i] = "CarEnter";
                        break;

                    case  ArriveStation1:
                        row1[i] = "ArriveStation1";
                            break;

                    case ArriveStation2:
                            row1[i] = "ArriveStation2";
                               break;

                    case ArriveStation3:
                               row1[i] = "ArriveStation3";
                               break;
                    case RestTimeGetSt1_1:
                               row1[i] = "RestTimeGetSt1_1";
                               break;

                    case RestTimeGetSt1_2:
                               row1[i] = "RestTimeGetSt1_2";
                               break;
                    case RestTimeGetSt1_3:
                               row1[i] = "RestTimeGetSt1_3";
                               break;
                    case RestTimeGetSt1_4:
                               row1[i] = "RestTimeGetSt1_4";
                               break;
                    case RestTimeGetSt1_5:
                               row1[i] = "RestTimeGetSt1_5";
                               break;
                    case RestTimeGetSt1_6:
                               row1[i] = "RestTimeGetSt1_6";
                               break;

                        case RestTimeEndSt1_1:
                               row1[i] = "RestTimeEndSt1_1";
                               break;

                    case RestTimeEndSt1_2:
                               row1[i] = "RestTimeEndSt1_2";
                               break;
                    case RestTimeEndSt1_3:
                               row1[i] = "RestTimeEndSt1_3";
                               break;
                    case RestTimeEndSt1_4:
                               row1[i] = "RestTimeEndSt1_4";
                               break;
                    case RestTimeEndSt1_5:
                               row1[i] = "RestTimeEndSt1_5";
                               break;
                    case RestTimeEndSt1_6:
                               row1[i] = "RestTimeEndSt1_6";
                               break;

                    case RestTimeGetSt2_7:
                               row1[i] = "RestTimeGetSt2_7";
                               break;
                    case RestTimeGetSt2_8:
                               row1[i] = "RestTimeGetSt2_8";
                               break;

                    case RestTimeEndSt2_7:
                               row1[i] = "RestTimeEndSt2_7";
                               break;

                    case RestTimeEndSt2_8:
                               row1[i] = "RestTimeEndSt2_8";
                               break;

                    case OrderEnd1:
                               row1[i] = "OrderEnd1";
                               break;

                    case OrderEnd2:
                               row1[i] = "OrderEnd2";
                               break;
                    case OrderEnd3:
                               row1[i] = "OrderEnd3";
                               break;

                    case OrderEnd4:
                               row1[i] = "OrderEnd4";
                               break;
                    case OrderEnd5:
                               row1[i] = "OrderEnd5";
                               break;
                    case OrderEnd6:
                               row1[i] = "OrderEnd6";
                               break;

                    case PayEnd1:
                               row1[i] = "PayEnd1";
                               break;
                    case PayEnd2:
                               row1[i] = "PayEnd2";
                               break;
                    case PayEnd3:
                               row1[i] = "PayEnd3";
                               break;
                    case PayEnd4:
                               row1[i] = "PayEnd4";
                               break;
                    case PayEnd5:
                               row1[i] = "PayEnd5";
                               break;
                    case PayEnd6:
                               row1[i] = "PayEnd6";
                               break;
                  
                    case GiveEnd7:
                               row1[i] = "GiveEnd7";
                               break;


                    case GiveEnd8:
                         row1[i] = "GiveEnd8";
                               break;
                        
                    case EatEnd:
                         row1[i] = "EatEnd";
                               break;
                    case Exit:
                               row1[i] = "Exit";
                               break;
                    case remove:
                               row1[i] = "Remove";//the end of the day;
                               break;
              
                         

                }
                row2[i] = item[time];
                i++;

            }

            FELDT.Rows.Add(row1);
            FELDT.Rows.Add(row2);
            dataGridView2.DataSource = FELDT;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<double> CurrentFEL = (from k in FEL orderby k[time] ascending select k).FirstOrDefault();
            FELMaker();

            clock = CurrentFEL[time];

            switch (Convert.ToInt16(CurrentFEL[action]))
            {
                case remove:
                    CurrentAction = "Remove";
                    Fremove();
                    break;
                case BusEnter:   //bus arrival
                    CurrentAction = "BusEnter";
                    FBusEnter();
                    break;
                case WalkEnter://passenger arrival
                    CurrentAction = "WalkEnter";
                    FWalkEnter();
                    break;

                case CarEnter://car arrival
                    CurrentAction = "CarEnter";
                    FCarEnter();
                    break;

                case ArriveStation1://arriving to station 1
                    CurrentAction = "ArriveStation1";
                    FArriveStation1();
                    break;
                case ArriveStation2://arriving to station 2
                    CurrentAction = "ArriveStation2";
                    FArriveStation2();
                    break;
                case ArriveStation3://arriving to station 3
                    CurrentAction = "ArriveStation3";
                    FArriveStation3();
                    break;
                //------------------------------------------------------------------------
                case RestTimeGetSt1_1://the arrival of the rest time for station 1
                    CurrentAction = "RestTimeGetSt1_1";
                    FRestTimeGetSt1_1();
                    break;
                case RestTimeGetSt1_2:
                    CurrentAction = "RestTimeGetSt1_2";
                    FRestTimeGetSt1_2();
                    break;

                case RestTimeGetSt1_3:
                    CurrentAction = "RestTimeGetSt1_3";
                    FRestTimeGetSt1_3();
                    break;


                case RestTimeGetSt1_4:
                    CurrentAction = "RestTimeGetSt1_4";
                    FRestTimeGetSt1_4();
                    break;

                case RestTimeGetSt1_5:
                    CurrentAction = "RestTimeGetSt1_5";
                    FRestTimeGetSt1_3();
                    break;
                case RestTimeGetSt1_6:
                    CurrentAction = "RestTimeGetSt1_6";
                    FRestTimeGetSt1_6();
                    break;
                //--------------------------------------------------------------
                case RestTimeEndSt1_1://The end of the rest time for the employee in station 1
                    CurrentAction = "RestTimeEndSt1_1";
                    FRestTimeEndSt1_1();
                    break;
                case RestTimeEndSt1_2:
                    CurrentAction = "RestTimeEndSt1_2";
                    FRestTimeEndSt1_2();
                    break;

                case RestTimeEndSt1_3:
                    CurrentAction = "RestTimeEndSt1_3";
                    FRestTimeEndSt1_3();
                    break;


                case RestTimeEndSt1_4:
                    CurrentAction = "RestTimeEndSt1_4";
                    FRestTimeEndSt1_4();
                    break;

                case RestTimeEndSt1_5:
                    CurrentAction = "RestTimeEndSt1_5";
                    FRestTimeEndSt1_5();
                    break;
                case RestTimeEndSt1_6:
                    CurrentAction = "RestTimeEndSt1_6";
                    FRestTimeEndSt1_6();
                    break;
                //---------------------------------------------------------------------
                case RestTimeGetSt2_7:
                    CurrentAction = "RestTimeGetSt2_7";
                    FRestTimeGetSt2_7();
                    break;

                case RestTimeGetSt2_8:
                    CurrentAction = "RestTimeGetSt2_8";
                    FRestTimeGetSt2_8();
                    break;
                //--------------------------------------------------------------
                case RestTimeEndSt2_7:
                    CurrentAction = "RestTimeEndSt2_7";
                    FRestTimeEndSt2_7();
                    break;

                case RestTimeEndSt2_8:
                    CurrentAction = "RestTimeEndSt2_8";
                    FRestTimeEnd2_8();
                    break;
                //------------------------------------------------------------

                case OrderEnd1:
                    CurrentAction = "OrderEnd1";
                    FOrderEnd1();

                    break;
                case OrderEnd2:
                    CurrentAction = "OrderEnd2";
                    FOrderEnd2();
                    break;
                case OrderEnd3:
                    CurrentAction = "OrderEnd3";
                    FOrderEnd3();
                    break;
                case OrderEnd4:
                    CurrentAction = "OrderEnd4";
                    FOrderEnd4();
                    break;
                case OrderEnd5:
                    CurrentAction = "OrderEnd5";
                    FOrderEnd5();
                    break;
                case OrderEnd6:
                    CurrentAction = "OrderEnd6";
                    FOrderEnd6();
                    break;
                //------------------------------------------------------

                case PayEnd1:
                    CurrentAction = "PayEnd1";
                    FpayEnd1();
                    break;

                case PayEnd2:
                    CurrentAction = "PayEnd2";
                    FpayEnd2();
                    break;

                case PayEnd3:
                    CurrentAction = "PayEnd3";
                    FpayEnd3();
                    break;

                case PayEnd4:
                    CurrentAction = "PayEnd4";
                    FpayEnd4();
                    break;

                case PayEnd5:
                    CurrentAction = "PayEnd5";
                    FpayEnd5();
                    break;
                case PayEnd6:
                    CurrentAction = "PayEnd6";
                    FPayEnd6();
                    break;
                //------------------------------------------------------
                case GiveEnd7:
                    CurrentAction = " GiveEnd7";
                    FGiveEnd7();
                    break;

                case GiveEnd8:
                    CurrentAction = " GiveEnd8";
                    FGiveEnd8();
                    break;
                //-----------------------------------------------


                case EatEnd:
                    CurrentAction = "EatEnd";
                    FEatEnd();
                    break;

                case Exit:
                    CurrentAction = "Exit";
                    FExit();
                    break;


            }
            TraceMaker();
               FEL.Remove(CurrentFEL);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }


        public int i { get; set; }

        private void label119_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
