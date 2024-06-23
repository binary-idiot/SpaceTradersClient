using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SpaceTraders.Shared.Models.API;

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
		
		var queryStr = await query.GetEndpointWithParams();
		//TODO: Convert to new assert syntax https://docs.nunit.org/articles/nunit/release-notes/Nunit4.0-MigrationGuide.html
		ClassicAssert.AreEqual("/test?param=test&other=false", queryStr);
	}

	[Test]
	public async Task BuildQueryNoParamsTest()
	{
		var query = new ApiQuery()
		{
			Endpoint = "/test"
		};
		
		var queryStr = await query.GetEndpointWithParams();
		ClassicAssert.AreEqual("/test", queryStr);
	}
}