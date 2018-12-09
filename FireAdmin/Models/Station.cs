using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FireAdmin.Models
{
	public class Station
	{
		[Required]
		public Guid StationId { get; set; }

		[Required(ErrorMessage = "Please enter a Station Name.")]
		[DisplayName("Station")]
		public string StationName { get; set; }
		public bool IsDeleted { get; set; }
		public Guid DeletedBy { get; set; }
		public DateTime DateDeleted { get; set; }
	}
}

public static class StationGrid
{
	public static WebGridColumn[] StationGridColumns(this HtmlHelper htmlHelper, WebGrid grid)
	{
		System.Security.Principal.IPrincipal user = htmlHelper.ViewContext.HttpContext.User;
		var columns = new List<WebGridColumn>();
		columns.Add(grid.Column(columnName: "StationName"));
		columns.Add(grid.Column(
						header: "Watches",
						format: item => htmlHelper.ActionLink(linkText: "Watches", actionName: "/Watch/Index/", routeValues: new { id = item.StationId }),
						style: "column-action"
						));
		if (user.IsInRole(role: "OPCS"))
		{
			columns.Add(grid.Column(header: "", format: (item) => htmlHelper.ActionLink(linkText: "Edit", actionName: "Edit", routeValues: new { id = item.StationId }), style: "column-action"));
			columns.Add(grid.Column(header: "", format: (item) => htmlHelper.ActionLink(linkText: "Delete", actionName: "Delete", routeValues: new { id = item.StationId }, htmlAttributes: new { oneclick = "return confirm('Are you sure that you want to delete " + item.StationName + "?');" }), style: "column-action"));
		}
		return columns.ToArray();
	}
}