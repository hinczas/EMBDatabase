using EMBDatabase.Context;
using EMBDatabase.Models;
using EMBDatabase.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EMBDatabase.Classes
{
    public class DataService
    {
        private EMBContext db;

        public DataService()
        {
            db = new EMBContext();
        }

        public ApiPart GetPart(int id)
        {
            var part = db.Part.Where(a => a.Id == id).FirstOrDefault();
            var epart = AutoMapper.Mapper.Map<ApiPart>(part);
            return epart;
        }
        public List<ApiPart> GetParts()
        {
            var parts = AutoMapper.Mapper.Map<List<ApiPart>>(db.Part.ToList());
            return parts;
        }

        public ExportManufacturer GetManufacturer(int id)
        {
            var man = db.Manufacturer.Where(a => a.Id == id).FirstOrDefault();
            var eman = AutoMapper.Mapper.Map<ExportManufacturer>(man);
            return eman;
        }
        public List<ExportManufacturer> GetManufacturers()
        {
            var mans = AutoMapper.Mapper.Map<List<ExportManufacturer>>(db.Manufacturer.ToList());
            return mans;
        }

        public ExportLocation GetLocation(int id)
        {
            var loc = db.Location.Where(a => a.Id == id).FirstOrDefault();
            var eloc = AutoMapper.Mapper.Map<ExportLocation>(loc);
            return eloc;
        }
        public List<ExportLocation> GetLocations()
        {
            var locs = AutoMapper.Mapper.Map<List<ExportLocation>>(db.Location.ToList());
            return locs;
        }

        public ExportPackage GetPackage(int id)
        {
            var item = db.Package.Where(a => a.Id == id).FirstOrDefault();
            var eitem = AutoMapper.Mapper.Map<ExportPackage>(item);
            return eitem;
        }
        public List<ExportPackage> GetPackages()
        {
            var items = AutoMapper.Mapper.Map<List<ExportPackage>>(db.Package.ToList());
            return items;
        }

        public ExportType GetType(int id)
        {
            var item = db.Type.Where(a => a.Id == id).FirstOrDefault();
            var eitem = AutoMapper.Mapper.Map<ExportType>(item);
            return eitem;
        }
        public List<ExportType> GetTypes()
        {
            var items = AutoMapper.Mapper.Map<List<ExportType>>(db.Type.ToList());
            return items;
        }

        public bool AddPart(ApiPart apiPart)
        {
            // Manufacturer setup
            if (apiPart.Manufacturer_Id==null)
            {
                if (!string.IsNullOrEmpty(apiPart.Manufacturer_Name))
                {
                    var man = db.Manufacturer.Where(a => a.Name.ToLower().Equals(apiPart.Manufacturer_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Manufacturer_Id = man.Id;
                    } else
                    {
                        db.Manufacturer.Add(new Manufacturer()
                        {
                            Name = apiPart.Manufacturer_Name,
                            Full_Name = apiPart.Manufacturer_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Manufacturer.Where(a => a.Name.Equals(apiPart.Manufacturer_Name)).FirstOrDefault();

                        apiPart.Manufacturer_Id = newMan.Id;
                    }
                }
            }

            // Package setup
            if (apiPart.Package_Id == null)
            {
                if (!string.IsNullOrEmpty(apiPart.Package_Name))
                {
                    var man = db.Package.Where(a => a.Name.ToLower().Equals(apiPart.Package_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Package_Id = man.Id;
                    }
                    else
                    {
                        db.Package.Add(new Package()
                        {
                            Name = apiPart.Package_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Package.Where(a => a.Name.Equals(apiPart.Package_Name)).FirstOrDefault();

                        apiPart.Package_Id = newMan.Id;
                    }
                }
            }

            // Location setup
            if (apiPart.Location_Id == null)
            {
                if (!string.IsNullOrEmpty(apiPart.Location_Name))
                {
                    var man = db.Location.Where(a => a.Name.ToLower().Equals(apiPart.Location_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Location_Id = man.Id;
                    }
                    else
                    {
                        db.Location.Add(new Location()
                        {
                            Name = apiPart.Location_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Location.Where(a => a.Name.Equals(apiPart.Location_Name)).FirstOrDefault();

                        apiPart.Location_Id = newMan.Id;
                    }
                }
            }

            // Type setup
            if (apiPart.Type_Id == null)
            {
                if (!string.IsNullOrEmpty(apiPart.Type_Name))
                {
                    var man = db.Type.Where(a => a.Name.ToLower().Equals(apiPart.Type_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Type_Id = man.Id;
                    }
                    else
                    {
                        db.Type.Add(new Models.Type()
                        {
                            Name = apiPart.Type_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Type.Where(a => a.Name.Equals(apiPart.Type_Name)).FirstOrDefault();

                        apiPart.Type_Id = newMan.Id;
                    }
                }
            }

            // Finally create Part
            try
            {
                var part = AutoMapper.Mapper.Map<Part>(apiPart);
                part.CreateDate = DateTime.Now;

                db.Part.Add(part);
                db.SaveChangesAsync();

                return true;
            } catch
            {
                return false;
            }
        }

        public T GetItem<T>(int id)
            where T: BaseModel
        {
            var result = db.Set<T>().Where(a => a.Id == id).FirstOrDefault();
            return result;
        }
        
        public bool UpdateItem<T>(int id, string fieldName, string fieldValue)
            where T : class
        {            
            System.Type itemType = typeof(T);
            PropertyInfo itemProperty = itemType.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            
            var itemToUpdate = db.Set<T>().Find(id);
            
            if (itemToUpdate != null)
            {
                var targetType = itemProperty.PropertyType;

                if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    targetType = Nullable.GetUnderlyingType(targetType);
                }

                if (itemProperty != null && !string.IsNullOrEmpty(fieldValue))
                {
                    itemProperty.SetValue(itemToUpdate, Convert.ChangeType(fieldValue, targetType));
                }

                db.SaveChangesAsync();

                return true;
            } else
            {
                return false;
            } 

        }
    }
}