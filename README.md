# Rezerwacja sal
## Wymagania
- Net SDK 8 + https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Baza danych SQLServer
- jeżeli nie istnieje narzędzie dotnet ef należy je zainstalować za pomocą komendy

dotnet tool install --global dotnet-ef 
## Kroki uruchomieniowe programu
-Wszelkie komendy należy uruchamić w katalogu RoomReservation
- W pliku RoomReservation/appsettings.json należy odnaleść linię "DefaultConnection": "" i uzupełnić w niej dane dostępowe do serwera bazodanowego
- Następnie należy wykonać aktualizację bazy danych za pomocą komendy
    
 dotnet ef database update
   
 
- Następnie by uruchomić program należy użyć komendy 

dotnet run

 Aplikacja uruchomi się na otwartym porcie np
 Now listening on: http://localhost:5183
 
