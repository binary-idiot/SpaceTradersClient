using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SpaceTraders.Models.API;

namespace SpaceTraders.Test;

[TestFixture]
public class ApiQueryTests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public async Task BuildQueryTest()
	{
		var query = new ApiQuery()
		{
			Endpoint = "/test",
			Params = new Dictionary<string, string>()
			{
				{ "param", "test" },
				{"other", "false"}
			}
		};
		
		var queryStr = await query.BuildQuery();
		Assert.AreEqual("/test?param=test&other=false", queryStr);
	}

	[Test]
	public async Task BuildQueryNoParamsTest()
	{
		var query = new ApiQuery()
		{
			Endpoint = "/test"
		};
		
		var queryStr = await query.BuildQuery();
		Assert.AreEqual("/test", queryStr);
	}
}