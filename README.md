<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Restaurant Management – Backend Overview</title>

<style>
    body {
        font-family: Arial, sans-serif;
        background: #f7f7f7;
        margin: 0;
        padding: 0;
        line-height: 1.7;
    }

    .container {
        width: 90%;
        max-width: 1000px;
        margin: 40px auto;
        background: white;
        padding: 30px;
        border-radius: 12px;
        box-shadow: 0 4px 20px rgba(0,0,0,0.1);
    }

    h1, h2, h3 {
        color: #333;
        margin-bottom: 10px;
    }

    h1 {
        font-size: 32px;
        border-bottom: 3px solid #ff6b00;
        padding-bottom: 10px;
    }

    h2 {
        font-size: 26px;
        margin-top: 25px;
    }

    p {
        font-size: 16px;
        color: #444;
    }

    ul {
        margin-left: 20px;
    }

    li {
        margin-bottom: 8px;
    }

    pre {
        background: #222;
        color: #fff;
        padding: 15px;
        border-radius: 8px;
        overflow-x: auto;
        font-size: 14px;
    }

    code {
        color: #ffb86c;
    }

    .section {
        margin-bottom: 40px;
    }

    .tag {
        display: inline-block;
        background: #ff6b00;
        padding: 5px 10px;
        margin-right: 5px;
        margin-bottom: 8px;
        border-radius: 5px;
        color: white;
        font-size: 14px;
    }

    .footer {
        margin-top: 40px;
        text-align: center;
        color: #777;
        font-size: 14px;
    }

</style>
</head>

<body>

<div class="container">

    <h1>🍽️ Restaurant Management System – Backend</h1>

    <div class="section">
        <h2>📌 Overview</h2>
        <p>
            Restaurant Management is a backend RESTful API built with <strong>ASP.NET Core</strong>,
            designed to manage menu items, categories, customers, employees, and orders.
            It follows clean architecture principles for better maintainability and scalability.
        </p>
    </div>

    <div class="section">
        <h2>🛠 Tech Stack</h2>
        <span class="tag">ASP.NET Core</span>
        <span class="tag">Entity Framework Core</span>
        <span class="tag">SQL Server</span>
        <span class="tag">Repository Pattern</span>
        <span class="tag">Unit of Work</span>
        <span class="tag">Dependency Injection</span>
    </div>

    <div class="section">
        <h2>📂 Project Structure</h2>

<pre><code>
├── Core
│   ├── Entities
│   ├── Interfaces
│   ├── DTOs
│
├── Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Migrations
│
├── API
│   ├── Controllers
│   ├── Extensions
│   └── appsettings.json
</code></pre>
    </div>

    <div class="section">
        <h2>🚀 How to Run the Project</h2>

        <h3>1️⃣ Clone the Project</h3>
<pre><code>git clone https://github.com/raniataweel88/ResturantManagement.git</code></pre>

        <h3>2️⃣ Update Database Connection</h3>
<pre><code>
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=RestaurantDb;Trusted_Connection=True;"
}
</code></pre>

        <h3>3️⃣ Apply Migrations</h3>
<pre><code>dotnet ef database update</code></pre>

        <h3>4️⃣ Run the API</h3>
<pre><code>dotnet run</code></pre>

        <h3>5️⃣ Open Swagger</h3>
<pre><code>https://localhost:5001/swagger</code></pre>
    </div>

    <div class="section">
        <h2>📌 Future Improvements</h2>
        <ul>
            <li>Add Authentication (JWT)</li>
            <li>Add Logging & Global Exception Handler</li>
            <li>Add API Validation</li>
            <li>Improve Repository Layer</li>
            <li>Enhance Database Indexing</li>
        </ul>
    </div>

    <p class="footer">Made with ❤️ by Rania Taweel</p>

</div>

</body>
</html>
