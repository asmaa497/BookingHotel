# BookingHotel
## Use Identity for Authentication 
## Packages 
<ul>
  <li>Microsoft.AspNetCore.Authentication.JwtBearer<li/>
  <li>Microsoft.AspNetCore.Identity.EntityFrameworkCore<li/>
  <li>Microsoft.EntityFrameworkCore.Design<li/>
  <li>Microsoft.EntityFrameworkCore<li/>
  <li>Microsoft.EntityFrameworkCore.SqlServer<li/>
  <li>Microsoft.EntityFrameworkCore.Tools<li/>
  <li>AutoMapper<li/>
<ul/>


## make a 3-Tier Booking System for a Hotel that has several branches.

1. Booking Database (SQL Server)
2. Web service (API) to have all the logic of the system, e.g. find availability for the selected 
time, book, cancel, update the DB with the booked or cancelled room(s). (.Net/ C#)
3. Application interface Back-end (ASP.Net MVC/C#), this should NOT have any logic and 
should only communicate with the Web service (API).
4. Application interface Front-end (HTML, CSS, Bootstrap, JavaScript, jQuery)
5. A report-like page to display all the rooms in the hotel and their status, e.g., booked, 
available. This page should authenticate the user before displaying the report.


Required Considerations
The system should deal with the hotel branches / locations. The booking needs to be in 
a specific branch / location of the hotel.
The system should allow booking several rooms under one booking name.
The system should give facility to booking rooms of different types, e.g., single, double, 
suite, and should allow booking multiple persons in a double room or a suite.
The system should give a discounted price (95%) if the customer has booked previously 
in the hotel
