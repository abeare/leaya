using electrocalculator.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using electrocalculator.Analysis;
namespace electrocalculator.Controllers
{
    public class calculatorController : Controller
    {
        // GET: Calculator
        [HttpGet]
       
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        
        public ActionResult Index(components component)
        {
            components comp = new components();
            comp.Nodes = component.Nodes;
            comp.Resistor = component.Resistor;
            comp.Capacitor = component.Capacitor;
            comp.Inductor = component.Inductor;
            comp.kp = component.kp;
            comp.km = component.km;
            Session["components"] = comp;
           

            bool checkForm=checkForIntValue(component);
            if (checkForm)
                return redirections(comp);
            else
                return RedirectToAction("noComponent");

        }

        
        [HttpGet]
        public ActionResult resistor()
        {
            if (Session["components"] == null) return RedirectToAction("noComponent"); 
            return View();

        }

        [HttpPost]
        public ActionResult resistor(FormCollection fc)
        {
            components component = new components();
            components comp = Session["components"] as components;
            component.Nodes = comp.Nodes;
            component.Inductor = comp.Inductor;
            component.Capacitor = comp.Capacitor;
            component.Resistor = null;

            getForm(fc,"r");

            return redirections(component);

        }

        [HttpGet]
        public ActionResult capacitor()
        {
            if (Session["components"] == null) return RedirectToAction("noComponent"); 
            return View();

        }

        [HttpPost]
        public ActionResult capacitor(FormCollection fc)
        {
            components component = new components();
            components comp = Session["components"] as components;
            component.Nodes = comp.Nodes;
            component.Inductor = comp.Inductor;
            component.Capacitor =null;
            component.Resistor = null;

            getForm(fc,"c");

            return redirections(component);

        }

        [HttpGet]
        public ActionResult inductor()
        {
            if (Session["components"] == null) return RedirectToAction("noComponent");
            return View();

        }

        [HttpPost]
        public ActionResult Inductor(FormCollection fc)
        {
            components component = new components();
            components comp = Session["components"] as components;
            component.Nodes = comp.Nodes;
            component.Inductor = null;
            component.Capacitor = null;
            component.Resistor = null;

            getForm(fc, "i");

            return redirections(component);

        }

        public ActionResult resultpage()
        {
           
            LocalAnalysis local_analysis = new LocalAnalysis();
            components comp = Session["components"] as components;
            // if node is not integer
            int i;
            if (!int.TryParse(comp.Nodes, out i))
                return RedirectToAction("noComponent");

            local_analysis.Node = i;
            List<string> RcheckForDouble= Session["Rvalue"] as List<string>;
            List<string> CcheckForDouble = Session["Cvalue"] as List<string>;
            List<string> IcheckForDouble = Session["Ivalue"] as List<string>;
            if (checkFordouble(RcheckForDouble, CcheckForDouble, IcheckForDouble))
            {
                if(Session["Rvalue"] != null)
                { 
                    resistor Resistor = new resistor();
                    Resistor.ResistorsValue = Session["Rvalue"] as List<double>;
                    Resistor.ResistorsPositiveNodes= Session["Rpositive"] as List<int>;
                    Resistor.ResistorsNegativeNodes= Session["Rnegative"] as List<int>;
                    local_analysis.Resistor = Resistor;
                }

                if (Session["Cvalue"] != null)
                {
                    capacitor Capacitor = new capacitor();
                    Capacitor.CapacitorsValue = Session["Cvalue"] as List<double>;
                    Capacitor.CapacitorsPositiveNodes = Session["Cpositive"] as List<int>;
                    Capacitor.CapacitorsNegativeNodes = Session["Cnegative"] as List<int>;

                    local_analysis.Capacitor = Capacitor;
                }

                if (Session["Ivalue"] != null)
                {
                    inductor Inductor = new inductor();
                    Inductor.IductorsValue = Session["Ivalue"] as List<double>;
                    Inductor.IductorsPositiveNodes = Session["Ipositive"] as List<int>;
                    Inductor.IductorsNegativeNodes = Session["Inegative"] as List<int>;

                    local_analysis.Inductor = Inductor;
                }


                // 
                local_analysis.AnalysisTheCircute();

                int kp = comp.kp;
                int km = comp.km;

               double[] result= local_analysis.Answer(kp, km);

                return View(result);
            }
            return RedirectToAction("noComponent");


        }

       
        public ActionResult noComponent()
        {

            return View();

        }



