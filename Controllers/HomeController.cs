using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankSystem.Models;

namespace BankSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ShowRate();
            return View();
        } 
              

        public ActionResult ShowRate()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string response = wc.DownloadString("https://www.cbr.ru/");
            string rateDollar = System.Text.RegularExpressions.Regex.Match(response, @"</i>([0-9]+\,[0-9]+)</div>").Groups[1].Value;
            string rateEuro = System.Text.RegularExpressions.Regex.Match(response, @"</i>([0-9]+\,[0-9]+)</div>
                  </td>
                </tr>
              </tbody>
            </table>").Groups[1].Value;
            
            ViewBag.Dollar = rateDollar;
            ViewBag.Euro = rateEuro;
            return View();
        }

        //public ActionResult DropDb()
        //{
        //    foreach(Credit b in db.Credits)
        //    {
        //        db.Credits.Remove(b);
        //    }
        //    db.SaveChanges();
        //    return View();
        //}
       
    }
}