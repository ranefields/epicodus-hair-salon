using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using HairSalon.ViewModels;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            return View(model);
        }
        [HttpPost("/stylists/add")]
        public ActionResult AddStylist()
        {
            string name = Request.Form["stylist-name"];
            string phone = Request.Form["stylist-phone"];
            string email = Request.Form["stylist-email"];
            Stylist stylist = new Stylist(name, phone, email);
            stylist.Save();
            return Redirect("/");
        }
        [HttpGet("/stylists/{stylistId}")]
        public ActionResult StylistDetails(int stylistId)
        {
            StylistDetailsModel model = new StylistDetailsModel(stylistId);
            return View(model);
        }
        [HttpPost("/stylists/{stylistId}/clients/add")]
        public ActionResult AddClientToStylist(int stylistId)
        {
            string name = Request.Form["client-name"];
            string phone = Request.Form["client-phone"];
            Client client = new Client(name, phone, stylistId);
            client.Save();
            return Redirect($"/stylists/{stylistId}");
        }
        [HttpPost("/stylists/{stylistId}/clients/remove/{clientId}")]
        public ActionResult RemoveClient(int stylistId, int clientId)
        {
            Client.RemoveAtId(clientId);
            return Redirect($"/stylists/{stylistId}");
        }
        [HttpPost("/stylists/{stylistId}/clients/update/{clientId}")]
        public ActionResult UpdateClient(int stylistId, int clientId)
        {
            string name = Request.Form["client-name"];
            string phone = Request.Form["client-phone"];
            Client newClientInfo = new Client(name, phone, stylistId);
            Client.UpdateAtId(clientId, newClientInfo);
            return Redirect($"/stylists/{stylistId}");
        }
    }
}
