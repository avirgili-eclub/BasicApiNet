# BasicApiNet
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/d18fb5699b7b4093a718170289139a0a)](https://app.codacy.com/gh/avirgili-eclub/BasicApiNet?utm_source=github.com&utm_medium=referral&utm_content=avirgili-eclub/BasicApiNet&utm_campaign=Badge_Grade)

<p>This is a basic example of a web API built on .NET Core 8. The main goal of this API is to demonstrate basic CRUD (Create, Read, Update, Delete) operations using two main entities: <code>Country</code> and <code>City</code>.</p>

<h2>Key Features</h2>

<ul>
  <li>Utilizes .NET Core 8 for API development.</li>
  <li>Implements dependency injection architecture for better modularity and maintainability of the code.</li>
  <li>Uses a local SQLite database to persistently store data.</li>
  <li>Employs Entity Framework Core as the ORM (Object-Relational Mapping) for interacting with the database.</li>
  <li>A middleware has been added to handle exceptions globally, improving error management in the application.</li>
  <li>Unit tests are used for repository testing.</li>
</ul>

<h2>Prerequisites</h2>

<ul>
  <li>.NET Core 8 must be installed on your system.</li>
  <li>A C# compatible development environment is required for editing and compiling the project.</li>
</ul>

<h2>Installation and Configuration</h2>

<ol>
  <li>Clone this repository to your local machine:</li>
</ol>

<pre><code>git clone https://github.com/your-username/BasicApiNet.git
</code></pre>

<ol start="2">
  <li>Navigate to the project directory:</li>
</ol>

<pre><code>cd BasicApiNet
</code></pre>

<ol start="3">
  <li>Open the project in your preferred development environment.</li>
  <li>Build the project using the command:</li>
</ol>

<pre><code>dotnet build
</code></pre>

<ol start="5">
  <li>Run the migrations to set up the SQLite database:</li>
</ol>

<pre><code>dotnet ef migrations add InitialCreate -p BasicApiNet.Access -s BasicApiNet.Host -o Data/Migrations
</code></pre>

<ol start="6">
  <li>Apply the migrations to create the database:</li>
</ol>

<pre><code>dotnet ef database update
</code></pre>

<ol start="7">
  <li>Start the application by running the command:</li>
</ol>

<pre><code>dotnet run
</code></pre>

<h2>Usage</h2>

<ul>
  <li>Once the application is running, you first have to login (admin:admin123) to generate the jwt token, copy the token to access protected endpoints.</li>
  <li>Once you have the token set you can make HTTP requests via the API to perform CRUD operations on the <code>Country</code> and <code>City</code> entities.</li>
  <li>Make sure to review the API documentation to learn about the available endpoints and their respective operations.</li>
</ul>
