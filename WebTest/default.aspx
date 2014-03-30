<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="_03252014._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Section Elements</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link rel="stylesheet" type="text/css" href="~/Content/layout.css" display="screen" />
</head>
<body>
    <form id="form1" runat="server">
<header id="page_header">
			<h1>Hello! <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h1>
    
			<nav>
				<a href="">MENU1 | </a>
				<a href="">MENU2 | </a>
				<a href="">MENU3 | </a>
			</nav>
		</header>
		<section id="sidebar">
			<header>
				<h3>ITEM</h3>
			</header>
			<nav>
				<ul>
					<li><a href="default.aspx">About C#</a></li>
					<li><a href="">About ASP.NET</a></li>
					<li><a href="">About MS-SQL</a></li>
                    <li><a href="WebForm1.aspx">JQuery Test!</a></li>
				</ul>
			</nav>
		</section>
		<section id="posts">
			<header>
				<h1>Interview Questions.</h1>
			</header>
			<article class="post">
				<header>
					<hgroup>
						<h2>C#</h2>
						<h3>OOP</h3>
					</hgroup>
				</header>
				<aside>
					<p>&quot;What is object-oriented programming?&quot;</p>
				</aside>
				<p>
					OOP is a technique to develop logical modules, such as classes that contain properties, methods, fields, and events. 
                    An object is created in the program to represent a class.
                    Therefore, an object encapsulates all the features, such as data and behavior that are associated to a class.
                    OOP allows developers to develop modular programs and assemble them as software. 
                    Objects are used to access data and behaviors of different software modules, such as classes, namespaces, and sharable assemblies. 
                    .NET Framework supports only OOP languages, such as Visual Basic.NET, Visual C#, and Visual C++.
				</p>
			</article>
            <article class="post">
				<header>
					<hgroup>
						<h3>What is a class?</h3>
					</hgroup>
				</header>
				<p>
					A class describes all the attributes of objects, as well as the methods that implement the behavior of member objects.
                    It is a comprehensive data type, which represents a blue print of objects. It is a template of object.
                    A class contains data and behavior of an entity.
				</p>
			</article>
            <article class="post">
				<header>
					<hgroup>
						<h3>What is an object?</h3>
					</hgroup>
				</header>
				<p>
					They are instance of classes. It is a basic unit of a system. An object is an entity that has attributes, behavior, and identity.
                    Attributes and behavior of an object are defined by the class definition.
				</p>
			</article>
			<footer>
				<p></p>
			</footer>
		</section>
		<br /><br />
		<footer id="page_footer">
			<hr />	
			<nav>
				<a href="">About1 | </a>
				<a href="">About2 | </a>
				<a href="">About3 | </a>
			</nav>
			<p>Copyright(C) aaaaa. All rights reserved.</p>
		</footer>
    </form>
</body>
</html>
