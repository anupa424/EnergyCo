# DigiShopping
Steps to follow
1. Clone the repository
2.Navigate inside the DigiShopping project
3. Update connection string in appsettings.json
4. run the command `dotnet ef migrations add InitialCreate` to create database schema
5. run the command `dotnet ef database update` to apply migrations
6.Run the application using `dotnet run`
7.https://localhost:7186/shoppingcart/checkout - api End Point
Json Body 
{
"CustomerId": "8e4e8991-aaee-495b-9f24-52d5d0e509c5",
"LoyaltyCard":"CTX0000001",
"TransactionDate":"2020-01-03",
"Basket": [
{
"ProductId":"PRD01",
"UnitPrice":"1.2",
"Quantity":"3"
},
{
"ProductId":"PRD02",
"UnitPrice":"2.0",
"Quantity":"2"
},
{
"ProductId":"PRD03",
"UnitPrice":"5.0",
"Quantity":"1"
}
]
}
