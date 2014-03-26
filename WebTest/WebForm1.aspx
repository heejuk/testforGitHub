<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_03252014.WebForm1" %>

<!DOCTYPE html>
<html>
	<head>
        <title></title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
		<style type="text/css">
			body{padding:10px; 
				margin:10px;}
			#myDiv{border:1px solid red; 
				width:620px; 
				height:620px;
				}
			.innerdiv{width:198px; 
				height:198px; 
				text-align:center;}
			td{border:1px dotted green; 
				width:200px; 
				height:200px; 
				cursor:pointer; 
				text-align:center;
				font:120pt bold;}
			.selected_div{background:green;}
			.clicked_div{background:yellow;}
		</style>
		<script type="text/javascript">
		    $(document).ready(function () {
		        $("td").bind("mouseenter mouseleave", function (e) {
		            if (e.type == "mouseenter") {
		                $("#myTable").find("div").removeClass("selected_div");
		                $(this).find("div").addClass("selected_div");
		            }
		            else {
		                $("#myTable").find("div").removeClass("selected_div");
		            }
		        });

		        $("td").one("click", function () {
		            var active_div = $(this).find("div");
		            active_div.addClass("clicked_div");
		            $("#resultDiv").html(active_div.text());
		        });
		    });
		</script>
	</head>
	<body>
		<div id="myDiv">
			<table id="myTable">
				<tr>
					<td><div class="innerdiv">1</div></td>
					<td><div class="innerdiv">2</div></td>
					<td><div class="innerdiv">3</div></td>
				</tr>
				<tr>
					<td><div class="innerdiv">4</div></td>
					<td><div class="innerdiv">5</div></td>
					<td><div class="innerdiv">6</div></td>
				</tr>
				<tr>
					<td><div class="innerdiv">7</div></td>
					<td><div class="innerdiv">8</div></td>
					<td><div class="innerdiv">9</div></td>
				</tr>
			</table>
		</div>
		<div id="resultDiv"></div>
		
	</body>
</html>