        // My oun methods
        private void getForm(FormCollection fc,string compType)
        {
            if(compType=="r")
            {
                List<string> Rvalues = new List<string>();
                List<string> Rpositive= new List<string>();
                List<string> Rnegative= new List<string>();
                Session["Rvalue"]=null;
                Session["Rpositive"] = null;
                Session["Rnegative"] = null;

                foreach(string var in fc.AllKeys){
                    string v = fc[var];
                    if (var.StartsWith("Resistor"))
                    {
                        Rvalues.Add(v);
                    }

                    if (var.StartsWith("Positive"))
                    {
                        Rpositive.Add(v);
                    }

                    if (var.StartsWith("Negative"))
                    {
                        Rnegative.Add(v);
                    }

                }
                Session["Rvalue"] = Rvalues;
                Session["Rpositive"] = Rpositive;
                Session["Rnegative"] = Rnegative;
            }

            if (compType == "c")
            {
                List<string> Cvalues = new List<string>();
                List<string> Cpositive = new List<string>();
                List<string> Cnegative = new List<string>();
                Session["Cvalue"] = null;
                Session["Cpositive"] = null;
                Session["Cnegative"] = null;

                foreach (string var in fc.AllKeys)
                {
                    string v = fc[var];
                    if (var.StartsWith("Capacitor"))
                    {
                        Cvalues.Add(v);
                    }

                    if (var.StartsWith("Positive"))
                    {
                        Cpositive.Add(v);
                    }

                    if (var.StartsWith("Negative"))
                    {
                        Cnegative.Add(v);
                    }

                }
                Session["Cvalue"] = Cvalues;
                Session["Cpositive"] = Cpositive;
                Session["Cnegative"] = Cnegative;
            }

            if (compType == "i")
            {
                List<string> Ivalues = new List<string>();
                List<string> Ipositive = new List<string>();
                List<string> Inegative = new List<string>();
                Session["Ivalue"] = null;
                Session["Ipositive"] = null;
                Session["Inegative"] = null;

                foreach (string var in fc.AllKeys)
                {
                    string v = fc[var];
                    if (var.StartsWith("Inductor"))
                    {
                        Ivalues.Add(v);
                    }

                    if (var.StartsWith("Positive"))
                    {
                        Ipositive.Add(v);
                    }

                    if (var.StartsWith("Negative"))
                    {
                        Inegative.Add(v);
                    }

                }
                Session["Ivalue"] = Ivalues;
                Session["Ipositive"] = Ipositive;
                Session["Inegative"] = Inegative;
            }

        }

       public RedirectToRouteResult redirections(components component , string last="nolast")
        {
           if(last=="nolast")
           { 
               if(component.Resistor!=null)
               {
                return RedirectToAction("resistor");
               }
               if (component.Capacitor != null)
               {
                   return RedirectToAction("capacitor");
               }
               if (component.Inductor != null)
               {
                   return RedirectToAction("inductor");
               }
                //this i add it 
                return RedirectToAction("resultpage");
            }
           //if (last == "last")
           //{
           //        return RedirectToAction("resultpage");
              
           //}
           return RedirectToAction("noComponent");
        }

       private bool checkForIntValue(components comp)
       {
           try
           {
              
           int Nodes = Convert.ToInt32(comp.Nodes);
           int Resistor = Convert.ToInt32(comp.Resistor);
           int Capacitor = Convert.ToInt32(comp.Capacitor);
           int Inductor = Convert.ToInt32(comp.Inductor);
            return true;
           }
           catch{
               return false;
           }
       }

       private bool checkFordouble(List<string> RList, List<string> CList, List<string> IList)
       {
          
               try
               { 
                if(RList != null)
                   foreach (string var in RList) {
                         double Inductor = Convert.ToDouble(var);
                    }
                if (CList != null)
                    foreach (string var in CList)
                   {
                       double Inductor = Convert.ToDouble(var);
                   }
                if (IList != null)
                    foreach (string var in IList)
                   {
                       double Inductor = Convert.ToDouble(var);
                   }
                if (RList != null)
                {
                    List<double> Rcompvalue = todouble(Session["Rvalue"] as List<string>);
                   List<int> RPcompvalue = toint(Session["Rpositive"] as List<string>);
                   List<int> RNcompvalue = toint(Session["Rnegative"] as List<string>);
                   Session["Rvalue"] =Rcompvalue;
                   Session["Rpositive"] =RPcompvalue;
                   Session["Rnegative"] =RNcompvalue;
                }

                if (CList != null)
                { 
                    List<double> Ccompvalue = todouble(Session["Cvalue"] as List<string>);
                   List<int> CPcompvalue = toint(Session["Cpositive"] as List<string>);
                   List<int> CNcompvalue = toint(Session["Cnegative"] as List<string>);
                   Session["Cvalue"] = Ccompvalue;
                   Session["Cpositive"] = CPcompvalue;
                   Session["Cnegative"] = CNcompvalue;
                }

                if (IList != null)
                { 
                    List<double> Icompvalue = todouble(Session["Ivalue"] as List<string>);
                   List<int> IPcompvalue = toint(Session["Ipositive"] as List<string>);
                   List<int> INcompvalue = toint(Session["Inegative"] as List<string>);
                   Session["Ivalue"] = Icompvalue;
                   Session["Ipositive"] = IPcompvalue;
                   Session["Inegative"] = INcompvalue;
                }
                   return true;
               }
               catch
               {
                    return false;
               }

               
           
           
           
       }

       public List<double> todouble(List<string> list)
       {
           List<double> li = new List<double>();
           foreach (string var in list)
           {
               li.Add(Convert.ToDouble(var));
           }
           return li;
       }

       public List<int> toint(List<string> list)
       {
           List<int> li = new List<int>();
           foreach (string var in list)
           {
               li.Add(Convert.ToInt32(var));
           }
           return li;
       }

    }
}