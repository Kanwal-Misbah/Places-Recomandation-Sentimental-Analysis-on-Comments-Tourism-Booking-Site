using Fin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fin.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult IndexUser()
        {
            return View();
        }

        //start of report

        public ActionResult Reports(ReportFilterModel rfm)
        {
            if (rfm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.AddDays(-10).ToString("s");
                rfm.DateFrom = System.DateTime.Today.AddDays(-10);
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(rfm.DateFrom).ToString("s");
            }
            if (rfm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                rfm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(rfm.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_ID.ToString(), Text = x.Cat_Name }).ToList();

            if (rfm.Category == null)
            {
                ViewBag.Package = db.PACKAGEs.Select(x => new SelectListItem { Value = x.Package_id.ToString(), Text = x.Package_title + "(" + x.Package_description + ")" }).ToList();
            }
            else
            {
                ViewBag.Package = db.PACKAGEs.Where(x => x.Category_FID == rfm.Category).Select(x => new SelectListItem { Value = x.Package_id.ToString(), Text = x.Package_title + "(" + x.Package_description + ")" }).ToList();
            }
            var bookings = new List<Booking>();
            if (rfm.Category != null)
            {
                if (rfm.Package != null)
                {
                    bookings = db.Bookings.Where(x => x.Booking_date >= rfm.DateFrom && x.Booking_date <= rfm.DateTo && x.PACKAGE.Category_FID == rfm.Category && x.Package_FID == rfm.Package).ToList();

                }
                else
                {

                    bookings = db.Bookings.Where(x => x.Booking_date >= rfm.DateFrom && x.Booking_date <= rfm.DateTo && x.PACKAGE.Category_FID == rfm.Category).ToList();
                }

            }
            if (rfm.Category == null && rfm.Package == null)
            {
                bookings = db.Bookings.Where(x => x.Booking_date >= rfm.DateFrom && x.Booking_date <= rfm.DateTo).ToList();

            }
            //if (rfm.Package != null)
            //{
            //    bookings = db.Bookings.Where(x => x.Booking_date >= rfm.DateFrom && x.Booking_date <= rfm.DateTo && x.Package_FID == rfm.Package).ToList();

            //}
            //var b = db.Bookings.Select(x => x.Booking_id).ToList();

            //if (rfm.Category != null)
            //{
            //    var p = db.PACKAGEs.Where(x => x.Category_FID == rfm.Category).Select(x => x.Package_id).ToList();

            //    if (rfm.Package != null)
            //    {
            //        p = db.PACKAGEs.Where(x => x.Package_id == rfm.Package).Select(x => x.Package_id).ToList();
            //    }
            //    b = db.Bookings.Where(x => p.Contains(x.Package_FID).Select(x => x.Booking_id)).ToList();
            //}

            //var o = db.Bookings.Where(x => x.Booking_date >= rfm.DateFrom && x.Booking_date <= rfm.DateTo && b.Contains(x.Booking_id)).ToList();
            return View(bookings);
        }

        //end of report

        public ActionResult BusinessUserOrders()
        {
            var bookings = new List<Booking>();
            if (Session["BussinessUser"] != null)
            {
                var b = (Business_User)Session["BussinessUser"];
                bookings = db.Bookings.Where(x => x.PACKAGE.FID_Business_User == b.Bus_User_Id).ToList();
            }
            return View(bookings);
        }
        public ActionResult IndexVisiter()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult RegisterUser()
        {
            return View("RegisterUser");
        }
        [HttpPost]
        public ActionResult RegisterUser(User u)
        {
            return View(u);
        }
        public ActionResult IndexBusinessUser()
        {
            return View();
        }

        public ActionResult LoginBusinessUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginBusinessUser(Business_User b)
        {
            var result1 = db.Business_User.Where(x => x.Bus_Email == b.Bus_Email && x.Bus_Password == b.Bus_Password).FirstOrDefault();
            if (result1 != null)
            {
                Session["BussinessUser"] = result1;
                return RedirectToAction("BusinessUserOrders", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(Admin a)
        {
            var result2 = db.Admins.Where(y => y.Admin_Email == a.Admin_Email && y.Admin_Password == a.Admin_Password).FirstOrDefault();
            if (result2 != null)
            {
                Session["Admin"] = result2;
                return RedirectToAction("Reports", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
        [HttpPost]
        public ActionResult LoginUser(User u)
        {
            var result3 = db.Users.Where(z => z.Email == u.Email && z.Password == u.Password).FirstOrDefault();
            if (result3 != null)
            {
                Session["LoginUser"] = result3;
                return RedirectToAction("IndexVisiter", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
        public ActionResult LoginUser()
        {
            return View();
        }
        public ActionResult LogoutUser()
        {
            Session["LoginUser"] = null;
            return RedirectToAction("IndexVisiter");
        }
        public ActionResult DisplayCategories()
        {

            return View(db.Categories.ToList());



        }
        public ActionResult DisplayDestinations(int? id)
        {
            if (id == null)
            {
                return View(db.Destinations.ToList());
            }
            else
            {
                return View(db.Destinations.Where(d => d.Category_FID == id).ToList());
            }
        }
        //Need to make new method with name of DisplayPackages and  all parameters like Services, User Rating, Categories
        // and When you take result from list you have to add Where Clause where you have to add conditions for these search operations
        public ActionResult DisplayPackages(int? id, string type)
        {
            var pc = new PackCom();
            pc.Buser = db.Business_User.ToList();
            pc.cat = db.Categories.ToList();
            pc.des = db.Destinations.ToList();
            if (id == null)
            {
                // packages= db.PACKAGEs.ToList();
                pc.pack = db.PACKAGEs.ToList();

            }
            else
            {
                if (type == "category")
                {
                    pc.pack = db.PACKAGEs.Where(x => x.Category_FID == id).ToList();
                }
                else if (type == "business")
                {
                    pc.pack = db.PACKAGEs.Where(x => x.FID_Business_User == id).ToList();
                }
                else if (type == "destination")
                {
                    pc.pack = db.PACKAGEs.Where(x => x.Destination_FID == id).ToList();
                }
            }
            return View(pc);
            //pc.pack = new List<PACKAGE>();
            //pc.com = new List<COMMENT>();
            //pc.Buser = new List<Business_User>();
            //pc.cat = new List<Category>();
            //pc.des = new List<Destination>();

            //if (id == null)
            //{
            //    pc.pack = db.PACKAGEs.ToList();
            //    pc.com = db.COMMENTs.ToList();
            //    pc.Buser = db.Business_User.ToList();
            //    pc.cat = db.Categories.ToList();
            //    pc.des = db.Destinations.ToList();

            //    return View(pc);
            //}
            //else
            //{

            //    var packages = db.PACKAGEs.Where(p => p.Destination_FID == id).ToList();
            //    var package = db.PACKAGEs.Where(p => p.Category_FID == id).ToList();

            //    var pkgByDestinationId = packages.FirstOrDefault(u => u.Destination_FID == id);
            //    var pkgByCategoryId = package.FirstOrDefault(u => u.Category_FID == id);

            //    var com = pkgByDestinationId.COMMENTs;
            //    if (com != null)
            //    {
            //        pc.com = new List<COMMENT>();
            //        pc.com.AddRange(com);
            //    }
            //    //var comm = pkgByCategoryId.COMMENTs;
            //    //if (comm != null)
            //    //{
            //    //    pc.com = new List<COMMENT>();
            //    //    pc.com.AddRange(comm);
            //    //}
            //    //var pckg = pkgByCategoryId.Package_title;
            //    //if (pckg != null)
            //    //{
            //    //    pc.pack = new List<PACKAGE>();
            //    //    pc.pack.Add(pkgByDestinationId);
            //    //}
            //    //if (pkgByCategoryId != null)
            //    //{
            //    //    pc.pack = new List<PACKAGE>();
            //    //    pc.pack.Add(pkgByCategoryId);

            //    //}
            //    var user = pkgByDestinationId.Business_User;
            //    if (user != null)
            //    {
            //        pc.Buser = new List<Business_User>();
            //        pc.Buser.Add(user);
            //    }
            //    var cat = pkgByDestinationId.Category;
            //    if (cat != null)
            //    {
            //        pc.cat = new List<Category>();
            //        pc.cat.Add(cat);
            //    }




            //    var dest = pkgByDestinationId.Destination;
            //    if (dest != null)
            //    {
            //        pc.des = new List<Destination>();
            //        pc.des.Add(dest);
            //    }


            //    if (pkgByDestinationId != null)
            //    {
            //        pc.pack = new List<PACKAGE>();
            //        pc.pack.Add(pkgByDestinationId);

            //    }

            // return View(pc);
        }


        //// ViewData["Com"] = db.COMMENTs.Where<COMMENT>(a => a.CommentId == id).ToList();
        //if (id == null)
        //{
        //    var packages = db.PACKAGEs.ToList();
        //    return View(packages);
        //}
        //else
        //{
        //    var packages = db.PACKAGEs.Where(p => p.Category_FID == id).ToList();
        //    return View(packages);
        //}
        //}
        public ActionResult PackageDetails(int id)
        {
            var sugestionNames = new List<string>();
            var pck = db.PACKAGEs.Where(x => x.Package_id == id).FirstOrDefault();
            string Baseurl = $"https://walrus-app-2-xj4xv.ondigitalocean.app/Recommendation/{pck.Package_title}";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(Baseurl).GetAwaiter().GetResult();
                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    sugestionNames = JsonConvert.DeserializeObject<List<string>>(result);
                // Do stuff...
            }


            var suggestionPkgs = db.PACKAGEs.Where(x => sugestionNames.Contains(x.Package_title)).ToList();

            TempData["Suggestions"] = suggestionPkgs;

            return View(pck);
        }
        class xyz { public List<string> MyProperty { get; set; } }


        //         if (id == null)
        //         {
        //            return View(pd);
        //    }
        //         else
        //         {
        //        var packages = db.PACKAGEs.Where(p => p.Package_id == id).ToList();
        //        return View(packages);
        //}
        //ViewData["Com"] = db.COMMENTs.Where<COMMENT>(a => a.CommentId == id).ToList();
        //if (id == null)
        //{
        //    var packages = db.PACKAGEs.ToList();
        //    return View(packages);
        //}
        //else
        //{
        //    var packages = db.PACKAGEs.Where(p => p.Category_FID == id).ToList();
        //    return View(packages);

        //}

        //}
        public ActionResult AddToWishList(int id)
        {
            if (Session["LoginUser"] == null)
            {
                TempData["loginMessage"] = "<script> alert(' Please login first for next proceeding ') </script>";
                return RedirectToAction("LoginUser");
            }
            //List<PACKAGE> list;
            //if (Session["mywish"] == null)
            //{
            //    list = new List<PACKAGE>();
            //}
            //else
            //{
            //    list = (List<PACKAGE>)Session["mywish"];
            //}

            var loginUser = (Fin.Models.User)Session["LoginUser"];
            var isPkgAlreadyInWishList = db.WishLists.Any(x => x.Package_FID == id && x.User_FID == loginUser.User_Id);

            if (isPkgAlreadyInWishList)
            {
                TempData["Message"] = "<script> alert(' This pkg already in your WishList ') </script>";

                return RedirectToAction("wishlist");

            }
            var wishlist = new WishList
            {

                Wishlist_Date = DateTime.Now,
                User_FID = loginUser.User_Id,
                Package_FID = id,

            };
            db.WishLists.Add(wishlist);
            db.SaveChanges();
            //list.Add(db.PACKAGEs.Where(p => p.Package_id == id).FirstOrDefault());
            //Session["mywish"] = list;
            return RedirectToAction("wishlist");
        }
        public ActionResult RemoveFromWish(int id)
        {
            //List<PACKAGE> list = (List<PACKAGE>)Session["mywish"];
            //list.RemoveAt(RowNo);
            //Session["mywish"] = list;
            var wish = db.WishLists.FirstOrDefault(x => x.Wishlist_id == id);

            db.WishLists.Remove(wish);
            db.SaveChanges();
            return RedirectToAction("wishlist");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Booking(int? id)
        {

            if (Session["LoginUser"] == null)
            {
                TempData["loginMessage"] = "<script> alert(' Please login first for next proceeding ') </script>";
                return RedirectToAction("LoginUser");
            }
            var pck = db.PACKAGEs.Where(x => x.Package_id == id).FirstOrDefault();


            return View(pck);
            PackCom pc = new PackCom();
            pc.pack = new List<PACKAGE>();
            pc.com = new List<COMMENT>();
            pc.Buser = new List<Business_User>();
            pc.cat = new List<Category>();

            pc.des = new List<Destination>();
            if (id == null)
            {
                pc.pack = db.PACKAGEs.ToList();
                pc.com = db.COMMENTs.ToList();
                pc.Buser = db.Business_User.ToList();
                pc.cat = db.Categories.ToList();
                pc.des = db.Destinations.ToList();

                return View(pc);
            }
            else
            {

                var packages = db.PACKAGEs.Where(p => p.Package_id == id).ToList();
                var pkgByDestinationId = packages.FirstOrDefault(u => u.Package_id == id);

                var com = pkgByDestinationId.COMMENTs;
                if (com != null)
                {
                    pc.com = new List<COMMENT>();
                    pc.com.AddRange(com);
                }
                var user = pkgByDestinationId.Business_User;
                if (user != null)
                {
                    pc.Buser = new List<Business_User>();
                    pc.Buser.Add(user);
                }
                var cat = pkgByDestinationId.Category;
                if (cat != null)
                {
                    pc.cat = new List<Category>();
                    pc.cat.Add(cat);
                }

                var dest = pkgByDestinationId.Destination;
                if (dest != null)
                {
                    pc.des = new List<Destination>();
                    pc.des.Add(dest);
                }

                if (pkgByDestinationId != null)
                {
                    pc.pack = new List<PACKAGE>();
                    pc.pack.Add(pkgByDestinationId);
                }
                return View(pc);
            }
        }

        public ActionResult wishlist()
        {
            if (Session["LoginUser"] == null)
            {
                TempData["loginMessage"] = "<script> alert(' Please login first for next proceeding ') </script>";
                return RedirectToAction("LoginUser");
            }
            var loginUser = (Fin.Models.User)Session["LoginUser"];
            var wishList = db.WishLists.Where(w => w.User_FID == loginUser.User_Id).ToList();

            return View(wishList);
        }
        public ActionResult PayNow()
        {

            return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=kanwalmisbah26@gmail.com&item_name=Fin&return=https://localhost:44353/Home/TourBookedSaved&amount=" + double.Parse(Session["TotalAmount"].ToString()));


        }
        [HttpPost]
        public ActionResult SaveBooking(int numberOfMembers, int packageId)
        {
            var package = db.PACKAGEs.Where(p => p.Package_id == packageId).FirstOrDefault();
            var totalPrice = numberOfMembers * package.Package_Price;
            Session["TotalAmount"] = totalPrice / 285;
            Session["numberOfMembers"] = numberOfMembers;
            Session["package"] = package;


            return RedirectToAction("PayNow");
        }
        public ActionResult TourBookedSaved()
        {
            var numberOfMembers = (int)Session["numberOfMembers"];
            var package = (PACKAGE)Session["package"];
            var loginUser = (Fin.Models.User)Session["loginUser"];
            var packageBooked = new Booking
            {
                Booking_date = DateTime.Now,
                No_of_Members = numberOfMembers,
                Package_FID = package.Package_id,
                Payment_status = "Paid",
                Total_Payment = numberOfMembers * package.Package_Price,
                User_FID = loginUser.User_Id,
                Departure = package.Departure_Address,
            };
            db.Bookings.Add(packageBooked);
            db.SaveChanges();

            if (Session["package"] != null)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("leadtotravel248@gmail.com");
                //Change it
                mail.To.Add(loginUser.Email);
                mail.Subject = "Tour Confirmation";
                mail.Body = "<b>Your Tour Is Confirmed</b>";
                //Add it
                mail.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("leadtotravel248@gmail.com", "ydhbxhtsftqmdojm");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            Session["numberOfMembersCopy"] = Session["numberOfMembers"];
            Session["numberOfMembers"] = null;
            Session["packageCopy"] = Session["package"];
            Session["package"] = null;
            Session["TotalAmount"] = null;
            //Email Send1

            //SMS Send
            return RedirectToAction("TourBooked");

        }
        //public ActionResult Comments()
        //{

        //    var comment = (PACKAGE)Session["comment"];
        //    var loginUser = (Fin.Models.User)Session["loginUser"];
        //    var addcomment = new COMMENT
        //    {
        //        ComentedOn = DateTime.Now,
        //        ComentDescription = numberOfMembers,
        //        Package_FID = package.Package_id,
        //        Payment_status = "Paid",
        //        Total_Payment = numberOfMembers * package.Package_Price,
        //        User_FID = loginUser.User_Id,
        //        Departure = package.Departure_Address,
        //    };
        //}
        public ActionResult TourBooked()
        {

            var o = db.Bookings.ToList();

            return View(o);
        }

        [HttpPost]
        public ActionResult SaveComment(string comment, int pkgId)
        {
            if (Session["LoginUser"] == null)
            {
                TempData["loginMessage"] = "<script> alert(' Please login first for next proceeding ') </script>";

                return RedirectToAction("LoginUser");
            }
            
            HttpClient client = new HttpClient();
            string apiUrl = $"https://starfish-app-iy8hv.ondigitalocean.app/Scoring1/{comment}";
            var response = client.GetAsync(apiUrl).Result;
            var rating = 0;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                int.TryParse(responseContent, out rating);
                
            }
            var loginUser = (Fin.Models.User)Session["LoginUser"];
            var commentEntity = new COMMENT
            {
                ComentDescription = comment,
                ComentedOn = DateTime.Now,
                User_FID = loginUser.User_Id,
                Package_FID = pkgId,
                Rating = rating,
            };
            db.COMMENTs.Add(commentEntity);
            db.SaveChanges();
            TempData["Message"] = "<script> alert(' Thanks for your Feedback ') </script>";

            return RedirectToAction("PackageDetails", new { id = pkgId });
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Recomandation()
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync($"https://walrus-app-t7hhh.ondigitalocean.app/Recommendation/");

                // Ensure the response was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Extract the body or content from the response
                string bodyContent = ExtractBodyContent(responseContent);

                // Pass the body or content to the view
                return View((object)bodyContent);
            }
        }
        public async Task<ActionResult> Recomandationcoments(int id)
        {
            var pkg = db.PACKAGEs.FirstOrDefault(x => x.Package_id == id);

            using (HttpClient client = new HttpClient())
            {
                var com = string.Join(",", pkg.COMMENTs.Select(x => x.ComentDescription).ToList());
                HttpResponseMessage response = await client.GetAsync($"https://walrus-app-t7hhh.ondigitalocean.app/Recommendation/{com}");

                // Ensure the response was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Extract the body or content from the response
                string bodyContent = ExtractBodyContent(responseContent);

                // Pass the body or content to the view
                return View((object)bodyContent);
            }
        }

        private string ExtractBodyContent(string htmlContent)
        {
            // Perform extraction of body or content from the HTML
            // You can use any method or library suitable for parsing HTML, such as HtmlAgilityPack or AngleSharp

            // Here's a simple example using regex to extract the body content
            Regex regex = new Regex("<body>(.*?)</body>", RegexOptions.Singleline);
            Match match = regex.Match(htmlContent);

            // Extract the captured group value
            string bodyContent = match.Groups[1].Value;

            return bodyContent;
        }



    }
}

