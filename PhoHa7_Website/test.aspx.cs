using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

        }

    }

    async Task<object> getSquareOrderDetails(string orderID)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://connect.squareup.com/v2/orders/batch-retrieve"))
            {
                request.Headers.TryAddWithoutValidation("Square-Version", "2023-06-08");
                request.Headers.TryAddWithoutValidation("Authorization", "Bearer EAAAEBkTIFhqhIKegI5ih6bRhV_b0bfyhQyg-VPvUBH20wGiLM9kHLLBA7KQFgi1");

                request.Content = new StringContent("{\n    \"order_ids\": [\n      \"" + orderID + "\"\n    ],\n    \"location_id\": \"LS7M0EPVEJZNB\"\n  }");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = await httpClient.SendAsync(request).Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(response);
            }
        }
    }
}