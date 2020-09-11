using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Npgsql;
using ServiceStack;
using NHibernate.Id;
using NHibernate.Tool.hbm2ddl;


namespace People
{
    class Program
    {
        private static List<human> people = new List<human>();
       

        static void Main(string[] args)
        {
            try
            {
                var nhConfig = new Configuration();
                nhConfig.Configure();
                var sessionFactory = nhConfig.BuildSessionFactory();
                Console.WriteLine("NHibernate Configured!");
                Console.ReadKey();


                using (var session = sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        //Variant for 2 tables: human and gender

                        ICriteria criteria = session.CreateCriteria<human>();
                        criteria.CreateAlias("gender", "Gender", JoinType.LeftOuterJoin);  //или выставить lazy="false" в human.hbm.xml внутри <many-to-one> но это плохо
                        criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());
                        IList<human> list = criteria.List<human>();
                        people = list.ToList();

                        foreach (var item in people)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        Console.WriteLine("+++++++++++++");

                        //UPDATE
                        //var hmnUpdate = session.Get<human>(1);
                        //hmnUpdate.gender.gendername = "female";
                        //hmnUpdate.firstname = "Alisa";
                        //hmnUpdate.lastname = "Kovaleva";
                        //session.SaveOrUpdate(hmnUpdate);



                        //SAVE INTO DB
                        //save human when gender doesn't exist(so we add new into DB)
                        //human hmnSave = new human();
                        //hmnSave.lastname = "Orlov";
                        //hmnSave.firstname = "Oleg";
                        //hmnSave.DOB = "15th od March";
                        //hmnSave.SSN = 111111111;
                        //hmnSave.activeStatus = 1;
                        //hmnSave.gender = new gender { gendername = "not male,not female" };
                        //session.SaveOrUpdate(hmnSave);


                        //save human when gender exists
                        //gender fmlGender = session.Query<gender>()
                        //.Where(g => g.gendername == "female").FirstOrDefault();
                        //Console.WriteLine("id of female=" + fmlGender.GenderId.ToString());

                        //human hmnSave1 = new human();
                        //hmnSave1.lastname = "Titarenko";
                        //hmnSave1.firstname = "Oksana";
                        //hmnSave1.DOB = "9th od February";
                        //hmnSave1.SSN = 333333333;
                        //hmnSave1.gender = fmlGender;
                        //hmnSave1.activeStatus = 1;
                        //session.SaveOrUpdate(hmnSave1);


                        //save gender
                        //gender someGender= new gender { gendername = "smth" };
                        //session.SaveOrUpdate(someGender);


                        //DELETE
                        //Delete human
                        //var hmnDelete = session.Get<human>(1);
                        //session.Delete(hmnDelete);

                        //Delete gender(1.Try to delete 'female'.2.Instead 'male write 'animal')
                        //var hmnIsThereMale = session.Query<human>()
                        //.Where(h => h.gender.gendername == "female");
                        //Console.WriteLine(hmnIsThereMale);

                        //foreach (var item in hmnIsThereMale)
                        //{
                        //    Console.WriteLine(item.ToString());
                        //}
                        //Console.WriteLine("+++++++++++++");


                        //if (hmnIsThereMale.Count() > 0)
                        //{
                        //    Console.WriteLine("\nThe record can't be deleted.There are records with gender 'female'.");
                        //}
                        //else if (hmnIsThereMale.Count() == 0)
                        //{
                        //    var gndrDelete = session.Query<gender>()
                        //    .Where(g => g.gendername == "female").First();
                        //    session.Delete(gndrDelete);
                        //}
                        //Console.WriteLine("+++++++++++++");



                        //Using activeStatus instead of deleting
                        //var hmnChangeStatus = session.Query<human>()
                        //.Where(h => h.lastname == "Sokolova").First();
                        //hmnChangeStatus.activeStatus = 0;
                        //session.SaveOrUpdate(hmnChangeStatus);

                        //var hmnActiveStatus = session.Query<human>()
                        //.Where(h => h.activeStatus == 1);
                       
                        //foreach (var item in hmnActiveStatus)
                        //{
                        //    Console.WriteLine(item.ToString());
                        //}
                        //Console.WriteLine("+++++++++++++");





                        var validator = new SchemaValidator(nhConfig);
                        validator.Validate();
                        list = criteria.List<human>();
                        people = list.ToList();

                        foreach (var item in people)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        Console.WriteLine("+++++++++++++");
                        tx.Commit();
                       
                    }

                    session.Flush();
                    session.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (" + ex.GetType().Name + "): " + ex.Message);
                Console.WriteLine("ListCount=" + people.Count.ToString());
            }
        }
        
    }
}
