# Asp.Net-Mvc-Tutorials:

Live Demo <a href="http://www.mvctutorials.somee.com/" target="_blank">Click here</a><br/>
In this project we are using ADO.NET Entity Data Model. If you don't know about Entity Framework <a href="https://www.c-sharpcorner.com/article/introduce-entity-framework-with-ado-net-entity-data-model/" target="_blank">Click Here </a>
<br/>
Create Database in SQL Server i have already added database bakup file in MvcTutorial >> DataBase >> MvcTutorial.bak <br/> 
and also added a Script file MvcTutorialscript.sql 
<a href="https://github.com/RajanMistry88/Asp.Net-Mvc-Tutorials/tree/master/MvcTutorial/DataBase" target="_blank">DataBase</a>

After you create DataBase Download the repo open web.config file and update connectionStrings with your Sql Credentials and Build the project and run. 
source=DELL\SQLEXPRESS; -- Server Name you have to replace only server name.<br/>
catalog=MvcTutorial; -- DataBase Name is diffrent then you have to replace

connectionStrings:<br/>
add name="MvcTutorialEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;
provider connectionstring=&quot;data source=DELL\SQLEXPRESS;initial catalog=MvcTutorial;integratedsecurity=True;
MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient"

<ul><b>Topic Cover in this Project</b>
  <li><b>(Html.BeginForm, Html.AjaxBeginForm, Model Validation, ViewBag, JQuery, HttpGet, HttpPost, Session Manage)</b></li>
  <li>User Registration (Insert Operation)</li>
  <li>Login (Authentication and Session Managing Operation)</li>
  <li>Forgot Password (Authenticate and Update Operation)</li>
  <li>Profile Update (Read and Update Operation)</li>
  <li>Delete Account (Delete Operation)</li>
  <li>Logout (Session Dismiss)</li>
</ul>

