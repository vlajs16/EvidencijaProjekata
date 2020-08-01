using DAL;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedData
{
    public class Seed
    {
        public static void SeedScientificAreas(IRCContext context)
        {
            if (!context.ScientificAreas.Any())
            {
                var areasData = System.IO.File.ReadAllText("../SeedData/Data/ScientificAreaJson.json");
                var areas = JsonConvert.DeserializeObject<List<ScientificArea>>(areasData);
                foreach (var area in areas)
                {
                    context.ScientificAreas.Add(area);
                }
                context.SaveChanges();
            }
        }

        public static void SeedPositions(IRCContext context)
        {
            if (!context.Positions.Any())
            {
                var positionsData = System.IO.File.ReadAllText("../SeedData/Data/PositionJson.json");
                var positions = JsonConvert.DeserializeObject<List<Position>>(positionsData);
                foreach (var pos in positions)
                {
                    context.Positions.Add(pos);
                }
                context.SaveChanges();
            }
        }

        public static void SeedCity(IRCContext context)
        {
            if (!context.Cities.Any())
            {
                var citiesData = System.IO.File.ReadAllText("../SeedData/Data/CityJson.json");
                var cities = JsonConvert.DeserializeObject<List<City>>(citiesData);
                foreach (var city in cities)
                {
                    context.Cities.Add(city);
                }
                context.SaveChanges();
            }
        }


        public static void SeedCompanies(IRCContext context)
        {
            if (!context.Companies.Any())
            {
                var companyData = System.IO.File.ReadAllText("../SeedData/Data/CompanyJson.json");
                var companies = JsonConvert.DeserializeObject<List<Company>>(companyData);
                foreach (var company in companies)
                {
                    foreach (var loc in company.Locations)
                    {
                        loc.City = context.Cities.FirstOrDefault(l => l.Name == loc.City.Name);
                        loc.CityID = loc.City.CityID;
                    }

                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("1234", out passwordHash, out passwordSalt);
                    company.PasswordHash = passwordHash;
                    company.PasswordSalt = passwordSalt;


                    context.Companies.Add(company);
                }

                

                context.SaveChanges();
            }
        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void SeedEmployees(IRCContext context)
        {
            if (!context.Employees.Any())
            {
                var employeesData = System.IO.File.ReadAllText("../SeedData/Data/EmployeeJson.json");
                var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesData);

                foreach(var emp in employees)
                {
                    emp.Positions = new List<EmployeePosition>();
                    switch (emp.Username)
                    {
                        case "miki":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Декан").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                        case "aco":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Продекан за наставу").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                        case "sanja":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Продекан за међународну сарадњу").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                        case "aki":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Продекан за организацију и финансије").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                        case "mare":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Продекан за научно-истраживачки рад").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                        case "goca":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Секретар факултета").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                        case "irc":
                            {
                                emp.Positions.Add(new EmployeePosition
                                {
                                    PositionID = context.Positions.FirstOrDefault(x => x.Name == "Ирц админ").PositionID,
                                    DateFrom = DateTime.Today.AddYears(-2)
                                });
                                break;
                            }
                    }

                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("1234", out passwordHash, out passwordSalt);
                    emp.PasswordHash = passwordHash;
                    emp.PasswordSalt = passwordSalt;


                    context.Employees.Add(emp);
                }
                context.SaveChanges();

            }
        }
    }
}
