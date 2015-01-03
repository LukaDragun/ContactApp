using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;

namespace ContactApp.Controllers
{
    public class HomeController : Controller
    {
        OleDbCommand cmd1= new OleDbCommand();
        OleDbCommand cmd2 = new OleDbCommand();
        OleDbConnection cn = new OleDbConnection();
        OleDbDataReader dr1,dr2;

        public ActionResult Index()
        {
            return View();
        }

        public void Delete(int id)
        {
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Luka\Documents\Visual Studio 2013\Projects\ContactApp\ContactApp\bin\database.accdb";
            cmd1.Connection = cn;
            cn.Open();
            cmd1.CommandText = "DELETE FROM Kontakt WHERE ID=" + id;
            cmd1.ExecuteNonQuery();
            cn.Close();
        }


        public void Insert (Contact contact)
        {
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Luka\Documents\Visual Studio 2013\Projects\ContactApp\ContactApp\bin\database.accdb";
            cmd1.Connection = cn;
            cn.Open();
            if (contact.id != 0)
                cmd1.CommandText = "UPDATE Kontakt SET Ime='" + contact.name + "',Prezime='" + contact.surname + "',Organizacija='" + contact.organization + "',Adresa='" + contact.adress + "' WHERE ID=" + contact.id;
            else
            {
                cmd1.CommandText = "INSERT INTO Kontakt (Ime,Prezime,Organizacija,Adresa) VALUES ('" + contact.name + "','" + contact.surname + "','" + contact.organization + "','" + contact.adress + "')";
                cmd1.ExecuteNonQuery();
                cmd1.CommandText = "SELECT LAST (ID) FROM Kontakt";
                dr1 = cmd1.ExecuteReader();
                dr1.Read();
                contact.id = Convert.ToInt32(dr1[0]);
                dr1.Close();
            }

            cmd1.CommandText = "DELETE FROM Email WHERE ID="+contact.id;
            cmd1.ExecuteNonQuery();
            cmd1.CommandText = "DELETE FROM Tag WHERE ID=" + contact.id;
            cmd1.ExecuteNonQuery();
            cmd1.CommandText = "DELETE FROM Phone WHERE ID=" + contact.id;
            cmd1.ExecuteNonQuery();

            if (contact.emails != null)
            {
                foreach (email el in contact.emails)
                {
                    cmd1.CommandText = "INSERT INTO Email VALUES (" + contact.id + ",'" + el.val + "')";
                    cmd1.ExecuteNonQuery();
                }
            }

            if (contact.tags != null)
            {
                foreach (tag el in contact.tags)
                {
                    cmd1.CommandText = "INSERT INTO Tag VALUES (" + contact.id + ",'" + el.val + "')";
                    cmd1.ExecuteNonQuery();
                }
            }

            if (contact.phones != null)
            {
                foreach (phone el in contact.phones)
                {
                    cmd1.CommandText = "INSERT INTO Phone VALUES (" + contact.id + ",'" + el.val + "')";
                    cmd1.ExecuteNonQuery();
                }
            }
            cn.Close();
        }
        
        public ActionResult Search(Search search)
        {
            List<int> srresults= new List<int>();
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Luka\Documents\Visual Studio 2013\Projects\ContactApp\ContactApp\bin\database.accdb";
            cmd1.Connection = cn;
            cn.Open();
            if(search.action==1)
            cmd1.CommandText = "SELECT ID FROM Kontakt WHERE Ime='"+search.searchtext+"'";
            else if(search.action==2)
            cmd1.CommandText = "SELECT ID FROM Kontakt WHERE Prezime='" + search.searchtext + "'";
            else if (search.action == 3)
            cmd1.CommandText = "SELECT ID FROM Tag WHERE Tag='" + search.searchtext + "'";
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    srresults.Add(Convert.ToInt32(dr1[0]));
                }
            }
            cn.Close();
            List<Contact> searchresults = new List<Contact>();
            foreach (int id in srresults)
                searchresults.Add(ReadDB(id)[0]);

            return Json(searchresults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read(int id)
        {
        return Json(ReadDB(id), JsonRequestBehavior.AllowGet);
        }
        

                public List<Contact> ReadDB(int id=0)
                {
                    cn.ConnectionString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Luka\Documents\Visual Studio 2013\Projects\ContactApp\ContactApp\bin\database.accdb";
                    cmd1.Connection= cn;
                    cmd2.Connection= cn;
                    List<Contact> contactlist = new List<Contact>();

                    cn.Open();
                    if (id==0)
                    cmd1.CommandText = "SELECT * FROM Kontakt";
                    else
                    cmd1.CommandText = "SELECT * FROM Kontakt WHERE ID="+id;
                    dr1 = cmd1.ExecuteReader();
                    if(dr1.HasRows)
                    {
                        while(dr1.Read())
                        {
                            Contact contact = new Contact();
                            List<email> emails= new List<email>();
                            List<tag> tags= new List<tag>();
                            List<phone> phones= new List<phone>();
                            contact.id = Convert.ToInt32(dr1[0]);
                            contact.name = dr1[1].ToString();
                            contact.surname = dr1[2].ToString();
                            contact.organization = dr1[3].ToString();
                            contact.adress = dr1[4].ToString();
                    
                   
                           cmd2.CommandText = "SELECT Email FROM Email WHERE ID="+contact.id;
                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                email emailv = new email();
                                emailv.val=dr2[0].ToString();
                                emails.Add(emailv);
                                }
                            }
                            dr2.Close();

                            cmd2.CommandText = "SELECT Phone FROM Phone WHERE ID=" + contact.id;
                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    phone phonev = new phone();
                                    phonev.val = dr2[0].ToString();
                                    phones.Add(phonev);
                                }
                            }
                            dr2.Close();

                            cmd2.CommandText = "SELECT Tag FROM Tag WHERE ID=" + contact.id;
                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    tag tagv = new tag();
                                    tagv.val = dr2[0].ToString();
                                    tags.Add(tagv);
                                }
                            }
                            dr2.Close();
                            contact.phones=phones.ToArray();
                            contact.emails = emails.ToArray();
                            contact.tags = tags.ToArray();
                            contactlist.Add(contact);
                        }
                    dr1.Close();
                    cn.Close();
                    }
                    return contactlist;
                }

    }

    public class Search
    {
        public int action { get; set; }
        public string searchtext { get; set; }
    }

    public class Contact
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string organization{ get; set; }
        public string adress{ get; set; }
        public email[] emails { get; set; }
        public phone[] phones { get; set; }
        public tag[] tags { get; set; }
    }
    
    public class email
    {
        public string val { get; set; }
    }

    public class phone
    {
        public string val { get; set; }
    }

    public class tag
    {
        public string val { get; set; }
    }
}