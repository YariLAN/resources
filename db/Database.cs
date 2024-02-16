using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace db
{
    public static class Database
    {
        /// <summary>
        ///Update or Insert Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></p
        public static bool Upsert<T>(T data)
        {
            try
            {
                using (var db = new LiteDatabase($"./Database.db"))
                {
                    return db.GetCollection<T>().Upsert(data);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static T GetData<T>(string fieldName, BsonValue data)
        {
            try
            {
                using (var db = new LiteDatabase($"./Database.db"))
                {
                    return db.GetCollection<T>().FindOne(Query.EQ(fieldName, data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default(T);
            }
        }

        public static List<T> GetCollectionFromSelection<T>(string field, BsonValue data)
        {
            try
            {
                using (var db = new LiteDatabase($"./Database.db"))
                {
                    return db.GetCollection<T>().Find(Query.EQ(field, data)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T GetById<T>(int id)
        {
            try
            {
                using (var db = new LiteDatabase($"./Database.db"))
                {
                    return db.GetCollection<T>().FindById(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default(T);
            }
        }
    }
}
