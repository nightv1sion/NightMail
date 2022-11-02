# NightMail 

is a service for the exchange of electronic intra-system messages  

# How to run using Docker

- clone this repository
- go to the clonned repository folder and open terminal there
- create a https certificate
         
         dotnet dev-certs https --clean
         
         dotnet dev-certs https -ep ./backend/conf.d/https/nightmail.pfx -p SomePassword123!
         
         dotnet dev-certs https --trust
         
- run docker-compose

      docker-compose up --build
- wait for it to start and go to "http://localhost:8080/"

# Technologies


![NightMail Technologies (1)](https://user-images.githubusercontent.com/92179208/199552179-4bad0d50-98c8-4ba7-9321-6ceca4921f35.png)
