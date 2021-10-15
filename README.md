# Nascimento.Software.Projeto.RallyTotalV2


A simple project just for learning and improving .NET skills. I created the database and tables by hand, and used Dapper to access it

The main goal is to create an E-WRC market clone https://www.ewrc-market.com/   plus a Rally news Portal, like DirtFish

In this first commit the user can create a sale and see all cars available to buy

Create a sale page, I used SummerNote plugin and a upload file technique that I learn with a indian guy from youtube (Thank you mate)
<img src="https://github.com/Gabriel-0216/Nascimento.Software.RallyTotal/blob/master/ImagesFolder/001_CreatingSale.PNG">

https://www.youtube.com/watch?v=7nFErqpxtbg&ab_channel=ASP.NETMVC 
Follow him, this guy's great

Also, before creating a sale you must create a car category and a person (Seller)

ToDo: Sale -> Details/Delete/Edit

News Portal coming soon


V2:

Details page
<img src="https://github.com/Gabriel-0216/Nascimento.Software.RallyTotal/blob/master/ImagesFolder/003_Details.PNG">

Major changes: 
- You can't delete a person if he have an active sale.
- Details page
- Improvements to grid details

V3: 
<img src="https://github.com/Gabriel-0216/Nascimento.Software.RallyTotal/blob/master/ImagesFolder/003_SalesGrid.PNG">
Minor changes:
Delete a sale routine
Improvements at home page
Improvements at Sales index
Contact Us page

14/10/2021: 
Sale edit working
<b>THINK ASYNC!</b>
Controllers and repositories working with async programming.
Author entity, controller, pages created. 

ToDo:

Category Controller -> Block a delete if any active sale use it; </br>
Database scripts for Authors table; </br>
News Portal </br>

