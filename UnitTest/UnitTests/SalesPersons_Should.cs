using DALlab4.Entities;
using Lab4.ApiContollers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.UnitTests
{
    public class SalesPersons_Should
    {
        DbContextOptions<AdventureWorks2014Context> _dbContextOptions;

        public SalesPersons_Should()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AdventureWorks2014Context>()
                            .UseInMemoryDatabase(databaseName: "Test_database")
                            .Options;
        }

        [Fact]
        public async void GetSalesPerson()
        {
            using (var context = new AdventureWorks2014Context(_dbContextOptions))
            {
                var salesPersonsAPI = new SalesPersonsController(context);
                for (int i = 0; i < 10; ++i)
                {
                    SalesPerson tmpSalesPerson = new SalesPerson();
                    tmpSalesPerson.Bonus = i + 1 ;
                    salesPersonsAPI.PostSalesPerson(tmpSalesPerson).Wait();
                }
            }

            using (var context = new AdventureWorks2014Context(_dbContextOptions))
            {
                var salesPersonsAPI = new SalesPersonsController(context);
                var result = await salesPersonsAPI.GetSalesPerson(5);
                var okResult = result as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                SalesPerson salesPerson = okResult.Value as SalesPerson;
                Assert.NotNull(salesPerson);
                Assert.Equal(5, salesPerson.Bonus);
            }
        }
    }
}
