# StoreManagement Overview
>StoreMangaement App is a desktop application that can be used to manage a store, controlling the store's inventory, creating customer orders, and keeping track of the status of all orders. This App hasn't (yet) been tested in real, it was written as a way to advance my development skills.

## Core Features
* Add Products to inventory
* Keep track of inventory status
* Submitting a order
* Keep track of all orders
* Email a reminder for unpaid bills

## Built With
* C# 
* MSSql
* Entity Framework Core
* WPF/xaml 
* MahApps


## Features in more detail
There is currently three pages to the project (besides the Home page). The pages are responsive and have the option to navigate backwords and forwards.  
To see this in action [check this out](https://user-images.githubusercontent.com/70821594/148854479-d752bf57-dd87-4b76-82e0-3d46ceca1631.mp4 "Navigation & Responsive demo")

#### Page 1 - [Inventory page](https://user-images.githubusercontent.com/70821594/148854523-7bc8b3c5-1708-45f1-aaec-5e7d425e4686.mp4 "Inventory page") - Features
* Add new product details, including a picture which can be uploaded (via the provided link) from the users computer
* Update/delete a product thats selected from the DataGrid
* The status for low in stock products are highlighted in red font (in the data grid), and for out of stock products it would blink continuously 
* Search a product by product name or category
* Seperate DataGrid for Low in stock products and for out of stock products
#### Page 2 - [Order page](https://user-images.githubusercontent.com/70821594/148867590-c47861e1-4d80-4039-878a-f1d402059886.mp4 "Order page") - Features
* There is an option to insert a customers details (needed if not paying for order)
* Search a product by category name and/or by product name
* Once a product is selected from the grid, it gets added to the Shopping Basket grid
* Each time the same product is selected again, the quantity for that product gets updated in the Shopping Basket grid
* In the Shopping Basket grid, there is a running total for each product
* If the Not-paid/Partly-paid Radio button is selected and the customers details hasn't been inserted then a message box appears to fill in required details
#### Page 3 - [Order Controller page](https://user-images.githubusercontent.com/70821594/148854714-7aaa1d6c-22c3-46a9-aea8-222ade4b73c4.mp4 "Order Controller Page") - Features
* Three Data Grids, one for all orders, one for search orders, and one for unpaid orders
* Search Grid to search orders by date, name, address, or email
* Update payment by first selecting a order from one of the grids
* Send an email reminder if order not paid, just with one click, the email gets generated with all the order details








