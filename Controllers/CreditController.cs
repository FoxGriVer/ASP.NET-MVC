using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankSystem.Models;

namespace BankSystem.Controllers
{
    public class CreditController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowData()
        {
            IEnumerable<Credit> credits = db.Credits;
            return View(credits);
        }

        public ActionResult Delete(int id)
        {
            Credit creditDel = db.Credits.FirstOrDefault(x => x.Id == id);
            if(creditDel != null)
            {
                db.Credits.Remove(creditDel);
                db.SaveChanges();
            }
            ShowData();
            return View("ShowData");
        }

        [HttpGet]
        public ActionResult TakeCredit()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult TakeCredit(Credit takedCredit)
        {
            db.Credits.Add(takedCredit);
            db.SaveChanges();
            ShowData();
            return View("ShowData");
        }

        public ActionResult CountPercent(int id)
        {
            Credit creditForCounting = db.Credits.FirstOrDefault(x => x.Id == id);
            if (creditForCounting != null)
            {
                int AmmountOfCredit = creditForCounting.AmmountOfMoney;
                int NumberOfMonths = creditForCounting.NumberOfMonths;
                double Rate = creditForCounting.Percent;

                double Progression = 1 + Rate / 1200;
                double mej1 = Math.Pow(Progression,NumberOfMonths);
                double KoefEjPl = mej1 * (Progression - 1) / (mej1 - 1);

                double AmmountOfCreditWithPercents = KoefEjPl * AmmountOfCredit;
                string result = AmmountOfCreditWithPercents.ToString("#");
                ViewBag.Ammount = result;
                ViewBag.Credit = creditForCounting;
            }
            return View("CountPercent");
        }

        public ActionResult CountDiffPercent(int id)
        {
            Credit creditForCounting = db.Credits.FirstOrDefault(x => x.Id == id);

            if (creditForCounting != null)
            {
                int AmmountOfCredit = creditForCounting.AmmountOfMoney;
                int NumberOfMonths = creditForCounting.NumberOfMonths;
                double Rate = creditForCounting.Percent;
                int f = AmmountOfCredit / NumberOfMonths; //сумма в счёт погашения основного долга (одна и та же каждый месяц) 

                double[] array = new double[NumberOfMonths];
                List<Diffs> list_diffs = new List<Diffs>();
                for (int i = 1; i <= array.Length; i++)
                {
                    Diffs dif = new Diffs();

                    double res1 = (AmmountOfCredit - f * (i - 1)) * Rate / 1200; //проценты, начисленные за пользование кредитом на i-м месяце.
                    double res2 = f + res1; // платеж в месяц
                    dif.month = i;

                    string result1 = res1.ToString("#");
                    string result2 = res2.ToString("#");
                    dif.p_i = result1;
                    dif.AmmountPerMonth = result2;
                    list_diffs.Add(dif);                  
                }
                return View(list_diffs);
            }
            return View();
        }

        public class Diffs
        {
            public string p_i;
            public string AmmountPerMonth;
            public int month = 0;
        }
    }
}